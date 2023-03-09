﻿using AutoMapper;
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
    public class MemberToPHCMappingService : LegacyServiceBase<MemberToPHCMapping>, IMemberToPHCMappingService
    {
        private readonly IMemberToPHCMappingRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public MemberToPHCMappingService(IMemberToPHCMappingRepository repository, IUnitOfWork unitOfWork, IMapper _mapper) : base(repository, unitOfWork, _mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
    }
}
