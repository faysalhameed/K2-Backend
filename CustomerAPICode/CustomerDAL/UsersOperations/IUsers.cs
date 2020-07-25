using CustomerBO.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerDAL.UsersOperations
{
    interface IUsers
    {
        int SaveUser(UserBO obj);
    }
}
