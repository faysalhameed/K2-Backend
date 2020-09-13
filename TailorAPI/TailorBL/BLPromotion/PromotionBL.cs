using logginglibrary;
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
        private ILog logger;

        public PromotionBL(ILog templogger)
        {
            this.logger = templogger;
        }
        public async Task<List<PromotionBOResponse>> getPromotions(PromotionBO obj)
        {
            try
            {
                TailorDALClass objdal = new TailorDALClass(logger);
                var result = await objdal.PromotionListingfunction(obj.city, obj.listingcount, obj.gender);

                logger.Information("Calling Promotion Listing Data Layer Object within getPromotions Method inside Class PromotionBL.");

                if (result != null)
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
