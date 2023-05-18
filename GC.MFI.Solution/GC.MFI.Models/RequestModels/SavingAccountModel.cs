using GC.MFI.Models.DbModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public int productId { get; set; }
        [Required]
        public Decimal savingsInstallment { get; set; }
        public string createUser { get; set; }
        public DateTime createDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime OpeningDate { get; set; }
        [Required]
        public virtual List<NomineeXPortalSavingSummary> MemberNominees { get; set; } 
    }
}
