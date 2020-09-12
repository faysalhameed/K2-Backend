using System;
using System.Collections.Generic;

namespace TailorDAL.Models
{
    public partial class Tailorstitchingdetails
    {
        public int Stitchid { get; set; }
        public int? Tailorid { get; set; }
        public string Stitchtype { get; set; }
        public decimal? Stitchcharges { get; set; }
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
