using CustomerBO.Device;
using CustomerDAL.Device;
using CustomerDAL.Logging;
using logginglibrary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CustomerBL.Device
{
    public class DeviceBL
    {
        private ILog logger;

        public DeviceBL(ILog templogger)
        {
            this.logger = templogger;
        }

        public DeviceBOResponse VerifyDevice(DeviceVerificationBO obj)
        {
            try
            {
                DeviceDAL objdal = new DeviceDAL(logger);
                var result = objdal.AuthDeviceID(obj.deviceid);
                if(result != null)
                {
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                logger.Error(LogUserLogin.CreateErrorMsg("DeviceBL", "VerifyDevice", DateTime.Now.ToString(), ex.ToString()));
                return null;
            }
        }
    }
}
