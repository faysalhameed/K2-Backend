using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerBO.SaveAddress
{
    public class SaveAddressBO
    {
        public int customerid { get; set; }
        public string deviceid { get; set; }
        public string devicetype { get; set; }
        public string sessiontoken { get; set; }
    }

    public class SaveAddressBOContainer
    {
        public SaveAddressBO userdata { get; set; }
    }

}
