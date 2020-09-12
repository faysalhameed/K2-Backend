using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TailorBO.BOPromotion;
using TailorDAL.DALLayerCode;

namespace TailorBL.BLPromotion
{
    public class PromotionBL
    {
        public async Task<List<PromotionBOResponse>> getPromotions(PromotionBO obj)
        {
            try
            {
                TailorDALClass objdal = new TailorDALClass();
                var result = await objdal.PromotionListingfunction(obj.city, obj.listingcount, obj.gender);
                if(result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
