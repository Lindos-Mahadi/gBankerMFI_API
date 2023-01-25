using GC.MFI.Models.DbModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Models.RequestModels
{
    public class SavingAccountModel
    {
        public int officeId { get; set; }
        public int memberId { get; set; }
        public int productId { get; set; }
        public Decimal savingsInstallment { get; set; }
        public string createUser { get; set; }
        public DateTime createDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime OpeningDate { get; set; }

        public virtual List<NomineeXPortalSavingSummary> MemberNominees { get; set; } 
    }
}
