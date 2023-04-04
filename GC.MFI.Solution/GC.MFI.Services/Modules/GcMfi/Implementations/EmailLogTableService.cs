﻿using AutoMapper;
using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Interfaces;
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
    public class EmailLogTableService : ServiceBase<EmailLogTableViewModel, EmailLogTable>, IEmailLogTableService
    {
        private readonly IEmailLogTableRepository repository;
        private readonly IMapper _mapper;
        public EmailLogTableService(IEmailLogTableRepository repository, IUnitOfWork unitOfWork, IMapper _mapper) : base(repository, unitOfWork, _mapper)
        {
            this.repository = repository;
            this._mapper = _mapper;
        }
    }
}
