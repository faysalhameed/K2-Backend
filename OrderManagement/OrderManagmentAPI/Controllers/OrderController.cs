using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using logginglibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using NLog.Targets;
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

        private ILog logger;

        public OrderController(ILog logger)
        {
            this.logger = logger;
        }
        #region Get Categoires 

        public async Task<IActionResult> GetDressCategories()
        {
            try
            {
                CategoriesBL objBL = new CategoriesBL(); //(logger);

                logger.Information("Calling GetDressCategoryBL() method in class CategoriesBL with in class OrderController and method GetDressCategories");

                var result = await objBL.GetDressCategoryBL();
                if (result != null)
                {
                    var objData = new { issuccessfull = true, responsemessage = "successfully fetch list", dresscategorieslist = result };

                    logger.Information(System.Text.Json.JsonSerializer.Serialize(objData));

                    return new OkObjectResult(objData);
                }
                else
                {
                    var objData = new { issuccessfull = false, responsemessage = "Error while fetching list", dresscategorieslist = (string)null };

                    logger.Information(System.Text.Json.JsonSerializer.Serialize(objData));

                    return new OkObjectResult(objData);
                }
                
            }
            catch (Exception ex)
            {
                //logger.Error(LoggingRequest.CreateErrorMsg("UserController", "GetDressCategories", DateTime.Now.ToString(), ex.ToString()));
                //return BadRequest();
                var objData = new { issuccessfull = false, responsemessage = ex.ToString(), dresscategorieslist = (string)null };

                logger.Error(System.Text.Json.JsonSerializer.Serialize(objData));

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
                    logger.Information("Param object empty in method GetMeasurementTemplate in class Order Controller");
                    return BadRequest();
                }

                int _id = -1;
                int.TryParse(Param.userdata.dresscategoryid, out _id);

                MeasurementTemplateBL objBL = new MeasurementTemplateBL(); //(logger);

                logger.Information("Calling GetMeasurementTemplateBL method of class MeasurementTemplateBL from GetMeasurementTemplate in class Order Controller");

                var result = await objBL.GetMeasurementTemplateBL(_id);
                if (result != null)
                {
                    var objData = new { issuccessfull = true, responsemessage = "successfully fetch list", measurementtemplatelist = result };

                    logger.Information(System.Text.Json.JsonSerializer.Serialize(objData));

                    return new OkObjectResult(objData);
                }
                else
                {
                    var objData = new { issuccessfull = false, responsemessage = "Error while fetching list", measurementtemplatelist = (string)null };

                    logger.Information(System.Text.Json.JsonSerializer.Serialize(objData));

                    return new OkObjectResult(objData);
                }

            }
            catch (Exception ex)
            {
                //logger.Error(LoggingRequest.CreateErrorMsg("UserController", "GetDressCategories", DateTime.Now.ToString(), ex.ToString()));
                //return BadRequest();
                var objData = new { issuccessfull = false, responsemessage = ex.ToString(), dresscategorieslist = (string)null };

                logger.Error(System.Text.Json.JsonSerializer.Serialize(objData));

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

                logger.Information("Calling CreateOrderBL method of class OrderBL from CreateOrder method in class OrderController");

                var result = await objBL.CreateOrderBL(Param);

                logger.Information(System.Text.Json.JsonSerializer.Serialize(result));

                var objData = new { issuccessfull = result.Item2, responsemessage = result.Item3, OrderNumber = result.Item1.ToString() };

                logger.Information(System.Text.Json.JsonSerializer.Serialize(objData));

                return new OkObjectResult(objData);
            }
            catch (Exception ex)
            {
                var objData = new { issuccessfull = false, responsemessage = ex.ToString(), OrderNumber = "-1" };

                logger.Error(System.Text.Json.JsonSerializer.Serialize(objData));

                return new OkObjectResult(objData);
            }
        }

        #endregion


    }
}
