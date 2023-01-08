using GC.MFI.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XenterSolution.Models.ViewModels;

namespace GC.MFI.Models.ViewModels
{
    public class ProductViewModel : ViewModelBase, IViewModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal AvailableQuantity { get; set; }
        public Guid ProductTypeId { get; set; }
        public Guid UnitOfMeasureId { get; set; }
        public Guid CompanyId { get; set; }
        public Guid SiteId { get; set; }
    }
}
