using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using CustomerBO.User;
using CustomerDAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using logginglibrary;
using CustomerDAL.Logging;
using System.Dynamic;

namespace CustomerDAL.UsersOperations
{
    public class UserDAL : IUsers
    {
        private ILog logger;

        public UserDAL(ILog templogger)
        {
            this.logger = templogger;
        }

        #region Reset Password 

        public int ResetPassword(string email, string password,string deviceID, string DeviceType)
        {
            try
            {
                using (var dbContext = new SilaeeContext())
                {
                    string sql = "sp_ResetPassword @CustomerMailAddress,@CustomerPassword,@deviceID,@deviceType,@ResultParam OUT";
                    List<SqlParameter> parameterList = new List<SqlParameter>();
                    parameterList.Add(new SqlParameter("@CustomerMailAddress", email));
                    parameterList.Add(new SqlParameter("@CustomerPassword", password));
                    parameterList.Add(new SqlParameter("@deviceID", deviceID));
                    parameterList.Add(new SqlParameter("@deviceType", DeviceType));
                    var out1 = new SqlParameter
                    {
                        ParameterName = "@ResultParam",
                        DbType = System.Data.DbType.Int32,
                        Direction = System.Data.ParameterDirection.Output
                    };
                    parameterList.Add(out1);
                    var result = dbContext.Database.
                        ExecuteSqlCommand(sql, parameterList);
                    var out1Value = (int)out1.Value;
                    return out1Value;
                }
            }
            catch (Exception ex)
            {
                logger.Error(LogUserLogin.CreateErrorMsg("UserDAL", "ResetPassword", DateTime.Now.ToString(), ex.ToString()));
                return -1;
            }
        }

        public int ValidateEmailAddress(string emailAddress)
        {
            try
            {
                using (var dbContext = new SilaeeContext())
                {
                    string sql = "sp_ValidateEmail @CustomerMailAddress,@ResultParam OUT";
                    List<SqlParameter> parameterList = new List<SqlParameter>();
                    parameterList.Add(new SqlParameter("@CustomerMailAddress", emailAddress));
                    var out1 = new SqlParameter
                    {
                        ParameterName = "@ResultParam",
                        DbType = System.Data.DbType.Int32,
                        Direction = System.Data.ParameterDirection.Output
                    };
                    parameterList.Add(out1);
                    var result = dbContext.Database.
                        ExecuteSqlCommand(sql, parameterList);
                    var out1Value = (int)out1.Value;
                    return out1Value;
                }
            }
            catch (Exception ex)
            {
                logger.Error(LogUserLogin.CreateErrorMsg("UserDAL", "ValidateEmailAddress", DateTime.Now.ToString(), ex.ToString()));
                return -1;
            }
        }

        


        #endregion

        #region Login/Logout Activity 

        public void LoginActivity(LoginActivityBO objActivity)
        {
            try
            {
                using (var dbContext = new SilaeeContext())
                {
                    string sql = "sp_SaveLoginResponse @CustomerID,@LoginDate,@LogOutDate,@LoginSuccess," +
                        "@creationDate,@ModifyDate,@createdbyUserTypeID,@CreatedbyUserID,@modifiedbyUserTypeID," +
                        "@modifiedbyUserID,@createdbyDeviceID,@modifiedbyDeviceID,@SessionTokken,@Authmedium," +
                        "@DeviceType,@UniqueDeviceID";
                    //"@UniqueDeviceID,@DeviceType,@ResultParam OUT";
                    List<SqlParameter> parameterList = new List<SqlParameter>();
                    parameterList.Add(new SqlParameter("@CustomerID", objActivity.customerID));
                    parameterList.Add(new SqlParameter("@LoginDate", objActivity.LoginDate));
                    parameterList.Add(new SqlParameter("@LogOutDate", objActivity.logOut));
                    parameterList.Add(new SqlParameter("@LoginSuccess", objActivity.LoginSucess));
                    parameterList.Add(new SqlParameter("@creationDate", objActivity.creationDate));
                    parameterList.Add(new SqlParameter("@ModifyDate", objActivity.ModifyDate));
                    parameterList.Add(new SqlParameter("@createdbyUserTypeID", objActivity.createdbyUserTypeID));
                    parameterList.Add(new SqlParameter("@CreatedbyUserID", objActivity.CreatedbyUserID));
                    parameterList.Add(new SqlParameter("@modifiedbyUserTypeID", objActivity.modifiedbyUserTypeID));
                    parameterList.Add(new SqlParameter("@modifiedbyUserID", objActivity.modifiedbyUserID));
                    parameterList.Add(new SqlParameter("@createdbyDeviceID", -1));
                    parameterList.Add(new SqlParameter("@modifiedbyDeviceID", -1));
                    parameterList.Add(new SqlParameter("@SessionTokken", objActivity.SessionToken));
                    parameterList.Add(new SqlParameter("@Authmedium", objActivity.Authmedium));
                    parameterList.Add(new SqlParameter("@DeviceType", objActivity.deviceType));
                    parameterList.Add(new SqlParameter("@UniqueDeviceID", objActivity.DeviceID));

                    var result = dbContext.Database.
                        ExecuteSqlCommand(sql, parameterList);
                
                }
            }
            catch (Exception ex)
            {
                logger.Error(LogUserLogin.CreateErrorMsg("UserDAL", "LoginActivity", DateTime.Now.ToString(), ex.ToString()));
            }
        }

        #endregion

        #region Create Profile 

        public int SaveUser(Userdata obj)
        {
            try
            {
                using (var dbContext = new SilaeeContext())
                {
                    string sql = "sp_SaveCustomerProfile @CustomerFirstName,@CustomerLastName,@CustomerAge,@CustomerGender," +
                        "@CustomereMailAddress,@CustomerWebsite,@CustomerCountry,@CustomerCity,@CustomerProvince," +
                        "@CustomerZipCode,@CustomerAddress,@CustomerMobileNumber,@CustomerCNIC,@CustomerPicture," +
                        "@CustomerPassword,@CustomerRating,@CustomerWalletAmount,@CustomerProfileStatus,@CreationDateTime,@ModifiedDateTime," +
                        "@CreatedbyUserTypeId,@CreatedbyUserId,@ModifiedbyUserTypeId,@ModifiedbyUserId," +
                        "@CreatedbyDeviceId,@ModifiedbyDeviceId,@UniqueDeviceID,@DeviceType,@ResultParam OUT";
                        //"@UniqueDeviceID,@DeviceType,@ResultParam OUT";
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

                    parameterList.Add(new SqlParameter("@CreationDateTime", obj.creationdate));
                    parameterList.Add(new SqlParameter("@ModifiedDateTime", obj.modifieddatetime));

                    parameterList.Add(new SqlParameter("@CreatedbyUserTypeId", obj.createdbyusertypeid));
                    parameterList.Add(new SqlParameter("@CreatedbyUserId", obj.createdbyuserid));
                    parameterList.Add(new SqlParameter("@ModifiedbyUserTypeId", obj.modifiedbyusertypeid));
                    parameterList.Add(new SqlParameter("@ModifiedbyUserId", obj.modifiedbyuserid));
                    parameterList.Add(new SqlParameter("@CreatedbyDeviceId", -1)); //obj.createdbydeviceid));
                    parameterList.Add(new SqlParameter("@ModifiedbyDeviceId", -1)); //obj.modifiedbydeviceid));
                    parameterList.Add(new SqlParameter("@UniqueDeviceID", obj.deviceid));
                    parameterList.Add(new SqlParameter("@DeviceType", obj.devicetype));
                    var out1 = new SqlParameter
                    {
                        ParameterName = "@ResultParam",
                        DbType = System.Data.DbType.Int32,
                        Direction = System.Data.ParameterDirection.Output
                    };
                    parameterList.Add(out1); //new SqlParameter("@ResultParam", obj.ModifiedbyDeviceId));
                    var result = dbContext.Database.
                        ExecuteSqlCommand(sql, parameterList);
                    var out1Value = (int)out1.Value;
                    return out1Value;
                    
                }
            }
            catch (Exception ex)
            {
                logger.Error(LogUserLogin.CreateErrorMsg("UserDAL", "SaveUser", DateTime.Now.ToString(), ex.ToString()));
                return - 1;
               // return null; // also we can log exception through common layer }
            }
        }

        #endregion

        #region Login API DAL Function

        public  LoginResponseBO CustomLoginUser(string UserName, string Password,string deviceID)
        {
            LoginResponseBO objResponse = new LoginResponseBO();
            try
            {
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

                        var UserDeviceIDParam = cmd.CreateParameter();
                        UserDeviceIDParam.ParameterName = "DeviceID";
                        UserDeviceIDParam.Value = deviceID;
                        cmd.Parameters.Add(UserDeviceIDParam);

                        
                        dbContext.Database.OpenConnection();
                        
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows) 
                            {
                                if(reader.Read())
                                {
                                    string _deviceID = reader["Uniquedeviceid"].ToString();

                                    if(!string.IsNullOrEmpty(_deviceID))
                                    {
                                        objResponse.issuccessful = true;
                                        objResponse.loginresponsecode = 1;
                                        objResponse.customerid = Convert.ToInt32(reader["CustomerID"]);
                                        objResponse.customerfirstname = reader["CustomerFirstName"].ToString();
                                        objResponse.customerlastname = reader["CustomerLastName"].ToString();
                                        objResponse.userdata = new Userdata();
                                        objResponse.userdata.customerage = Convert.ToInt32(reader["CustomerAge"].ToString());
                                        objResponse.userdata.customeremailaddress = reader["CustomereMailAddress"].ToString();
                                        objResponse.userdata.customerwebsite = reader["CustomerWebsite"].ToString();
                                        objResponse.userdata.customercountry = reader["CustomerCountry"].ToString();
                                        objResponse.userdata.customercity = reader["CustomerCity"].ToString();
                                        objResponse.userdata.customerprovince = reader["CustomerProvince"].ToString();
                                        objResponse.userdata.customeraddress = reader["CustomerAddress"].ToString();
                                        objResponse.userdata.customermobilenumber = reader["CustomerMobileNumber"].ToString();
                                        objResponse.userdata.customerothercontactnumber = reader["CustomerOtherContactNumber"].ToString();
                                        objResponse.userdata.customercnic = reader["CustomerCNIC"].ToString();
                                        objResponse.userdata.customerpicture = reader["CustomerPicture"].ToString();
                                        objResponse.userdata.customerrating = Convert.ToDecimal(reader["CustomerRating"].ToString());
                                        objResponse.userdata.customerprofilestatus = Convert.ToBoolean(reader["CustomerProfileStatus"].ToString());
                                        objResponse.userdata.deviceid = reader["Uniquedeviceid"].ToString();
                                        objResponse.responsemessage = "Login Successfully !";
                                    }
                                    else
                                    {
                                        //otp case
                                        objResponse.issuccessful = true;
                                        objResponse.loginresponsecode = 2;
                                        objResponse.customerid = Convert.ToInt32(reader["CustomerID"]);
                                        objResponse.customerfirstname = reader["CustomerFirstName"].ToString();
                                        objResponse.customerlastname = reader["CustomerLastName"].ToString();
                                        objResponse.userdata = new Userdata();
                                        objResponse.userdata.customerage = Convert.ToInt32(reader["CustomerAge"].ToString());
                                        objResponse.userdata.customeremailaddress = reader["CustomereMailAddress"].ToString();
                                        objResponse.userdata.customerwebsite = reader["CustomerWebsite"].ToString();
                                        objResponse.userdata.customercountry = reader["CustomerCountry"].ToString();
                                        objResponse.userdata.customercity = reader["CustomerCity"].ToString();
                                        objResponse.userdata.customerprovince = reader["CustomerProvince"].ToString();
                                        objResponse.userdata.customeraddress = reader["CustomerAddress"].ToString();
                                        objResponse.userdata.customermobilenumber = reader["CustomerMobileNumber"].ToString();
                                        objResponse.userdata.customerothercontactnumber = reader["CustomerOtherContactNumber"].ToString();
                                        objResponse.userdata.customercnic = reader["CustomerCNIC"].ToString();
                                        objResponse.userdata.customerpicture = reader["CustomerPicture"].ToString();
                                        objResponse.userdata.customerrating = Convert.ToDecimal(reader["CustomerRating"].ToString());
                                        objResponse.userdata.customerprofilestatus = Convert.ToBoolean(reader["CustomerProfileStatus"].ToString());
                                        objResponse.userdata.deviceid = "";
                                        objResponse.responsemessage = "Login Successfully !";
                                    }

                                    
                                }
                            }
                            else
                            {
                                objResponse.issuccessful = false;
                                objResponse.loginresponsecode = -1;
                                objResponse.customerid = -1;
                                objResponse.customerfirstname = "";
                                objResponse.customerlastname = "";
                                objResponse.userdata = new Userdata();
                                objResponse.responsemessage = "No user found";
                            }
                        }
                    }
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                logger.Error(LogUserLogin.CreateErrorMsg("UserDAL", "CustomLoginUser", DateTime.Now.ToString(), ex.ToString()));
                objResponse.issuccessful = false;
                objResponse.loginresponsecode = -1;
                objResponse.customerid = -1;
                objResponse.customerfirstname = "";
                objResponse.customerlastname = "";
                objResponse.userdata = new Userdata();
                objResponse.responsemessage = "Exception occured !";
                return objResponse;
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
                        "@CreatedbyDeviceId,@ModifiedbyDeviceId,@UniqueDeviceID,@DeviceType,@CustomerID OUT";
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
                    parameterList.Add(new SqlParameter("@CreatedbyDeviceId", -1)); //obj.createdbydeviceid));
                    parameterList.Add(new SqlParameter("@ModifiedbyDeviceId", -1)); //obj.modifiedbydeviceid));
                    parameterList.Add(new SqlParameter("@UniqueDeviceID", obj.deviceid));
                    parameterList.Add(new SqlParameter("@DeviceType", obj.devicetype));
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
                logger.Error(LogUserLogin.CreateErrorMsg("UserDAL", "GetUserID", DateTime.Now.ToString(), ex.ToString()));
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
                objEntity.deviceid = objLoginUser.deviceid;
                objEntity.devicetype = objLoginUser.devicetype;
                return objEntity;
            }
            catch (Exception ex)
            {
                logger.Error(LogUserLogin.CreateErrorMsg("UserDAL", "CreateUserObject", DateTime.Now.ToString(), ex.ToString()));
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
                logger.Error(LogUserLogin.CreateErrorMsg("UserDAL", "LoginMediumUser", DateTime.Now.ToString(), ex.ToString()));
                return -1;
            }
        }

        #endregion

        #region Update API DAL Function 

        public int UpdateCustomerInfo(Userdata obj)
        {
            try
            {
                using (var dbContext = new SilaeeContext())
                {
                    string sql = "sp_UpdateCustomerProfile @CustomerID,@CustomerFirstName,@CustomerLastName,@CustomerAge,@CustomerGender," +
                        "@CustomereMailAddress,@CustomerWebsite,@CustomerCountry,@CustomerCity,@CustomerProvince," +
                        "@CustomerZipCode,@CustomerAddress,@CustomerMobileNumber,@CustomerCNIC,@CustomerPicture," +
                        "@CustomerPassword,@CustomerRating,@CustomerWalletAmount,@CustomerProfileStatus,@CreationDateTime,@ModifiedDateTime," +
                        "@CreatedbyUserTypeId,@CreatedbyUserId,@ModifiedbyUserTypeId,@ModifiedbyUserId," +
                        "@CreatedbyDeviceId,@ModifiedbyDeviceId,@UniqueDeviceID,@DeviceType,@sessiontoken,@ResultParam OUT";
                    List<SqlParameter> parameterList = new List<SqlParameter>();
                    parameterList.Add(new SqlParameter("@CustomerID", obj.customerid));
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

                    parameterList.Add(new SqlParameter("@CreationDateTime", obj.creationdate));
                    parameterList.Add(new SqlParameter("@ModifiedDateTime", obj.modifieddatetime));

                    parameterList.Add(new SqlParameter("@CreatedbyUserTypeId", obj.createdbyusertypeid));
                    parameterList.Add(new SqlParameter("@CreatedbyUserId", obj.createdbyuserid));
                    parameterList.Add(new SqlParameter("@ModifiedbyUserTypeId", obj.modifiedbyusertypeid));
                    parameterList.Add(new SqlParameter("@ModifiedbyUserId", obj.modifiedbyuserid));
                    parameterList.Add(new SqlParameter("@CreatedbyDeviceId", -1)); //obj.createdbydeviceid));
                    parameterList.Add(new SqlParameter("@ModifiedbyDeviceId", -1)); //obj.modifiedbydeviceid));
                    parameterList.Add(new SqlParameter("@UniqueDeviceID", obj.deviceid));
                    parameterList.Add(new SqlParameter("@DeviceType", obj.devicetype));
                    parameterList.Add(new SqlParameter("@sessiontoken", obj.sessiontoken));
                    var out1 = new SqlParameter
                    {
                        ParameterName = "@ResultParam",
                        DbType = System.Data.DbType.Int32,
                        Direction = System.Data.ParameterDirection.Output
                    };
                    parameterList.Add(out1); //new SqlParameter("@ResultParam", obj.ModifiedbyDeviceId));
                    var result = dbContext.Database.
                        ExecuteSqlCommand(sql, parameterList);
                    var out1Value = (int)out1.Value;
                    return out1Value;

                }
            }
            catch (Exception ex)
            {
                logger.Error(LogUserLogin.CreateErrorMsg("UserDAL", "UpdateCustomerINfo", DateTime.Now.ToString(), ex.ToString()));
                return -1;
                // return null; // also we can log exception through common layer }
            }
        }



        #endregion

        #region Change password DAL function

        public async Task<string> ChangePassword(ChangePasswordBO obj)
        {
            try
            {
                return await Task.Run(() =>
                {
                    using (var dbContext = new SilaeeContext())
                    {
                        using (var cmd = dbContext.Database.GetDbConnection().CreateCommand())
                        {
                            cmd.CommandText = "sp_ChangePassword";
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            var UserPasswordParam = cmd.CreateParameter();
                            UserPasswordParam.ParameterName = "password";
                            UserPasswordParam.Value = obj.newpassword;
                            cmd.Parameters.Add(UserPasswordParam);

                            var UserCustomerIDParam = cmd.CreateParameter();
                            UserCustomerIDParam.ParameterName = "customerID";
                            UserCustomerIDParam.Value = obj.customerid;
                            cmd.Parameters.Add(UserCustomerIDParam);

                            var UserDeviceIDParam = cmd.CreateParameter();
                            UserDeviceIDParam.ParameterName = "deviceID";
                            UserDeviceIDParam.Value = obj.deviceid;
                            cmd.Parameters.Add(UserDeviceIDParam);

                            var UserDeviceTypeParam = cmd.CreateParameter();
                            UserDeviceTypeParam.ParameterName = "deviceType";
                            UserDeviceTypeParam.Value = obj.devicetype;
                            cmd.Parameters.Add(UserDeviceTypeParam);

                            dbContext.Database.OpenConnection();

                            using (var reader = cmd.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    if (reader.Read())
                                    {
                                        return reader["CustomereMailAddress"].ToString();
                                    }
                                }
                                else
                                {
                                    return "";
                                }
                            }
                        }
                        return "";
                    }


                });
                
            }
            catch (Exception ex)
            {
                logger.Error(LogUserLogin.CreateErrorMsg("UserDAL", "ChangePassword", DateTime.Now.ToString(), ex.ToString()));
                return null;
            }
        }

        #endregion

        #region OTP 

        public async Task<string> ValidateEmailAddressOrPhoneNumber(OtpBO obj)
        {
            try
            {
                string ResponseValue = "";
                using (var dbContext = new SilaeeContext())
                {
                    using (var cmd = dbContext.Database.GetDbConnection().CreateCommand())
                    {
                        cmd.CommandText = "sp_ValidateForgetPasswordOTP";
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        var MailAddressParam = cmd.CreateParameter();
                        MailAddressParam.ParameterName = "CustomerMailAddress";
                        MailAddressParam.Value = obj.customeremailaddress;
                        cmd.Parameters.Add(MailAddressParam);

                        var MobileNumberParam = cmd.CreateParameter();
                        MobileNumberParam.ParameterName = "CustomerMobileNumber";
                        MobileNumberParam.Value = obj.customermobilenumber;
                        cmd.Parameters.Add(MobileNumberParam);

                        var DeviceIDParam = cmd.CreateParameter();
                        DeviceIDParam.ParameterName = "DeviceID";
                        DeviceIDParam.Value = obj.deviceid;
                        cmd.Parameters.Add(DeviceIDParam);

                        var OtpTypeParam = cmd.CreateParameter();
                        OtpTypeParam.ParameterName = "Otptype";
                        OtpTypeParam.Value = obj.otptype;
                        cmd.Parameters.Add(OtpTypeParam);

                        var OtpParam = cmd.CreateParameter();
                        OtpParam.ParameterName = "Otp";
                        OtpParam.Value = obj.otp;
                        cmd.Parameters.Add(OtpParam);

                        var DeviceTypeParam = cmd.CreateParameter();
                        DeviceTypeParam.ParameterName = "devicetype";
                        DeviceTypeParam.Value = obj.devicetype;
                        cmd.Parameters.Add(DeviceTypeParam);

                        await dbContext.Database.OpenConnectionAsync();

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (await reader.ReadAsync())
                                {
                                    ResponseValue = reader["Otp"].ToString();
                                }
                            }
                        }
                    }
                    return ResponseValue;
                }
            }
            catch (Exception ex)
            {
                logger.Error(LogUserLogin.CreateErrorMsg("UserDAL", "ValidateEmailAddressOrPhoneNumber", DateTime.Now.ToString(), ex.ToString()));
                return "";
            }
        }

        public async Task<int> VerifyOtpDAL(OtpBO obj)
        {
            try
            {
                using (var dbContext = new SilaeeContext())
                {
                    string sql = "sp_ValidateOTPCode @CustomerMailAddress,@CustomerMobileNumber," +
                        "@DeviceID,@Otp,@ResultParam OUT";
                    List<SqlParameter> parameterList = new List<SqlParameter>();
                    parameterList.Add(new SqlParameter("@CustomerMailAddress", obj.customeremailaddress));
                    parameterList.Add(new SqlParameter("@CustomerMobileNumber", obj.customermobilenumber));
                    parameterList.Add(new SqlParameter("@DeviceID", obj.deviceid));
                    parameterList.Add(new SqlParameter("@Otp", obj.otp));
                    var out1 = new SqlParameter
                    {
                        ParameterName = "@ResultParam",
                        DbType = System.Data.DbType.Int32,
                        Direction = System.Data.ParameterDirection.Output
                    };
                    parameterList.Add(out1);
                    var result = await dbContext.Database.
                        ExecuteSqlCommandAsync(sql, parameterList);
                    var out1Value = (int)out1.Value;
                    return out1Value;
                }
            }
            catch (Exception ex)
            {
                logger.Error(LogUserLogin.CreateErrorMsg("UserDAL", "ValidateEmailAddress", DateTime.Now.ToString(), ex.ToString()));
                return -1;
            }
        }


        #endregion

        #region Verify Session token 

        public async Task<int> VerifyToken(string sessionToken, string deviceID)
        {
            try
            {
                using (var dbContext = new SilaeeContext())
                {
                    string sql = "sp_ValidateSessionToken @sessionToken,@deviceID,@ResultParam OUT";
                    List<SqlParameter> parameterList = new List<SqlParameter>();
                    parameterList.Add(new SqlParameter("@sessionToken", sessionToken));
                    parameterList.Add(new SqlParameter("@deviceID", deviceID));
                    var out1 = new SqlParameter
                    {
                        ParameterName = "@ResultParam",
                        DbType = System.Data.DbType.Int32,
                        Direction = System.Data.ParameterDirection.Output
                    };
                    parameterList.Add(out1);
                    var result = await dbContext.Database.
                        ExecuteSqlCommandAsync(sql, parameterList);
                    var out1Value = (int)out1.Value;
                    return out1Value;
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
