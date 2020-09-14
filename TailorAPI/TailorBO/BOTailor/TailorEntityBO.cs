using System;
using System.Collections.Generic;
using System.Text;

namespace TailorBO.BOTailor
{
    public class TailorEntityBO
    {
        public int customerid { get; set; }
        public string sessiontoken { get; set; }
        public string city { get; set; }
        public string searchtype { get; set; }
        public decimal devicelatitude { get; set; }
        public decimal devicelongitude { get; set; }
        public string deviceid { get; set; }
        public string devicetype { get; set; }
        public int listingcount { get; set; }
    }

    public class TailorEntityBOContainer
    {
        public TailorEntityBO userdata { get; set; }
    }


}
