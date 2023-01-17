using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.DataAccess.Repository.Implementations
{
    public class DivisionRepository: IDivisionRepository
    {
        private readonly GBankerDbContext _context;
        public DivisionRepository(GBankerDbContext context)
        {
            this._context = context;
        }

        public async Task<List<Division>> GetDivisionByCountry(string countryId)
        {
            try
            {
                var parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@SearchByCode", countryId));
                parameter.Add(new SqlParameter("@SearchBy", "con"));
                parameter.Add(new SqlParameter("@SearchType", "div"));

                var result = await Task.Run(() => _context.Division
               .FromSqlRaw(@"exec Proc_GetLocationList @SearchByCode, @SearchBy, @SearchType", parameter.ToArray()));

                return result.ToList();
            }
            catch(Exception ex)
            {
                throw;
            }           
        }
    }
}
