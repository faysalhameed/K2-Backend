using CustomerBO.Promotions;
using CustomerBO.Tailor;
using CustomerDAL.Logging;
using logginglibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CustomerBL.Tailor
{
    public class TailorBL
    {
        private ILog logger;

        public TailorBL(ILog templogger)
        {
            this.logger = templogger;
        }

        public async Task<List<Tailorlist>> GetTailorData(TailorBOContainer objTailor)
        {
            try
            {
               var result = await CallTailorAPI(objTailor);
               if(result != null)
               {
                    if (result.issuccessfull)
                    {
                        return result.tailorlist;
                    }
                    return null;
               }
                return null;
            }
            catch (Exception ex)
            {
                logger.Error(LogUserLogin.CreateErrorMsg("TailorBL", "GetTailorData", DateTime.Now.ToString(), ex.ToString()));
                return null;
            }
        }

        private async Task<TailorListResponse> CallTailorAPI(TailorBOContainer objTailor)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(objTailor), Encoding.UTF8, "application/json");
                    //var postTask = await client.PostAsync("https://localhost:44364/api/Tailor/TailorListing", content);
                    var postTask = await client.PostAsync("http://silayeebackend-001-site2.dtempurl.com/api/Tailor/TailorListing", content);
                    if (postTask.IsSuccessStatusCode)
                    {
                        string jsonResponse = await postTask.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<TailorListResponse>(jsonResponse);
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                logger.Error(LogUserLogin.CreateErrorMsg("TailorBL", "CallTailorAPI", DateTime.Now.ToString(), ex.ToString()));
                return null;
            }
        }


        public async Task<List<PromotionBOResponse>> GetPromotionData(PromotionBOcontainer objPromotion)
        {
            try
            {
                var result = await CallTailorAPI(objPromotion);
                if (result != null)
                {
                    if (result.issuccessfull)
                    {
                        return result.promotionlist;
                    }
                    return null;
                }
                return null;
            }
            catch (Exception ex)
            {
                logger.Error(LogUserLogin.CreateErrorMsg("TailorBL", "GetPromotionData", DateTime.Now.ToString(), ex.ToString()));
                return null;
            }
        }

        private async Task<PromotionObjResponse> CallTailorAPI(PromotionBOcontainer objPromo)
        {
            try
            {
                var requestObj = new
                {
                    city = objPromo.userdata.city,
                    listingcount = objPromo.userdata.listingcount,
                    gender = objPromo.userdata.gender
                };
                var RequestObjFinal = new
                {
                    userdata = requestObj
                };
                using (var client = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(RequestObjFinal), Encoding.UTF8, "application/json");
                    var postTask = await client.PostAsync("http://silayeebackend-001-site2.dtempurl.com/api/Tailor/PromotionListing", content);
                    if (postTask.IsSuccessStatusCode)
                    {
                        string jsonResponse = await postTask.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<PromotionObjResponse>(jsonResponse);
                    }
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
