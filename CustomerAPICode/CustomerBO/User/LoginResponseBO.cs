using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerBO.User
{
    public class LoginResponseBO
    {
        public bool issuccessful { get; set; }
        public int customerid { get; set; }
        public string customerfirstname { get; set; }
        public string customerlastname { get; set; }
        public string responsemessage { get; set; }
        public string Sesssiontoken { get; set; }
        public int loginresponsecode { get; set; }
        public Userdata userdata { get; set; }

    }
}
