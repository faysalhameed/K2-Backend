using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerBO.Tailor
{
    public class TailorListResponse
    {
        public bool issuccessfull { get; set; }
        public string responsemessage { get; set; }
        public List<Tailorlist> tailorlist { get; set; }
    }

    public class Tailorlist
    {
        public int tailorid { get; set; }
        public string tailorcompanytitle { get; set; }
        public string tailorcompanyimage { get; set; }
        public string tailorrating { get; set; }
        public string tailorlatitude { get; set; }
        public string tailorlongitude { get; set; }
    }

}
