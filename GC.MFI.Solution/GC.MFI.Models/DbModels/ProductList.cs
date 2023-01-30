using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Models.DbModels
{
    public class ProductList
    {
        public short ProductID { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal? MinLimit { get; set; }
        public decimal? MaxLimit { get; set; }
    }

    public class ProductListForSavingSummary
    {
        public short ProductID { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal? MinLimit { get; set; }
        public decimal? MaxLimit { get; set; }
    }
}
