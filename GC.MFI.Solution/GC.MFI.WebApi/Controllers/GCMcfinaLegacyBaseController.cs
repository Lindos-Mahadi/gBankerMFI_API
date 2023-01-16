using GC.MFI.Models.DbModels;
using GC.MFI.Models.DbModels.BaseModels;
using GC.MFI.Services;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using XenterSolution.Models.ViewModels;

namespace GC.MFI.WebApi.Controllers
{
    [ApiController]
    public class GCMcfinaLegacyBaseController<TDbModel> : ControllerBase
        where TDbModel : class, ILegacyDbModelBase
    {
        private readonly ILegacyServiceBase<TDbModel> service;
        public GCMcfinaLegacyBaseController(ILegacyServiceBase<TDbModel> service)
        {
            this.service = service;
        }
        #region Common Actions
        [HttpGet]
        [Route("getall")]
        public virtual IEnumerable<TDbModel> GetAll()
        {
            try
            {
                var results = service.GetAll();
                return results;
            }
            catch (Exception ex)
            {
                LogError(ex, null);
                throw;
            }
        }
        [HttpGet]
        [Route("getbyid")]
        public virtual TDbModel GetById(long id)
        {
            try
            {
                var record = service.GetById(id);
                return record;
            }
            catch (Exception ex)
            {
                LogError(ex, null);
                throw;
            }
        }
        #endregion

        #region Error Handling
        protected void LogError(Exception ex, ViewModelBase viewModel)
        {
            //Log error and return customized error request...

        }
        #endregion
    }
}
