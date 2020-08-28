using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using CustomerBO.User;
using CustomerDAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace CustomerDAL.Logging
{
    public static class LogUserLogin
    {
        public static string CreateErrorMsg(string ClassName, string MethodName, string Datetime, string exceptionMsg)
        {
            try
            {
                string msg = "";
                msg += Environment.NewLine + "***Error Message**" + Environment.NewLine;
                msg += "ClassName : " + ClassName + Environment.NewLine;
                msg += "Method name : " + MethodName + Environment.NewLine;
                msg += "DateTime : " + Datetime + Environment.NewLine;
                msg += "Exception Msg : " + exceptionMsg + Environment.NewLine;
                msg += "************************";
                return msg;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }


        //public static void SaveLoginResponse(int customerID, DateTime LoginDate, DateTime LogOut, bool LoginSuccess, DateTime creationDate,
        //    DateTime ModifyDate, int createdbyUserTypeID, int CreatedbyUserID, int modifiedbyUserTypeID, int modifiedbyUserID,
        //    int createdbyDeviceID, int modifiedbyDeviceID, string SessionTokken, string Authmedium)
        //{
        //    try
        //    {
        //        using (var dbContext = new SilaeeContext())
        //        {
        //            string sql = "sp_SaveLoginResponse @CustomerID,@LoginDate,@LogOutDate,@LoginSuccess," +
        //                "@creationDate,@ModifyDate,@createdbyUserTypeID,@CreatedbyUserID,@modifiedbyUserTypeID," +
        //                "@modifiedbyUserID,@createdbyDeviceID,@modifiedbyDeviceID,@SessionTokken,@Authmedium";
        //            List<SqlParameter> parameterList = new List<SqlParameter>();
        //            parameterList.Add(new SqlParameter("@CustomerID", customerID));
        //            parameterList.Add(new SqlParameter("@LoginDate", LoginDate));
        //            parameterList.Add(new SqlParameter("@LogOutDate", LogOut));
        //            parameterList.Add(new SqlParameter("@LoginSuccess", obj.customergender));
        //            parameterList.Add(new SqlParameter("@creationDate", obj.customeremailaddress));
        //            parameterList.Add(new SqlParameter("@ModifyDate", obj.customerwebsite));
        //            parameterList.Add(new SqlParameter("@createdbyUserTypeID", obj.customercountry));
        //            parameterList.Add(new SqlParameter("@CreatedbyUserID", obj.customercity));
        //            parameterList.Add(new SqlParameter("@modifiedbyUserTypeID", obj.customerprovince));
        //            parameterList.Add(new SqlParameter("@modifiedbyUserID", obj.customerzipcode));
        //            parameterList.Add(new SqlParameter("@createdbyDeviceID", obj.customeraddress));
        //            parameterList.Add(new SqlParameter("@modifiedbyDeviceID", obj.customermobilenumber));
        //            parameterList.Add(new SqlParameter("@SessionTokken", obj.customercnic));
        //            parameterList.Add(new SqlParameter("@Authmedium", obj.customerpicture));

        //            var out1 = new SqlParameter
        //            {
        //                ParameterName = "@ResultParam",
        //                DbType = System.Data.DbType.Int32,
        //                Direction = System.Data.ParameterDirection.Output
        //            };
        //            parameterList.Add(out1); //new SqlParameter("@ResultParam", obj.ModifiedbyDeviceId));
        //            int result = dbContext.Database.
        //                ExecuteSqlCommand(sql, parameterList);
        //            var out1Value = (int)out1.Value;
        //            return out1Value;

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return -1;
        //        // return null; // also we can log exception through common layer }
        //    }
        //}
    }
}
