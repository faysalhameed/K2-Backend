using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerBO.User
{
    public class ChangePasswordBO
    {
        public int customerid { get; set; }
        public string newpassword { get; set; }
        public string sessiontoken { get; set; }
        public string deviceid { get; set; }
        public string devicetype { get; set; }
    }

    public class ChangePasswordContainer
    {
        public ChangePasswordBO userdata { get; set; }
    }

}
