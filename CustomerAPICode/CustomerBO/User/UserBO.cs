using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerBO.User
{
    public class Userdata
    {
        public int customerid { get; set; }
        public string customerfirstname { get; set; }
        public string customerlastname { get; set; }
        public int customerage { get; set; }
        public string customergender { get; set; }
        public string customeremailaddress { get; set; }
        public string customerwebsite { get; set; }
        public string customercountry { get; set; }
        public string customercity { get; set; }
        public string customerprovince { get; set; }
        public string customerzipcode { get; set; }
        public string customeraddress { get; set; }
        public string customermobilenumber { get; set; }
        public string customerothercontactnumber { get; set; }
        public string customercnic { get; set; }
        public string customerpicture { get; set; }
        public string customerpassword { get; set; }
        public decimal customerrating { get; set; }
        public decimal customerwalletamount { get; set; }
        public bool customerprofilestatus { get; set; }
        public DateTime creationdate { get; set; }
        public DateTime modifieddatetime { get; set; }
        public int createdbyusertypeid { get; set; }
        public int createdbyuserid { get; set; }
        public int modifiedbyusertypeid { get; set; }
        public int modifiedbyuserid { get; set; }
        public int createdbydeviceid { get; set; }
        public int modifiedbydeviceid { get; set; }
     
    }

    public class Root
    {
        public Userdata userdata { get; set; }

    }
}
