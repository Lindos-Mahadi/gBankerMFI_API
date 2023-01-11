using AutoMapper;
using GC.MFI.DataAccess;
using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models.DbModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Services.Modules.GcMfi.Implementations
{
    public class OfficeService : IOfficeService
    {
        private readonly IOfficeRepository _repository;
        public OfficeService(IOfficeRepository repository)
        {
            this._repository = repository;
        }

        public async Task<IEnumerable<Office>> GetAll(string search)
        {
            var officeList = await _repository.GetAll();
            officeList.OrderByDescending(l => l.CreateDate);
            if(search != null)
                officeList = officeList.Where(t=> t.OfficeName.ToUpper()!.Contains(search.ToUpper()) || t.OfficeCode.ToUpper()!.Contains(search.ToUpper()));
            return officeList.Skip(1).Take(10);
        }
    }

    
}
