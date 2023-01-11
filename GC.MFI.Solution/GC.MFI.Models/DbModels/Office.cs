using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Models.DbModels
{
    public class Office
    {
        public int OfficeID { get; set; }
        public string OfficeCode { get; set; }
        public string OfficeName { get; set; }
        public byte OfficeLevel { get; set; }
        public string FirstLevel { get; set; }
        public string SecondLevel { get; set; }
        public string ThirdLevel { get; set; }
        public string FourthLevel { get; set; }
        [Column(TypeName = "date")]
        public DateTime OperationStartDate { get; set; }

        [StringLength(155)]
        public string OfficeAddress { get; set; }

        [StringLength(10)]
        public string PostCode { get; set; }

        public int? GeoLocationID { get; set; }

        [StringLength(45)]
        public string Email { get; set; }

        [StringLength(35)]
        public string Phone { get; set; }

        public bool IsActive { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? InActiveDate { get; set; }

        [Required]
        [StringLength(35)]
        public string CreateUser { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }
        public int OrgID { get; set; }

        public int? InvestorID { get; set; }

        public int? UnionID { get; set; }
    }
}
