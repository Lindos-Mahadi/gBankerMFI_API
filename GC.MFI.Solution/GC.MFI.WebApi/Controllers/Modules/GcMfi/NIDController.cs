using GC.MFI.Models.Models;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace GC.MFI.WebApi.Controllers.Modules.GcMfi
{
    [Route("api/[controller]")]
    [ApiController]
    public class NIDController : ControllerBase
    {
        private readonly ILogger<NIDController> logger;
        private readonly INIDService nIDService;
        public NIDController(ILogger<NIDController> logger, INIDService nIDService)
        {
            this.logger = logger;
            this.nIDService = nIDService;
        }

        [HttpPost]
        [Route("/verifyNID")]
        public async Task<NIDVerificationResponse> VerifyNID(NIDVerificationRequest req)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    throw new Exception("Insert All Required Fields");
                }
                return await nIDService.GetNIDInfo(req);
                //return new NIDVerificationResponse
                //{
                //    data = new Data
                //    {
                //        nid = new Nid
                //        {
                //            fullNameEN = "MD asd asd",
                //            fathersNameEN = "Md asd",
                //            mothersNameEN = "Mrs asdas",
                //            spouseNameEN = "",
                //            presentAddressEN = "asdasd",
                //            permenantAddressEN = "asdasd",
                //            fullNameBN = "মোঃ আসিফুল আরেফিন",
                //            fathersNameBN = "মোঃ আব্দুল হক",
                //            mothersNameBN = "",
                //            spouseNameBN = "",
                //            presentAddressBN = "",
                //            permanentAddressBN = "",
                //            gender = "male",
                //            profession = "service",
                //            //dateOfBirth = "1995-01-06T00:00:00",
                //            nationalIdNumber = "",
                //            oldNationalIdNumber = "",
                //            photoUrl = ""
                //        }
                //    },
                //    errors = new List<Error>()
                //};
            }
            catch(Exception ex)
            {
                //logger.Log(LogLevel.Error,  ex);
                throw;
            }
        }

    }
}
