using AutoMapper;
using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GC.MFI.Services
{
    public class ServiceBase<TViewModel, TDbModel> : IServiceBase<TViewModel, TDbModel>
        where TViewModel : IViewModelBase
        where TDbModel : class, IDbModelBase
    {
        private IRepository<TDbModel> repository;
        private IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;
        public ServiceBase(IRepository<TDbModel> repository, IUnitOfWork unitOfWork, IMapper _mapper)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this._mapper = _mapper;
        }
        public virtual TViewModel Create(TViewModel objectToCreate)
        {
            var dbModel = _mapper.Map<TDbModel>(objectToCreate);
            repository.Add(dbModel);
            Save();
            var viewModel = _mapper.Map<TViewModel>(dbModel);
            return viewModel;
        }

        public virtual TViewModel CreateWithoutSave(TViewModel objectToCreate)
        {
            var dbModel = _mapper.Map<TDbModel>(objectToCreate);
            repository.Add(dbModel);
            var viewModel = _mapper.Map<TViewModel>(dbModel);
            return viewModel;
        }

        public virtual void Delete(Expression<Func<TDbModel, bool>> condition)
        {
            repository.Delete(condition);
            Save();
        }

        public virtual IEnumerable<TViewModel> GetAll()
        {
            var results = repository.GetAll();
            var mappedResult = _mapper.Map<IEnumerable<TViewModel>>(results);
            return mappedResult;
        }
        public virtual IEnumerable<TViewModel> GetAll<TKey>(Func<TDbModel, TKey> orderby)
        {
            var results = repository.GetAll().OrderBy(orderby);
            var mappedResult = _mapper.Map<IEnumerable<TViewModel>>(results);
            return mappedResult;
        }
        public virtual TViewModel GetById(Guid id)
        {
            var dbModel = repository.GetById(id);
            var viewModel = _mapper.Map<TViewModel>(dbModel);
            return viewModel;
        }



        public void Save()
        {
            unitOfWork.Commit();
        }

        public void Update(TViewModel objectToUpdate)
        {
            var dbModel = _mapper.Map<TDbModel>(objectToUpdate);
            repository.Update(dbModel);
            Save();
        }

        public virtual TViewModel Get(Expression<Func<TDbModel, bool>> where)
        {
            var dbModel = repository.Get(where);
            var viewModel = _mapper.Map<TViewModel>(dbModel);
            return viewModel;
        }
        public virtual IEnumerable<TViewModel> GetMany(Expression<Func<TDbModel, bool>> condition)
        {
            var result = repository.GetMany(condition);
            var viewModels = _mapper.Map<IEnumerable<TViewModel>>(result);
            return viewModels;
        }

        public IEnumerable<TViewModel> GetAllActiveRecords()
        {
            var result = repository.GetMany(m => m.Status == "A");
            var viewModels = _mapper.Map<IEnumerable<TViewModel>>(result);
            return viewModels.OrderByDescending(l => l.UpdateDate);
        }
    }
}
