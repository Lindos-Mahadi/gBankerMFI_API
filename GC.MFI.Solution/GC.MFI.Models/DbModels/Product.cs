using System;
using System.Collections.Generic;

namespace GC.MFI.Models.DbModels
{
    public partial class Product : DbModelBase, IDbModelBase
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
