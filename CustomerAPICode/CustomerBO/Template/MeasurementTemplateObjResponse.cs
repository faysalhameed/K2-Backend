using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerBO.Template
{
    public class MeasurementTemplateObjResponse
    {
        public bool issuccessfull { get; set; }
        public string responsemessage { get; set; }
        public List<MeasurementTemplateBOResponse> measurementtemplatelist { get; set; }
    }
}
