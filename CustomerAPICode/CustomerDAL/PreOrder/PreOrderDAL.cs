using CustomerBO.PreOrder;
using CustomerDAL.Logging;
using CustomerDAL.Models;
using logginglibrary;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDAL.PreOrder
{
    public class PreOrderDAL
    {
        private ILog logger;
        public PreOrderDAL(ILog templogger)
        {
            this.logger = templogger;
        }

        public async Task<Tuple<int, string>> FuncPreOrderDAL(DataTable PreorderType, DataTable PreOrderProdutType, DataTable PreOrderProductImageType, 
            DataTable PreOrderProductAddOnType, DataTable PreOrderProductMeasurmentType, String deviceID, string DeviceType, 
            string SessionToken, int customerID)
        {
            try
            {
                using (var dbContext = new SilaeeContext())
                {
                    using (var cmd = dbContext.Database.GetDbConnection().CreateCommand())
                    {
                        cmd.CommandText = "sp_InsertUpdatePreorders";
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        var dtPreOrderTypeParam = cmd.CreateParameter();
                        dtPreOrderTypeParam.ParameterName = "Preordertypeobj";
                        dtPreOrderTypeParam.Value = PreorderType;
                        cmd.Parameters.Add(dtPreOrderTypeParam);

                        var dtPreOrderProductTypeParam = cmd.CreateParameter();
                        dtPreOrderProductTypeParam.ParameterName = "Preorderproducttypeobj";
                        dtPreOrderProductTypeParam.Value = PreOrderProdutType;
                        cmd.Parameters.Add(dtPreOrderProductTypeParam);

                        var dtPreOrderProductImageTypeParam = cmd.CreateParameter();
                        dtPreOrderProductImageTypeParam.ParameterName = "Preorderproductimagetypeobj";
                        dtPreOrderProductImageTypeParam.Value = PreOrderProductImageType;
                        cmd.Parameters.Add(dtPreOrderProductImageTypeParam);

                        var dtPreOrderProductAddOnTypeParam = cmd.CreateParameter();
                        dtPreOrderProductAddOnTypeParam.ParameterName = "Preorderproductaddontypeobj";
                        dtPreOrderProductAddOnTypeParam.Value = PreOrderProductAddOnType;
                        cmd.Parameters.Add(dtPreOrderProductAddOnTypeParam);

                        var dtPreOrderProductMeasurmentTypeParam = cmd.CreateParameter();
                        dtPreOrderProductMeasurmentTypeParam.ParameterName = "Preorderproductmeasurementtypeobj";
                        dtPreOrderProductMeasurmentTypeParam.Value = PreOrderProductMeasurmentType;
                        cmd.Parameters.Add(dtPreOrderProductMeasurmentTypeParam);

                        var DeviceIDParam = cmd.CreateParameter();
                        DeviceIDParam.ParameterName = "Deviceid";
                        DeviceIDParam.Value = deviceID;
                        cmd.Parameters.Add(DeviceIDParam);

                        var DeviceTypeParam = cmd.CreateParameter();
                        DeviceTypeParam.ParameterName = "Devicetype";
                        DeviceTypeParam.Value = DeviceType;
                        cmd.Parameters.Add(DeviceTypeParam);

                        var SessiontokenParam = cmd.CreateParameter();
                        SessiontokenParam.ParameterName = "Sessiontoken";
                        SessiontokenParam.Value = SessionToken;
                        cmd.Parameters.Add(SessiontokenParam);

                        var CustomeridParam = cmd.CreateParameter();
                        CustomeridParam.ParameterName = "Customerid";
                        CustomeridParam.Value = customerID;
                        cmd.Parameters.Add(CustomeridParam);

                        await dbContext.Database.OpenConnectionAsync();

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                return new Tuple<int, string>(-1, "DeviceID OR SessionToken Not Available");
                            }
                            else
                            {
                                return new Tuple<int, string>(1, "Action successfully perform.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(LogUserLogin.CreateErrorMsg("PreOrderDAL", "FuncPreOrderDAL", DateTime.Now.ToString(), ex.ToString()));
                return new Tuple<int, string>(-3, ex.ToString());
            }
        }
    }

    }

