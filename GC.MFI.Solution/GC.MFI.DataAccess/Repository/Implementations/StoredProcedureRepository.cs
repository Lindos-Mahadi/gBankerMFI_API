﻿using GC.MFI.DataAccess.Repository.Interfaces;
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
    public class StoredProcedureRepository: IStoredProcedureRepository
    {
        private readonly GBankerDbContext _context;
        public StoredProcedureRepository(GBankerDbContext context)
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

        public async Task<List<Center>> GetCenterListByOffice(int OfficeId)
        {
            try
            {
                var parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@OfficeId", OfficeId));
                
                var result = await Task.Run(() => _context.Center
                .FromSqlRaw(@"exec GetOnlyCenter @OfficeId", parameter.ToArray()));

                return result.ToList();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<List<MainProduct>> GetMainProductList(string PaymentFrequecy, int officeId)
        {
            try
            {
                var parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@PaymentFrq", PaymentFrequecy));
                parameter.Add(new SqlParameter("@OfficeID", officeId));

                var result = await Task.Run(() => _context.MainProduct
                .FromSqlRaw(@"exec getMainProductListAccordingToOffice @PaymentFrq, @OfficeID", parameter.ToArray()));

                return result.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<SubMainProduct>> GetSubMainProdutList(string MainProductCode, string freq)
        {
            try
            {
                var parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@MainProductCode", MainProductCode));
                parameter.Add(new SqlParameter("@freq", freq));

                var result = await Task.Run(() => _context.SubMainProduct
                .FromSqlRaw(@"exec getSubMainProductList @MainProductCode, @freq", parameter.ToArray()));

                return result.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<ProductList>> GetProductList(string MainProductCode, string freq, int OfficeID)
        {
            try
            {
                var parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@MainProductCode", MainProductCode));
                parameter.Add(new SqlParameter("@freq", freq));
                parameter.Add(new SqlParameter("@OfficeID", OfficeID));

                var result = await Task.Run(() => _context.ProductList
                .FromSqlRaw(@"exec getSubMainProductListTAccordingTOOffice @MainProductCode, @freq, @OfficeID", parameter.ToArray()));

                return result.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}