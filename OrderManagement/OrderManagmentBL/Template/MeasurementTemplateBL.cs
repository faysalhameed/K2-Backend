using OrderManagmentBO.Template;
using OrderManagmentDAL.Template;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagmentBL.Template
{
    public class MeasurementTemplateBL
    {
        public async Task<List<MeasurementTemplateBOResponse>> GetMeasurementTemplateBL(int id)
        {
            try
            {
                MeasurementTemplateDAL objDal = new MeasurementTemplateDAL(); //(logger);
                var result = await objDal.GetMeasurementTemplateDAL(id);
                return result.Item1;
            }
            catch (Exception ex)
            {
                //logger.Error(LoggingRequest.CreateErrorMsg("DressBL", "GetTopDress", DateTime.Now.ToString(), ex.ToString()));
                return null;
            }
        }
    }
}
