using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerBO.Categories
{
    public class CategoriesBOResponse
    {
        public int dresscategoryid { get; set; }
        public string categoryname { get; set; }
        public string gender { get; set; }
    }

    public class CategoriesObjResponse
    {
        public bool issuccessfull { get; set; }
        public string responsemessage { get; set; }
        public List<CategoriesBOResponse> dresscategorieslist { get; set; }

    }

}
