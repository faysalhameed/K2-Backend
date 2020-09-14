using CustomerBO.Device;
using CustomerDAL.Device;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CustomerBL.Device
{
    public class DeviceBL
    {
        public DeviceBOResponse VerifyDevice(DeviceVerificationBO obj)
        {
            try
            {
                DeviceDAL objdal = new DeviceDAL();
                var result = objdal.AuthDeviceID(obj.deviceid);
                if(result != null)
                {
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
