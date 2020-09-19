using logginglibrary;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorBO.BOPromotion;
using TailorBO.BOTailor;
using TailorDAL.Models;

namespace TailorDAL.DALLayerCode
{
    public class TailorDALClass
    {
        private ILog logger;
        public TailorDALClass(ILog templogger)
        {
            this.logger = templogger;
        }

        public async Task<List<TailorBOResponse>> TailorListingfunction(string city, int listingCount,string Gender,string name,int agefrom , int ageto)
        {
            try
            {
                List<TailorBOResponse> lst = new List<TailorBOResponse>();
                using (var dbContext = new TailorContext(logger))
                {
                    using (var cmd = dbContext.Database.GetDbConnection().CreateCommand())
                    {
                        cmd.CommandText = "sp_RetrieveTailorListingAdvanced";
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        var cityParam = cmd.CreateParameter();
                        cityParam.ParameterName = "city";
                        cityParam.Value = city;
                        cmd.Parameters.Add(cityParam);

                        var listingParam = cmd.CreateParameter();
                        listingParam.ParameterName = "listingcount";
                        listingParam.Value = listingCount;
                        cmd.Parameters.Add(listingParam);

                        var GenderParam = cmd.CreateParameter();
                        GenderParam.ParameterName = "gender";
                        GenderParam.Value = Gender;
                        cmd.Parameters.Add(GenderParam);

                        var NameParam = cmd.CreateParameter();
                        NameParam.ParameterName = "name";
                        NameParam.Value = name;
                        cmd.Parameters.Add(NameParam);

                        var AgetoParam = cmd.CreateParameter();
                        AgetoParam.ParameterName = "ageto";
                        AgetoParam.Value = ageto;
                        cmd.Parameters.Add(AgetoParam);

                        var AgeFromParam = cmd.CreateParameter();
                        AgeFromParam.ParameterName = "agefrom";
                        AgeFromParam.Value = agefrom;
                        cmd.Parameters.Add(AgeFromParam);

                        await dbContext.Database.OpenConnectionAsync();

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    lst.Add(new TailorBOResponse()
                                    {
                                        tailorid = Convert.ToInt32(reader["Tailorid"]),
                                        tailorcompanytitle = reader["Tailorcompanytitle"].ToString(),
                                        tailorcompanyimage = reader["Tailorcompanyimage"].ToString(),
                                        tailorrating = reader["Tailorrating"].ToString(),
                                        tailorlatitude = reader["Tailorlatitude"].ToString(),
                                        tailorlongitude = reader["Tailorlongitude"].ToString()
                                    });
                                }
                            }
                        }

                        logger.Information("Returning list of tailors from tailor database by calling procedure sp_RetrieveTailorListing from TailorListingfunction method with in class TailorDALClass. Data = " + String.Join(";", lst.Select(o => o.ToString())));
                        return lst;
                    }
                }
            }
            catch (Exception ex)
            {
                //logger.Error(LogUserLogin.CreateErrorMsg("UserDAL", "SaveUser", DateTime.Now.ToString(), ex.ToString()));
                logger.Error("ERROR while fetching TAILOR LISTINGS from TailorListingfunction method with in class TailorDALClass. Error =" + ex.Message);

                return null;
                // return null; // also we can log exception through common layer }
            }
        }

        public async Task<List<TailorBOResponse>> TailorListingfunction(string city, int listingCount,string Gender)
        {
            try
            {
                List<TailorBOResponse> lst = new List<TailorBOResponse>();
                using (var dbContext = new TailorContext(logger))
                {
                    using (var cmd = dbContext.Database.GetDbConnection().CreateCommand())
                    {
                        cmd.CommandText = "sp_RetrieveTailorListingAdvanced";
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        var cityParam = cmd.CreateParameter();
                        cityParam.ParameterName = "city";
                        cityParam.Value = city;
                        cmd.Parameters.Add(cityParam);

                        var listingParam = cmd.CreateParameter();
                        listingParam.ParameterName = "listingcount";
                        listingParam.Value = listingCount;
                        cmd.Parameters.Add(listingParam);

                        var GenderParam = cmd.CreateParameter();
                        GenderParam.ParameterName = "gender";
                        GenderParam.Value = Gender;
                        cmd.Parameters.Add(GenderParam);

                        await dbContext.Database.OpenConnectionAsync();

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    lst.Add(new TailorBOResponse() { 
                                      tailorid = Convert.ToInt32(reader["Tailorid"]),
                                      tailorcompanytitle = reader["Tailorcompanytitle"].ToString(),
                                      tailorcompanyimage = reader["Tailorcompanyimage"].ToString(),
                                      tailorrating = reader["Tailorrating"].ToString(),
                                      tailorlatitude = reader["Tailorlatitude"].ToString(),
                                      tailorlongitude = reader["Tailorlongitude"].ToString()
                                    });
                                }
                            }
                        }

                        logger.Information("Returning list of tailors from tailor database by calling procedure sp_RetrieveTailorListing from TailorListingfunction method with in class TailorDALClass. Data = " + String.Join(";", lst.Select(o => o.ToString())));
                        return lst;
                    }
                }
            }
            catch (Exception ex)
            {
                //logger.Error(LogUserLogin.CreateErrorMsg("UserDAL", "SaveUser", DateTime.Now.ToString(), ex.ToString()));
                logger.Error("ERROR while fetching TAILOR LISTINGS from TailorListingfunction method with in class TailorDALClass. Error =" + ex.Message);

                return null;
                // return null; // also we can log exception through common layer }
            }
        }

        public async Task<List<PromotionBOResponse>> PromotionListingfunction(PromotionBO promotionObject)
        {
            try
            {
                List<PromotionBOResponse> lst = new List<PromotionBOResponse>();
                using (var dbContext = new TailorContext(logger))
                {
                    using (var cmd = dbContext.Database.GetDbConnection().CreateCommand())
                    {
                        cmd.CommandText = "sp_RetrievePromotionsAdvanced";
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        var cityParam = cmd.CreateParameter();
                        cityParam.ParameterName = "city";
                        cityParam.Value = promotionObject.city;
                        cmd.Parameters.Add(cityParam);

                        var listingParam = cmd.CreateParameter();
                        listingParam.ParameterName = "listingcount";
                        listingParam.Value = promotionObject.listingcount;
                        cmd.Parameters.Add(listingParam);

                        var GenderParam = cmd.CreateParameter();
                        GenderParam.ParameterName = "gendertype";
                        GenderParam.Value = promotionObject.gender;
                        cmd.Parameters.Add(GenderParam);

                        var AgefromParam = cmd.CreateParameter();
                        AgefromParam.ParameterName = "agefrom";
                        AgefromParam.Value = promotionObject.agefrom;
                        cmd.Parameters.Add(AgefromParam);

                        var AgetoParam = cmd.CreateParameter();
                        AgetoParam.ParameterName = "ageto";
                        AgetoParam.Value = promotionObject.ageto;
                        cmd.Parameters.Add(AgetoParam);

                        var PromotionsourcetypeParam = cmd.CreateParameter();
                        PromotionsourcetypeParam.ParameterName = "type";
                        PromotionsourcetypeParam.Value = promotionObject.promotionsourcetype;
                        cmd.Parameters.Add(PromotionsourcetypeParam);

                        var PagecountParam = cmd.CreateParameter();
                        PagecountParam.ParameterName = "pagecount";
                        PagecountParam.Value = promotionObject.pagecount;
                        cmd.Parameters.Add(PagecountParam);


                        await dbContext.Database.OpenConnectionAsync();

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    lst.Add(new PromotionBOResponse()
                                    {
                                        productid = Convert.ToInt32((reader["Productid"] == null) ? "-1" : reader["Productid"].ToString()),
                                        promotiondatetime = Convert.ToDateTime(reader["Pormotionenddatetime"].ToString()).ToString(),
                                        promotiongendertype = reader["Promotiongendertype"].ToString(),
                                        promotionid = Convert.ToInt32(reader["Promotionid"].ToString()),
                                        promotionimage = reader["Promotionimage"].ToString(),
                                        tailorid = Convert.ToInt32(reader["Tailorid"].ToString())
                                    });
                                }
                            }
                        }

                        logger.Information("Returning list of promotions from tailor database by calling procedure sp_RetrievePromotions from PromotionListingfunction method with in class TailorDALClass. Data = " + String.Join(";", lst.Select(o => o.ToString())));

                        return lst;
                    }
                }
            }
            catch (Exception ex)
            {
                //logger.Error(LogUserLogin.CreateErrorMsg("UserDAL", "SaveUser", DateTime.Now.ToString(), ex.ToString()));

                logger.Error("ERROR while fetching promotions from PromotionListingfunction method with in class TailorDALClass. Error =" + ex.Message);
                return null;
                // return null; // also we can log exception through common layer }
            }
        }



    }
}
