using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using GC.MFI.Services;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GC.MFI.WebApi.Controllers.Modules.GcMfi
{
    [Route("api/gcmfi/member")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly ILogger<MemberController> _logger;
        private readonly IMemberService service;

        public MemberController(ILogger<MemberController> logger, IMemberService service)
        {
            _logger = logger;
            this.service = service;
        }

        // GET: api/<MemberController>
        #region Common Actions
        [HttpGet]
        [Route("getall")]
        public virtual IEnumerable<Member> GetAll()
        {
            try
            {
                var results = service.GetAll();
                return results;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // GET api/<MemberController>/5
        [HttpGet]
        [Route("getbyid")]
        public virtual Member GetById(long id)
        {
            try
            {
                var record = service.GetById(id);
                return record;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST api/<MemberController>
        [HttpPost]
        [Route("create")]
        public virtual Member Create(Member objectToSave)
        {
            try
            {
                objectToSave.CreateUser = "Administrator";
                objectToSave.CreateDate = DateTime.UtcNow;
                //objectToSave.UpdateUser = "";
                //objectToSave.UpdateDate = DateTime.UtcNow;
                //objectToSave.Status = "A";

                var result = service.Create(objectToSave);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPatch]
        [Route("edit")]
        public virtual Member Edit(Member objectToSave)
        {
            try
            {
                //objectToSave.UpdateUser = "Administrator";
                //objectToSave.UpdateDate = DateTime.UtcNow;

                service.Update(objectToSave);
                return objectToSave;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("delete")]
        public virtual ActionResult Delete(long id)
        {
            try
            {
                service.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion
    }
}
