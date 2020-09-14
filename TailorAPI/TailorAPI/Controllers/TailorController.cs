using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using logginglibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TailorBL.BLPromotion;
using TailorBL.BLTailor;
using TailorBO.BOPromotion;
using TailorBO.BOTailor;

namespace TailorAPI.Controllers
{
    [Route("api/[controller]/[action]/{id?}")]
    [ApiController]
    public class TailorController : ControllerBase
    {

        private ILog logger;

        public TailorController(ILog logger)
        {
            this.logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> TailorListing(TailorEntityBOContainer Param)
        {
            try
            {
                if (Param == null)
                {
                    return BadRequest();
                }

                TailorBLL objBL = new TailorBLL(logger);
                var result = await objBL.TailorListingBL(Param.userdata);

                logger.Information("Calling Tailor Business Layer Object within TailorListing Method inside Class TailorController. Data = " + String.Join(";", result.Select(o => o.ToString())));

                if(result != null)
                {
                    var SuccessMsg = new { issuccessfull = true, responsemessage = "", tailorlist = result };
                    return new OkObjectResult(SuccessMsg);
                }
                else
                {
                    var faulty = new { issuccessfull = false, responsemessage = "", tailorlist = (string) null };
                    return new OkObjectResult(faulty);
                }
            }
            catch (Exception ex)
            {
               // logger.Error(LoggingRequest.CreateErrorMsg("Tailor Controller", "TailorListing", DateTime.Now.ToString(), ex.ToString()));
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> PromotionListing(PromotionBOContainer Param)
        {
            try
            {
                if (Param == null)
                {
                    return BadRequest();
                }

                PromotionBL objBL = new PromotionBL(logger);
                var result = await objBL.getPromotions(Param.userdata);

                logger.Information("Calling Promotion Business Layer Object within PromotionListing Method inside Class TailorController. Data = " + String.Join(";", result.Select(o => o.ToString())));

                if (result != null)
                {
                    var SuccessMsg = new { issuccessfull = true, responsemessage = "", promotionlist = result };
                    return new OkObjectResult(SuccessMsg);
                }
                else
                {
                    var faulty = new { issuccessfull = false, responsemessage = "", promotionlist = (string)null };
                    return new OkObjectResult(faulty);
                }
            }
            catch (Exception ex)
            {
                // logger.Error(LoggingRequest.CreateErrorMsg("Tailor Controller", "TailorListing", DateTime.Now.ToString(), ex.ToString()));
                return BadRequest();
            }
        }


    }
}
