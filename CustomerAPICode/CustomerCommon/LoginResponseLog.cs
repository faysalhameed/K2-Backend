using CustomerBO.User;
using CustomerDAL.UsersOperations;
using logginglibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerCommon
{

    public class LoginResponseLog
    {
        private ILog logger;


        public LoginResponseLog(ILog templogger)
        {
            this.logger = templogger;
        }
        public  void LoginResponse(LoginActivityBO obj)
        {
            try
            {
                UserDAL objDAL = new UserDAL(logger);
                objDAL.LoginActivity(obj);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
