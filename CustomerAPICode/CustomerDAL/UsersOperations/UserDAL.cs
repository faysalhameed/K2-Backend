using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using CustomerBO.User;
using CustomerDAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

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
                    parameterList.Add(new SqlParameter("@CustomerGender", obj.CustomerGender));
                    parameterList.Add(new SqlParameter("@CustomereMailAddress", obj.CustomereMailAddress));
                    parameterList.Add(new SqlParameter("@CustomerWebsite",obj.CustomerWebsite));
                    parameterList.Add(new SqlParameter("@CustomerCountry", obj.CustomerCountry));
                    parameterList.Add(new SqlParameter("@CustomerCity", obj.CustomerCity));
                    parameterList.Add(new SqlParameter("@CustomerProvince", obj.CustomerProvince));
                    parameterList.Add(new SqlParameter("@CustomerZipCode", obj.CustomerZipCode));
                    parameterList.Add(new SqlParameter("@CustomerAddress", obj.CustomerAddress));
                    parameterList.Add(new SqlParameter("@CustomerMobileNumber", obj.CustomerMobileNumber));
                    parameterList.Add(new SqlParameter("@CustomerCNIC", obj.CustomerCNIC));
                    parameterList.Add(new SqlParameter("@CustomerPicture",obj.CustomerPicture));
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


        #region Login API DAL Function

        public LoginResponseBO CustomLoginUser(string UserName, string Password)
        {
            try
            {
                LoginResponseBO objResponse = new LoginResponseBO();
                using (var dbContext = new SilaeeContext())
                {
                    using (var cmd = dbContext.Database.GetDbConnection().CreateCommand())
                    { 
                        cmd.CommandText = "sp_AuthenticateUserCustom";
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        var UserNameParam = cmd.CreateParameter();
                        UserNameParam.ParameterName = "UserName";
                        UserNameParam.Value = UserName;
                        cmd.Parameters.Add(UserNameParam);

                        var UserPasswordParam = cmd.CreateParameter();
                        UserPasswordParam.ParameterName = "Password";
                        UserPasswordParam.Value = Password;
                        cmd.Parameters.Add(UserPasswordParam);

                        dbContext.Database.OpenConnection();
                        
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows) 
                            {
                                if(reader.Read())
                                {
                                    objResponse.issuccessful = true;
                                    objResponse.customerid = Convert.ToInt32(reader["CustomerID"]);
                                    objResponse.customerfirstname = reader["CustomerFirstName"].ToString();
                                    objResponse.customerlastname = reader["CustomerLastName"].ToString();
                                    objResponse.responsemessage = "Login Successfully !";
                                }
                            }
                            else
                            {
                                objResponse.issuccessful = false;
                                objResponse.customerid = -1;
                                objResponse.customerfirstname = "";
                                objResponse.customerlastname = "";
                                objResponse.responsemessage = "No user found";
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

        private int GetUserID(Userdata obj)
        {
            try
            {
                using (var dbContext = new SilaeeContext())
                {
                    string sql = "sp_AuthenticateMedium @CustomerFirstName,@CustomerLastName,@CustomerAge,@CustomerGender," +
                        "@CustomereMailAddress,@CustomerWebsite,@CustomerCountry,@CustomerCity,@CustomerProvince," +
                        "@CustomerZipCode,@CustomerAddress,@CustomerMobileNumber,@CustomerCNIC,@CustomerPicture," +
                        "@CustomerPassword,@CustomerRating,@CustomerWalletAmount,@CustomerProfileStatus," +
                        "@CreatedbyUserTypeId,@CreatedbyUserId,@ModifiedbyUserTypeId,@ModifiedbyUserId," +
                        "@CreatedbyDeviceId,@ModifiedbyDeviceId,@CustomerID OUT";
                    List<SqlParameter> parameterList = new List<SqlParameter>();
                    parameterList.Add(new SqlParameter("@CustomerFirstName", obj.CustomerFirstName));
                    parameterList.Add(new SqlParameter("@CustomerLastName", obj.CustomerLastName));
                    parameterList.Add(new SqlParameter("@CustomerAge", obj.CustomerAge));
                    parameterList.Add(new SqlParameter("@CustomerGender", obj.CustomerGender));
                    parameterList.Add(new SqlParameter("@CustomereMailAddress", obj.CustomereMailAddress));
                    parameterList.Add(new SqlParameter("@CustomerWebsite", obj.CustomerWebsite));
                    parameterList.Add(new SqlParameter("@CustomerCountry", obj.CustomerCountry));
                    parameterList.Add(new SqlParameter("@CustomerCity", obj.CustomerCity));
                    parameterList.Add(new SqlParameter("@CustomerProvince", obj.CustomerProvince));
                    parameterList.Add(new SqlParameter("@CustomerZipCode", obj.CustomerZipCode));
                    parameterList.Add(new SqlParameter("@CustomerAddress", obj.CustomerAddress));
                    parameterList.Add(new SqlParameter("@CustomerMobileNumber", obj.CustomerMobileNumber));
                    parameterList.Add(new SqlParameter("@CustomerCNIC", obj.CustomerCNIC));
                    parameterList.Add(new SqlParameter("@CustomerPicture", obj.CustomerPicture));
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
                        ParameterName = "@CustomerID",
                        DbType = System.Data.DbType.Int32,
                        Direction = System.Data.ParameterDirection.Output
                    };
                    parameterList.Add(out1);
                    int result = dbContext.Database.
                        ExecuteSqlCommand(sql, parameterList);
                    var out1Value = (int)out1.Value;
                    return out1Value;

                }
            }
            catch (Exception ex)
            {
                return -1;
                // return null; // also we can log exception through common layer }
            }
        }

        private Userdata CreateUserObject(LoginBO objLoginUser)
        {
            try
            {
                Userdata objEntity = new Userdata();
                objEntity.CustomereMailAddress = objLoginUser.emailaddress;
                objEntity.CustomerMobileNumber = objLoginUser.phonenumber;
                objEntity.CustomerFirstName = objLoginUser.userfirstname;
                objEntity.CustomerLastName = objLoginUser.userlastname;
                objEntity.CustomerAge = 0;
                objEntity.CustomerGender = "Default";
                objEntity.CustomerPassword = "Default-Password";
                objEntity.CustomerWebsite = "";
                objEntity.CustomerCountry = "";
                objEntity.CustomerCity = "";
                objEntity.CustomerProvince = "";
                objEntity.CustomerZipCode = "";
                objEntity.CustomerAddress = "";
                objEntity.CustomerCNIC = "";
                objEntity.CustomerPicture = "";
                return objEntity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int LoginMediumUser(LoginBO obj)
        {
            try
            {
                LoginResponseBO objResponse = new LoginResponseBO();
                var objEntity = CreateUserObject(obj);
                int id = GetUserID(objEntity);
                if(id > 0)
                {
                    // success
                    return id;
                }
                else
                {
                    // error 
                    return -1;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        #endregion


    }
}
