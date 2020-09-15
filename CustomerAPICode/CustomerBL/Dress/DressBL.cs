using CustomerBO.TopDress;
using CustomerCommon;
using CustomerDAL.Dress;
using logginglibrary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CustomerBL.Dress
{
    public class DressBL
    {
        private ILog logger;
        public DressBL(ILog templogger)
        {
            this.logger = templogger;
        }

        public async Task<Tuple<List<TopDressBOResult>,int>> GetTopDress(TopDressBO Param)
        {
            try
            {
                int _age = -1;
                int _ageto = -1;
                int.TryParse(Param.agefrom, out _age);
                int.TryParse(Param.ageto, out _ageto);
                DressDAL objDal = new DressDAL(logger);
                var result = await objDal.GetTopDressDAL(Param, _age, _ageto);
                return new Tuple<List<TopDressBOResult>, int>(result.Item1,result.Item2);
            }
            catch (Exception ex)
            {
                logger.Error(LoggingRequest.CreateErrorMsg("DressBL", "GetTopDress", DateTime.Now.ToString(), ex.ToString()));
                return null;
            }
        }

    }
}
