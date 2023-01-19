﻿using GC.MFI.Models.DbModels.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Models.DbModels
{
    [Table("MemberPassBookRegister")]
    public class MemberPassBookRegister : LegacyDbModelBase, ILegacyDbModelBase
    {
        public long MemberPassBookRegisterID { get; set; }

        [Required]

        public long MemberPassBookNO { get; set; }


        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long MemberID { get; set; }


        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OfficeID { get; set; }

        public int CenterID { get; set; }

        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrgID { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? PassBookStartDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? PassBookCloseDate { get; set; }

        public int Status { get; set; }
        public long? LotNo { get; set; }
        public bool? IsActive { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? InActiveDate { get; set; }

        [Required]
        [StringLength(15)]
        public string CreateUser { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }
    }
}
