using System;
using System.Collections.Generic;

namespace TailorDAL.Models
{
    public partial class Reviewfortailors
    {
        public int Reviewid { get; set; }
        public int? UsertypeId { get; set; }
        public int? Userid { get; set; }
        public string Useremailaddress { get; set; }
        public string Userfirstname { get; set; }
        public string Userlastname { get; set; }
        public string Reviewrating { get; set; }
        public string Reviewcomments { get; set; }
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
