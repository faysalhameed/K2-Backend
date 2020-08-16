using CustomerBO.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerDAL.UsersOperations
{
    interface IUsers
    {
        int SaveUser(Userdata obj);
        void LoginActivity(LoginActivityBO obj);
        int ValidateEmailAddress(string emailAddress);

    }
}
