using AutoMapper;
using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using GC.MFI.Services.Modules.Security.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Services.Modules.GcMfi.Implementations
{
    public class SavingsAccCloseService : ServiceBase<SavingsAccCloseViewModel, SavingsAccClose>, ISavingsAccCloseService
    {
        private ISavingsAccCloseRepository _repository;
        private ISavingSummaryRepository _savingSummaryRepository;
        private IMapper _mapper;
        public SavingsAccCloseService(ISavingsAccCloseRepository repository, ISavingSummaryRepository savingSummaryRepository, IUnitOfWork unitOfWork, IMapper _mapper) : base(repository, unitOfWork, _mapper)
        {
            this._repository = repository;
            this._savingSummaryRepository = savingSummaryRepository;
            this._mapper = _mapper;
        }

        public override SavingsAccCloseViewModel Create(SavingsAccCloseViewModel acc)
        {
            var getSavingClose = _repository.Get(t=> t.SavingAccountID == acc.SavingAccountID);
            if(getSavingClose != null)
            {
                return null;
            }
            acc.Status = "P";
            acc.CreateDate = DateTime.UtcNow;
            acc.UpdateDate = DateTime.UtcNow;
            var status = _savingSummaryRepository.GetById(acc.SavingAccountID);
            if (status == null) { return null; }
            status.SavingStatus = 4;
            return base.Create(acc);
        }


    }
}

