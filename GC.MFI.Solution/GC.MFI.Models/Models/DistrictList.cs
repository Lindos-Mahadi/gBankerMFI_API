using GC.MFI.Models.DbModels.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Models.Models
{
    public class DistrictList : LegacyDbModelBase, ILegacyDbModelBase
    {
        public string DistrictName { get; set; }
        public string DistrictCode { get; set; }
    }
}
