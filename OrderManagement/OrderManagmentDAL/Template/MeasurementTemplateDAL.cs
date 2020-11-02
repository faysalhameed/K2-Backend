using Microsoft.EntityFrameworkCore;
using OrderManagmentBO.Template;
using OrderManagmentDAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagmentDAL.Template
{
    public class MeasurementTemplateDAL
    {

        public async Task<Tuple<List<MeasurementTemplateBOResponse>, int, string>> GetMeasurementTemplateDAL(int id)
        {
            try
            {
                List<MeasurementTemplateBOResponse> LstResponse = new List<MeasurementTemplateBOResponse>();
                using (var dbContext = new OrderContext())
                {
                    using (var cmd = dbContext.Database.GetDbConnection().CreateCommand())
                    {
                        cmd.CommandText = "sp_Getdresscategoriesmeasurementtemplate";
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        var Param = cmd.CreateParameter();
                        Param.ParameterName = "dresscategoryid";
                        Param.Value = id;
                        cmd.Parameters.Add(Param);

                        await dbContext.Database.OpenConnectionAsync();

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    LstResponse.Add(new MeasurementTemplateBOResponse()
                                    {
                                        categoryname = reader["categoryname"].ToString(),
                                        attributename = reader["attributename"].ToString(),
                                        gender = reader["gender"].ToString(),
                                        sizemax = Convert.ToInt32(reader["sizemax"].ToString()),
                                        sizemin = Convert.ToInt32(reader["sizemin"].ToString())
                                    });
                                }
                                return new Tuple<List<MeasurementTemplateBOResponse>, int, string>(LstResponse, 1, "Success");
                            }
                            else
                            {
                                return new Tuple<List<MeasurementTemplateBOResponse>, int, string>(LstResponse, -2, "No Record Found");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //logger.Error(LogUserLogin.CreateErrorMsg("SaveAddressDAL", "GetSaveAddressDAL", DateTime.Now.ToString(), ex.ToString()));
                return new Tuple<List<MeasurementTemplateBOResponse>, int, string>(null, -3, ex.ToString());
            }
        }

    }
}
