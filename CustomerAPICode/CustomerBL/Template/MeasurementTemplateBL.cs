using CustomerBO.Template;
using CustomerCommon;
using CustomerDAL.Logging;
using logginglibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CustomerBL.Template
{
    public class MeasurementTemplateBL
    {
        private ILog logger;
        public MeasurementTemplateBL(ILog templogger)
        {
            this.logger = templogger;
        }

        public async Task<List<MeasurementTemplateBOResponse>> GetMeasurementTemplateBL(MeasurementTemplateBOContainer Param)
        {
            try
            {
                var result = await CallMeasurementTemplateAPI(Param);
                if (result != null)
                {
                    if (result.issuccessfull)
                    {
                        return result.measurementtemplatelist;
                    }
                    return null;
                }
                return null;
            }
            catch (Exception ex)
            {
                logger.Error(LoggingRequest.CreateErrorMsg("MeasurementTemplateBL", "GetMeasurementTemplateBL", DateTime.Now.ToString(), ex.ToString()));
                return null;
            }
        }

        private async Task<MeasurementTemplateObjResponse> CallMeasurementTemplateAPI(MeasurementTemplateBOContainer obj)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                    var postTask = await client.PostAsync("https://localhost:44364/api/Order/GetMeasurementTemplate", content);
                    //var postTask = await client.PostAsync("http://silayeebackend-001-site2.dtempurl.com/api/Order/GetMeasurementTemplate", content);
                    if (postTask.IsSuccessStatusCode)
                    {
                        string jsonResponse = await postTask.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<MeasurementTemplateObjResponse>(jsonResponse);
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
