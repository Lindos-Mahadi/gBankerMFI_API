﻿using AutoMapper;
using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Services.Modules.GcMfi.Implementations
{
    public class LoanSummaryService : LegacyServiceBase<LoanSummary>, ILoanSummaryService
    {
        private readonly ILoanSummaryRepository _repository;
        private readonly IMapper _mapper;
        public LoanSummaryService(ILoanSummaryRepository repository, IUnitOfWork unitOfWork, IMapper _mapper) : base(repository, unitOfWork, _mapper)
        {
            _repository = repository;
            this._mapper = _mapper;
        }

        public PagedResponse<IQueryable<LoanSummaryViewModel>> GetAllPortalLoanSummaryPaged(PaginationFilter<LoanSummaryViewModel> filter, long Id)
        {
            var response = _repository.GetAllPortalLoanSummaryPaged(filter, Id);
            return response;
        }
    }
}