using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Models.ViewModels
{
    public class GHealthSignUpResponse
    {
        public int success { get; set; }
        public string message { get; set; }
        public string barcode { get; set; }
    }
}
