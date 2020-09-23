using CustomerBO.Measurements;
using CustomerDAL.Logging;
using CustomerDAL.Models;
using logginglibrary;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDAL.Measurements
{
    public class MeasurementDAL
    {
        private ILog logger;
        public MeasurementDAL(ILog templogger)
        {
            this.logger = templogger;
        }

        public async Task<Tuple<List<MeasurementBOResponse>, int, string>> GetMeasurementDAL(MeasurementBO objParam)
        {
            try
            {
                List<MeasurementBOResponse> LstResponse = new List<MeasurementBOResponse>();
                using (var dbContext = new SilaeeContext())
                {
                    using (var cmd = dbContext.Database.GetDbConnection().CreateCommand())
                    {
                        cmd.CommandText = "sp_Getcustomersavedmeasurements";
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        var CustomerIDParam = cmd.CreateParameter();
                        CustomerIDParam.ParameterName = "customerid";
                        CustomerIDParam.Value = objParam.customerid;
                        cmd.Parameters.Add(CustomerIDParam);

                        var DeviceIDParam = cmd.CreateParameter();
                        DeviceIDParam.ParameterName = "deviceid";
                        DeviceIDParam.Value = objParam.deviceid;
                        cmd.Parameters.Add(DeviceIDParam);

                        var DeviceTypeParam = cmd.CreateParameter();
                        DeviceTypeParam.ParameterName = "devicetype";
                        DeviceTypeParam.Value = objParam.devicetype;
                        cmd.Parameters.Add(DeviceTypeParam);

                        var SessionTokenParam = cmd.CreateParameter();
                        SessionTokenParam.ParameterName = "sessiontoken";
                        SessionTokenParam.Value = objParam.sessiontoken;
                        cmd.Parameters.Add(SessionTokenParam);


                        await dbContext.Database.OpenConnectionAsync();

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {

                                    if (reader.FieldCount == 2)
                                    {
                                        return new Tuple<List<MeasurementBOResponse>, int, string>(null, -1, "DeviceID OR SessionToken Not Available");
                                    }
                                    else
                                        LstResponse.Add(new MeasurementBOResponse()
                                        {
                                            measurementid = Convert.ToInt32(reader["Measurementid"].ToString()),
                                            attribute = reader["Attribute"].ToString(),
                                            attributesize = reader["Attributesize"].ToString(),
                                            dresscategory = reader["Dresscategory"].ToString()
                                        });
                                }
                                return new Tuple<List<MeasurementBOResponse>, int, string>(LstResponse, 1, "Success");
                            }
                            else
                            {
                                return new Tuple<List<MeasurementBOResponse>, int, string>(LstResponse, -2, "No Record Found");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(LogUserLogin.CreateErrorMsg("MeasurementDAL", "GetMeasurementDAL", DateTime.Now.ToString(), ex.ToString()));
                return new Tuple<List<MeasurementBOResponse>, int, string>(null, -3, ex.ToString());
            }
        }


    }
}
