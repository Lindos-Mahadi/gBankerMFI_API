﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XenterSolution.Models.ViewModels;

namespace GC.MFI.Models.ViewModels
{
    public class EmailLogTableViewModel : ViewModelBase
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime SendDate { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
