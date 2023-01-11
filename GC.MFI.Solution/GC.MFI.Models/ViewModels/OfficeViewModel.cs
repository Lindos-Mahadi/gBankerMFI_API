using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Models.ViewModels
{
    public class OfficeViewModel
    {
        public int OfficeID { get; set; }
        public string OfficeCode { get; set; }
        public string OfficeName { get; set; }
        public Int16 OfficeLevel { get; set; }
        public string FirstLevel { get; set; }
        public string SecondLevel { get; set; }
        public string ThirdLevel { get; set; }
        public string FourthLevel { get; set; }
        public DateTime OperationStartDate { get; set; }

        public string OfficeAddress { get; set; }
        public string PostCode { get; set; }

        public int? GeoLocationID { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public bool IsActive { get; set; }

        public DateTime? InActiveDate { get; set; }

        public string CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        public int OrgID { get; set; }

        public int? InvestorID { get; set; }

        public int? UnionID { get; set; }
    }
}
