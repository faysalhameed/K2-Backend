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
                    parameterList.Add(new SqlParameter("@CustomerFirstName", obj.customerfirstname));
                    parameterList.Add(new SqlParameter("@CustomerLastName", obj.customerlastname));
                    parameterList.Add(new SqlParameter("@CustomerAge", obj.customerage));
                    parameterList.Add(new SqlParameter("@CustomerGender", obj.customergender));
                    parameterList.Add(new SqlParameter("@CustomereMailAddress", obj.customeremailaddress));
                    parameterList.Add(new SqlParameter("@CustomerWebsite",obj.customerwebsite));
                    parameterList.Add(new SqlParameter("@CustomerCountry", obj.customercountry));
                    parameterList.Add(new SqlParameter("@CustomerCity", obj.customercity));
                    parameterList.Add(new SqlParameter("@CustomerProvince", obj.customerprovince));
                    parameterList.Add(new SqlParameter("@CustomerZipCode", obj.customerzipcode));
                    parameterList.Add(new SqlParameter("@CustomerAddress", obj.customeraddress));
                    parameterList.Add(new SqlParameter("@CustomerMobileNumber", obj.customermobilenumber));
                    parameterList.Add(new SqlParameter("@CustomerCNIC", obj.customercnic));
                    parameterList.Add(new SqlParameter("@CustomerPicture",obj.customerpicture));
                    parameterList.Add(new SqlParameter("@CustomerPassword", obj.customerpassword));
                    parameterList.Add(new SqlParameter("@CustomerRating", obj.customerrating));
                    parameterList.Add(new SqlParameter("@CustomerWalletAmount", obj.customerwalletamount));
                    parameterList.Add(new SqlParameter("@CustomerProfileStatus", obj.customerprofilestatus));                 
                    parameterList.Add(new SqlParameter("@CreatedbyUserTypeId", obj.createdbyusertypeid));
                    parameterList.Add(new SqlParameter("@CreatedbyUserId", obj.createdbyuserid));
                    parameterList.Add(new SqlParameter("@ModifiedbyUserTypeId", obj.modifiedbyusertypeid));
                    parameterList.Add(new SqlParameter("@ModifiedbyUserId", obj.modifiedbyuserid));
                    parameterList.Add(new SqlParameter("@CreatedbyDeviceId", obj.createdbydeviceid));
                    parameterList.Add(new SqlParameter("@ModifiedbyDeviceId", obj.modifiedbydeviceid));
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
                    parameterList.Add(new SqlParameter("@CustomerFirstName", obj.customerfirstname));
                    parameterList.Add(new SqlParameter("@CustomerLastName", obj.customerlastname));
                    parameterList.Add(new SqlParameter("@CustomerAge", obj.customerage));
                    parameterList.Add(new SqlParameter("@CustomerGender", obj.customergender));
                    parameterList.Add(new SqlParameter("@CustomereMailAddress", obj.customeremailaddress));
                    parameterList.Add(new SqlParameter("@CustomerWebsite", obj.customerwebsite));
                    parameterList.Add(new SqlParameter("@CustomerCountry", obj.customercountry));
                    parameterList.Add(new SqlParameter("@CustomerCity", obj.customercity));
                    parameterList.Add(new SqlParameter("@CustomerProvince", obj.customerprovince));
                    parameterList.Add(new SqlParameter("@CustomerZipCode", obj.customerzipcode));
                    parameterList.Add(new SqlParameter("@CustomerAddress", obj.customeraddress));
                    parameterList.Add(new SqlParameter("@CustomerMobileNumber", obj.customermobilenumber));
                    parameterList.Add(new SqlParameter("@CustomerCNIC", obj.customercnic));
                    parameterList.Add(new SqlParameter("@CustomerPicture", obj.customerpicture));
                    parameterList.Add(new SqlParameter("@CustomerPassword", obj.customerpassword));
                    parameterList.Add(new SqlParameter("@CustomerRating", obj.customerrating));
                    parameterList.Add(new SqlParameter("@CustomerWalletAmount", obj.customerwalletamount));
                    parameterList.Add(new SqlParameter("@CustomerProfileStatus", obj.customerprofilestatus));
                    parameterList.Add(new SqlParameter("@CreatedbyUserTypeId", obj.createdbyusertypeid));
                    parameterList.Add(new SqlParameter("@CreatedbyUserId", obj.createdbyuserid));
                    parameterList.Add(new SqlParameter("@ModifiedbyUserTypeId", obj.modifiedbyusertypeid));
                    parameterList.Add(new SqlParameter("@ModifiedbyUserId", obj.modifiedbyuserid));
                    parameterList.Add(new SqlParameter("@CreatedbyDeviceId", obj.createdbydeviceid));
                    parameterList.Add(new SqlParameter("@ModifiedbyDeviceId", obj.modifiedbydeviceid));
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
                objEntity.customeremailaddress = objLoginUser.emailaddress;
                objEntity.customermobilenumber = objLoginUser.phonenumber;
                objEntity.customerfirstname = objLoginUser.userfirstname;
                objEntity.customerlastname = objLoginUser.userlastname;
                objEntity.customerage = 0;
                objEntity.customergender = "Default";
                objEntity.customerpassword = "Default-Password";
                objEntity.customerwebsite = "";
                objEntity.customercountry = "";
                objEntity.customercity = "";
                objEntity.customerprovince = "";
                objEntity.customerzipcode = "";
                objEntity.customeraddress = "";
                objEntity.customercnic = "";
                objEntity.customerpicture = "";
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

        #region Update API DAL Function 

        public LoginResponseBO UpdateCustomerInfo()
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        #endregion


    }
}
