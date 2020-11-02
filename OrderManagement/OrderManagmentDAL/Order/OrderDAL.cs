using Microsoft.EntityFrameworkCore;
using OrderManagmentDAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagmentDAL.Order
{
    public class OrderDAL
    {
        

        public async Task<Tuple<int,bool, string>> FuncOrderDAL(DataTable OrderType, DataTable OrderProdutType, DataTable OrderProductImageType,
            DataTable PreOrderProductAddOnType, DataTable PreOrderProductMeasurmentType, String deviceID,int customerID)
        {
            try
            {
                using (var dbContext = new OrderContext())
                {
                    using (var cmd = dbContext.Database.GetDbConnection().CreateCommand())
                    {
                        cmd.CommandText = "sp_CreateOrders";
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        var dtPreOrderTypeParam = cmd.CreateParameter();
                        dtPreOrderTypeParam.ParameterName = "Ordertypeobj";
                        dtPreOrderTypeParam.Value = OrderType;
                        cmd.Parameters.Add(dtPreOrderTypeParam);

                        var dtPreOrderProductTypeParam = cmd.CreateParameter();
                        dtPreOrderProductTypeParam.ParameterName = "Orderproducttypeobj";
                        dtPreOrderProductTypeParam.Value = OrderProdutType;
                        cmd.Parameters.Add(dtPreOrderProductTypeParam);

                        var dtPreOrderProductImageTypeParam = cmd.CreateParameter();
                        dtPreOrderProductImageTypeParam.ParameterName = "Orderproductimagetypeobj";
                        dtPreOrderProductImageTypeParam.Value = OrderProductImageType;
                        cmd.Parameters.Add(dtPreOrderProductImageTypeParam);

                        var dtPreOrderProductAddOnTypeParam = cmd.CreateParameter();
                        dtPreOrderProductAddOnTypeParam.ParameterName = "Orderproductaddontypeobj";
                        dtPreOrderProductAddOnTypeParam.Value = PreOrderProductAddOnType;
                        cmd.Parameters.Add(dtPreOrderProductAddOnTypeParam);

                        var dtPreOrderProductMeasurmentTypeParam = cmd.CreateParameter();
                        dtPreOrderProductMeasurmentTypeParam.ParameterName = "Orderproductmeasurementtypeobj";
                        dtPreOrderProductMeasurmentTypeParam.Value = PreOrderProductMeasurmentType;
                        cmd.Parameters.Add(dtPreOrderProductMeasurmentTypeParam);

                        var DeviceIDParam = cmd.CreateParameter();
                        DeviceIDParam.ParameterName = "Deviceid";
                        DeviceIDParam.Value = deviceID;
                        cmd.Parameters.Add(DeviceIDParam);

                        var CustomeridParam = cmd.CreateParameter();
                        CustomeridParam.ParameterName = "Customerid";
                        CustomeridParam.Value = customerID;
                        cmd.Parameters.Add(CustomeridParam);

                        await dbContext.Database.OpenConnectionAsync();

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                int OrderNumber = -1;
                                while(reader.Read())
                                {
                                    try
                                    {
                                        OrderNumber = Convert.ToInt32(reader["Orderid"]);
                                        reader.Close();
                                        return new Tuple<int,bool, string>(OrderNumber, true, "Order Saved.");
                                    }
                                    catch (Exception ex)
                                    {
                                        return new Tuple<int,bool, string>(-1, false, ex.ToString());
                                    }
                                }
                                return new Tuple<int,bool, string>(-1,false, "Unable to save order.");
                            }
                            else
                            {
                                return new Tuple<int,bool, string>(-1,false, "Unable to save order.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
                return new Tuple<int, bool, string>(-3,false, ex.ToString());
            }
        }

    }
}
