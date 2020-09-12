using System;
using System.Collections.Generic;
using System.Text;

namespace TailorBO.BOPromotion
{
    public class PromotionBOResponse
    {
        public int promotionid { get; set; }
        public string promotionimage { get; set; }
        public string promotiongendertype { get; set; }
        public string promotiondatetime { get; set; }
        public int productid { get; set; }
        public int tailorid { get; set; }

    }
}
