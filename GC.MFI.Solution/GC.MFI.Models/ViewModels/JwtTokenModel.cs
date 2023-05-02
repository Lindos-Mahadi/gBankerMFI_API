using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Models.ViewModels
{
    public class JwtTokenModel
    {
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public long MemberID { get; set; }
        public string OfficeId { get; set; }
        public long PortalMemberId { get; set; }

    }
}
