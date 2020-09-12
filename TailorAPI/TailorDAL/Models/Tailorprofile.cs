using System;
using System.Collections.Generic;

namespace TailorDAL.Models
{
    public partial class Tailorprofile
    {
        public int Tailorid { get; set; }
        public string Tailorfirstname { get; set; }
        public string Tailorlastname { get; set; }
        public int? Tailorage { get; set; }
        public string Tailoremailaddress { get; set; }
        public string Tailorwebsite { get; set; }
        public string Tailorcountry { get; set; }
        public string Tailorcity { get; set; }
        public string Tailorprovince { get; set; }
        public string Tailorzipcode { get; set; }
        public string Tailoraddress { get; set; }
        public string Tailormobilenumber { get; set; }
        public string Tailorothercontactnumber { get; set; }
        public string Tailorcnic { get; set; }
        public string Tailorpicture { get; set; }
        public string Tailorpassword { get; set; }
        public string Tailorrating { get; set; }
        public decimal? Tailorwalletamount { get; set; }
        public string Tailorprofilestatus { get; set; }
        public DateTime? Creationdatetime { get; set; }
        public DateTime? Modifieddatetime { get; set; }
        public int? Createdbyusertyperid { get; set; }
        public int? Createdbyuserid { get; set; }
        public int? Modifiedbyusertypeid { get; set; }
        public int? Modifiedbyuserid { get; set; }
        public int? Createdbydeviceid { get; set; }
        public int? Modifiedbydeviceid { get; set; }
        public string Tailorcompanytitle { get; set; }
        public string Tailorcompanyimage { get; set; }
        public string Tailorlatitude { get; set; }
        public string Tailorlongitude { get; set; }
    }
}
