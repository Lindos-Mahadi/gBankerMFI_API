using GC.MFI.Models.DbModels;
using System;
using System.Text.Json.Serialization;
using XenterSolution.Models.ViewModels;

namespace GC.MFI.Models.ViewModels
{
    public class OderDetailsViewModel : ViewModelBase, IViewModelBase
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
        public OrderViewModel order { get; set; }
    }
}
