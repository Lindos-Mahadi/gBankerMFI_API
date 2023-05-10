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
    public class LoanAccRescheduleService : ServiceBase<LoanAccRescheduleViewModel, LoanAccReschedule>, ILoanAccRescheduleService
    {
        private readonly ILoanAccRescheduleRepository _repository;
        private readonly ILoanSummaryRepository _loanSummaryRepository;
        private readonly IMapper _mapper;
        public LoanAccRescheduleService(ILoanAccRescheduleRepository repository, ILoanSummaryRepository _loanSummaryRepository, IUnitOfWork unitOfWork, IMapper _mapper) : base(repository, unitOfWork, _mapper)
        {
            this._repository = repository;
            this._mapper = _mapper;
            this._loanSummaryRepository = _loanSummaryRepository;   
        }


        public override LoanAccRescheduleViewModel Create(LoanAccRescheduleViewModel objectToCreate)
        {
            var GetLoan = _repository.Get(t=> t.LoanID == objectToCreate.LoanID);
            if (GetLoan != null)
                return null;
            objectToCreate.Status = "P";
            objectToCreate.CreateDate = DateTime.UtcNow;
            objectToCreate.UpdateDate = DateTime.UtcNow;
            var RStatus = _loanSummaryRepository.GetById(objectToCreate.LoanID);
            if (RStatus == null) return null;
            RStatus.LoanStatus = 4;
            return base.Create(objectToCreate);
        }
    }
}

