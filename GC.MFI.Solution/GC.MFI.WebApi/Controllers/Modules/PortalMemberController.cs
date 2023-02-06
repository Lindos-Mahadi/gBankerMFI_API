﻿using GC.MFI.Models.DbModels;
using GC.MFI.Models.ViewModels;
using GC.MFI.Services.Modules.BntPos.Interfaces;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using GC.MFI.WebApi.Controllers.Modules.Pos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;
using static System.Net.Mime.MediaTypeNames;

namespace GC.MFI.WebApi.Controllers.Modules
{
    [Route("api/gcmfi/portalmember")]
    public class PortalMemberController : GcMfiMembePortalBaseController<PortalMemberViewModel, PortalMember>
    {
        private readonly ILogger<PortalMemberController> _logger;
        private readonly IPortalMemberService _service;


        public PortalMemberController(ILogger<PortalMemberController> logger, IPortalMemberService service) : base(service)
        {
            this._logger = logger;
            this._service = service;
        }

        [HttpGet]
        [Route("getmemberbyid")]
        public async Task<MemberProfile> GetMemberById(long Id)
        {
            var member = await _service.GetMemberById(Id);
            return member;
        }

        [HttpGet]
        [Route("getEducationList")]
        public async Task<string> getEducationList()
        {
            var jObject = new[]
            {
                new { Text = "Pre-Primary", Value = "1" },
                new  { Text = "Primary", Value = "2" },
                new { Text = "JSC", Value = "JSC" },
                new { Text = "Secondary", Value = "3" },
                new { Text = "Higher Secondary", Value = "4" },
                new { Text = "Diploma", Value = "DIP" },
                new { Text = "Graduate", Value = "5" },
                new { Text = "PostGraduate", Value = "6" },
                new { Text = "Illiterate", Value = "ILL" },
                new { Text = "Other", Value = "7" }
            };

            return JsonConvert.SerializeObject(jObject);
        }
    }
}
