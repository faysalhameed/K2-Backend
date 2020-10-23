using CustomerBL.Template;
using CustomerBO.PreOrder;
using CustomerBO.Template;
using CustomerDAL.PreOrder;
using logginglibrary;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CustomerBL.PreOrder
{
    public class PreOrderBL
    {
        private ILog logger;
        public PreOrderBL(ILog templogger)
        {
            this.logger = templogger;
        }

        private async Task<Tuple<int, bool, string>> CartValidations(PreOrderBO obj)
        {
            try
            {
                #region Address verification

                if (string.IsNullOrEmpty(obj.userdata.preorder.pickaddress) 
                    ||
                   string.IsNullOrEmpty(obj.userdata.preorder.pickcity)
                    ||
                   string.IsNullOrEmpty(obj.userdata.preorder.pickcountry)
                   ||
                   string.IsNullOrEmpty(obj.userdata.preorder.pickprovince)
                   ||
                   string.IsNullOrEmpty(obj.userdata.preorder.pickzipcode)
                   ||
                   string.IsNullOrEmpty(obj.userdata.preorder.dropaddress)
                   ||
                   string.IsNullOrEmpty(obj.userdata.preorder.dropcity)
                   ||
                   string.IsNullOrEmpty(obj.userdata.preorder.dropcountry)
                   ||
                   string.IsNullOrEmpty(obj.userdata.preorder.dropprovince)
                   ||
                   string.IsNullOrEmpty(obj.userdata.preorder.dropzipcode)
                 )
                {
                    return new Tuple<int, bool, string>(-1, false, "Pick or Drop address missing.");
                }

                #endregion

                #region Tailor Selection Check

                if (string.IsNullOrEmpty(obj.userdata.preorder.tailorid))
                {
                    return new Tuple<int, bool, string>(-2, false, "Tailor not selected.");
                }

                #endregion

                #region Product selection Check 

                if(obj.userdata.preorder.preorderproducts == null && obj.userdata.preorder.preorderproducts.Count < 1)
                {
                    return new Tuple<int, bool, string>(-3, false, "Product selection missing !");
                }

                #endregion

                #region Product Image selection Check 

                bool _isImageFound = true;
                int productCount = obj.userdata.preorder.preorderproducts.Count;
                for (int i = 0; i < productCount; i++)
                {
                    int _count = obj.userdata.preorder.preorderproducts[i].preorderproductimages.Count;
                    if(_count < 1)
                    {
                        _isImageFound = false;
                        break;
                    }
                }

                if(!_isImageFound)
                {
                    return new Tuple<int, bool, string>(-3, false, "Product image missing !");
                }

                #endregion

                
                return new Tuple<int, bool, string>(1, true, "Validation clear");
            }
            catch (Exception ex)
            {
                return new Tuple<int, bool, string>(-2, false, ex.ToString());
            }
        }

        public async Task<List<MeasurementTemplateBOResponse>> FetchMeasurmentTemplate(string deviceid, string dresscategoryid, string sessiontoken)
        {
            try
            {
                MeasurementTemplateBO objMeasurementTemplate = new MeasurementTemplateBO();
                objMeasurementTemplate.deviceid = deviceid;
                objMeasurementTemplate.dresscategoryid = dresscategoryid;
                objMeasurementTemplate.sessiontoken = sessiontoken;
                MeasurementTemplateBOContainer objMeasurementTemplateContainer = new MeasurementTemplateBOContainer();
                objMeasurementTemplateContainer.userdata = objMeasurementTemplate;
                MeasurementTemplateBL objTemplateBL = new MeasurementTemplateBL(logger);
                var result = await objTemplateBL.GetMeasurementTemplateBL(objMeasurementTemplateContainer);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private async Task<Tuple<bool,int,string>> CartValidationsNew(PreOrderBO obj)
        {
            try
            {
                #region Product Count Check 

                int _totalproductcount = -1;
                int.TryParse(obj.userdata.preorder.totalproductcount, out _totalproductcount);
                if(_totalproductcount < 1)
                {
                    return new Tuple<bool, int, string>(false, -4, "No product found.");
                }

                #endregion

                #region Attributes checks (image, addon, measurement etc)

                for (int i = 0; i < obj.userdata.preorder.preorderproducts.Count; i++)
                {
                    bool _isproductimagesavailable, _isproductaddonavailable, _isproductmeasurementavailable = false;
                    bool.TryParse(obj.userdata.preorder.preorderproducts[i].isproductimagesavailable, out _isproductimagesavailable);
                    bool.TryParse(obj.userdata.preorder.preorderproducts[i].isproductaddonavailable, out _isproductaddonavailable);
                    bool.TryParse(obj.userdata.preorder.preorderproducts[i].isproductmeasurementavailable, out _isproductmeasurementavailable);
                    if(!_isproductimagesavailable)
                    {
                        return new Tuple<bool, int, string>(false,-5,"Image selection missing.");
                    }
                    else if(!_isproductaddonavailable)
                    {
                        return new Tuple<bool, int, string>(false, -6, "Product addon missing.");
                    }
                    else if(!_isproductmeasurementavailable)
                    {
                        return new Tuple<bool, int, string>(false,-7,"Product measurement missing.");
                    }
                }

                #endregion
                return new Tuple<bool, int, string>(true, 1, "");
            }
            catch (Exception ex)
            {
                return new Tuple<bool, int, string>(false,-3,ex.ToString());
            }
        }

        public async Task<Tuple<int,string>> FuncPreOrderBL(PreOrderBO obj)
        {
            try
            {
                #region Cart Validation

                if(obj.userdata.preorder.status.ToLower() == "cart")
                {
                    #region Old Logic Commented Code
                    //var res =  await CartValidations(obj);
                    //if(res.Item1 > 1)
                    //{
                    //    #region Measurement List Check 

                    //    int productCount = obj.userdata.preorder.preorderproducts.Count;
                    //    bool isMeasurementListfound = true;
                    //    for (int j = 0; j < productCount; j++)
                    //    {
                    //        int _measurementCount = obj.userdata.preorder.preorderproducts[j].preorderproductmeasurements.Count;
                    //        if (_measurementCount < 1)
                    //        {
                    //            var list = await FetchMeasurmentTemplate(obj.userdata.preorder.deviceid, obj.userdata.preorder.preorderproducts[j].dresscategoryid, obj.userdata.preorder.sessiontoken);
                    //            if (list == null)
                    //            {
                    //                isMeasurementListfound = false;
                    //                break;
                    //            }
                    //            else
                    //            {
                    //                for (int k = 0; k < list.Count; k++)
                    //                {
                    //                    Preorderproductmeasurement objMeasure = new Preorderproductmeasurement();
                    //                    objMeasure.attributeid = "-1";
                    //                    objMeasure.preorderproductmeasurementid = "-1";
                    //                    objMeasure.attributename = list[k].attributename;
                    //                    objMeasure.attributevalue = list[k].sizemax.ToString(); // min or max value ? 
                    //                    obj.userdata.preorder.preorderproducts[j].preorderproductmeasurements.Add(objMeasure);
                    //                }
                    //            }
                    //        }
                    //        else
                    //        {
                    //            for (int k = 0; k < _measurementCount; k++)
                    //            {
                    //                var tempObj = obj.userdata.preorder.preorderproducts[j].preorderproductmeasurements[k];
                    //                if(string.IsNullOrEmpty(tempObj.attributename) || string.IsNullOrEmpty(tempObj.attributevalue))
                    //                {
                    //                    var list = await FetchMeasurmentTemplate(obj.userdata.preorder.deviceid, obj.userdata.preorder.preorderproducts[j].dresscategoryid, obj.userdata.preorder.sessiontoken);
                    //                    if (list == null)
                    //                    {
                    //                        isMeasurementListfound = false;
                    //                        break;
                    //                    }
                    //                    else
                    //                    {
                    //                        for (int l = 0; l < list.Count; l++)
                    //                        {
                    //                            if(string.IsNullOrEmpty(obj.userdata.preorder.preorderproducts[j].preorderproductmeasurements[k].attributename))
                    //                            {
                    //                                // check count if not equal the return 
                    //                                // void below code 
                    //                            }
                    //                            Preorderproductmeasurement objMeasure = new Preorderproductmeasurement();
                    //                            objMeasure.attributeid = "-1";
                    //                            objMeasure.preorderproductmeasurementid = "-1";
                    //                            objMeasure.attributename = list[k].attributename;
                    //                            objMeasure.attributevalue = list[k].sizemax.ToString(); // min or max value ? 
                    //                            obj.userdata.preorder.preorderproducts[j].preorderproductmeasurements.Add(objMeasure);
                    //                        }
                    //                    }
                    //                }
                    //            }
                    //        }
                    //    }

                    //    if (!isMeasurementListfound)
                    //    {
                    //        return new Tuple<int, string>(-1, "Measurment missing !");
                    //    }

                    //    #endregion
                    //}
                    //else
                    //{
                    //    return new Tuple<int, string>(res.Item1, res.Item3);
                    //}
                    #endregion
                    var response = await CartValidationsNew(obj);
                    if(!response.Item1)
                    {
                        return new Tuple<int, string>(response.Item2, response.Item3);
                    }
                }

                #endregion

                #region Pre Order Type

                DataTable TBLPreordertype = new DataTable();
                TBLPreordertype.Columns.Add(new DataColumn("Preorderid", typeof(int)));
                TBLPreordertype.Columns.Add(new DataColumn("Customerid", typeof(int)));
                TBLPreordertype.Columns.Add(new DataColumn("Tailorid", typeof(int)));
                TBLPreordertype.Columns.Add(new DataColumn("Status", typeof(string)));
                TBLPreordertype.Columns.Add(new DataColumn("Totalestimatedcost", typeof(decimal)));
                TBLPreordertype.Columns.Add(new DataColumn("Pickcountry", typeof(string)));
                TBLPreordertype.Columns.Add(new DataColumn("Pickprovince", typeof(string)));
                TBLPreordertype.Columns.Add(new DataColumn("Pickcity", typeof(string)));
                TBLPreordertype.Columns.Add(new DataColumn("Pickzipcode", typeof(string))); 
                TBLPreordertype.Columns.Add(new DataColumn("Pickaddress", typeof(string)));
                TBLPreordertype.Columns.Add(new DataColumn("Dropcountry", typeof(string)));
                TBLPreordertype.Columns.Add(new DataColumn("Dropprovince", typeof(string)));
                TBLPreordertype.Columns.Add(new DataColumn("Dropcity", typeof(string)));
                TBLPreordertype.Columns.Add(new DataColumn("Dropzipcode", typeof(string)));
                TBLPreordertype.Columns.Add(new DataColumn("Dropaddress", typeof(string)));

                TBLPreordertype.Columns.Add(new DataColumn("istailoravailable", typeof(bool)));
                TBLPreordertype.Columns.Add(new DataColumn("totalproductcount", typeof(int)));
                TBLPreordertype.Columns.Add(new DataColumn("ispickaddressavailable", typeof(bool)));
                TBLPreordertype.Columns.Add(new DataColumn("isdropaddressavailable", typeof(bool)));
                

                int preorderID, customerID, tailorID = -1;
                decimal totalEstimatedCost = -1;
                bool _istailoravailable, _ispickaddressavailable, _isdropaddressavailable = false;
                int _totalproductcount = -1;
                int.TryParse(obj.userdata.preorder.totalproductcount, out _totalproductcount);
                bool.TryParse(obj.userdata.preorder.istailoravailable, out _istailoravailable);
                bool.TryParse(obj.userdata.preorder.ispickaddresscomplete, out _ispickaddressavailable);
                bool.TryParse(obj.userdata.preorder.isdropaddresscomplete, out _isdropaddressavailable);

                int.TryParse(obj.userdata.preorder.preorderid, out preorderID);
                int.TryParse(obj.userdata.preorder.customerid, out customerID);
                int.TryParse(obj.userdata.preorder.tailorid, out tailorID);

                decimal.TryParse(obj.userdata.preorder.totalestimatedcost, out totalEstimatedCost);

                DataRow drPreOrderType = TBLPreordertype.NewRow();
                drPreOrderType["Preorderid"] = preorderID;
                drPreOrderType["Customerid"] = customerID;
                drPreOrderType["Tailorid"] = tailorID;
                drPreOrderType["Status"] = obj.userdata.preorder.status;
                drPreOrderType["Totalestimatedcost"] = totalEstimatedCost;
                drPreOrderType["Pickcountry"] = obj.userdata.preorder.pickcountry;
                drPreOrderType["Pickprovince"] = obj.userdata.preorder.pickprovince;
                drPreOrderType["Pickcity"] = obj.userdata.preorder.pickcity;
                drPreOrderType["Pickzipcode"] = obj.userdata.preorder.pickzipcode;
                drPreOrderType["Pickaddress"] = obj.userdata.preorder.pickaddress;
                drPreOrderType["Dropcountry"] = obj.userdata.preorder.dropcountry;
                drPreOrderType["Dropprovince"] = obj.userdata.preorder.dropprovince;
                drPreOrderType["Dropcity"] = obj.userdata.preorder.dropcity;
                drPreOrderType["Dropzipcode"] = obj.userdata.preorder.dropzipcode;
                drPreOrderType["Dropaddress"] = obj.userdata.preorder.dropaddress;

                drPreOrderType["istailoravailable"] = _istailoravailable;
                drPreOrderType["totalproductcount"] = _totalproductcount;
                drPreOrderType["ispickaddressavailable"] = _ispickaddressavailable;
                drPreOrderType["isdropaddressavailable"] = _isdropaddressavailable;
                

                TBLPreordertype.Rows.Add(drPreOrderType); 

                #endregion

                #region Pre order product type & image & addon

                #region PreOrderProductImageType Declartion

                DataTable TBLPreorderProductImagetype = new DataTable();
                TBLPreorderProductImagetype.Columns.Add(new DataColumn("Preorderproductimageid", typeof(int)));
                TBLPreorderProductImagetype.Columns.Add(new DataColumn("Preorderproductid", typeof(int)));
                TBLPreorderProductImagetype.Columns.Add(new DataColumn("Image", typeof(string)));

                #endregion

                #region PreOrderProductAddOn Declartion 

                DataTable TBLPreorderProductAddOntype = new DataTable();
                TBLPreorderProductAddOntype.Columns.Add(new DataColumn("Preorderproductaddonid", typeof(int)));
                TBLPreorderProductAddOntype.Columns.Add(new DataColumn("Preorderproductid", typeof(int)));
                TBLPreorderProductAddOntype.Columns.Add(new DataColumn("Addontype", typeof(string)));
                TBLPreorderProductAddOntype.Columns.Add(new DataColumn("Addonvalue", typeof(string)));

                #endregion

                #region PreOrder Measurement Declaration

                DataTable TBLPreorderProductMeasurementtype = new DataTable();
                TBLPreorderProductMeasurementtype.Columns.Add(new DataColumn("Preorderproductmeasurementid", typeof(int)));
                TBLPreorderProductMeasurementtype.Columns.Add(new DataColumn("Preorderproductid", typeof(int)));
                TBLPreorderProductMeasurementtype.Columns.Add(new DataColumn("Attributeid", typeof(int)));
                TBLPreorderProductMeasurementtype.Columns.Add(new DataColumn("Attributename", typeof(string)));
                TBLPreorderProductMeasurementtype.Columns.Add(new DataColumn("Attributevalue", typeof(decimal)));

                #endregion

                DataTable TBLPreorderProducttype = new DataTable();
                TBLPreorderProducttype.Columns.Add(new DataColumn("Preorderproductid", typeof(int)));
                TBLPreorderProducttype.Columns.Add(new DataColumn("Preorderid", typeof(int)));
                TBLPreorderProducttype.Columns.Add(new DataColumn("Productid", typeof(int)));
                TBLPreorderProducttype.Columns.Add(new DataColumn("Dresscategoryid", typeof(int)));
                TBLPreorderProducttype.Columns.Add(new DataColumn("Estimatedcost", typeof(decimal)));
                TBLPreorderProducttype.Columns.Add(new DataColumn("Productsource", typeof(string)));
                TBLPreorderProducttype.Columns.Add(new DataColumn("Productsourceid", typeof(int)));
                TBLPreorderProducttype.Columns.Add(new DataColumn("Tailorstitchost", typeof(decimal)));
                // new code added
                TBLPreorderProducttype.Columns.Add(new DataColumn("isproductimagesavailable", typeof(bool)));
                TBLPreorderProducttype.Columns.Add(new DataColumn("isproductaddonavailable", typeof(bool)));
                TBLPreorderProducttype.Columns.Add(new DataColumn("isproductmeasurementavailable", typeof(bool)));


                int _preorderproductID = 0;
                
                for (int i = 0; i < obj.userdata.preorder.preorderproducts.Count; i++)
                {

                    int _preorderproductimageID = 0;
                    int _preorderproductaddonID = 0;
                    int _preorderproductmeasurementID = 0;
                    
                    bool _isproductimagesavailable = false;
                    bool _isproductaddonavailable = false;
                    bool _isproductmeasurementavailable = false;
                    bool.TryParse(obj.userdata.preorder.preorderproducts[i].isproductimagesavailable, out _isproductimagesavailable);
                    bool.TryParse(obj.userdata.preorder.preorderproducts[i].isproductaddonavailable, out _isproductaddonavailable);
                    bool.TryParse(obj.userdata.preorder.preorderproducts[i].isproductmeasurementavailable, out _isproductmeasurementavailable);

                    int preorderproductID, _preorderID, _productID, _dressCategoryID, ProductSourceID = -1;
                    decimal EstimatedCost, TailorStitchCost = -1;

                    int.TryParse(obj.userdata.preorder.preorderproducts[i].preorderproductid, out preorderproductID);
                    if(preorderproductID < 1)
                    {
                        --_preorderproductID;
                        preorderproductID = _preorderproductID;
                    }
                    int.TryParse(obj.userdata.preorder.preorderid, out _preorderID);
                    int.TryParse(obj.userdata.preorder.preorderproducts[i].productid, out _productID);
                    int.TryParse(obj.userdata.preorder.preorderproducts[i].dresscategoryid, out _dressCategoryID);
                    int.TryParse(obj.userdata.preorder.preorderproducts[i].productsourceid, out ProductSourceID);

                    decimal.TryParse(obj.userdata.preorder.preorderproducts[i].estimatedcost, out EstimatedCost);
                    decimal.TryParse(obj.userdata.preorder.preorderproducts[i].tailorstitchcost, out TailorStitchCost);

                    DataRow drPreOrderProductType = TBLPreorderProducttype.NewRow();

                    drPreOrderProductType["Preorderproductid"] = preorderproductID;
                    drPreOrderProductType["Preorderid"] = _preorderID;
                    drPreOrderProductType["Productid"] = _productID;
                    drPreOrderProductType["Dresscategoryid"] = _dressCategoryID;
                    drPreOrderProductType["Estimatedcost"] = EstimatedCost;
                    drPreOrderProductType["Productsource"] = obj.userdata.preorder.preorderproducts[i].productsource;
                    
                    #region PreOrder Product image type 
                    
                    int imglength = obj.userdata.preorder.preorderproducts[i].preorderproductimages.Count;
                    int AddOnLength = obj.userdata.preorder.preorderproducts[i].preorderproductaddons.Count;
                    int MeasurementLength = obj.userdata.preorder.preorderproducts[i].preorderproductmeasurements.Count;

                    for (int j  = 0; j < imglength; j++)
                    {
                        DataRow drProductImageType = TBLPreorderProductImagetype.NewRow();
                        int _preorderProductImageID = -1;
                        int.TryParse(obj.userdata.preorder.preorderproducts[i].preorderproductimages[j].preorderproductimageid,out _preorderProductImageID);
                        if(_preorderProductImageID < 1)
                        {
                            --_preorderproductimageID;
                            _preorderProductImageID = _preorderproductimageID;
                        }
                        string _imgPreOrderImage = obj.userdata.preorder.preorderproducts[i].preorderproductimages[j].image;
                        drProductImageType["Preorderproductimageid"] = _preorderProductImageID;
                        drProductImageType["Preorderproductid"] = preorderproductID;
                        drProductImageType["Image"] = _imgPreOrderImage; //obj.userdata.preorder.preorderproducts[i].productsource;
                        TBLPreorderProductImagetype.Rows.Add(drProductImageType);
                    }

                    #endregion

                    #region AddOn 

                    for (int k = 0; k < AddOnLength; k++)
                    {
                        int _preorderProductAddonID = -1;
                        int.TryParse(obj.userdata.preorder.preorderproducts[i].preorderproductaddons[k].preorderproductaddonid, out _preorderProductAddonID);
                        if(_preorderProductAddonID < 1)
                        {
                            --_preorderproductaddonID;
                            _preorderProductAddonID = _preorderproductaddonID;
                        }
                        DataRow drAddonType = TBLPreorderProductAddOntype.NewRow();
                        drAddonType["Preorderproductaddonid"] = _preorderProductAddonID;
                        drAddonType["Preorderproductid"] = preorderproductID;
                        // need to confirm from faisal bhai both are same addon type
                        drAddonType["Addontype"] = obj.userdata.preorder.preorderproducts[i].preorderproductaddons[k].addontype; 
                        drAddonType["Addonvalue"] = obj.userdata.preorder.preorderproducts[i].preorderproductaddons[k].addonvalue;
                        TBLPreorderProductAddOntype.Rows.Add(drAddonType);
                    }

                    #endregion

                    #region MeasurmentList

                    for (int l = 0; l < MeasurementLength; l++)
                    {
                        int _preorderProductMeasurementID = -1;
                        int AttributeID = -1;
                        decimal _AttributeValue = 0;
                        int.TryParse(obj.userdata.preorder.preorderproducts[i].preorderproductmeasurements[l].attributeid, out AttributeID);
                        int.TryParse(obj.userdata.preorder.preorderproducts[i].preorderproductmeasurements[l].preorderproductmeasurementid, out _preorderProductMeasurementID);
                        if(_preorderProductMeasurementID < 1)
                        {
                            --_preorderproductmeasurementID;
                            _preorderProductMeasurementID = _preorderproductmeasurementID;
                        }
                        decimal.TryParse(obj.userdata.preorder.preorderproducts[i].preorderproductmeasurements[l].attributevalue, out _AttributeValue);
                        DataRow drMeasurementType = TBLPreorderProductMeasurementtype.NewRow();
                        drMeasurementType["Preorderproductmeasurementid"] = _preorderProductMeasurementID;
                        drMeasurementType["Preorderproductid"] = preorderproductID;
                        drMeasurementType["Attributeid"] = AttributeID;
                        drMeasurementType["Attributename"] = obj.userdata.preorder.preorderproducts[i].preorderproductmeasurements[l].attributename;
                        drMeasurementType["Attributevalue"] = _AttributeValue;
                        TBLPreorderProductMeasurementtype.Rows.Add(drMeasurementType);
                    }

                    #endregion

                    drPreOrderProductType["Productsourceid"] = ProductSourceID;
                    drPreOrderProductType["Tailorstitchost"] = TailorStitchCost;

                    drPreOrderProductType["isproductimagesavailable"] = _isproductimagesavailable;
                    drPreOrderProductType["isproductaddonavailable"] = _isproductaddonavailable;
                    drPreOrderProductType["isproductmeasurementavailable"] = _isproductmeasurementavailable;
                    

                    TBLPreorderProducttype.Rows.Add(drPreOrderProductType); 
                }

                #endregion

                #region DAL Logic 

                int _customerID = -1;
                int.TryParse(obj.userdata.preorder.customerid, out _customerID);

                PreOrderDAL objDAL = new PreOrderDAL(logger);
                var result = await objDAL.FuncPreOrderDAL(TBLPreordertype, TBLPreorderProducttype, TBLPreorderProductImagetype, TBLPreorderProductAddOntype,
                    TBLPreorderProductMeasurementtype, obj.userdata.preorder.deviceid, obj.userdata.preorder.devicetype, obj.userdata.preorder.sessiontoken
                    , _customerID);

                #endregion

                return result;

            }
            catch (Exception ex)
            {
                return new Tuple<int, string>(-2, ex.ToString());
            }
        }
    }
}
