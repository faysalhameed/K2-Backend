using CustomerBO.User;
using CustomerDAL.UsersOperations;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CustomerBL.User
{
    public class UserBL
    {
        #region Customer Profile Creation API
        private bool ValidateUserInfo(Userdata obj)
        {
            if(string.IsNullOrEmpty(obj.CustomereMailAddress))
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

        // -1 : return value from sp side
        // -2 : validation failed for request object
        // -3 : bad request object 
        // -4 : Exception in code 
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

        public async Task<bool> ValidateFaceBookToken(string token)
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
