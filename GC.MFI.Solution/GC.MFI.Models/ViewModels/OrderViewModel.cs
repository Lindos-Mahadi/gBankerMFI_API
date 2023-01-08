﻿using GC.MFI.Models.DbModels;
using System.Collections.Generic;
using System;
using XenterSolution.Models.ViewModels;

namespace GC.MFI.Models.ViewModels
{
    public class OrderViewModel : ViewModelBase, IViewModelBase
    {
        public string OrderNo { get; set; }
        public string BillNo { get; set; }
        public Guid TableId { get; set; }
        public Guid ServiceTypesId { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal NetPrice { get; set; }
        public decimal ReceivedAmount { get; set; }
        public decimal ReturnedAmount { get; set; }
        public Guid DeliveryCompanyId { get; set; }
        public Guid PaymentTypeId { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid CompanyId { get; set; }
        public Guid SiteId { get; set; }
        public Guid DailyShiftId { get; set; }
        public string Comments { get; set; }
        public string PaymentRef { get; set; }
        public List<OrderDetail> orderDetails { get; set; }

    }
}
