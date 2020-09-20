using CustomerBO.TopDress;
using CustomerDAL.Logging;
using CustomerDAL.Models;
using logginglibrary;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDAL.Dress
{
    public class DressDAL
    {
        private ILog logger;

        public DressDAL(ILog templogger)
        {
            this.logger = templogger;
        }

        public async Task<Tuple<List<TopDressBOResult>,int,string>> GetTopDressDAL(TopDressBO objParam, int age, int ageto)
        {
            try
            {
                List<TopDressBOResult> LstResponse = new List<TopDressBOResult>();
                using (var dbContext = new SilaeeContext())
                {
                    using (var cmd = dbContext.Database.GetDbConnection().CreateCommand())
                    {
                        cmd.CommandText = "sp_GetCustomerTopDresses";
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        var ListingParam = cmd.CreateParameter();
                        ListingParam.ParameterName = "listingcount";
                        ListingParam.Value = objParam.listingcount;
                        cmd.Parameters.Add(ListingParam);

                        var CityParam = cmd.CreateParameter();
                        CityParam.ParameterName = "city";
                        CityParam.Value = objParam.area;
                        cmd.Parameters.Add(CityParam);

                        var GenderParam = cmd.CreateParameter();
                        GenderParam.ParameterName = "gender";
                        GenderParam.Value = objParam.gender;
                        cmd.Parameters.Add(GenderParam);

                        var DressCategoryParam = cmd.CreateParameter();
                        DressCategoryParam.ParameterName = "dresscategory";
                        DressCategoryParam.Value = objParam.dresscategory;
                        cmd.Parameters.Add(DressCategoryParam);

                        var TypeParam = cmd.CreateParameter();
                        TypeParam.ParameterName = "type";
                        TypeParam.Value = objParam.type;
                        cmd.Parameters.Add(TypeParam);

                        var SessionTokenParam = cmd.CreateParameter();
                        SessionTokenParam.ParameterName = "sessiontoken";
                        SessionTokenParam.Value = objParam.sessiontoken;
                        cmd.Parameters.Add(SessionTokenParam);

                        var DeviceIDParam = cmd.CreateParameter();
                        DeviceIDParam.ParameterName = "deviceid";
                        DeviceIDParam.Value = objParam.deviceid;
                        cmd.Parameters.Add(DeviceIDParam);

                        var DeviceTypeParam = cmd.CreateParameter();
                        DeviceTypeParam.ParameterName = "devicetype";
                        DeviceTypeParam.Value = objParam.devicetype;
                        cmd.Parameters.Add(DeviceTypeParam);

                        var AgeFromParam = cmd.CreateParameter();
                        AgeFromParam.ParameterName = "agefrom";
                        AgeFromParam.Value = age;
                        cmd.Parameters.Add(AgeFromParam);

                        var AgeToParam = cmd.CreateParameter();
                        AgeToParam.ParameterName = "ageto";
                        AgeToParam.Value = ageto;
                        cmd.Parameters.Add(AgeToParam);

                        var PagecountParam = cmd.CreateParameter();
                        PagecountParam.ParameterName = "pagecount";
                        PagecountParam.Value = ageto;
                        cmd.Parameters.Add(PagecountParam);

                        await dbContext.Database.OpenConnectionAsync();

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {

                                    if (reader.FieldCount == 2)
                                    {
                                        return new Tuple<List<TopDressBOResult>, int, string>(null, -1, "DeviceID OR SessionToken Not Available");
                                    }
                                    else
                                        LstResponse.Add(new TopDressBOResult()
                                        {
                                            age = Convert.ToInt32(reader["Customerage"].ToString()),
                                            city = reader["Customercity"].ToString(),
                                            customername = reader["Customername"].ToString(),
                                            dislikes = Convert.ToInt32(reader["dislikes"].ToString()),
                                            dresscategory = reader["Productcategory"].ToString(),
                                            gender = reader["Customergender"].ToString(),
                                            likes = Convert.ToInt32(reader["likes"].ToString()),
                                            productimage = reader["productimage"].ToString(),
                                            ranking = Convert.ToInt32(reader["ranking"].ToString()),
                                            type = reader["Productprinttype"].ToString(),
                                            typeid = Convert.ToInt32(reader["Productprinttypeid"].ToString()),
                                            typename = reader["Productprinttypecompany"].ToString()

                                        });
                                }
                                return new Tuple<List<TopDressBOResult>, int, string>(LstResponse, 1, "Success");
                            }
                            else
                            {
                                return new Tuple<List<TopDressBOResult>, int, string>(LstResponse, -2, "No Record Found");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(LogUserLogin.CreateErrorMsg("DressDAL", "GetTopDressDAL", DateTime.Now.ToString(), ex.ToString()));
                return new Tuple<List<TopDressBOResult>, int, string>(null, -3, ex.ToString());
            }
        }


    }
}
