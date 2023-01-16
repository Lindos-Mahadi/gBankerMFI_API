using AutoMapper;
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
            
            CreateMap<Product, ProductViewModel>();
            CreateMap<Order, OrderViewModel>();
            CreateMap<OrderDetail, OderDetailsViewModel>();


            // Portal Member ViewModel
            CreateMap<PortalMember, PortalMemberViewModel>();

            // Member ViewModel
            CreateMap<Member, MemberViewModel>();
            CreateMap<Country, CountryViewModel>();
        }
    }
}
