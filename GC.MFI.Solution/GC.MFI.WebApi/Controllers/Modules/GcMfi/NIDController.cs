using GC.MFI.DataAccess;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.Models;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;


namespace GC.MFI.WebApi.Controllers.Modules.GcMfi
{
    [Route("api/[controller]")]
    [ApiController]
    public class NIDController : ControllerBase
    {
        private readonly ILogger<NIDController> logger;
        private readonly INIDService nIDService;
        private readonly INIDLoggingService loggingService;
        private readonly IConfiguration configuration;
        public NIDController(
            ILogger<NIDController> logger, 
            INIDService nIDService, 
            INIDLoggingService loggingService,
            IConfiguration configuration)
        {
            this.logger = logger;
            this.nIDService = nIDService;
            this.loggingService = loggingService;
            this.configuration = configuration;
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

                var UsePorichoyAPI = configuration["UsePorichoyAPI"];

                if(UsePorichoyAPI == "True")
                {
                    return await nIDService.GetNIDInfo(req);
                }
                return new NIDVerificationResponse
                {
                    data = new Data
                    {
                        nid = new Nid
                        {
                            fullNameEN = "MD Mahinur Rahman",
                            fathersNameEN = "Md Shamsul Rahman",
                            mothersNameEN = "Mrs Amena Begum",
                            spouseNameEN = "",
                            presentAddressEN = "House-240, Sector-14, Uttara Dhaka",
                            permenantAddressEN = "asdasd",
                            fullNameBN = "মোঃ আসিফুল আরেফিন",
                            fathersNameBN = "মোঃ আব্দুল হক",
                            mothersNameBN = "",
                            spouseNameBN = "",
                            presentAddressBN = "",
                            permanentAddressBN = "",
                            gender = "male",
                            profession = "service",
                            //dateOfBirth = "1995-01-06T00:00:00",
                            nationalIdNumber = "",
                            oldNationalIdNumber = "",
                            photoUrl = ""
                        }
                    },
                    errors = new List<Error>()
                };
            }
            catch (Exception ex)
            {
                //logger.Log(LogLevel.Error,  ex);
                throw;
            }
        }

    }
}
