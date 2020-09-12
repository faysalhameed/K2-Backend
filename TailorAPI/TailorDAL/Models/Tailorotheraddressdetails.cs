using System;
using System.Collections.Generic;

namespace TailorDAL.Models
{
    public partial class Tailorotheraddressdetails
    {
        public int Addressid { get; set; }
        public int? Tailorid { get; set; }
        public bool? Isdefault { get; set; }
        public string Tailorcountry { get; set; }
        public string Tailorcity { get; set; }
        public string Tailorprovince { get; set; }
        public string Tailorzipcode { get; set; }
        public string Tailoraddress { get; set; }
        public DateTime? Creationdatetime { get; set; }
        public DateTime? Modifieddatetime { get; set; }
        public int? Createdbyusertypeid { get; set; }
        public int? Createdbyuserid { get; set; }
        public int? Modifiedbyusertypeid { get; set; }
        public int? Modifiedbyuserid { get; set; }
        public int? Createdbydeviceid { get; set; }
        public int? Modifiedbydeviceid { get; set; }
    }
}
