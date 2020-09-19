using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerBO.Promotions
{
    public class PromotionBO
    {
        public int customerid { get; set; }
        public string sessiontoken { get; set; }
        public string deviceid { get; set; }
        public string devicetype { get; set; }
        public string city { get; set; }
        public int listingcount { get; set; }
        public string gender { get; set; }

        public int agefrom { get; set; }

        public int ageto { get; set; }

        public string promotionsourcetype { get; set; }

        public int pagecount { get; set; }
    }

    public class PromotionBOcontainer
    {
        public PromotionBO userdata { get; set; }
    }
}
