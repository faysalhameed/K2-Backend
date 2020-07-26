using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using CustomerBO.User;
using CustomerDAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerDAL.UsersOperations
{
    public class UserDAL : IUsers
    {
        
        public int SaveUser(Userdata obj)
        {
            try
            {
                using (var dbContext = new SilaeeContext())
                {
                    #region Commented Code
                    //using (var cmd = dbContext.Database.GetDbConnection().CreateCommand())
                    //{
                    //    cmd.CommandText = "exec uspGetOrgChart";
                    //    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //    var personIdParam = cmd.CreateParameter();
                    //    personIdParam.ParameterName = "ContactID";
                    //    personIdParam.Value = id;

                    //    cmd.Parameters.Add(personIdParam);

                    //    dbContext.Database.OpenConnection();
                    //    using (var result1 = cmd.ExecuteReader())
                    //    {
                    //        if (result1.HasRows)
                    //        {
                    //            // do something with results
                    //        }
                    //    }
                    //}
                    #endregion

                    string sql = "sp_SaveCustomerProfile @CustomerFirstName,@CustomerLastName,@CustomerAge,@CustomerGender," +
                        "@CustomereMailAddress,@CustomerWebsite,@CustomerCountry,@CustomerCity,@CustomerProvince," +
                        "@CustomerZipCode,@CustomerAddress,@CustomerMobileNumber,@CustomerCNIC,@CustomerPicture," +
                        "@CustomerPassword,@CustomerRating,@CustomerWalletAmount,@CustomerProfileStatus," +
                        "@CreatedbyUserTypeId,@CreatedbyUserId,@ModifiedbyUserTypeId,@ModifiedbyUserId," +
                        "@CreatedbyDeviceId,@ModifiedbyDeviceId,@ResultParam OUT";
                    List<SqlParameter> parameterList = new List<SqlParameter>();
                    parameterList.Add(new SqlParameter("@CustomerFirstName", obj.CustomerFirstName));
                    parameterList.Add(new SqlParameter("@CustomerLastName", obj.CustomerLastName));
                    parameterList.Add(new SqlParameter("@CustomerAge", obj.CustomerAge));
                    parameterList.Add(new SqlParameter("@CustomerGender", (string.IsNullOrEmpty(obj.CustomerGender)) ? "" : obj.CustomerGender));
                    parameterList.Add(new SqlParameter("@CustomereMailAddress", obj.CustomereMailAddress));
                    parameterList.Add(new SqlParameter("@CustomerWebsite", (string.IsNullOrEmpty(obj.CustomerWebsite)) ? "" : obj.CustomerWebsite));
                    parameterList.Add(new SqlParameter("@CustomerCountry", (string.IsNullOrEmpty(obj.CustomerCountry)) ? "" : obj.CustomerCountry));
                    parameterList.Add(new SqlParameter("@CustomerCity", (string.IsNullOrEmpty(obj.CustomerCity)) ? "" : obj.CustomerCity));
                    parameterList.Add(new SqlParameter("@CustomerProvince", (string.IsNullOrEmpty(obj.CustomerProvince)) ? "" : obj.CustomerProvince));
                    parameterList.Add(new SqlParameter("@CustomerZipCode", (string.IsNullOrEmpty(obj.CustomerZipCode)) ? "" : obj.CustomerZipCode));
                    parameterList.Add(new SqlParameter("@CustomerAddress", (string.IsNullOrEmpty(obj.CustomerAddress)) ? "" : obj.CustomerAddress));
                    parameterList.Add(new SqlParameter("@CustomerMobileNumber", obj.CustomerMobileNumber));
                    parameterList.Add(new SqlParameter("@CustomerCNIC", (string.IsNullOrEmpty(obj.CustomerCNIC)) ? "" : obj.CustomerCNIC));
                    parameterList.Add(new SqlParameter("@CustomerPicture", (string.IsNullOrEmpty(obj.CustomerPicture)) ? "" : obj.CustomerPicture));
                    parameterList.Add(new SqlParameter("@CustomerPassword", obj.CustomerPassword));
                    parameterList.Add(new SqlParameter("@CustomerRating", obj.CustomerRating));
                    parameterList.Add(new SqlParameter("@CustomerWalletAmount", obj.CustomerWalletAmount));
                    parameterList.Add(new SqlParameter("@CustomerProfileStatus", obj.CustomerProfileStatus));                 
                    parameterList.Add(new SqlParameter("@CreatedbyUserTypeId", obj.CreatedbyUserTypeId));
                    parameterList.Add(new SqlParameter("@CreatedbyUserId", obj.CreatedbyUserId));
                    parameterList.Add(new SqlParameter("@ModifiedbyUserTypeId", obj.ModifiedbyUserTypeId));
                    parameterList.Add(new SqlParameter("@ModifiedbyUserId", obj.ModifiedbyUserId));
                    parameterList.Add(new SqlParameter("@CreatedbyDeviceId", obj.CreatedbyDeviceId));
                    parameterList.Add(new SqlParameter("@ModifiedbyDeviceId", obj.ModifiedbyDeviceId));
                    var out1 = new SqlParameter
                    {
                        ParameterName = "@ResultParam",
                        DbType = System.Data.DbType.Int32,
                        Direction = System.Data.ParameterDirection.Output
                    };
                    parameterList.Add(out1); //new SqlParameter("@ResultParam", obj.ModifiedbyDeviceId));
                    int result = dbContext.Database.
                        ExecuteSqlCommand(sql, parameterList);
                    var out1Value = (int)out1.Value;
                    return out1Value;
                    
                }
            }
            catch (Exception ex)
            {
                return - 1;
               // return null; // also we can log exception through common layer }
            }
        }
    }
}
