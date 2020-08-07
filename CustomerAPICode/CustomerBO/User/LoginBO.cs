using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerBO.User
{
    public class LoginBO
    {
        public string emailaddress { get; set; }
        public string phonenumber { get; set; }
        public string userpassword { get; set; }
        public int deviceid { get; set; }
        public string devicetype { get; set; }
        public DateTime logindate { get; set; }
        public string authenticationmedium { get; set; }
        public string userfirstname { get; set; }
        public string userlastname { get; set; }
        public string authenticationtoken { get; set; }
        public int CustomerID { get; set; }
    }

    public class LoginRootBO
    {
        public LoginBO logindata { get; set; }
    }

}
