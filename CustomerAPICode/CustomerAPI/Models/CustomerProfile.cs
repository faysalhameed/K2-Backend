using System;
using System.Collections.Generic;

namespace CustomerAPI.Models
{
    public partial class CustomerProfile
    {
        public int CustomerId { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public int? CustomerAge { get; set; }
        public string CustomerGender { get; set; }
        public string CustomereMailAddress { get; set; }
        public string CustomerWebsite { get; set; }
        public string CustomerCountry { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerProvince { get; set; }
        public string CustomerZipCode { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerMobileNumber { get; set; }
        public string CustomerOtherContactNumber { get; set; }
        public string CustomerCnic { get; set; }
        public string CustomerPicture { get; set; }
        public string CustomerPassword { get; set; }
        public decimal? CustomerRating { get; set; }
        public decimal? CustomerWalletAmount { get; set; }
        public bool? CustomerProfileStatus { get; set; }
        public DateTime? CreationDateTime { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public int? CreatedbyUserTypeId { get; set; }
        public int? CreatedbyUserId { get; set; }
        public int? ModifiedbyUserTypeId { get; set; }
        public int? ModifiedbyUserId { get; set; }
        public int? CreatedbyDeviceId { get; set; }
        public int? ModifiedbyDeviceId { get; set; }
        public string Uniquedeviceid { get; set; }
    }
}
