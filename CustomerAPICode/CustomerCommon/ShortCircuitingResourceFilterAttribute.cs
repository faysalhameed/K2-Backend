using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CustomerCommon
{
    public class ShortCircuitingResourceFilterAttribute : Attribute, IResourceFilter
    {
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            //ReadBodyAsString(context.HttpContext.Request);
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {

        }

        private async void ReadBodyAsString(HttpRequest request)
        {
            var initialBody = request.Body; // Workaround

            try
            {
                using (var reader = new StreamReader(request.Body))
                {
                    var body = await reader.ReadToEndAsync();
                    LoggingRequest.LogRequest(request.Method, request.IsHttps, request.Host.ToString(), request.ContentType, body.ToString());
                }
            }
            catch(Exception ex)
            {

            }
        }


    }
}
