using CustomerBO.Device;
using CustomerDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerDAL.Device
{
    public class DeviceDAL
    {
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
                return null;
            }
        }
    }
}
