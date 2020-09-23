using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerBO.Measurements
{
    public class MeasurementBO
    {
        public int customerid { get; set; }
        public string deviceid { get; set; }
        public string devicetype { get; set; }
        public string sessiontoken { get; set; }
    }

    public class MeasurementBOContainer
    {
        public MeasurementBO userdata { get; set; }
    }
}
