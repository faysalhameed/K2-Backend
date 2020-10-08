using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerBO.Categories
{
    public class CategoriesBO
    {
        public string sessiontoken { get; set; }
        public string deviceid { get; set; }
    }

    public class CategoriesBOContainer
    {
        public CategoriesBO userdata { get; set; }
    }
}
