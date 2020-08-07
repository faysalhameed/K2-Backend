using CustomerBO.User;
using CustomerCommon;
using CustomerDAL.UsersOperations;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CustomerBL.User
{
    public class UserBL
    {
        #region Customer Profile Creation API

        // -1 : return value from sp side
        // -2 : validation failed for request object
        // -3 : bad request object 
        // -4 : Exception in code 

        private bool ValidateUserInfo(Userdata obj)
        {
            #region Optional Values check

            if(obj.creationdate == DateTime.MinValue)
            {
                obj.creationdate = DateTime.Now;
            }
            if (obj.modifieddatetime == DateTime.MinValue)
            {
                obj.modifieddatetime = DateTime.Now;
            }
            if(obj.customerwebsite == null)
            {
                obj.customerwebsite = "";
            }
            if(obj.customercountry == null)
            {
                obj.customercountry = "";
            }
            if(obj.customercity == null)
            {
                obj.customercity = "";
            }
            if(obj.customerprovince == null)
            {
                obj.customerprovince = "";
            }
            if(obj.customerzipcode == null)
            {
                obj.customerzipcode = "";
            }
            if(obj.customeraddress == null)
            {
                obj.customeraddress = "";
            }
            if(obj.customercnic == null)
            {
                obj.customercnic = "";
            }
            if(obj.customerpicture == null)
            {
                obj.customerpicture = "";
            }


            #endregion


            if (string.IsNullOrEmpty(obj.customeremailaddress))
            {
                return false;
            }
            else if(string.IsNullOrEmpty(obj.customermobilenumber))
            {
                return false;
            }
            else if(string.IsNullOrEmpty(obj.customerpassword))
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
                    UserDAL objDal = new UserDAL();
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
                return -4;
            }
        }

        #endregion

        #region Login Process 

        public async Task<Tuple<int,string,string>> Login(LoginRootBO obj)
        {
            try
            {
                if(obj == null || obj.logindata == null)
                {
                    return new Tuple<int,string,string>(-2,"",""); // all data missing 
                }

                //if (string.IsNullOrEmpty(obj.logindata.emailaddress) || string.IsNullOrEmpty(obj.logindata.userpassword))
                //{
                //    return -3; // missing email or password 
                //}

                if (!string.IsNullOrEmpty(obj.logindata.authenticationmedium))
                {
                    UserDAL objDAL = new UserDAL();
                    if (obj.logindata.authenticationmedium.ToLower() == "custom")
                    {
                        // simple just check and return response
                        if (!string.IsNullOrEmpty(obj.logindata.emailaddress) || !string.IsNullOrEmpty(obj.logindata.userpassword))
                        {
                            var response = objDAL.CustomLoginUser(obj.logindata.emailaddress,obj.logindata.userpassword);
                            return new Tuple<int, string, string>(response.customerid,response.customerfirstname, response.customerlastname);
                        }
                        return new Tuple<int, string, string>(-5,"",""); // email or password is missing for authentication
                    }
                    else if (obj.logindata.authenticationmedium.ToLower() == "facebook")
                    {
                        bool result = await ValidateFaceBookToken(obj.logindata.authenticationtoken);
                        if(result)
                        {
                            // check if profile exist if not then create profile and return result
                            var response = objDAL.LoginMediumUser(obj.logindata);
                            return new Tuple<int, string, string>(response,"","");
                        }
                        return new Tuple<int, string, string>(-4,"",""); // token auth failed
                    }
                    else if (obj.logindata.authenticationmedium.ToLower() == "google")
                    {
                        bool result = await ValidateGoogleToken(obj.logindata.authenticationtoken);
                        if(result)
                        {
                            // check if profile exist if not then create profile and return result 
                            var response = objDAL.LoginMediumUser(obj.logindata);
                            return new Tuple<int, string, string>(response,"","");
                        }
                        return new Tuple<int, string, string>(-4,"",""); // token auth failed
                    }
                    else
                    {
                        // faulty msg return no pre defined auth medium  defined
                        return new Tuple<int, string, string>(-6,"","");
                    }
                }
                else
                {
                    // faulty msg return no auth defined
                    return new Tuple<int, string, string>(-3,"","");
                }
            }
            catch (Exception ex)
            {
                return new Tuple<int, string, string>(-7,"",""); // exception
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
                return false;
            }
        }
        #endregion

        #region Update Process 

        public async Task<Tuple<int, string, string>> UpdateCustomer(LoginRootBO obj)
        {
            try
            {
                if (obj == null || obj.logindata == null)
                {
                    return new Tuple<int, string, string>(-2, "", ""); // all data missing 
                }

                UserDAL objDAL = new UserDAL();
                var response = objDAL.UpdateCustomerInfo(); // param need to define later
               // LoginResponseLog.LoginResponse();// later
                return new Tuple<int, string, string>(response.customerid, response.customerfirstname, response.customerlastname);


            }
            catch (Exception ex)
            {
                return new Tuple<int, string, string>(-7, "", ""); // exception
            }
        }


        #endregion

    }
}
