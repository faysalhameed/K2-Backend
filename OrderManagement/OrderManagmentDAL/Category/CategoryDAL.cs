using Microsoft.EntityFrameworkCore;
using OrderManagmentBO.Categories;
using OrderManagmentDAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagmentDAL.Category
{
    public class CategoryDAL
    {
        //private ILog logger;
        //public CategoryDAL(ILog templogger)
        //{
        //    this.logger = templogger;
        //}

        public async Task<Tuple<List<CategoriesBOResponse>, int, string>> GetDressCategoriesDAL()
        {
            try
            {
                List<CategoriesBOResponse> LstResponse = new List<CategoriesBOResponse>();
                using (var dbContext = new OrderContext())
                {
                    using (var cmd = dbContext.Database.GetDbConnection().CreateCommand())
                    {
                        cmd.CommandText = "sp_Getdresscategories";
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        await dbContext.Database.OpenConnectionAsync();

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    LstResponse.Add(new CategoriesBOResponse()
                                    {
                                        categoryname = reader["Categoryname"].ToString(),
                                        dresscategoryid = Convert.ToInt32(reader["Dresscategoryid"].ToString()),
                                        gender = reader["gender"].ToString()
                                    });
                                }
                                return new Tuple<List<CategoriesBOResponse>, int, string>(LstResponse, 1, "Success");
                            }
                            else
                            {
                                return new Tuple<List<CategoriesBOResponse>, int, string>(LstResponse, -2, "No Record Found");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //logger.Error(LogUserLogin.CreateErrorMsg("SaveAddressDAL", "GetSaveAddressDAL", DateTime.Now.ToString(), ex.ToString()));
                return new Tuple<List<CategoriesBOResponse>, int, string>(null, -3, ex.ToString());
            }
        }
    
    }
}
