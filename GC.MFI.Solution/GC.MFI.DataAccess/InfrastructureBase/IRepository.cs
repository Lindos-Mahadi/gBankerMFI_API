﻿
using GC.MFI.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GC.MFI.DataAccess.InfrastructureBase
{
    public interface IRepository<TDbModel>     
        where TDbModel : IDbModelBase
    {       
        void Add(TDbModel entity);
        void Update(TDbModel entity);
        void Delete(TDbModel entity);
        void Delete(Expression<Func<TDbModel, bool>> where);
        TDbModel GetById(long id);
        TDbModel GetById(Guid id);
        TDbModel Get(Expression<Func<TDbModel, bool>> where);
        IEnumerable<TDbModel> GetAll();
        IEnumerable<TDbModel> GetAll(Expression<Func<TDbModel, bool>> where);
        IQueryable<TDbModel> GetMany(Expression<Func<TDbModel, bool>> where);
        IQueryable<TDbModel> GetPaged(string filterColumnName, string filterValue, int startRowIndex, string sortColumn, string sortOrder, int per_page, out long TotalCount);
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        void EndTransaction();
        int GetCount(Expression<Func<TDbModel, bool>> condition);
    }
}
