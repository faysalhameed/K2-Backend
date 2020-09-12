using System;
using System.Collections.Generic;

namespace TailorDAL.Models
{
    public partial class Tailorproductdetails
    {
        public int Productid { get; set; }
        public int? Tailorid { get; set; }
        public string Producttitle { get; set; }
        public string Productdescription { get; set; }
        public decimal? Productcharges { get; set; }
        public byte[] Productimage { get; set; }
        public bool? Isactive { get; set; }
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
