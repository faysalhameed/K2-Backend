using CustomerBO.Measurements;
using CustomerCommon;
using CustomerDAL.Measurements;
using logginglibrary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CustomerBL.Measurements
{
    public class MeasurementBL
    {
        private ILog logger;
        public MeasurementBL(ILog templogger)
        {
            this.logger = templogger;
        }

        public async Task<Tuple<List<MeasurementBOResponse>, int>> GetMeasurementBL(MeasurementBO Param)
        {
            try
            {
                MeasurementDAL objDal = new MeasurementDAL(logger);
                var result = await objDal.GetMeasurementDAL(Param);
                return new Tuple<List<MeasurementBOResponse>, int>(result.Item1, result.Item2);
            }
            catch (Exception ex)
            {
                logger.Error(LoggingRequest.CreateErrorMsg("DressBL", "GetTopDress", DateTime.Now.ToString(), ex.ToString()));
                return null;
            }
        }


    }
}
