using OrderManagmentBO.Order;
using OrderManagmentDAL.Order;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagmentBL.Order
{
    public class OrderBL
    {
        public async Task<Tuple<int,bool,string>> CreateOrderBL(OrderBO obj)
        {
            try
            {
                #region Order Type

                DataTable TBLPreordertype = new DataTable();
                TBLPreordertype.Columns.Add(new DataColumn("Orderid", typeof(int)));
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
                drPreOrderType["Orderid"] = preorderID;
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
                TBLPreorderProductImagetype.Columns.Add(new DataColumn("Orderproductimageid", typeof(int)));
                TBLPreorderProductImagetype.Columns.Add(new DataColumn("Orderproductid", typeof(int)));
                TBLPreorderProductImagetype.Columns.Add(new DataColumn("Image", typeof(string)));

                #endregion

                #region PreOrderProductAddOn Declartion 

                DataTable TBLPreorderProductAddOntype = new DataTable();
                TBLPreorderProductAddOntype.Columns.Add(new DataColumn("Orderproductaddonid", typeof(int)));
                TBLPreorderProductAddOntype.Columns.Add(new DataColumn("Orderproductid", typeof(int)));
                TBLPreorderProductAddOntype.Columns.Add(new DataColumn("Addontype", typeof(string)));
                TBLPreorderProductAddOntype.Columns.Add(new DataColumn("Addonvalue", typeof(string)));

                #endregion

                #region PreOrder Measurement Declaration

                DataTable TBLPreorderProductMeasurementtype = new DataTable();
                TBLPreorderProductMeasurementtype.Columns.Add(new DataColumn("Orderproductmeasurementid", typeof(int)));
                TBLPreorderProductMeasurementtype.Columns.Add(new DataColumn("Orderproductid", typeof(int)));
                TBLPreorderProductMeasurementtype.Columns.Add(new DataColumn("Attributeid", typeof(int)));
                TBLPreorderProductMeasurementtype.Columns.Add(new DataColumn("Attributename", typeof(string)));
                TBLPreorderProductMeasurementtype.Columns.Add(new DataColumn("Attributevalue", typeof(decimal)));

                #endregion

                DataTable TBLPreorderProducttype = new DataTable();
                TBLPreorderProducttype.Columns.Add(new DataColumn("Orderproductid", typeof(int)));
                TBLPreorderProducttype.Columns.Add(new DataColumn("Orderid", typeof(int)));
                TBLPreorderProducttype.Columns.Add(new DataColumn("Productid", typeof(int)));
                TBLPreorderProducttype.Columns.Add(new DataColumn("Dresscategoryid", typeof(int)));
                TBLPreorderProducttype.Columns.Add(new DataColumn("Estimatedcost", typeof(decimal)));
                TBLPreorderProducttype.Columns.Add(new DataColumn("Productsource", typeof(string)));
                TBLPreorderProducttype.Columns.Add(new DataColumn("Productsourceid", typeof(int)));
                TBLPreorderProducttype.Columns.Add(new DataColumn("Tailorstitchost", typeof(decimal)));
                

                int _preorderproductID = 0;

                for (int i = 0; i < obj.userdata.preorder.preorderproducts.Count; i++)
                {

                    int _preorderproductimageID = 0;
                    int _preorderproductaddonID = 0;
                    int _preorderproductmeasurementID = 0;

                    

                    int preorderproductID, _preorderID, _productID, _dressCategoryID, ProductSourceID = -1;
                    decimal EstimatedCost, TailorStitchCost = -1;

                    int.TryParse(obj.userdata.preorder.preorderproducts[i].preorderproductid, out preorderproductID);
                    if (preorderproductID < 1)
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

                    drPreOrderProductType["Orderproductid"] = preorderproductID;
                    drPreOrderProductType["Orderid"] = _preorderID;
                    drPreOrderProductType["Productid"] = _productID;
                    drPreOrderProductType["Dresscategoryid"] = _dressCategoryID;
                    drPreOrderProductType["Estimatedcost"] = EstimatedCost;
                    drPreOrderProductType["Productsource"] = obj.userdata.preorder.preorderproducts[i].productsource;

                    #region PreOrder Product image type 

                    int imglength = obj.userdata.preorder.preorderproducts[i].preorderproductimages.Count;
                    int AddOnLength = obj.userdata.preorder.preorderproducts[i].preorderproductaddons.Count;
                    int MeasurementLength = obj.userdata.preorder.preorderproducts[i].preorderproductmeasurements.Count;

                    for (int j = 0; j < imglength; j++)
                    {
                        DataRow drProductImageType = TBLPreorderProductImagetype.NewRow();
                        int _preorderProductImageID = -1;
                        int.TryParse(obj.userdata.preorder.preorderproducts[i].preorderproductimages[j].preorderproductimageid, out _preorderProductImageID);
                        if (_preorderProductImageID < 1)
                        {
                            --_preorderproductimageID;
                            _preorderProductImageID = _preorderproductimageID;
                        }
                        string _imgPreOrderImage = obj.userdata.preorder.preorderproducts[i].preorderproductimages[j].image;
                        drProductImageType["Orderproductimageid"] = _preorderProductImageID;
                        drProductImageType["Orderproductid"] = preorderproductID;
                        drProductImageType["Image"] = _imgPreOrderImage; //obj.userdata.preorder.preorderproducts[i].productsource;
                        TBLPreorderProductImagetype.Rows.Add(drProductImageType);
                    }

                    #endregion

                    #region AddOn 

                    for (int k = 0; k < AddOnLength; k++)
                    {
                        int _preorderProductAddonID = -1;
                        int.TryParse(obj.userdata.preorder.preorderproducts[i].preorderproductaddons[k].preorderproductaddonid, out _preorderProductAddonID);
                        if (_preorderProductAddonID < 1)
                        {
                            --_preorderproductaddonID;
                            _preorderProductAddonID = _preorderproductaddonID;
                        }
                        DataRow drAddonType = TBLPreorderProductAddOntype.NewRow();
                        drAddonType["Orderproductaddonid"] = _preorderProductAddonID;
                        drAddonType["Orderproductid"] = preorderproductID;
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
                        if (_preorderProductMeasurementID < 1)
                        {
                            --_preorderproductmeasurementID;
                            _preorderProductMeasurementID = _preorderproductmeasurementID;
                        }
                        decimal.TryParse(obj.userdata.preorder.preorderproducts[i].preorderproductmeasurements[l].attributevalue, out _AttributeValue);
                        DataRow drMeasurementType = TBLPreorderProductMeasurementtype.NewRow();
                        drMeasurementType["Orderproductmeasurementid"] = _preorderProductMeasurementID;
                        drMeasurementType["Orderproductid"] = preorderproductID;
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

                OrderDAL objDAL = new OrderDAL();
                var result = await objDAL.FuncOrderDAL(TBLPreordertype, TBLPreorderProducttype, TBLPreorderProductImagetype, TBLPreorderProductAddOntype,
                    TBLPreorderProductMeasurementtype, obj.userdata.preorder.deviceid, _customerID);

                #endregion
                return result;
            }
            catch (Exception ex)
            {
                return new Tuple<int, bool, string>(-1, false, ex.ToString());
            }
        }
    }
}
