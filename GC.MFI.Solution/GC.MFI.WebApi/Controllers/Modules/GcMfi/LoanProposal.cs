
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace GC.MFI.WebApi.Controllers.Modules.GcMfi
{
    [Route("api/gcmfi/member")]
    [ApiController]
    public class LoanProposal : ControllerBase
    {
        [HttpGet]
        [Route("GetFrequency")]
        public List<dynamic> GetFrequency()
        {
            try
            {
                var frequency = new List<dynamic>
                {
                    new
                    {
                        Text = "Weekly",
                        Value = "W"
                    },
                    new
                    {
                        Text = "Monthly",
                        Value = "M"

                    },
                    new
                    {
                        Text = "Fortnightly",
                        Value = "F"

                    },
                    new
                    {
                        Text = "Daily",
                        Value = "D"

                    }
                };
                return frequency;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
