using CustomerBO.Settings;
using CustomerDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerDAL.SettingOperations
{
    public class SettingDAL : ISettings
    {
        public List<SettingListBO> getEmailSettings()
        {
            try
            {
                List<SettingListBO> LstResponse = new List<SettingListBO>();
                using (var dbContext = new SilaeeContext())
                {
                    using (var cmd = dbContext.Database.GetDbConnection().CreateCommand())
                    {
                        cmd.CommandText = "sp_FetchEmailSettings";
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        dbContext.Database.OpenConnection();

                        using (var reader = cmd.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                if (reader.HasRows)
                                {
                                    
                                    LstResponse.Add(new SettingListBO()
                                    {
                                        vSettingName = reader["vSettingName"].ToString(),
                                        vSettingValue = reader["vSettingValue"].ToString()
                                    });
                                    
                                }
                            }
                        }
                    }
                    return LstResponse;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
