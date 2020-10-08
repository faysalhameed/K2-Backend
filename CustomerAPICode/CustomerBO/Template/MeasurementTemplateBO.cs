using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerBO.Template
{
    public class MeasurementTemplateBO
    {
        public string sessiontoken { get; set; }
        public string deviceid { get; set; }
        public string dresscategoryid { get; set; }
    }

    public class MeasurementTemplateBOContainer
    {
        public MeasurementTemplateBO userdata { get; set; }
    }
}
