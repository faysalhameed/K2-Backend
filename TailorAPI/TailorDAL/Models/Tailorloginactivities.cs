using System;
using System.Collections.Generic;

namespace TailorDAL.Models
{
    public partial class Tailorloginactivities
    {
        public int Activityid { get; set; }
        public int? Tailorid { get; set; }
        public DateTime? Logindatetime { get; set; }
        public DateTime? Logoutdatetime { get; set; }
        public bool? Loginsuccessful { get; set; }
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
