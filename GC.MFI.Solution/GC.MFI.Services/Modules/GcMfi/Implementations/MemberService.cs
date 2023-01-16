using GC.MFI.DataAccess;
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
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _repository;
        public MemberService(IMemberRepository repository)
        {
            this._repository = repository;
        }

        public Member Create(Member mModel)
        {
            _repository.Save();
            return _repository.Create(mModel);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public async Task<IEnumerable<Member>> GetAll(string search)
        {
            var memberList = await _repository.GetAll(search);
            //memberList.OrderByDescending(l => l.CreateDate);
            //if (search != null)
            //    memberList = memberList.Where(t => t.FirstName.ToUpper()!.Contains(search.ToUpper()) || t.LastName.ToUpper()!.Contains(search.ToUpper()));
            return memberList;
        }

        public Member GetById(long id)
        {
            
            return _repository.GetById(id);
        }

        public void Save()
        {
            _repository.Save();
        }

        public void Update(Member mModel)
        {
            _repository.Update(mModel);
        }
    }
}
