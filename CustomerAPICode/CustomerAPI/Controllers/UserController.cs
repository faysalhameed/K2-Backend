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
    // Db Password is : K@2Te@m123
    //[Route("api/[controller]")]
    //[ApiController]
    [Route("api/[controller]/[action]/{id?}")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Account Creation API
        //[Route("AddCustomer")]
        [HttpPost]
        public async Task<IActionResult> AddCustomer(Root Param) 
        {
            try
            {
                if(Param == null)
                {
                    return BadRequest();
                }
                UserBL objBL = new UserBL();
                int result = await objBL.AddUser(Param.userdata);
                if (result > 0)
                {
                    //var SuccessMsg = new { isSuccessful = true, ResponseMessage = "Profile created successfully !", CustomerEmailAddress = HttpStatusCode.OK, Result = "Saved Sucessfully" };
                    var SuccessMsg = new { issuccessful = true, responsemessage = "Profile created successfully !", customeremailaddress = Param.userdata.customeremailaddress, customermobilenumber = Param.userdata.customermobilenumber };
                    return new OkObjectResult(SuccessMsg);
                }
                else if (result == -2)
                {
                    //validation failed for request object
                    var FaultyMsg = new { issuccessful = false, responsemessage = "One or more fields missing", customeremailaddress = Param.userdata.customeremailaddress, customermobilenumber = Param.userdata.customermobilenumber };
                    return new OkObjectResult(FaultyMsg);
                }
                else if (result == -3)
                {
                    // -3 : bad request object
                    var FaultyMsg = new { issuccessful = false, responsemessage = "Bad Request !", customeremailaddress = Param.userdata.customeremailaddress, customermobilenumber = Param.userdata.customermobilenumber };
                    return new OkObjectResult(FaultyMsg);
                }
                else if (result == -4)
                {
                    // -4 : Exception in code
                    var FaultyMsg = new { issuccessful = false, responsemessage = "Error occured while processing", customeremailaddress = Param.userdata.customeremailaddress, customermobilenumber = Param.userdata.customermobilenumber };
                    return new OkObjectResult(FaultyMsg);
                }
                else if (result == -5)
                {
                    // -5 : Profile already exists ( coming from sp )
                    var FaultyMsg = new { issuccessful = false, responsemessage = "Profile already exists against email", customeremailaddress = Param.userdata.customeremailaddress, customermobilenumber = Param.userdata.customermobilenumber };
                    return new OkObjectResult(FaultyMsg);
                }
                else
                {
                    // return from sp
                    var FaultyMsg = new { issuccessful = false, responsemessage = "Unable to process request", customeremailaddress = Param.userdata.customeremailaddress, customermobilenumber = Param.userdata.customermobilenumber };
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
        [HttpPost]
        public async Task<IActionResult> Login(LoginRootBO Param)
        {
            try
            {
                if(Param == null)
                {
                    return BadRequest();
                }
                UserBL objBL = new UserBL();
                var result = await objBL.Login(Param);
                int respone = result.Item1;
                if (respone > 0)
                {
                    // true;
                    string firstname = "", lastname = "";
                    if(Param.logindata.authenticationmedium.ToLower() == "custom")
                    {
                        firstname = result.Item2;
                        lastname = result.Item3;
                    }
                    else
                    {
                        firstname = Param.logindata.userfirstname;
                        lastname = Param.logindata.userlastname;
                    }
                    var SuccessMsg = new { 
                        issuccessful = true, 
                        customerid = respone, 
                        customerfirstname = firstname,
                        customerlastname = lastname,
                        responsemessage = "Login successfully !",
                        sesssiontoken = Guid.NewGuid()
                    };
                    return new OkObjectResult(SuccessMsg);
                }
                else if (respone == -1)
                {
                    // from db side  
                    var FaultyMsg = new
                    {
                        issuccessful = false,
                        customerid = respone,
                        customerfirstname = Param.logindata.userfirstname,
                        customerlastname = Param.logindata.userlastname,
                        responsemessage = "Error Occured!",
                        sesssiontoken = ""
                    };
                    return new OkObjectResult(FaultyMsg);
                }
                else if(respone == -2)
                {
                    // all data missing 
                    var FaultyMsg = new
                    {
                        issuccessful = false,
                        customerid = respone,
                        customerfirstname = Param.logindata.userfirstname,
                        customerlastname = Param.logindata.userlastname,
                        responsemessage = "All field missings",
                        sesssiontoken = ""
                    };
                    return new OkObjectResult(FaultyMsg);
                }
                else if(respone == -3)
                {
                    // missing email or password 
                    var FaultyMsg = new
                    {
                        issuccessful = false,
                        customerid = respone,
                        customerfirstname = Param.logindata.userfirstname,
                        customerlastname = Param.logindata.userlastname,
                        responsemessage = "Email or password missing.",
                        sesssiontoken = ""
                    };
                    return new OkObjectResult(FaultyMsg);
                }
                else if(respone == -4)
                {
                    // token auth failed
                    var FaultyMsg = new
                    {
                        issuccessful = false,
                        customerid = respone,
                        customerfirstname = Param.logindata.userfirstname,
                        customerlastname = Param.logindata.userlastname,
                        responsemessage = "Token authentication failed.",
                        sesssiontoken = ""
                    };
                    return new OkObjectResult(FaultyMsg);
                }
                else if(respone == -5)
                {
                    // email or password is missing for authentication
                    var FaultyMsg = new
                    {
                        issuccessful = false,
                        customerid = respone,
                        customerfirstname = Param.logindata.userfirstname,
                        customerlastname = Param.logindata.userlastname,
                        responsemessage = "email or password is missing for authentication",
                        sesssiontoken = ""
                    };
                    return new OkObjectResult(FaultyMsg);
                }
                else if(respone == -6)
                {
                    // faulty msg return no pre defined auth medium  defined
                    var FaultyMsg = new
                    {
                        issuccessful = false,
                        customerid = respone,
                        customerfirstname = Param.logindata.userfirstname,
                        customerlastname = Param.logindata.userlastname,
                        responsemessage = "Authentication medium is missing",
                        sesssiontoken = ""
                    };
                    return new OkObjectResult(FaultyMsg);
                }
                else
                {
                    // exception 
                    var FaultyMsg = new
                    {
                        issuccessful = false,
                        customerid = respone,
                        customerfirstname = Param.logindata.userfirstname,
                        customerlastname = Param.logindata.userlastname,
                        responsemessage = "Exception occured",
                        sesssiontoken = ""
                    };
                    return new OkObjectResult(FaultyMsg);
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        #endregion

        #region Update API

        [HttpPost]
        public async Task<IActionResult> UpdateCustomer(LoginRootBO Param)
        {
            try
            {
                if (Param == null)
                {
                    return BadRequest();
                }
                UserBL objBL = new UserBL();
                var result = await objBL.UpdateCustomer(Param);
                int respone = result.Item1;
                if (respone > 0)
                {
                    // true;
                    string firstname = "", lastname = "";
                    if (Param.logindata.authenticationmedium.ToLower() == "custom")
                    {
                        firstname = result.Item2;
                        lastname = result.Item3;
                    }
                    else
                    {
                        firstname = Param.logindata.userfirstname;
                        lastname = Param.logindata.userlastname;
                    }
                    var SuccessMsg = new
                    {
                        issuccessful = true,
                        customerid = respone,
                        customerfirstname = firstname,
                        customerlastname = lastname,
                        responsemessage = "Login successfully !",
                        sesssiontoken = Guid.NewGuid()
                    };
                    return new OkObjectResult(SuccessMsg);
                }
                else if (respone == -1)
                {
                    // from db side  
                    var FaultyMsg = new
                    {
                        issuccessful = false,
                        customerid = respone,
                        customerfirstname = Param.logindata.userfirstname,
                        customerlastname = Param.logindata.userlastname,
                        responsemessage = "Error Occured!",
                        sesssiontoken = ""
                    };
                    return new OkObjectResult(FaultyMsg);
                }
                else if (respone == -2)
                {
                    // all data missing 
                    var FaultyMsg = new
                    {
                        issuccessful = false,
                        customerid = respone,
                        customerfirstname = Param.logindata.userfirstname,
                        customerlastname = Param.logindata.userlastname,
                        responsemessage = "All field missings",
                        sesssiontoken = ""
                    };
                    return new OkObjectResult(FaultyMsg);
                }
                else if (respone == -3)
                {
                    // missing email or password 
                    var FaultyMsg = new
                    {
                        issuccessful = false,
                        customerid = respone,
                        customerfirstname = Param.logindata.userfirstname,
                        customerlastname = Param.logindata.userlastname,
                        responsemessage = "Email or password missing.",
                        sesssiontoken = ""
                    };
                    return new OkObjectResult(FaultyMsg);
                }
                else if (respone == -4)
                {
                    // token auth failed
                    var FaultyMsg = new
                    {
                        issuccessful = false,
                        customerid = respone,
                        customerfirstname = Param.logindata.userfirstname,
                        customerlastname = Param.logindata.userlastname,
                        responsemessage = "Token authentication failed.",
                        sesssiontoken = ""
                    };
                    return new OkObjectResult(FaultyMsg);
                }
                else if (respone == -5)
                {
                    // email or password is missing for authentication
                    var FaultyMsg = new
                    {
                        issuccessful = false,
                        customerid = respone,
                        customerfirstname = Param.logindata.userfirstname,
                        customerlastname = Param.logindata.userlastname,
                        responsemessage = "email or password is missing for authentication",
                        sesssiontoken = ""
                    };
                    return new OkObjectResult(FaultyMsg);
                }
                else if (respone == -6)
                {
                    // faulty msg return no pre defined auth medium  defined
                    var FaultyMsg = new
                    {
                        issuccessful = false,
                        customerid = respone,
                        customerfirstname = Param.logindata.userfirstname,
                        customerlastname = Param.logindata.userlastname,
                        responsemessage = "Authentication medium is missing",
                        sesssiontoken = ""
                    };
                    return new OkObjectResult(FaultyMsg);
                }
                else
                {
                    // exception 
                    var FaultyMsg = new
                    {
                        issuccessful = false,
                        customerid = respone,
                        customerfirstname = Param.logindata.userfirstname,
                        customerlastname = Param.logindata.userlastname,
                        responsemessage = "Exception occured",
                        sesssiontoken = ""
                    };
                    return new OkObjectResult(FaultyMsg);
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        #endregion



    }




}
