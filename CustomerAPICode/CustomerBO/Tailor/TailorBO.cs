using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerBO.Tailor
{
    public class TailorBO
    {
        public int customerid { get; set; }
        public string sessiontoken { get; set; }
        public string city { get; set; }
        public string searchtype { get; set; }
        public string devicelatitude { get; set; }
        public string devicelongitude { get; set; }
        public int listingcount { get; set; }
        public string deviceid { get; set; }
        public string devicetype { get; set; }
        public int latlongpermissionallowed { get; set; }
    }

    public class TailorBOContainer
    {
        public TailorBO userdata { get; set; }
    }

}
