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
                if (req.nidNumber.Length == 10 || req.nidNumber.Length == 13 || req.nidNumber.Length == 17)
                {


                    var UsePorichoyAPI = configuration["UsePorichoyAPI"];

                    if (UsePorichoyAPI == "True")
                    {
                        return await nIDService.GetNIDInfo(req);
                    }
                    return new NIDVerificationResponse
                    {
                        data = new Data
                        {
                            nid = new Nid
                            {
                                fullNameEN = "",
                                fathersNameEN = "",
                                mothersNameEN = "",
                                spouseNameEN = "",
                                presentAddressEN = "",
                                permenantAddressEN = "",
                                fullNameBN = "",
                                fathersNameBN = "",
                                mothersNameBN = "",
                                spouseNameBN = "",
                                presentAddressBN = "",
                                permanentAddressBN = "",
                                gender = "",
                                profession = "",
                                //dateOfBirth = "1995-01-06T00:00:00",
                                nationalIdNumber = "",
                                oldNationalIdNumber = "",
                                photoUrl = ""
                            }
                        },
                        errors = new List<Error>()
                    };
                }else
                {
                    return new NIDVerificationResponse { data = null, errors = new List<Error>()
                    {
                       new Error
                       {
                           code = "400",
                           message = "Invalid NID"
                       } 
                    } };
                }
            }
            catch (Exception ex)
            {
                //logger.Log(LogLevel.Error,  ex);
                throw;
            }
        }

    }
}
