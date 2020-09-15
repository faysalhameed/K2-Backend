using CustomerBO.Device;
using CustomerDAL.Logging;
using CustomerDAL.Models;
using logginglibrary;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerDAL.Device
{
    public class DeviceDAL
    {
        private ILog logger;

        public DeviceDAL(ILog templogger)
        {
            this.logger = templogger;
        }

        public  DeviceBOResponse AuthDeviceID(string deviceID)
        {
            try
            {
                DeviceBOResponse objResponse = new DeviceBOResponse();
                using (var dbContext = new SilaeeContext())
                {
                    using (var cmd = dbContext.Database.GetDbConnection().CreateCommand())
                    {
                        cmd.CommandText = "sp_CheckDeviceAuthentication";
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        var DeviceIDParam = cmd.CreateParameter();
                        DeviceIDParam.ParameterName = "Deviceid";
                        DeviceIDParam.Value = deviceID;
                        cmd.Parameters.Add(DeviceIDParam);
                        dbContext.Database.OpenConnection();

                        using (var reader =  cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (reader.HasRows)
                                {
                                    objResponse.responsecode = Convert.ToInt32(reader["responsecode"].ToString());
                                    objResponse.responsemsg = reader["responsemessage"].ToString();
                                }
                            }
                        }
                    }
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                logger.Error(LogUserLogin.CreateErrorMsg("DeviceDAL", "AuthDeviceID", DateTime.Now.ToString(), ex.ToString()));
                return null;
            }
        }
    }
}
