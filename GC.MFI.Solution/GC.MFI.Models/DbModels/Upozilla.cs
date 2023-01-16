﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GC.MFI.Models.DbModels.BaseModels;

namespace GC.MFI.Models.DbModels
{
    [Table("Upozilla")]
    public partial class Upozilla : LegacyDbModelBase, ILegacyDbModelBase
    {
        public int UpozillaID { get; set; }

        public int DistrictID { get; set; }

        [StringLength(20)]
        public string UpozillaCode { get; set; }

        [StringLength(50)]
        public string UpozillaName { get; set; }

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

