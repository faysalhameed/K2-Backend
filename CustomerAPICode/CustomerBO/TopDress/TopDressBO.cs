using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerBO.TopDress
{
    public class TopDressBO
    {
        public string dresscategory { get; set; }
        public string gender { get; set; }
        public string agefrom { get; set; } // should be int after conversion
        public string ageto { get; set; } // should be int after conversion
        public string area { get; set; }
        public string type { get; set; }
        public string sessiontoken { get; set; }
        public string deviceid { get; set; }
        public string devicetype { get; set; }
        public int listingcount { get; set; }
    }

    public class TopDressBOContainer
    {
        public TopDressBO userdata { get; set; }
    }

}
