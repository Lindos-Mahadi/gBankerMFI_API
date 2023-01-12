using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.DataAccess.Repository.Implementations
{
    public class MemberRepository : IMemberRepository
    {
        private readonly BntPOSContext _context;

        public MemberRepository(BntPOSContext context)
        {
            _context = context;
        }

        public  IEnumerable<Member> GetAll()
        {
            var getOffice = _context.Member.AsEnumerable();
            return getOffice;
        }


    }
}
