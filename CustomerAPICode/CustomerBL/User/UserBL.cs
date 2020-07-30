using CustomerBO.User;
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

            if(obj.CreationDate == DateTime.MinValue)
            {
                obj.CreationDate = DateTime.Now;
            }
            if (obj.ModifiedDateTime == DateTime.MinValue)
            {
                obj.ModifiedDateTime = DateTime.Now;
            }
            if(obj.CustomerWebsite == null)
            {
                obj.CustomerWebsite = "";
            }
            if(obj.CustomerCountry == null)
            {
                obj.CustomerCountry = "";
            }
            if(obj.CustomerCity == null)
            {
                obj.CustomerCity = "";
            }
            if(obj.CustomerProvince == null)
            {
                obj.CustomerProvince = "";
            }
            if(obj.CustomerZipCode == null)
            {
                obj.CustomerZipCode = "";
            }
            if(obj.CustomerAddress == null)
            {
                obj.CustomerAddress = "";
            }
            if(obj.CustomerCNIC == null)
            {
                obj.CustomerCNIC = "";
            }
            if(obj.CustomerPicture == null)
            {
                obj.CustomerPicture = "";
            }


            #endregion


            if (string.IsNullOrEmpty(obj.CustomereMailAddress))
            {
                return false;
            }
            else if(string.IsNullOrEmpty(obj.CustomerMobileNumber))
            {
                return false;
            }
            else if(string.IsNullOrEmpty(obj.CustomerPassword))
            {
                return false;
            }
            else if (string.IsNullOrEmpty(obj.CustomerFirstName))
            {
                return false;
            }
            else if (string.IsNullOrEmpty(obj.CustomerLastName))
            {
                return false;
            }
            else if (string.IsNullOrEmpty(obj.CustomerGender))
            {
                return false;
            }
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

        public async Task<int> Login(LoginRootBO obj)
        {
            try
            {
                if(obj == null || obj.logindata == null)
                {
                    return -2;
                }

                if (!string.IsNullOrEmpty(obj.logindata.authenticationmedium))
                {
                    //UserBL objBL = new UserBL();
                    if (obj.logindata.authenticationmedium.ToLower() == "custom")
                    {

                    }
                    else if (obj.logindata.authenticationmedium.ToLower() == "facebook")
                    {
                        bool result = await ValidateFaceBookToken(obj.logindata.authenticationtoken);
                        if(result)
                        {

                        }
                        return -4; // token auth failed
                    }
                    else if (obj.logindata.authenticationmedium.ToLower() == "google")
                    {
                        bool result = await ValidateFaceBookToken(obj.logindata.authenticationtoken);
                        if(result)
                        {
 
                        }
                        return -4; // token auth failed
                    }
                    else
                    {
                        // faulty msg return no pre defined auth medium  defined
                        return -3;
                    }
                }
                else
                {
                    // faulty msg return no auth defined
                    return -3;
                }



            }
            catch (Exception ex)
            {
                throw;
            }
            return -1;
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
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://www.googleapis.com/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync("oauth2/v1/tokeninfo?access_token=" + token);
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

    }
}
