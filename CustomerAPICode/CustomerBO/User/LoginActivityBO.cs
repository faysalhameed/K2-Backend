using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerBO.User
{
    public class LoginActivityBO
    {
        public int customerID { get; set; }
        public DateTime LoginDate { get; set; }
        public DateTime logOut { get; set; }
        public bool LoginSucess { get; set; }
        public DateTime creationDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public int createdbyUserTypeID { get; set; }
        public int CreatedbyUserID { get; set; }
        public int modifiedbyUserTypeID { get; set; }
        public int modifiedbyUserID { get; set; }
        public string deviceType { get; set; }
        public string SessionToken { get; set; }
        public string Authmedium { get; set; }
        public string DeviceID { get; set; }
        
       
    }
}
