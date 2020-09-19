using System;
using System.Collections.Generic;
using System.Text;

namespace TailorBO.BOPromotion
{
    public class PromotionBO
    {
        public string city { get; set; }
        public int listingcount { get; set; }
        public string gender { get; set; }

        public int agefrom { get; set; }

        public int ageto { get; set; }

        public string promotionsourcetype { get; set; }

        public int pagecount  {get; set;}


    }

    public class PromotionBOContainer
    {
        public PromotionBO userdata { get; set; }
    }

}
