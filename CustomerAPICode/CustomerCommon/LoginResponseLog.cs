using CustomerBO.User;
using CustomerDAL.UsersOperations;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerCommon
{
    public static class LoginResponseLog
    {
        public static void LoginResponse(LoginActivityBO obj)
        {
            try
            {
                UserDAL objDAL = new UserDAL();
                objDAL.LoginActivity(obj);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
