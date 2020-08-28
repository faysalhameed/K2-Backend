using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerCommon
{
    public static class LoggingRequest
    {

        public static string CreateErrorMsg(string ClassName, string MethodName, string Datetime,string exceptionMsg)
        {
            try
            {
                string msg = "";
                msg += Environment.NewLine  + "***Error Message**" + Environment.NewLine ;
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


        public static void LogRequest(string MethodName, bool isHTTPS, string Host, string ContentType, string JsonBody )
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
