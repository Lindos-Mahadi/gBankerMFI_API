using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using GC.MFI.Services.Modules.Firebase.Interfaces;

namespace GC.MFI.Services.Modules.Firebase.Implementations;

public class FcmTokenService : IFcmTokenService
{
    private readonly IFcmTokenRepository _repository;
    private readonly IMapper _mapper;

    public FcmTokenService(IFcmTokenRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<FcmTokenViewModel> CreateOrUpdate(FcmTokenViewModel input)
    {
        var result = _repository.Get(token => token.UserId == input.UserId);

        if (result != null)
        {
            _mapper.Map(input, result);
            _repository.Update(result);
            return await Task.FromResult(_mapper.Map<FcmTokenViewModel>(result));
        }
        else
        {
            var token = _mapper.Map<FcmToken>(input);
            _repository.Add(token);

            return await Task.FromResult(_mapper.Map<FcmTokenViewModel>(token));
        }
    }

    public Task<FcmTokenViewModel> GetByUserId(string userId)
    {
        var result = _repository.Get(token => token.UserId == userId);

        return Task.FromResult(_mapper.Map<FcmTokenViewModel>(result));
    }
}