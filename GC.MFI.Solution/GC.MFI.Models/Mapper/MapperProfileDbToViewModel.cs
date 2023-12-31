﻿using AutoMapper;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Models.Mapper
{
    public class MapperProfileDbToViewModel : Profile
    {
        public MapperProfileDbToViewModel()
        {
            
            CreateMap<Order, OrderViewModel>();
            CreateMap<OrderDetail, OderDetailsViewModel>();


            // Portal Member ViewModel
            CreateMap<PortalMember, PortalMemberViewModel>();

            // Member ViewModel
            CreateMap<Member, MemberViewModel>();
            CreateMap<Country, CountryViewModel>();
            CreateMap<Country, DivisionViewModel>();

            // PortalLoanSummary ViewModel
            CreateMap<PortalLoanSummary, PortalLoanSummaryViewModel > ();
            CreateMap<SavingsAccClose, SavingsAccCloseViewModel>();
            CreateMap<PortalSavingSummary, PortalSavingSummaryViewModel>();

            CreateMap<PortalSavingSummary, PortalSavingSummaryFileUpload>();

            CreateMap<LoanAccReschedule, LoanAccRescheduleViewModel>();

            CreateMap<NomineeXPortalSavingSummary, NomineeXPortalSavingSummaryViewModel>();


            CreateMap<FileUploadTable, FileUploadTableViewModel>();

            CreateMap<NomineeXPortalSavingSummary, NomineeXPortalSavingSummaryFile>();

            CreateMap<PortalLoanSummary, PortalLoanSummaryFileUpload>();
            CreateMap<LoanSummary , LoanSummaryViewModel>();

            CreateMap<SavingSummary , SavingsSummaryViewModel>();

            CreateMap<NomineeXSavingSummary, NomineeXSavingsSummaryViewModel>();

            CreateMap<Logger, LoggerViewModel>();

            CreateMap<SMSLogTable, SMSLogTableViewModel>();
            CreateMap<EmailLogTable, EmailLogTableViewModel>();
            CreateMap<PortalMember, SignUpModel>();
            CreateMap<NotificationTable, NotificationTableViewModel>();

            CreateMap<PortalMember, MemberProfile>();
        }
    }
}
