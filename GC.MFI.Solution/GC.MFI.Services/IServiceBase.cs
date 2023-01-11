using GC.MFI.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions; 

namespace GC.MFI.Services
{

    public interface IServiceBase<TViewModel, TDbModel> 
        where TViewModel : IViewModelBase
        where TDbModel : class, IDbModelBase
    {
        IEnumerable<TViewModel> GetAll();
        IEnumerable<TViewModel> GetAllActiveRecords();
        IEnumerable<TViewModel> GetAll<TKey>(Func<TDbModel, TKey> orderby);
        TViewModel GetById(long id);
        TViewModel Create(TViewModel objectToCreate);
        TViewModel CreateWithoutSave(TViewModel objectToCreate);
        void Update(TViewModel objectToUpdate);
        void Delete(Expression<Func<TDbModel, bool>> condition);
        void Save();
        TViewModel Get(Expression<Func<TDbModel, bool>> condition);
        IEnumerable<TViewModel> GetMany(Expression<Func<TDbModel, bool>> condition);
    }
}
