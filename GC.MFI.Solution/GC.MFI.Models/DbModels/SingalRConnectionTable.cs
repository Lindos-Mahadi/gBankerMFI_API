using GC.MFI.Models.DbModels.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Models.DbModels
{
    public class SingalRConnectionTable : LegacyDbModelBase, ILegacyDbModelBase
    {
        public long Id { get; set; }
        public long MemberID { get; set; }
        public string ConnID { get; set; }
    }
}
