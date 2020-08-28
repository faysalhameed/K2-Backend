using CustomerBO.Settings;
using CustomerBO.User;
using CustomerCommon;
using CustomerDAL.SettingOperations;
using CustomerDAL.UsersOperations;
using logginglibrary;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CustomerBL.User
{
    public class UserBL
    {
        private ILog logger;

       
        public UserBL(ILog templogger)
        {
           this.logger = templogger;
        }

        #region Customer Activity 

        public LoginActivityBO CreateActivityObject(LoginBO obj)
        {
            try
            {
                LoginActivityBO objActivity = new LoginActivityBO();
                //objActivity.customerID = obj.CustomerID;
                
                if(obj.logindate == null || obj.logindate == DateTime.MinValue)
                {
                    objActivity.LoginDate = DateTime.Now;
                }
                else
                {
                    objActivity.LoginDate = obj.logindate;
                }

                objActivity.logOut = DateTime.Now;
                objActivity.creationDate = DateTime.Now;
                objActivity.ModifyDate = DateTime.Now;
                objActivity.createdbyUserTypeID = -1;
                objActivity.CreatedbyUserID = -1;
                objActivity.modifiedbyUserTypeID = -1;
                objActivity.modifiedbyUserID = -1;
                objActivity.deviceType = obj.devicetype;
                //objActivity.SessionToken = obj.authenticationtoken;
                objActivity.Authmedium = obj.authenticationmedium;
                objActivity.DeviceID = obj.deviceid;
               
                return objActivity;
                   
            }
            catch (Exception ex)
            {
                logger.Error(LoggingRequest.CreateErrorMsg("UserBL", "CreateActivityObject", DateTime.Now.ToString(), ex.ToString()));
                return null;
            }
        }

        #endregion


        #region Customer Profile Creation API

        // -1 : return value from sp side
        // -2 : validation failed for request object
        // -3 : bad request object 
        // -4 : Exception in code 

        private bool ValidateUpdateUserInfo(Userdata obj)
        {
            try
            {
                #region Optional Values check

                if (obj.creationdate == DateTime.MinValue)
                {
                    obj.creationdate = DateTime.Now;
                }
                if (string.IsNullOrEmpty(obj.customerpassword))
                {
                    obj.customerpassword = "";
                }
                if (string.IsNullOrEmpty(obj.customeremailaddress))
                {
                    obj.customeremailaddress = "";
                }
                if (obj.modifieddatetime == DateTime.MinValue)
                {
                    obj.modifieddatetime = DateTime.Now;
                }
                if (obj.customerwebsite == null)
                {
                    obj.customerwebsite = "";
                }
                if (obj.customercountry == null)
                {
                    obj.customercountry = "";
                }
                if (obj.customercity == null)
                {
                    obj.customercity = "";
                }
                if (obj.customerprovince == null)
                {
                    obj.customerprovince = "";
                }
                if (obj.customerzipcode == null)
                {
                    obj.customerzipcode = "";
                }
                if (obj.customeraddress == null)
                {
                    obj.customeraddress = "";
                }
                if (obj.customercnic == null)
                {
                    obj.customercnic = "";
                }
                if (obj.customerpicture == null)
                {
                    obj.customerpicture = "";
                }


                #endregion


                //if (string.IsNullOrEmpty(obj.customeremailaddress))
                //{
                //    return false;
                //}
                if (string.IsNullOrEmpty(obj.customermobilenumber))
                {
                    return false;
                }
                //else if (string.IsNullOrEmpty(obj.customerpassword))
                //{
                //    return false;
                //}
                else if (string.IsNullOrEmpty(obj.customerfirstname))
                {
                    return false;
                }
                else if (string.IsNullOrEmpty(obj.customerlastname))
                {
                    return false;
                }
                // Commented after discussion with faisal
                //else if (string.IsNullOrEmpty(obj.customergender))
                //{
                //    return false;
                //}
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                logger.Error(LoggingRequest.CreateErrorMsg("UserBL", "ValidateUpdateUserInfo", DateTime.Now.ToString(), ex.ToString()));
                return false;
            }
        }

        private bool ValidateUserInfo(Userdata obj)
        {
            try
            {
                #region Optional Values check

                if (obj.creationdate == DateTime.MinValue)
                {
                    obj.creationdate = DateTime.Now;
                }
                if (obj.modifieddatetime == DateTime.MinValue)
                {
                    obj.modifieddatetime = DateTime.Now;
                }
                if (obj.customerwebsite == null)
                {
                    obj.customerwebsite = "";
                }
                if (obj.customercountry == null)
                {
                    obj.customercountry = "";
                }
                if (obj.customercity == null)
                {
                    obj.customercity = "";
                }
                if (obj.customerprovince == null)
                {
                    obj.customerprovince = "";
                }
                if (obj.customerzipcode == null)
                {
                    obj.customerzipcode = "";
                }
                if (obj.customeraddress == null)
                {
                    obj.customeraddress = "";
                }
                if (obj.customercnic == null)
                {
                    obj.customercnic = "";
                }
                if (obj.customerpicture == null)
                {
                    obj.customerpicture = "";
                }


                #endregion


                if (string.IsNullOrEmpty(obj.customeremailaddress))
                {
                    return false;
                }
                else if (string.IsNullOrEmpty(obj.customermobilenumber))
                {
                    return false;
                }
                else if (string.IsNullOrEmpty(obj.customerpassword))
                {
                    return false;
                }
                else if (string.IsNullOrEmpty(obj.customerfirstname))
                {
                    return false;
                }
                else if (string.IsNullOrEmpty(obj.customerlastname))
                {
                    return false;
                }
                // Commented after discussion with faisal
                //else if (string.IsNullOrEmpty(obj.customergender))
                //{
                //    return false;
                //}
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                logger.Error(LoggingRequest.CreateErrorMsg("UserBL", "ValidateUserInfo", DateTime.Now.ToString(), ex.ToString()));
                return false;
            }
        }

        public async Task<int> AddUser(Userdata obj)
        {
            try
            {
                if(obj != null)
                {
                    bool isValidate = ValidateUserInfo(obj);
                    if(!isValidate)
                    {
                        return -2; // for validation failed
                    }
                    UserDAL objDal = new UserDAL(logger);
                    return await Task.Run(() => {

                        int respone = objDal.SaveUser(obj);
                        return respone;
                    });
                }
                else
                {
                    return -3; // for having bad object 
                }
            }
            catch (Exception ex)
            {
                logger.Error(LoggingRequest.CreateErrorMsg("UserBL", "AddUser", DateTime.Now.ToString(), ex.ToString()));
                return -4;
            }
        }

        #endregion

        #region Login Process 

        public async Task<Tuple<int,string,string,string>> Login(LoginRootBO obj)
        {
            try
            {
                if(obj == null || obj.logindata == null)
                {
                    return new Tuple<int,string,string,string>(-2,"","",""); // all data missing 
                }

                //if (string.IsNullOrEmpty(obj.logindata.emailaddress) || string.IsNullOrEmpty(obj.logindata.userpassword))
                //{
                //    return -3; // missing email or password 
                //}

                if (!string.IsNullOrEmpty(obj.logindata.authenticationmedium))
                {
                    UserDAL objDAL = new UserDAL(logger);
                    if (obj.logindata.authenticationmedium.ToLower() == "custom")
                    {
                        // simple just check and return response
                        if (!string.IsNullOrEmpty(obj.logindata.emailaddress) || !string.IsNullOrEmpty(obj.logindata.userpassword))
                        {
                            var response = objDAL.CustomLoginUser(obj.logindata.emailaddress,obj.logindata.userpassword);
                            return new Tuple<int, string, string,string>(response.customerid,response.customerfirstname, response.customerlastname,Guid.NewGuid().ToString());
                        }
                        return new Tuple<int, string, string,string>(-5,"","",""); // email or password is missing for authentication
                    }
                    else if (obj.logindata.authenticationmedium.ToLower() == "facebook")
                    {
                        bool result = await ValidateFaceBookToken(obj.logindata.authenticationtoken);
                        if(result)
                        {
                            // check if profile exist if not then create profile and return result
                            var response = objDAL.LoginMediumUser(obj.logindata);
                            return new Tuple<int, string, string,string>(response,"","",Guid.NewGuid().ToString());
                        }
                        return new Tuple<int, string, string,string>(-4,"","",""); // token auth failed
                    }
                    else if (obj.logindata.authenticationmedium.ToLower() == "google")
                    {
                        bool result = await ValidateGoogleToken(obj.logindata.authenticationtoken);
                        if(result)
                        {
                            // check if profile exist if not then create profile and return result 
                            var response = objDAL.LoginMediumUser(obj.logindata);
                            return new Tuple<int, string, string,string>(response,"","",Guid.NewGuid().ToString());
                        }
                        return new Tuple<int, string, string,string>(-4,"","",""); // token auth failed
                    }
                    else
                    {
                        // faulty msg return no pre defined auth medium  defined
                        return new Tuple<int, string, string,string>(-6,"","","");
                    }
                }
                else
                {
                    // faulty msg return no auth defined
                    return new Tuple<int, string, string,string>(-3,"","","");
                }
            }
            catch (Exception ex)
            {
                logger.Error(LoggingRequest.CreateErrorMsg("UserBL", "Login", DateTime.Now.ToString(), ex.ToString()));
                return new Tuple<int, string, string,string>(-7,"","",""); // exception
            }
        }

        private async Task<bool> ValidateFaceBookToken(string token)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://graph.facebook.com/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync("me?access_token=" + token);
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                // need logging code here
                logger.Error(LoggingRequest.CreateErrorMsg("UserBL", "ValidateFaceBookToken", DateTime.Now.ToString(), ex.ToString()));
                return false;
            }
        }

        private async Task<bool> ValidateGoogleToken(string token)
        {
            //https://oauth2.googleapis.com/tokeninfo?id_token=XYZ123
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://oauth2.googleapis.com/"); //("https://www.googleapis.com/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync("tokeninfo?id_token=" + token); //("oauth2/v1/tokeninfo?access_token=" + token);
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                // need logging code here
                logger.Error(LoggingRequest.CreateErrorMsg("UserBL", "ValidateGoogleToken", DateTime.Now.ToString(), ex.ToString()));
                return false;
            }
        }
        #endregion

        #region Update Process 

        public async Task<int> UpdateCustomer(Userdata obj)
        {
            try
            {
                if (obj != null)
                {
                    bool isValidate = ValidateUpdateUserInfo(obj);
                    if (!isValidate)
                    {
                        return -2; // for validation failed
                    }
                    UserDAL objDal = new UserDAL(logger);
                    return await Task.Run(() => {
                        if(obj.sessiontoken == null)
                        {
                            obj.sessiontoken = "";
                        }
                        int respone = objDal.UpdateCustomerInfo(obj);
                        return respone;
                    });
                }
                else
                {
                    return -3; // for having bad object 
                }
            }
            catch (Exception ex)
            {
                logger.Error(LoggingRequest.CreateErrorMsg("UserBL", "UpdateCustomer", DateTime.Now.ToString(), ex.ToString()));
                return -4;
            }
        }


        #endregion

        #region Forget Password 

        public SettingBO getEmailSettings()
        {
            try
            {
                SettingBO objResponse = new SettingBO();
                SettingDAL objDAL = new SettingDAL();
                var result = objDAL.getEmailSettings();
                if(result != null && result.Count > 0)
                {
                    for (int i = 0; i < result.Count; i++)
                    {
                        if(result[i].vSettingName == "FromEmailAddress")
                        {
                            objResponse.FromEmailAddress = result[i].vSettingValue;
                        }
                        else if (result[i].vSettingName == "SMTPPort")
                        {
                            objResponse.SMTPPort = Convert.ToInt32(result[i].vSettingValue);
                        }
                        else if (result[i].vSettingName == "SMTPHost")
                        {
                            objResponse.SMTPHost = result[i].vSettingValue;
                        }
                        else if (result[i].vSettingName == "SMTPEnableSSL")
                        {
                            objResponse.SMTPEnableSSL = Convert.ToBoolean(result[i].vSettingValue);
                        }
                        else if (result[i].vSettingName == "SMTPUseDefaultCredential")
                        {
                            objResponse.SMTPUseDefaultCredential = Convert.ToBoolean(result[i].vSettingValue);
                        }
                        else if (result[i].vSettingName == "FromEmailAddressPassword")
                        {
                            objResponse.FromEmailAddressPassword = result[i].vSettingValue;
                        }

                    }
                    return objResponse;
                }
                return null;
            }
            catch (Exception ex)
            {
                logger.Error(LoggingRequest.CreateErrorMsg("UserBL", "getEmailSettings", DateTime.Now.ToString(), ex.ToString()));
                return null;
            }
        }

        private string getHtml(string _heading, String Guid)
        {
            try
            {
                string messageBody = "<font><b>" + _heading + "</b></font><br>";
                string htmlTableStart = "<table style=\"border-collapse:collapse; text-align:center;\" >";
                string htmlTableEnd = "</table>";
                string htmlHeaderRowStart = "<tr style=\"background-color:#6FA1D2; color:#ffffff;\">";
                string htmlHeaderRowEnd = "</tr>";
                string htmlTrStart = "<tr style=\"color:#555555;\">";
                string htmlTrEnd = "</tr>";
                string htmlTdStart = "<td style=\" border-color:#5c87b2; border-style:solid; border-width:thin; padding: 5px;\">";
                string htmlTdEnd = "</td>";
                messageBody += htmlTableStart;
                messageBody += htmlHeaderRowStart;
                messageBody += htmlTdStart + "Reset Code" + htmlTdEnd;
                messageBody += htmlHeaderRowEnd;
                messageBody = messageBody + htmlTrStart;
                messageBody = messageBody + htmlTdStart + Guid + htmlTdEnd;
                messageBody = messageBody + htmlTrEnd;
                messageBody = messageBody + htmlTableEnd;
                return messageBody; // return HTML Table as string from this function  
            }
            catch (Exception ex)
            {
                logger.Error(LoggingRequest.CreateErrorMsg("UserBL", "getHTML", DateTime.Now.ToString(), ex.ToString()));
                return null;
            }
        }

        private bool EmailSending(string emailAddress, string guid)
        {
            try
            {
                
                var EmailSetting = getEmailSettings();
                if(EmailSetting != null)
                {
                    string _Mainheading = "Password Reset";
                    string _emailBody = getHtml(_Mainheading, guid);
                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    message.From = new MailAddress(EmailSetting.FromEmailAddress);
                    string _toArr = emailAddress;
                    message.To.Add(emailAddress);
                    message.Subject = "Reset password Code";
                    message.IsBodyHtml = true;
                    message.Body = _emailBody;
                    message.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
                    smtp.Port = EmailSetting.SMTPPort;
                    smtp.Host = EmailSetting.SMTPHost;
                    smtp.EnableSsl = EmailSetting.SMTPEnableSSL;
                    smtp.UseDefaultCredentials = EmailSetting.SMTPUseDefaultCredential;
                    smtp.Credentials = new NetworkCredential(EmailSetting.FromEmailAddress, EmailSetting.FromEmailAddressPassword);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);
                    return true;
                }
                return false;     
            }
            catch (Exception ex)
            {
                logger.Error(LoggingRequest.CreateErrorMsg("UserBL", "EmailSending", DateTime.Now.ToString(), ex.ToString()));
                return false;
            }
        }

        public async Task<Tuple<int,string>> ForgetPasswordBL(ForgotPasswordBO obj)
        {
            try
            {
                if (obj != null)
                {
                    return await Task.Run(() =>
                    {
                        UserDAL objDal = new UserDAL(logger);
                        int result = objDal.ValidateEmailAddress(obj.customeremailaddress);
                        if (result > 0)
                        {
                            string guid = Guid.NewGuid().ToString();
                            bool isEmailSent = EmailSending(obj.customeremailaddress, guid);
                            if(isEmailSent)
                            {
                                return new Tuple<int, string>(result, guid);
                            }
                            else
                            {
                                return new Tuple<int, string>(-5, "");
                            }
                            
                        }
                        else
                        {
                            return new Tuple<int, string>(result, "");
                        }
                    });
                }
                else
                {
                    return new Tuple<int, string>(-3, ""); // for having bad object 
                }
            }
            catch (Exception ex)
            {
                logger.Error(LoggingRequest.CreateErrorMsg("UserBL", "ForgetPasswordBL", DateTime.Now.ToString(), ex.ToString()));
                return new Tuple<int, string>(-4, ""); // exception
            }
        }

        public async Task<int> ResetPasswordBL(ForgotPasswordBO obj)
        {
            try
            {
                if (obj != null)
                {
                    return await Task.Run(() =>
                    {
                        UserDAL objDal = new UserDAL(logger);
                        return objDal.ResetPassword(obj.customeremailaddress, obj.password,obj.deviceid,obj.devicetype);
                    });
                }
                else
                {
                    return -2; // for having bad object 
                }
            }
            catch (Exception ex)
            {
                logger.Error(LoggingRequest.CreateErrorMsg("UserBL", "ResetPasswordBL", DateTime.Now.ToString(), ex.ToString()));
                return -3;
            }
        }

        #endregion

        #region Change Password 

        public async Task<Tuple<int,string>> ChangePassword(ChangePasswordBO obj)
        {
            try
            {
                if (obj != null)
                {
                    return await Task.Run(() =>
                    {
                        UserDAL objDal = new UserDAL(logger);
                        var response =  objDal.ChangePassword(obj); // change password ResetPassword(obj.customeremailaddress, obj.password, obj.deviceid, obj.devicetype);
                        if(!string.IsNullOrEmpty(response.Result))
                        {
                            return new Tuple<int, string>(1, response.Result);
                        }

                        return new Tuple<int, string>(-1,"");
                    
                    });
                }
                else
                {
                    return new Tuple<int, string>(-2, "");
                }
            }
            catch (Exception ex)
            {
                logger.Error(LoggingRequest.CreateErrorMsg("UserBL", "ChangePassword", DateTime.Now.ToString(), ex.ToString()));
                return new Tuple<int, string>(-3, "");
            }
        }

        #endregion

        #region OTP 

        public async Task<OTPResponseBO> OTPHandling(OtpBO obj)
        {
            try
            {
                OTPResponseBO objResponse = new OTPResponseBO();
                UserDAL objDal = new UserDAL(logger);
                // Common case
                if (string.IsNullOrEmpty(obj.customeremailaddress))
                {
                    obj.customeremailaddress = "";
                }
                if (string.IsNullOrEmpty(obj.customermobilenumber))
                {
                    obj.customermobilenumber = "";
                }
                // comparison cases
                if (obj.otptype.Trim().ToLower() == "forgetpassword" && string.IsNullOrEmpty(obj.otp))
                {
                    // generate otp from db 
                    var result = await objDal.ValidateEmailAddressOrPhoneNumber(obj);
                    if(result != null)
                    {
                        // set value in object and return 
                        bool returnVal = EmailSending(obj.customeremailaddress, result);
                        if(returnVal)
                        {
                            objResponse.issuccessfullCode = 1;
                            objResponse.ResponseCode = result;
                            objResponse.ResponseMsg = "Code sent successfully !";
                        }
                        else
                        {
                            objResponse.issuccessfullCode = 1;
                            objResponse.ResponseCode = result;
                            objResponse.ResponseMsg = "Email Sending failed !";
                        }
                        return objResponse;
                    }
                    else
                    {
                        objResponse.issuccessfullCode = -1;
                        objResponse.ResponseCode = "";
                        objResponse.ResponseMsg = "Invalid phone number or email !";
                        return objResponse;
                    }
                }
                else if(obj.otptype.Trim().ToLower() == "forgetpassword" && !string.IsNullOrEmpty(obj.otp))
                {
                    // check verification time
                    var result = await objDal.VerifyOtpDAL(obj);
                    if (result > 0)
                    {
                        objResponse.issuccessfullCode = 3;
                        objResponse.ResponseCode = obj.otp;
                        objResponse.ResponseMsg = "Code verify successfully !";
                    }
                    else
                    {
                        objResponse.issuccessfullCode = -3;
                        objResponse.ResponseCode = obj.otp;
                        objResponse.ResponseMsg = "Code verify Failed !";
                    }
                }
                else if (obj.otptype.Trim().ToLower() == "logintime" && string.IsNullOrEmpty(obj.otp))
                {
                    var result = await objDal.ValidateEmailAddressOrPhoneNumber(obj);
                    if (result != null)
                    {
                        // set value in object and return 
                        bool returnVal = EmailSending(obj.customeremailaddress, result);
                        if (returnVal)
                        {
                            objResponse.issuccessfullCode = 2;
                            objResponse.ResponseCode = result;
                            objResponse.ResponseMsg = "Code sent successfully !";
                        }
                        else
                        {
                            objResponse.issuccessfullCode = 2;
                            objResponse.ResponseCode = result;
                            objResponse.ResponseMsg = "Email Sending failed !";
                        }
                        return objResponse;
                    }
                    else
                    {
                        objResponse.issuccessfullCode = -2;
                        objResponse.ResponseCode = "";
                        objResponse.ResponseMsg = "Invalid phone number or email !";
                        return objResponse;
                    }
                }
                else if (obj.otptype.Trim().ToLower() == "logintime" && !string.IsNullOrEmpty(obj.otp))
                {
                    var result = await objDal.VerifyOtpDAL(obj);
                    if (result > 0)
                    {
                        objResponse.issuccessfullCode = 4;
                        objResponse.ResponseCode = obj.otp;
                        objResponse.ResponseMsg = "Code verify successfully !";
                    }
                    else
                    {
                        objResponse.issuccessfullCode = -4;
                        objResponse.ResponseCode = obj.otp;
                        objResponse.ResponseMsg = "Code verify Failed !";
                    }
                }
                else
                {
                    // return error code 
                }
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
