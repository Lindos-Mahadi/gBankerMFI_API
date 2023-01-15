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
        private readonly IMemberService _service;
        public MemberController(ILogger<MemberController> logger, IMemberService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IEnumerable<Member>> GetAll()
        {
            try
            {
                var memberList = await _service.GetAll();
                return memberList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("create")]
        public virtual Member Create(Member objectToSave)
        {
            try
            {
                var result = _service.Create(objectToSave);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        [Route("getbyid")]
        public virtual  Member GetById(long id)
        {
            try
            {
                var record =  _service.GetById(id);
                return record;
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

                _service.Update(objectToSave);
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
                _service.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
