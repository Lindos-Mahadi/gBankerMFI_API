using GC.MFI.Models.DbModels;
using GC.MFI.Services;
using Microsoft.AspNetCore.Mvc;
using XenterSolution.Models.ViewModels;

namespace GC.MFI.WebApi.Controllers
{
    //[Authorize]
    [ApiController]
    public class GcMfiMembePortalBaseController<TViewModel, TDbModel> : ControllerBase
        where TViewModel : IViewModelBase
        where TDbModel : class, IDbModelBase
    {
        private readonly IServiceBase<TViewModel, TDbModel> service;
        public GcMfiMembePortalBaseController(IServiceBase<TViewModel, TDbModel> service)
        {
            this.service = service;
        }
        #region Common Actions
        [HttpGet]
        [Route("getall")]
        public   virtual IEnumerable<TViewModel> GetAll()
        {
            try
            {
                var results =  service.GetAll();
                return results.OrderByDescending(l => l.CreateDate);
            }
            catch (Exception ex)
            {
                LogError(ex, null);
                throw;
            }
        }
        [HttpGet]
        [Route("getallactive")]
        public virtual IEnumerable<TViewModel> GetAllActive()
        {
            try
            {
                var results = service.GetAllActiveRecords();
                return results.OrderByDescending(l => l.UpdateDate);
            }
            catch (Exception ex)
            {
                LogError(ex, null);
                throw;
            }
        }
        [HttpGet]
        [Route("getbyid")]
        public virtual  TViewModel GetById(Guid id)
        {
            try
            {
                var record =   service.GetById(id);
                return record;
            }
            catch (Exception ex)
            {
                LogError(ex, null);
                throw;
            }
        }
        [HttpPost]
        [Route("create")]
        public virtual TViewModel Create(TViewModel objectToSave)
        {
            try
            {
                objectToSave.CreateUser = "Administrator";
                objectToSave.CreateDate = DateTime.UtcNow;
                objectToSave.UpdateUser = "";
                objectToSave.UpdateDate = DateTime.UtcNow;
                objectToSave.Status = "A";
                var result = service.Create(objectToSave);
                return result;
            }
            catch (Exception ex)
            {
                LogError(ex, null);
                throw;
            }
        }

        [HttpPatch]
        [Route("edit")]
        public virtual  TViewModel Edit(TViewModel objectToSave)
        {
            try
            {
                objectToSave.UpdateUser = "Administrator";
                objectToSave.UpdateDate = DateTime.UtcNow;

                service.Update(objectToSave);
                return objectToSave;
            }
            catch (Exception ex)
            {
                LogError(ex, null);
                throw;
            }
        }

        [HttpPost]
        [Route("delete")]
        public virtual ActionResult Delete(Guid id)
        {
            try
            {
                service.Delete(o => o.Id == id);
                return Ok();
            }
            catch (Exception ex)
            {
                LogError(ex, null);
                return BadRequest(ex);
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
