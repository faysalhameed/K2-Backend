using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using CustomerBL.User;
using CustomerBO.User;
using CustomerDAL.Models;

namespace CustomerAPI.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    [Route("api/[controller]/[action]/{id?}")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Account Creation API
        //[Route("AddCustomer")]
        [HttpPost]
        public async Task<IActionResult> AddCustomer(UserBO Param)
        {
            try
            {
                UserBL objBL = new UserBL();
                int result = await objBL.AddUser(Param);
                if (result > 0)
                {
                    //var SuccessMsg = new { isSuccessful = true, ResponseMessage = "Profile created successfully !", CustomerEmailAddress = HttpStatusCode.OK, Result = "Saved Sucessfully" };
                    var SuccessMsg = new { isSuccessful = true, ResponseMessage = "Profile created successfully !", CustomerEmailAddress = Param.CustomereMailAddress, CustomerMobileNumber = Param.CustomerMobileNumber };
                    return new OkObjectResult(SuccessMsg);
                }
                else if (result == -2)
                {
                    //validation failed for request object
                    var FaultyMsg = new { isSuccessful = false, ResponseMessage = "One or more fields missing", CustomerEmailAddress = Param.CustomereMailAddress, CustomerMobileNumber = Param.CustomerMobileNumber };
                    return new OkObjectResult(FaultyMsg);
                }
                else if (result == -3)
                {
                    // -3 : bad request object
                    var FaultyMsg = new { isSuccessful = false, ResponseMessage = "Bad Request !", CustomerEmailAddress = Param.CustomereMailAddress, CustomerMobileNumber = Param.CustomerMobileNumber };
                    return new OkObjectResult(FaultyMsg);
                }
                else if (result == -4)
                {
                    // -4 : Exception in code
                    var FaultyMsg = new { isSuccessful = false, ResponseMessage = "Error occured while processing", CustomerEmailAddress = Param.CustomereMailAddress, CustomerMobileNumber = Param.CustomerMobileNumber };
                    return new OkObjectResult(FaultyMsg);
                }
                else
                {
                    // return from sp
                    var FaultyMsg = new { isSuccessful = false, ResponseMessage = "Unable to process request", CustomerEmailAddress = Param.CustomereMailAddress, CustomerMobileNumber = Param.CustomerMobileNumber };
                    return new OkObjectResult(FaultyMsg);
                }
            }
            catch (Exception ex)
            {

                return BadRequest();
            }
        }

        #endregion

        #region Login API
        //[Route("Login")]
        [HttpGet]
        public async Task<IActionResult> Login(string Token)
        {
            UserBL objBL = new UserBL();
            bool result = await objBL.ValidateFaceBookToken(Token);
            return BadRequest();
        }

        #endregion

        #region Account Login API        
        [HttpPost]
        public async Task<IActionResult> Login(UserBO Param)
        {
            return null;
        }

        #endregion
    }
}
