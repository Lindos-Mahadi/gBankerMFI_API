using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GC.MFI.Models.DbModels
{
    public partial class OrderDetail : DbModelBase, IDbModelBase
    {
        public Guid MenuId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalPrice { get; set; }
        public string Comments { get; set; }
        public Guid OrderId { get; set; }
        [JsonIgnore]
        public Order order { get; set; }
    }
}
