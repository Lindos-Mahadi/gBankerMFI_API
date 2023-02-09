using AutoMapper;
using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.DbModels.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Services
{
    public class LegacyServiceBase<TDbModel> : ILegacyServiceBase<TDbModel>
        where TDbModel : class, Models.DbModels.BaseModels.ILegacyDbModelBase
    {
        private ILegacyRepository<TDbModel> repository;
        private IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;
        public LegacyServiceBase(ILegacyRepository<TDbModel> repository, IUnitOfWork unitOfWork, IMapper _mapper)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this._mapper = _mapper;
        }
        public virtual TDbModel Create(TDbModel objectToCreate)
        {
            var dbModel = _mapper.Map<TDbModel>(objectToCreate);
            repository.Add(dbModel);
            Save();
            var viewModel = _mapper.Map<TDbModel>(dbModel);
            return viewModel;
        }
        public virtual TDbModel[] BulkCreate(TDbModel[] objectToCreate)
        {
            var dbModel = _mapper.Map<TDbModel[]>(objectToCreate);
            repository.BulkInsert(dbModel);
            Save();
            var viewModel = _mapper.Map<TDbModel[]>(dbModel);
            return viewModel;
        }

        public virtual TDbModel CreateWithoutSave(TDbModel objectToCreate)
        {
            var dbModel = _mapper.Map<TDbModel>(objectToCreate);
            repository.Add(dbModel);
            var viewModel = _mapper.Map<TDbModel>(dbModel);
            return viewModel;
        }

        public virtual void Delete(Expression<Func<TDbModel, bool>> condition)
        {
            repository.Delete(condition);
            Save();
        }

        public virtual IEnumerable<TDbModel> GetAll()
        {
            var results = repository.GetAll();
            var mappedResult = _mapper.Map<IEnumerable<TDbModel>>(results);
            return mappedResult;
        }
        public virtual IEnumerable<TDbModel> GetAll<TKey>(Func<TDbModel, TKey> orderby)
        {
            var results = repository.GetAll().OrderBy(orderby);
            var mappedResult = _mapper.Map<IEnumerable<TDbModel>>(results);
            return mappedResult;
        }
        public virtual TDbModel GetById(long id)
        {
            var dbModel = repository.GetById(id);
            var viewModel = _mapper.Map<TDbModel>(dbModel);
            return viewModel;
        }

        public virtual TDbModel GetByIdShort(short id)
        {
            var dbModel = repository.GetByIdShort(id);
            var viewModel = _mapper.Map<TDbModel>(dbModel);
            return viewModel;
        }

        public void Save()
        {
            unitOfWork.Commit();
        }

        public void Update(TDbModel objectToUpdate)
        {
            var dbModel = _mapper.Map<TDbModel>(objectToUpdate);
            repository.Update(dbModel);
            Save();
        }

        public virtual TDbModel Get(Expression<Func<TDbModel, bool>> where)
        {
            var dbModel = repository.Get(where);
            var viewModel = _mapper.Map<TDbModel>(dbModel);
            return viewModel;
        }
        public virtual IEnumerable<TDbModel> GetMany(Expression<Func<TDbModel, bool>> condition)
        {
            var result = repository.GetMany(condition);
            var viewModels = _mapper.Map<IEnumerable<TDbModel>>(result);
            return viewModels;
        }
    }
}
