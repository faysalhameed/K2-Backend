using CustomerBO.Categories;
using CustomerCommon;
//using CustomerDAL.Categories;
using CustomerDAL.Logging;
using logginglibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CustomerBL.Categories
{
    public class CategoriesBL
    {
        private ILog logger;
        public CategoriesBL(ILog templogger)
        {
            this.logger = templogger;
        }

        public async Task<List<CategoriesBOResponse>> GetDressCategoryBL()
        {
            try
            {
                var result = await CallDressCategoryAPI();
                if (result != null)
                {
                    if (result.issuccessfull)
                    {
                        return result.dresscategorieslist;
                    }
                    return null;
                }
                return null;
            }
            catch (Exception ex)
            {
                logger.Error(LoggingRequest.CreateErrorMsg("CategoriesBL", "GetDressCategoryBL", DateTime.Now.ToString(), ex.ToString()));
                return null;
            }
        }

        private async Task<CategoriesObjResponse> CallDressCategoryAPI()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(""), Encoding.UTF8, "application/json");
                    var postTask = await client.PostAsync("https://localhost:44364/api/Order/GetDressCategories", content);
                    //var postTask = await client.PostAsync("http://silayeebackend-001-site2.dtempurl.com/api/Order/GetDressCategories", content);
                    if (postTask.IsSuccessStatusCode)
                    {
                        string jsonResponse = await postTask.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<CategoriesObjResponse>(jsonResponse);
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                logger.Error(LogUserLogin.CreateErrorMsg("CategoriesBL", "CallDressCategoryAPI", DateTime.Now.ToString(), ex.ToString()));
                return null;
            }
        }



    }
}
