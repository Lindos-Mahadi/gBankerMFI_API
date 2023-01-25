using AutoMapper;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;

namespace GC.MFI.Models.Mapper
{
    public class MapperProfileViewModelToDb : Profile
    {
        public MapperProfileViewModelToDb()
        {                 
            CreateMap<OrderViewModel, Order>();
            CreateMap<OderDetailsViewModel, OrderDetail>();

            // Portal Member ViewModel
            CreateMap< PortalMemberViewModel, PortalMember>();

            // Member ViewModel
            CreateMap<MemberViewModel, Member>();
            CreateMap<CountryViewModel, Country>();
            CreateMap<DivisionViewModel, Country>();

            // PortalLoanSummary ViewModel
            CreateMap<PortalLoanSummaryViewModel, PortalLoanSummary>();
        }
    }
}
