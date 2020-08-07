using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerCommon
{
    public static class LoginResponseLog
    {
        public static void LoginResponse(int customerID, DateTime LoginDate, DateTime LogOut,bool LoginSuccess,DateTime creationDate,
            DateTime ModifyDate, int createdbyUserTypeID, int CreatedbyUserID,  int modifiedbyUserTypeID, int modifiedbyUserID, 
            int createdbyDeviceID, int modifiedbyDeviceID, string SessionTokken, string Authmedium)
        {
            try
            {

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
