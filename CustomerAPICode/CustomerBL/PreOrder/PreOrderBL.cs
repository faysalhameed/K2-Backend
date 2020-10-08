using CustomerBO.PreOrder;
using CustomerDAL.PreOrder;
using logginglibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
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

        public async Task<Tuple<int,string>> FuncPreOrderBL(PreOrderBO obj)
        {
            try
            {
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

                int preorderID, customerID, tailorID = -1;
                decimal totalEstimatedCost = -1;

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

                

                for (int i = 0; i < obj.userdata.preorder.preorderproducts.Count; i++)
                {
                    int preorderproductID, _preorderID, _productID, _dressCategoryID, ProductSourceID = -1;
                    decimal EstimatedCost, TailorStitchCost = -1;

                    int.TryParse(obj.userdata.preorder.preorderproducts[i].preorderproductid, out preorderproductID);
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
                        DataRow drAddonType = TBLPreorderProductAddOntype.NewRow();
                        drAddonType["Preorderproductaddonid"] = _preorderProductAddonID;
                        drAddonType["Preorderproductid"] = preorderproductID;
                        // need to confirm from faisal bhai both are same addon type
                        drAddonType["Addontype"] = obj.userdata.preorder.preorderproducts[i].preorderproductaddons[k].addontype; 
                        drAddonType["Addonvalue"] = obj.userdata.preorder.preorderproducts[i].preorderproductaddons[k].addontype;
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
