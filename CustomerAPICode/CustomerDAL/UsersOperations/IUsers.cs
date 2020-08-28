using CustomerBO.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDAL.UsersOperations
{
    interface IUsers
    {
        int SaveUser(Userdata obj);
        void LoginActivity(LoginActivityBO obj);
        int ValidateEmailAddress(string emailAddress);

        Task<string> ChangePassword(ChangePasswordBO obj);

    }
}
