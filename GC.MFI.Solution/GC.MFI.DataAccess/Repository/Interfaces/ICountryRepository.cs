using GC.MFI.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.DataAccess.Repository.Interfaces
{
    public interface ICountryRepository
    {
        Task<IEnumerable<Country>> GetAll(string search);
    }
}
