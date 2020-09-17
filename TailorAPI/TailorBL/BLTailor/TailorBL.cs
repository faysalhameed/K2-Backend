using logginglibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorBO.BOTailor;
using TailorCommon;
using TailorDAL.DALLayerCode;

namespace TailorBL.BLTailor
{
    public class TailorBL
    {
        private ILog logger;
        public TailorBL(ILog templogger)
        {
            this.logger = templogger;
        }
        //public async Task<List<TailorBOResponse>> TailorListingBL(TailorEntityBO obj)
        //{
        //    try
        //    {
        //        TailorDALClass objDal = new TailorDALClass(logger);
        //        if(string.IsNullOrEmpty(obj.city))
        //        {
        //            obj.city = "all";
        //        }
        //        var result = await objDal.TailorListingfunction(obj.city, obj.listingcount);

        //        logger.Information("Calling Tailor Listing Data Layer Object within TailorListingBL Method inside Class TailoBL.");


        //        if (result != null && result.Count > 0)
        //        {
        //            if(obj.searchtype == "top")
        //            {
        //                return result;
        //            }
        //            else
        //            {
        //                List<TailorBOResponseSort> lstSort = new List<TailorBOResponseSort>();
        //                double _deviceLat = 0;
        //                double _deviceLong = 0;
        //                double.TryParse(obj.devicelatitude.ToString(), out _deviceLat);
        //                double.TryParse(obj.devicelongitude.ToString(), out _deviceLong);
        //                for (int i = 0; i < result.Count; i++)
        //                {
        //                    double _tempLat = 0;
        //                    double _tempLong = 0;
                            
        //                    double.TryParse(result[i].tailorlatitude, out _tempLat);
        //                    double.TryParse(result[i].tailorlongitude, out _tempLong);
        //                    double distance = Calculation.distance(_tempLat, _tempLong, _deviceLat, _deviceLong, "K");
        //                    lstSort.Add(new TailorBOResponseSort()
        //                    {
        //                        DistanceSort = distance,
        //                        tailorid = result[i].tailorid,
        //                        tailorcompanyimage = result[i].tailorcompanyimage,
        //                        tailorcompanytitle = result[i].tailorcompanytitle,
        //                        tailorlatitude = result[i].tailorlatitude,
        //                        tailorlongitude = result[i].tailorlongitude,
        //                        tailorrating = result[i].tailorrating
        //                    });

        //                    logger.Information("Nearest Item added in lstSort List in method TailorListingBL in class TailorBL");


        //                }
        //                lstSort.Sort((x, y) => x.DistanceSort.CompareTo(y.DistanceSort));
        //                result = null;
        //                result = new List<TailorBOResponse>();
        //                for (int i = 0; i < lstSort.Count; i++)
        //                {
        //                    result.Add(new TailorBOResponse()
        //                    {
        //                        tailorid = lstSort[i].tailorid,
        //                        tailorcompanyimage = lstSort[i].tailorcompanyimage,
        //                        tailorcompanytitle = lstSort[i].tailorcompanytitle,
        //                        tailorlatitude = lstSort[i].tailorlatitude,
        //                        tailorlongitude = lstSort[i].tailorlongitude,
        //                        tailorrating = lstSort[i].tailorrating
        //                    });
        //                }

        //                logger.Information("Return sorted Nearest item List with in  Business Layer Object within TailorListingBL Method inside Class TailorBL. Data = " + String.Join(";", result.Select(o => o.ToString())));

        //                return result;
        //            }
        //        }

        //        logger.Information("Nothing to return in Nearest sorted list." + String.Join(";", result.Select(o => o.ToString())));

        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error("Error Occurred in TailorListingBL methond in class TailorBL. Error = " + ex.Message);
        //        return null;
        //    }
        //}
    }
}
