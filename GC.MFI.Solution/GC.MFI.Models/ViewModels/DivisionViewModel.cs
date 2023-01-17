using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Models.ViewModels
{
    public class DivisionViewModel
    {
        public Byte DivisionID { get; set; }
        [StringLength(50)]
        public string DivisionCode { get; set; }
        [StringLength(150)]
        public int? CountryID { get; set; }
        public string DivisionName { get; set; }
        [StringLength(200)]
        public string DivisionAddress { get; set; }
        [StringLength(50)]
        public string ContactNo { get; set; }
        public int OfficeID { get; set; }
        public bool? IsActive { get; set; }
        public DateTime InActiveDate { get; set; }
        public DateTime CreateDate { get; set; }
        [StringLength(35)]
        public string CreateUser { get; set; }
        [StringLength(50)]
        public string ZoneCode { get; set; }
        [StringLength(50)]
        public string AreaCode { get; set; }
        [StringLength(50)]
        public string HOCode { get; set; }
    }
}
