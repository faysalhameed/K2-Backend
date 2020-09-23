using CustomerBO.SaveAddress;
using CustomerCommon;
using CustomerDAL.SaveAddress;
using logginglibrary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CustomerBL.SaveAddress
{
    public class SaveAddressBL
    {
        private ILog logger;
        public SaveAddressBL(ILog templogger)
        {
            this.logger = templogger;
        }

        public async Task<Tuple<List<SaveAddressBOResponse>, int>> GetSaveAddressBL(SaveAddressBO Param)
        {
            try
            {
                SaveAddressDAL objDal = new SaveAddressDAL(logger);
                var result = await objDal.GetSaveAddressDAL(Param);
                return new Tuple<List<SaveAddressBOResponse>, int>(result.Item1, result.Item2);
            }
            catch (Exception ex)
            {
                logger.Error(LoggingRequest.CreateErrorMsg("DressBL", "GetTopDress", DateTime.Now.ToString(), ex.ToString()));
                return null;
            }
        }
    }
}
