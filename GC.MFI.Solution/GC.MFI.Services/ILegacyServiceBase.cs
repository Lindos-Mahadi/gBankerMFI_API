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
    public interface ILegacyServiceBase<TDbModel>
        where TDbModel : class, ILegacyDbModelBase
    {
        IEnumerable<TDbModel> GetAll();
        IEnumerable<TDbModel> GetAll<TKey>(Func<TDbModel, TKey> orderby);
        TDbModel GetById(long id);
        TDbModel Create(TDbModel objectToCreate);
        TDbModel CreateWithoutSave(TDbModel objectToCreate);
        void Update(TDbModel objectToUpdate);
        void Delete(Expression<Func<TDbModel, bool>> condition);
        void Save();
        TDbModel Get(Expression<Func<TDbModel, bool>> condition);
        IEnumerable<TDbModel> GetMany(Expression<Func<TDbModel, bool>> condition);
    }
}
