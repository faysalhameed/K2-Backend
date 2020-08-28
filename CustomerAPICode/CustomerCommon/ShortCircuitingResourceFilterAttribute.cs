using logginglibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CustomerCommon
{
    public class ShortCircuitingResourceFilterAttribute : IResourceFilter
    {
        private ILog logger;

        public ShortCircuitingResourceFilterAttribute(ILog templogger)
        {
            logger = templogger;
        }


        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[32768];
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, read);
            }
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            context.HttpContext.Request.EnableBuffering();
            context.HttpContext.Request.Body.Seek(0, SeekOrigin.Begin);
            using (var reader = new StreamReader(context.HttpContext.Request.Body, Encoding.ASCII, true, 1024, true))
            {
                string RequestMethod = "";
                string RequestEndPoint = "";
                if (context.HttpContext.Request.Path != null)
                {
                    RequestMethod = context.HttpContext.Request.Method;
                    RequestEndPoint = context.HttpContext.Request.Path.Value;
                }
                
                var body = reader.ReadToEndAsync();
                if(!string.IsNullOrEmpty(RequestEndPoint))
                {
                    string _finalResult = "";
                    _finalResult += Environment.NewLine + "=========================New Request===============================" + Environment.NewLine;
                    _finalResult = "Request Type :" + RequestMethod + Environment.NewLine;
                    _finalResult += "Request EndPoint :" + RequestEndPoint + Environment.NewLine;
                    _finalResult += "Request Body :" + body.Result.ToString() + Environment.NewLine;
                    _finalResult += "Request DateTime :" + DateTime.Now.ToString() + Environment.NewLine; ;
                    _finalResult += "===================================================================" + Environment.NewLine;
                    logger.Information(_finalResult);
                }
                else
                {
                    logger.Information(body.Result.ToString());
                }
                
            }
            context.HttpContext.Request.Body.Position = 0;


        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {

        }

        


    }
}
