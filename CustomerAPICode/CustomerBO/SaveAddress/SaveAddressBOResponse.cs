using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerBO.SaveAddress
{
    public class SaveAddressBOResponse
    {
        public string country { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string zipcode { get; set; }
        public string address { get; set; }
    }
}
