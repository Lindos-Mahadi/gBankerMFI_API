using GC.MFI.Models.DbModels.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Models.DbModels
{
    public class FcmConnectionTable : LegacyDbModelBase, ILegacyDbModelBase
    {
        public long Id { get; set; }
        public long? MemberId { get; set; }
        public string FcmToken { get; set; }
        public string DeviceId { get; set; }
    }
}
