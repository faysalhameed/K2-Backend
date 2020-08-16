using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerBO.User
{
    public class ForgotPasswordBO
    {
        public string customeremailaddress { get; set; }
        public string deviceid { get; set; }
        public string devicetype { get; set; }
        public string password { get; set; }
    }

    public class ForgotPasswordcontainer
    {
        public ForgotPasswordBO userdata { get; set; }
    }
}
