using OrderManagmentBO.Categories;
using OrderManagmentDAL.Category;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagmentBL.Categories
{
    public class CategoriesBL
    {
        public async Task<Tuple<List<CategoriesBOResponse>, int>> GetDressCategoryBL()
        {
            try
            {
                CategoryDAL objDal = new CategoryDAL(); //(logger);
                var result = await objDal.GetDressCategoriesDAL();
                return new Tuple<List<CategoriesBOResponse>, int>(result.Item1, result.Item2);
            }
            catch (Exception ex)
            {
                //logger.Error(LoggingRequest.CreateErrorMsg("DressBL", "GetTopDress", DateTime.Now.ToString(), ex.ToString()));
                return null;
            }
        }
    }
}
