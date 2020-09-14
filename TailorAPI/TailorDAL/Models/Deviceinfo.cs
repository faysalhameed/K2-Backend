using System;
using System.Collections.Generic;

namespace TailorDAL.Models
{
    public partial class Deviceinfo
    {
        public int Deviceid { get; set; }
        public string Devicetype { get; set; }
        public bool? Isactive { get; set; }
    }
}
