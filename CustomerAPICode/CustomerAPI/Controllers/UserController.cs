﻿using System;
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
using CustomerCommon;
using logginglibrary;
using CustomerBO.Tailor;
using CustomerBL.Tailor;
using CustomerBO.Promotions;
using CustomerBO.Device;
using CustomerBL.Device;
using CustomerBO.TopDress;
using CustomerBL.Dress;
using CustomerBO.Measurements;
using CustomerBL.Measurements;
using CustomerBO.SaveAddress;
using CustomerBL.SaveAddress;
using CustomerBL.Categories;
using CustomerBO.Categories;
using CustomerBO.Template;
using CustomerBL.Template;
using CustomerBO.PreOrder;
using CustomerBL.PreOrder;

namespace CustomerAPI.Controllers
{
    // Db Password is : K@2Te@m123
    //[Route("api/[controller]")]
    //[ApiController]
   
    [Route("api/[controller]/[action]/{id?}")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private ILog logger;

        public UserController(ILog logger)
        {
            this.logger = logger;
        }

        #region Account Creation API
        //[Route("AddCustomer")]
        [HttpPost]
        public async Task<IActionResult> AddCustomer(Root Param)
        {
            try
            {
                if (Param == null)
                {
                    return BadRequest();
                }
                UserBL objBL = new UserBL(logger);
                int result = await objBL.AddUser(Param.userdata);
                if (result > 0)
                {
                    //var SuccessMsg = new { isSuccessful = true, ResponseMessage = "Profile created successfully !", CustomerEmailAddress = HttpStatusCode.OK, Result = "Saved Sucessfully" };
                    var SuccessMsg = new { issuccessful = true, responsemessage = "Profile created successfully !", customeremailaddress = Param.userdata.customeremailaddress, customermobilenumber = Param.userdata.customermobilenumber, customerid = result };
                    return new OkObjectResult(SuccessMsg);
                }
                else if (result == -2)
                {
                    //validation failed for request object
                    var FaultyMsg = new { issuccessful = false, responsemessage = "One or more fields missing", customeremailaddress = Param.userdata.customeremailaddress, customermobilenumber = Param.userdata.customermobilenumber, customerid = result };
                    return new OkObjectResult(FaultyMsg);
                }
                else if (result == -3)
                {
                    // -3 : bad request object
                    var FaultyMsg = new { issuccessful = false, responsemessage = "Bad Request !", customeremailaddress = Param.userdata.customeremailaddress, customermobilenumber = Param.userdata.customermobilenumber, customerid = result };
                    return new OkObjectResult(FaultyMsg);
                }
                else if (result == -4)
                {
                    // -4 : Exception in code
                    var FaultyMsg = new { issuccessful = false, responsemessage = "Error occured while processing", customeremailaddress = Param.userdata.customeremailaddress, customermobilenumber = Param.userdata.customermobilenumber, customerid = result };
                    return new OkObjectResult(FaultyMsg);
                }
                else if (result == -5)
                {
                    // -5 : Profile already exists ( coming from sp )
                    var FaultyMsg = new { issuccessful = false, responsemessage = "Profile already exists against email", customeremailaddress = Param.userdata.customeremailaddress, customermobilenumber = Param.userdata.customermobilenumber, customerid = result };
                    return new OkObjectResult(FaultyMsg);
                }
                else
                {
                    // return from sp
                    var FaultyMsg = new { issuccessful = false, responsemessage = "Unable to process request", customeremailaddress = Param.userdata.customeremailaddress, customermobilenumber = Param.userdata.customermobilenumber, customerid = result };
                    return new OkObjectResult(FaultyMsg);
                }
            }
            catch (Exception ex)
            {
                logger.Error(LoggingRequest.CreateErrorMsg("User Controller", "AddCustomer", DateTime.Now.ToString(), ex.ToString()));
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
                if (Param == null)
                {
                    return BadRequest();
                }
                UserBL objBL = new UserBL(logger);
                var result = await objBL.Login(Param);
                int respone = result.Item1;

                var activityResult = objBL.CreateActivityObject(Param.logindata);
                if (respone > 0)
                {
                    if (activityResult != null)
                    {
                        activityResult.customerID = respone;
                        activityResult.LoginSucess = true;
                        activityResult.SessionToken = result.Item4;
                        LoginResponseLog objLogin = new LoginResponseLog(logger);
                        objLogin.LoginResponse(activityResult);
                    }
                }
                else
                {
                    if (activityResult != null)
                    {
                        activityResult.customerID = respone;
                        activityResult.LoginSucess = false;
                        activityResult.SessionToken = result.Item4;
                        LoginResponseLog objlogin = new LoginResponseLog(logger);
                        objlogin.LoginResponse(activityResult);
                    }
                }

                if (respone > 0)
                {
                    // true;
                    string firstname = "", lastname = "";
                    if (Param.logindata.authenticationmedium.ToLower() == "custom")
                    {
                        firstname = result.Item2;
                        lastname = result.Item3;
                        string _msg = "";
                        if(result.Item5.loginresponsecode == 2)
                        {
                            _msg = "New device detected. Otp sent in email.";
                            var SuccessMsg = new
                            {
                                issuccessful = true,
                                loginresponsecode = result.Item5.loginresponsecode,
                                customerid = respone,
                                customerfirstname = firstname,
                                customerlastname = lastname,
                                responsemessage = _msg,
                                sesssiontoken = result.Item4, //Guid.NewGuid()
                                userdata = (string) null
                            };
                            return new OkObjectResult(SuccessMsg);
                        }
                        else
                        {
                            _msg = "Login successfully !";
                            var SuccessMsg = new
                            {
                                issuccessful = true,
                                loginresponsecode = result.Item5.loginresponsecode,
                                customerid = respone,
                                customerfirstname = firstname,
                                customerlastname = lastname,
                                responsemessage = _msg,
                                sesssiontoken = result.Item4, //Guid.NewGuid()
                                userdata = result.Item5.userdata
                            };
                            return new OkObjectResult(SuccessMsg);
                        }
                    }
                    else
                    {
                        firstname = Param.logindata.userfirstname;
                        lastname = Param.logindata.userlastname;
                        var SuccessMsg = new
                        {
                            issuccessful = true,
                            customerid = respone,
                            customerfirstname = firstname,
                            customerlastname = lastname,
                            responsemessage = "Login successfully !",
                            sesssiontoken = result.Item4 //Guid.NewGuid()
                        };
                        return new OkObjectResult(SuccessMsg);
                    }
                }
                else if (respone == -1)
                {
                    if (Param.logindata.authenticationmedium.ToLower() == "custom")
                    {
                        var FaultyMsg = new
                        {
                            issuccessful = false,
                            customerid = respone,
                            customerfirstname = Param.logindata.userfirstname,
                            customerlastname = Param.logindata.userlastname,
                            responsemessage = "Login Failed.",
                            sesssiontoken = ""
                        };
                        return new OkObjectResult(FaultyMsg);
                    }
                    else
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
                logger.Error(LoggingRequest.CreateErrorMsg("User Controller", "Login", DateTime.Now.ToString(), ex.ToString()));
                return BadRequest();
            }
        }

        #endregion

        #region Update API

        [HttpPost]
        public async Task<IActionResult> UpdateCustomer(Root Param)
        {
            try
            {
                if (Param == null)
                {
                    return BadRequest();
                }
                UserBL objBL = new UserBL(logger);
                var result = await objBL.UpdateCustomer(Param.userdata);
                if (result > 0)
                {
                    var SuccessMsg = new { issuccessful = true, responsemessage = "Profile Updated successfully !", customeremailaddress = Param.userdata.customeremailaddress, customermobilenumber = Param.userdata.customermobilenumber, customerid = Param.userdata.customerid, sessiontoken = Param.userdata.sessiontoken };
                    return new OkObjectResult(SuccessMsg);
                }
                else if (result == -2)
                {
                    //validation failed for request object
                    var FaultyMsg = new { issuccessful = false, responsemessage = "One or more fields missing", customeremailaddress = Param.userdata.customeremailaddress, customermobilenumber = Param.userdata.customermobilenumber, customerid = result, sessiontoken = Param.userdata.sessiontoken };
                    return new OkObjectResult(FaultyMsg);
                }
                else if (result == -3)
                {
                    // -3 : bad request object
                    var FaultyMsg = new { issuccessful = false, responsemessage = "Bad Request !", customeremailaddress = Param.userdata.customeremailaddress, customermobilenumber = Param.userdata.customermobilenumber, customerid = result, sessiontoken = Param.userdata.sessiontoken };
                    return new OkObjectResult(FaultyMsg);
                }
                else if (result == -4)
                {
                    // -4 : Exception in code
                    var FaultyMsg = new { issuccessful = false, responsemessage = "Error occured while processing", customeremailaddress = Param.userdata.customeremailaddress, customermobilenumber = Param.userdata.customermobilenumber, customerid = result, sessiontoken = Param.userdata.sessiontoken };
                    return new OkObjectResult(FaultyMsg);
                }
                else
                {
                    // return from sp
                    var FaultyMsg = new { issuccessful = false, responsemessage = "Unable to process request", customeremailaddress = Param.userdata.customeremailaddress, customermobilenumber = Param.userdata.customermobilenumber, customerid = result, sessiontoken = Param.userdata.sessiontoken };
                    return new OkObjectResult(FaultyMsg);
                }

            }
            catch (Exception ex)
            {
                logger.Error(LoggingRequest.CreateErrorMsg("User Controller", "UpdateCustomer", DateTime.Now.ToString(), ex.ToString()));
                return BadRequest();
            }
        }

        #endregion

        #region Forgetpassword 

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgotPasswordcontainer Param)
        {
            try
            {
                if (Param == null || Param.userdata == null || string.IsNullOrEmpty(Param.userdata.customeremailaddress))
                {
                    return BadRequest();
                }

                UserBL objBL = new UserBL(logger);
                var result = await objBL.ForgetPasswordBL(Param.userdata);
                if (result != null)
                {
                    if (result.Item1 > 0)
                    {
                        var SuccessMsg = new { issuccessful = true, responsemessage = "Token emailed successfully!", uniquetoken = result.Item2 };
                        return new OkObjectResult(SuccessMsg);
                    }
                    else if (result.Item1 == -2)
                    {
                        var FaultyMsg = new { issuccessful = false, responsemessage = "Email doesn't exist.", uniquetoken = result.Item2 };
                        return new OkObjectResult(FaultyMsg);
                    }
                    else if (result.Item1 == -3)
                    {
                        var FaultyMsg = new { issuccessful = false, responsemessage = "Param missing", uniquetoken = result.Item2 };
                        return new OkObjectResult(FaultyMsg);
                    }
                    else if (result.Item1 == -4)
                    {
                        var FaultyMsg = new { issuccessful = false, responsemessage = "Error occured while processing..", uniquetoken = result.Item2 };
                        return new OkObjectResult(FaultyMsg);
                    }
                    else if (result.Item1 == -5)
                    {
                        var FaultyMsg = new { issuccessful = false, responsemessage = "Failed to send email !", uniquetoken = result.Item2 };
                        return new OkObjectResult(FaultyMsg);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                logger.Error(LoggingRequest.CreateErrorMsg("User Controller", "ForgetPassword", DateTime.Now.ToString(), ex.ToString()));
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ForgotPasswordcontainer Param)
        {
            try
            {
                if (Param == null || Param.userdata == null || string.IsNullOrEmpty(Param.userdata.customeremailaddress))
                {
                    return BadRequest();
                }
                UserBL objBL = new UserBL(logger);
                var result = await objBL.ResetPasswordBL(Param.userdata);
                if (result > 0)
                {
                    var SuccessMsg = new { issuccessful = true, responsemessage = "password changed successfully!" };
                    return new OkObjectResult(SuccessMsg);
                }
                else
                {
                    var FaultyMsg = new { issuccessful = false, responsemessage = "Unable to process process request !" };
                    return new OkObjectResult(FaultyMsg);
                }
            }
            catch (Exception ex)
            {
                logger.Error(LoggingRequest.CreateErrorMsg("User Controller", "ResetPassword", DateTime.Now.ToString(), ex.ToString()));
                return BadRequest();
            }
        }

        #endregion

        #region UpdatePassword API

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordContainer Param)
        {
            try
            {
                if (Param == null || Param.userdata == null)
                {
                    return BadRequest();
                }
                UserBL objBL = new UserBL(logger);
                var result = await objBL.ChangePassword(Param.userdata);
                if (result.Item1 > 0)
                {
                    var SuccessMsg = new { issuccessful = true, responsemessage = "password changed successfully!", customeremailaddress = result.Item2, customerid = Param.userdata.customerid };
                    return new OkObjectResult(SuccessMsg);
                }
                else if (result.Item1 == -1)
                {
                    var FaultyMsg = new { issuccessful = false, responsemessage = "Unable to process process request !", customeremailaddress = "", customerid = Param.userdata.customerid };
                    return new OkObjectResult(FaultyMsg);
                }
                else if (result.Item1 == -2)
                {
                    var FaultyMsg = new { issuccessful = false, responsemessage = "Required parameters are missing.", customeremailaddress = "", customerid = Param.userdata.customerid };
                    return new OkObjectResult(FaultyMsg);
                }
                else if (result.Item1 == -3)
                {
                    var FaultyMsg = new { issuccessful = false, responsemessage = "Error occured while processing request.", customeremailaddress = "", customerid = Param.userdata.customerid };
                    return new OkObjectResult(FaultyMsg);
                }
                else
                {
                    var FaultyMsg = new { issuccessful = false, responsemessage = "Unable to process process request !" };
                    return new OkObjectResult(FaultyMsg);
                }
            }
            catch (Exception ex)
            {
                logger.Error(LoggingRequest.CreateErrorMsg("User Controller", "ChangePassword", DateTime.Now.ToString(), ex.ToString()));
                return BadRequest();
            }
        }

        #endregion

        #region Otp Verification API 

        public async Task<IActionResult> OTPVerification(OTPContainerBO Param)
        {
            try
            {
                if (Param == null || Param.userdata == null)
                {
                    return BadRequest();
                }
                UserBL objbl = new UserBL(logger);
                var result = await objbl.OTPHandling(Param.userdata);
                if (result != null && result.issuccessfullCode > 0)
                {
                    var SuccessMsg = new { issuccessfullCode = result.issuccessfullCode, responsemessage = "OTP sent successfully!" };
                    return new OkObjectResult(SuccessMsg);
                }
                else if(result != null && result.issuccessfullCode < 1)
                {
                    var SuccessMsg = new { issuccessfullCode = result.issuccessfullCode, responsemessage = "Failed to send OTP!" };
                    return new OkObjectResult(SuccessMsg);
                   
                }
                else
                {
                    var FaultyMsg = new { issuccessfullCode = result.issuccessfullCode, responsemessage = "Error occured" };
                    return new OkObjectResult(FaultyMsg);
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        #endregion

        #region TailorListing and promotion Listing API
        
        public async Task<IActionResult> FetchTailorList(TailorBOContainer Param)
        {
            try
            {
                if(Param == null || Param.userdata == null || string.IsNullOrEmpty(Param.userdata.sessiontoken) || string.IsNullOrEmpty(Param.userdata.deviceid) ) 
                {
                    return BadRequest();
                }

                UserBL objuserBL = new UserBL(logger);
                bool isVerify = await objuserBL.VerifySessionToken(Param.userdata.sessiontoken, Param.userdata.deviceid);
                if(!isVerify)
                {
                    // return faulty msg 
                    var objData = new { issuccessfull = false, responsemessage = "Token verification failed", tailorlist = (string) null };
                    //var FaultyMsg = new { reponse = objData };
                    return new OkObjectResult(objData);
                }
                else
                {
                    TailorBL objTailor = new TailorBL(logger);
                    var result = await objTailor.GetTailorData(Param);
                    if(result != null)
                    {
                        var objData = new { issuccessfull = true, responsemessage = "successfully fetch list", tailorlist = result };
                        //var SuccessObj = new { reponse = objData };
                        return new OkObjectResult(objData);
                    }
                    else
                    {
                        var objData = new { issuccessfull = false, responsemessage = "Error while fetching list", tailorlist = (string)null };
                        //var FaultyMsg = new { reponse = objData };
                        return new OkObjectResult(objData);
                    }
                }    
                
            }
            catch (Exception ex)
            {
               return BadRequest();
            }
        }

        public async Task<IActionResult> FetchPromotionList(PromotionBOcontainer Param)
        {
            try
            {
                if (Param == null || Param.userdata == null || string.IsNullOrEmpty(Param.userdata.sessiontoken) || string.IsNullOrEmpty(Param.userdata.deviceid))
                {
                    return BadRequest();
                }

                UserBL objuserBL = new UserBL(logger);
                bool isVerify = await objuserBL.VerifySessionToken(Param.userdata.sessiontoken, Param.userdata.deviceid);
                if (!isVerify)
                {
                    // return faulty msg 
                    var objData = new { issuccessfull = false, responsemessage = "Token verification failed", promotionlist = (string)null };
                    //var FaultyMsg = new { reponse = objData };
                    return new OkObjectResult(objData);
                }
                else
                {
                    TailorBL objTailor = new TailorBL(logger);
                    var result = await objTailor.GetPromotionData(Param);
                    if (result != null)
                    {
                        var objData = new { issuccessfull = true, responsemessage = "successfully fetch list", promotionlist = result };
                        //var SuccessObj = new { reponse = objData };
                        return new OkObjectResult(objData);
                    }
                    else
                    {
                        var objData = new { issuccessfull = false, responsemessage = "Error while fetching list", promotionlist = (string)null };
                        //var FaultyMsg = new { reponse = objData };
                        return new OkObjectResult(objData);
                    }
                }

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }


        // First the backend will check sessiontoken and deviceid existance in customerDB in customerloginactivities table.
        // Then the backend will call the tailorapi and fetch tailor listings.
        // if the searchtype = 'top' then call the tailor listing api with searchtype = 'top'
        // if the searchtype = 'nearest' then call the tailor listing api with searchtype = 'nearest'
        // Get the JSON from the tailor api response and generate a response for the app       

        //Request:

        //     {
        //"userdata" :
        //	{
        //		"customerid" : "",					
        //		"sessiontoken" : "",
        //		"city" : "", --> <individual city name> or 'all'
        //		"searchtype" : "", --> 'top' or 'nearest'
        //		“devicelatitude” : “”,
        //		“devicelongitude” : “”,
        //		“listingcount” : “”,
        //		"deviceid" : "",
        //		"devicetype" : ""	
        //	}
        //     }


        //      Response:

        //{
        //	"reponse" :
        //		{
        //		"issuccessfull" : "true/false",
        //              “responsemessage” : “”,				
        //		"tailorlist": [
        //        					{
        //          				"tailorid": "",
        //          				"tailorcompanytitle": "",
        //          				"tailorcompanyimage": "<base 64 string>",
        //			        "tailorReview" : "" <-- integer number between 1 and 5

        //                            },
        //        					{
        //          				"tailorid": "",
        //          				"tailorcompanytitle": "",
        //          				"tailorcompanyimage": "<base 64 string>",
        //			        "tailorReview" : "" <-- integer number between 1 and 5
        //        					},
        //  						]
        //		}
        //}




        #endregion

        #region PromotionListing

        // For this Api. you need to first check the sessiontoken and deviceid existance in customerDb in customerloginactivities table.
        // Secondly you need to call the tailorapi promotionlisting
        // Get the json from the promotionlisting api and generate a response for the app

        //Request:
        // {
        // "userdata" : 
        //  {
        //   "customerid" : "",
        //   "sessiontoken" : "",
        //   "city" : "",
        //   "deviceid" : "",
        //   "devicetype" : "",
        //   “listingcount” : “”	

        //  }
        //    }

        //Response:
        // {	
        // "response" : 
        //  {
        //   "issuccessful" : true/false,
        //   "responsemessage" : "",
        //   "promotionlist" :
        //   [
        //    {
        //    "promotionid" : "",
        //    "promotionenddate" : "",
        //    "promotionimage" : "<base 64 string>",
        //    "productid" : "",
        //    "tailorid" : ""

        //                },
        //    {
        //    "promotionid" : "",
        //    "promotionenddate" : "",
        //    "promotionimage" : "<base 64 string>",
        //    "productid" : "",
        //    "tailorid" : ""
        //    }					     
        //   ]	
        //  }			
        // }

        #endregion      

        #region privatenearestdistancecalculatingmethods
        private double distance(double lat1, double lon1, double lat2, double lon2, string unit)
        {
            if ((lat1 == lat2) && (lon1 == lon2))
            {
                return 0;
            }
            else
            {
                double theta = lon1 - lon2;
                double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
                dist = Math.Acos(dist);
                dist = rad2deg(dist);
                dist = dist * 60 * 1.1515;
                if (unit == "K")
                {
                    dist = dist * 1.609344;
                }
                else if (unit == "N")
                {
                    dist = dist * 0.8684;
                }
                return (dist);
            }
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts decimal degrees to radians             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        private double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts radians to decimal degrees             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        private double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
        #endregion


        #region Authorizd device

        public IActionResult DeviceVerification(DeviceVerificationBOContainer Param)
        {
            try
            {
                if (Param == null || Param.userdata == null || string.IsNullOrEmpty(Param.userdata.deviceid))
                {
                    return BadRequest();
                }

                DeviceBL objBL = new DeviceBL(logger);
                var response = objBL.VerifyDevice(Param.userdata);
                if (response != null)
                {
                    if (response.responsecode > 0)
                    {
                        var objData = new { issuccessfull = true, responsemessage = "verified device" };
                        return new OkObjectResult(objData);
                    }
                    else
                    {
                        var objData = new { issuccessfull = false, responsemessage = "UnAuthentic Device" };
                        return new OkObjectResult(objData);
                    }

                }
                else
                {
                    var objData = new { issuccessfull = false, responsemessage = "UnAuthentic Device" };
                    return new OkObjectResult(objData);
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        #endregion

        #region Top Dress API

        public async Task<IActionResult> FetchTopDress(TopDressBOContainer Param)
        {
            try
            {
                if(Param == null || Param.userdata == null)
                {
                    return BadRequest();
                }
                DressBL objBL = new DressBL(logger);
                var result = await objBL.GetTopDress(Param.userdata);
                if(result != null)
                {
                    if(result.Item2 > 0 || result.Item2 == -2)
                    {
                        var objData = new { issuccessfull = true, topdresslist = result.Item1, responsemessage = "Successfully Fetch" };
                        return new OkObjectResult(objData);
                    }
                    else if(result.Item2 == -1)
                    {
                        var objData = new { issuccessfull = false, topdresslist = (string) null, responsemessage = "Device Authentication failed" };
                        return new OkObjectResult(objData);
                    }
                    else
                    {
                        var objData = new { issuccessfull = false, topdresslist = (string)null, responsemessage = "Failed to fetch data..." };
                        return new OkObjectResult(objData);
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                logger.Error(LoggingRequest.CreateErrorMsg("UserController", "FetchTopDress", DateTime.Now.ToString(), ex.ToString()));
                return BadRequest();
            }
        }

        #endregion

        #region Saved Measurements API

        public async Task<IActionResult> GetSavedMeasurement(MeasurementBOContainer Param)
        {
            try
            {
                if(Param == null || Param.userdata == null)
                {
                    return BadRequest();
                }

                MeasurementBL objBL = new MeasurementBL(logger);
                var result = await objBL.GetMeasurementBL(Param.userdata);
                if (result != null)
                {
                    if (result.Item2 > 0 || result.Item2 == -2)
                    {
                        var objData = new { issuccessfull = true, measurementlist = result.Item1, responsemessage = "Successfully Fetch" };
                        return new OkObjectResult(objData);
                    }
                    else if (result.Item2 == -1)
                    {
                        var objData = new { issuccessfull = false, measurementlist = (string)null, responsemessage = "Device Authentication failed" };
                        return new OkObjectResult(objData);
                    }
                    else
                    {
                        var objData = new { issuccessfull = false, measurementlist = (string)null, responsemessage = "Failed to fetch data..." };
                        return new OkObjectResult(objData);
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                logger.Error(LoggingRequest.CreateErrorMsg("UserController", "GetSavedMeasurement", DateTime.Now.ToString(), ex.ToString()));
                return BadRequest();
            }
        }

        #endregion

        #region Save Address API

        public async Task<IActionResult> GetSavedAddress(SaveAddressBOContainer Param)
        {
            try
            {
                if (Param == null || Param.userdata == null)
                {
                    return BadRequest();
                }

                SaveAddressBL objBL = new SaveAddressBL(logger);
                var result = await objBL.GetSaveAddressBL(Param.userdata);
                if (result != null)
                {
                    if (result.Item2 > 0 || result.Item2 == -2)
                    {
                        var objData = new { issuccessfull = true, measurementlist = result.Item1, responsemessage = "Successfully Fetch" };
                        return new OkObjectResult(objData);
                    }
                    else if (result.Item2 == -1)
                    {
                        var objData = new { issuccessfull = false, measurementlist = (string)null, responsemessage = "Device Authentication failed" };
                        return new OkObjectResult(objData);
                    }
                    else
                    {
                        var objData = new { issuccessfull = false, measurementlist = (string)null, responsemessage = "Failed to fetch data..." };
                        return new OkObjectResult(objData);
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                logger.Error(LoggingRequest.CreateErrorMsg("UserController", "GetSavedMeasurement", DateTime.Now.ToString(), ex.ToString()));
                return BadRequest();
            }
        }


        #endregion

        #region Get Categoires 

        public async Task<IActionResult> GetDressCategories(CategoriesBOContainer Param)
        {
            try
            {
                if(Param == null || Param.userdata == null)
                {
                    return BadRequest();
                }

                UserBL objuserBL = new UserBL(logger);
                bool isVerify = await objuserBL.VerifySessionToken(Param.userdata.sessiontoken, Param.userdata.deviceid);
                if (!isVerify)
                {
                    var objData = new { issuccessfull = false, responsemessage = "Token verification failed", dresscategorieslist = (string)null };
                    return new OkObjectResult(objData);
                }
                else
                {
                    CategoriesBL objBL = new CategoriesBL(logger);
                    var result = await objBL.GetDressCategoryBL();
                    if (result != null)
                    {
                        var objData = new { issuccessfull = true, responsemessage = "successfully fetch list", dresscategorieslist = result };
                        return new OkObjectResult(objData);
                    }
                    else
                    {
                        var objData = new { issuccessfull = false, responsemessage = "Error while fetching list", dresscategorieslist = (string)null };
                        return new OkObjectResult(objData);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(LoggingRequest.CreateErrorMsg("UserController", "GetDressCategories", DateTime.Now.ToString(), ex.ToString()));
                return BadRequest();
            }
        }

        #endregion

        #region Get Measurement Template

        public async Task<IActionResult> GetMeasurementTemplate(MeasurementTemplateBOContainer Param)
        {
            try
            {
                if (Param == null || Param.userdata == null)
                {
                    return BadRequest();
                }

                UserBL objuserBL = new UserBL(logger);
                bool isVerify = await objuserBL.VerifySessionToken(Param.userdata.sessiontoken, Param.userdata.deviceid);
                if (!isVerify)
                {
                    var objData = new { issuccessfull = false, responsemessage = "Token verification failed", dresscategorieslist = (string)null };
                    return new OkObjectResult(objData);
                }
                else
                {
                    MeasurementTemplateBL objBL = new MeasurementTemplateBL(logger);
                    var result = await objBL.GetMeasurementTemplateBL(Param);
                    if (result != null)
                    {
                        var objData = new { issuccessfull = true, responsemessage = "successfully fetch list", dresscategorieslist = result };
                        return new OkObjectResult(objData);
                    }
                    else
                    {
                        var objData = new { issuccessfull = false, responsemessage = "Error while fetching list", dresscategorieslist = (string)null };
                        return new OkObjectResult(objData);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(LoggingRequest.CreateErrorMsg("UserController", "GetDressCategories", DateTime.Now.ToString(), ex.ToString()));
                return BadRequest();
            }
        }


        #endregion

        #region Order Management 

        public async Task<IActionResult> PreOrder(PreOrderBO Param)
        {
            try
            {
                if (Param == null || Param.userdata == null)
                {
                    return BadRequest();
                }

                PreOrderBL objBL = new PreOrderBL(logger);
                var result = await objBL.FuncPreOrderBL(Param);
                if (result.Item1 > 0)
                {
                    if(Param.userdata.preorder.status.ToLower() == "cart")
                    {
                        var objData = new { issuccessfull = true, responsemessage = "Order updated in Cart." };
                        return new OkObjectResult(objData);
                    }
                    else
                    {
                        var objData = new { issuccessfull = true, responsemessage = "Preorder created successfully." };
                        return new OkObjectResult(objData);
                    }
                }
                else
                {
                    var objData = new { issuccessfull = false, responsemessage = result.Item2 };
                    return new OkObjectResult(objData);
                }
                
            }
            catch (Exception ex)
            {
                logger.Error(LoggingRequest.CreateErrorMsg("UserController", "PreOrder", DateTime.Now.ToString(), ex.ToString()));
                return BadRequest();
            }
        }



        #endregion


        #region Create Order API 

        public async Task<IActionResult> CreateOrder(PreOrderBO Param)
        {
            try
            {
                if (Param == null || Param.userdata == null)
                {
                    return BadRequest();
                }

                #region Session token Check

                UserBL objuserBL = new UserBL(logger);
                bool isVerify = await objuserBL.VerifySessionToken(Param.userdata.preorder.sessiontoken, Param.userdata.preorder.deviceid);
                if (!isVerify)
                {
                    var objData = new { issuccessfull = false, responsemessage = "Token verification failed", orderid = (string)null };
                    return new OkObjectResult(objData);
                }
                else
                {
                    PreOrderBL objbl = new PreOrderBL(logger);
                    var response = await objbl.CallCreateOrderAPI(Param);
                    if(response.Item1)
                    {
                        var objData = new { issuccessfull = true, responsemessage = "Order created successfully.", ordernumber = response.Item2 };
                        return new OkObjectResult(objData);
                    }
                    else
                    {
                        var objData = new { issuccessfull = false, responsemessage = response.Item3, ordernumber = "-1" };
                        return new OkObjectResult(objData);
                    }
                }

                #endregion

            }
            catch (Exception ex)
            {
                logger.Error(LoggingRequest.CreateErrorMsg("UserController", "CreatOrder", DateTime.Now.ToString(), ex.ToString()));
                return BadRequest();
            }
           
        }

        #endregion



    }




}
