using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagmentBL.Categories;
using OrderManagmentBL.Order;
using OrderManagmentBL.Template;
using OrderManagmentBO.Order;
using OrderManagmentBO.Template;

namespace OrderManagmentAPI.Controllers
{
    [Route("api/[controller]/[action]/{id?}")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        #region Get Categoires 

        public async Task<IActionResult> GetDressCategories()
        {
            try
            {
                CategoriesBL objBL = new CategoriesBL(); //(logger);
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
            catch (Exception ex)
            {
                //logger.Error(LoggingRequest.CreateErrorMsg("UserController", "GetDressCategories", DateTime.Now.ToString(), ex.ToString()));
                //return BadRequest();
                var objData = new { issuccessfull = false, responsemessage = ex.ToString(), dresscategorieslist = (string)null };
                return new OkObjectResult(objData);
            }
        }

        #endregion


        #region Get Measurement Template 

        public async Task<IActionResult> GetMeasurementTemplate(MeasurementTemplateBOContainer Param)
        {
            try
            {
                if(Param == null || Param.userdata == null)
                {
                    return BadRequest();
                }

                int _id = -1;
                int.TryParse(Param.userdata.dresscategoryid, out _id);

                MeasurementTemplateBL objBL = new MeasurementTemplateBL(); //(logger);
                var result = await objBL.GetMeasurementTemplateBL(_id);
                if (result != null)
                {
                    var objData = new { issuccessfull = true, responsemessage = "successfully fetch list", measurementtemplatelist = result };
                    return new OkObjectResult(objData);
                }
                else
                {
                    var objData = new { issuccessfull = false, responsemessage = "Error while fetching list", measurementtemplatelist = (string)null };
                    return new OkObjectResult(objData);
                }

            }
            catch (Exception ex)
            {
                //logger.Error(LoggingRequest.CreateErrorMsg("UserController", "GetDressCategories", DateTime.Now.ToString(), ex.ToString()));
                //return BadRequest();
                var objData = new { issuccessfull = false, responsemessage = ex.ToString(), dresscategorieslist = (string)null };
                return new OkObjectResult(objData);
            }
        }

        #endregion

        #region Create Order 

        public async Task<IActionResult> CreateOrder(OrderBO Param)
        {
            try
            {
                if(Param == null && Param.userdata == null)
                {
                    return BadRequest();
                }

                OrderBL objBL = new OrderBL();
                var result = await objBL.CreateOrderBL(Param);
                var objData = new { issuccessfull = result.Item2, responsemessage = result.Item3, OrderNumber = result.Item1.ToString() };
                return new OkObjectResult(objData);
            }
            catch (Exception ex)
            {
                var objData = new { issuccessfull = false, responsemessage = ex.ToString(), OrderNumber = "-1" };
                return new OkObjectResult(objData);
            }
        }

        #endregion


    }
}
