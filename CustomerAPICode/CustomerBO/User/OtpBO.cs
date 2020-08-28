using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerBO.User
{
    public class OtpBO
    {
        public string customeremailaddress { get; set; }
        public string customermobilenumber { get; set; }
        public string otptype { get; set; }
        public string otp { get; set; }
        public string deviceid { get; set; }
        public string devicetype { get; set; }

    }

    public class OTPContainerBO
    {
        public OtpBO userdate { get; set; }
    }
}
