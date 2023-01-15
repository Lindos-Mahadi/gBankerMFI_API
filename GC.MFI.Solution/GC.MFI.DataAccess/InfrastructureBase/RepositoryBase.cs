using GC.MFI.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;

using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace GC.MFI.DataAccess.InfrastructureBase
{
    public abstract class RepositoryBase<TDbModel> : IRepository<TDbModel>
        where TDbModel :class, IDbModelBase         
    {
        private GBankerDbContext _dataContext;
        private readonly Microsoft.EntityFrameworkCore.DbSet<TDbModel> _dbset;
        private IDbContextTransaction dbTransaction = null;
        protected RepositoryBase(IDatabaseFactory  databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            _dbset = DataContext.Set<TDbModel>();
        }

        protected IDatabaseFactory DatabaseFactory { get; private set; }
        
        public int GetCount(Expression<Func<TDbModel, bool>> condition)
        {            
            var cnt = _dbset.Count(condition);
            return cnt;            
        }
     
        protected GBankerDbContext DataContext
        {
            get { return _dataContext ?? (_dataContext = DatabaseFactory.Get()); }

        }
        public void BeginTransaction()
        {
            if (dbTransaction == null)
                dbTransaction = _dataContext.Database.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

        }
        public void CommitTransaction()
        {
            if (dbTransaction != null)
            {
                dbTransaction.Commit();
                _dataContext.SaveChanges();
                dbTransaction.Dispose();
                dbTransaction = null;
            }
        }
        public void RollbackTransaction()
        {
            if (dbTransaction != null)
            {
                dbTransaction.Rollback();
                //_dataContext.SaveChanges();
                dbTransaction.Dispose();
                dbTransaction = null;
            }
        }
        public void EndTransaction()
        {
            if (dbTransaction != null)
            {
                _dataContext.SaveChanges();
                dbTransaction.Dispose();
                dbTransaction = null;
            }
        }

        public virtual void Add(TDbModel entity)
        {
            _dbset.Add(entity);
        }

        public virtual void Update(TDbModel entity)
        {
            _dbset.Attach(entity);
            _dataContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public virtual void Delete(TDbModel entity)
        {
            _dbset.Remove(entity);
        }

        public virtual void Delete(Expression<Func<TDbModel, bool>> where)
        {
            var objects = _dbset.Where<TDbModel>(where).AsEnumerable();
            foreach (var obj in objects)
                _dbset.Remove(obj);
        }

        public virtual TDbModel GetById(long id)
        {
            return _dbset.Find(id);
        }

        public virtual TDbModel GetById(Guid id)
        {
            return _dbset.Find(id);
        }

        public virtual IEnumerable<TDbModel> GetAll()
        {
            return _dbset;
        }

        public virtual IQueryable<TDbModel> GetMany(Expression<Func<TDbModel, bool>> where)
        {
            if (where != null)
                return _dbset.Where(where).AsQueryable();
            else
                return _dbset.AsQueryable();
        }

        public virtual TDbModel Get(Expression<Func<TDbModel, bool>> where)
        {
            return _dbset.Where(where).FirstOrDefault();
        }

        public virtual IEnumerable<TDbModel> GetAll(Expression<Func<TDbModel, bool>> where)
        {
            return _dbset.Where(where).ToList();
        }

        
        private Expression<Func<T1, object>> GetOrderByExpression<T1>(string sortColumn)
        {
            if (!string.IsNullOrEmpty(sortColumn))
            {
                var param = Expression.Parameter(typeof(T1));
                var field = Expression.Property(param, sortColumn);
                return Expression.Lambda<Func<T1, object>>(field, param);
            }
            else
                return null;
        }
        private bool IsNullable<TType>(TType value)
        {
            return Nullable.GetUnderlyingType(typeof(TType)) != null;
        }

        private SqlParameter CreateParameter(string parameterName, object parameterValue)
        {
            return new SqlParameter { Value = parameterValue == null ? DBNull.Value : parameterValue, ParameterName = parameterName };
        }

        private SqlParameter CreateParameter(string parameterName, object parameterValue, ParameterDirection direction)
        {
            return new SqlParameter { Value = parameterValue == null ? DBNull.Value : parameterValue, ParameterName = parameterName, Direction = direction };
        }

        public IQueryable<TDbModel> GetPaged(string filterColumnName, string filterValue, int startRowIndex, string sortColumn, string sortOrder, int per_page, out long TotalCount)
        {
            throw new NotImplementedException();
        }
    }
}
