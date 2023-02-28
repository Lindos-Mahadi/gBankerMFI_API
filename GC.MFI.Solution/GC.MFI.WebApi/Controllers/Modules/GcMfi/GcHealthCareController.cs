using GC.MFI.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Org.BouncyCastle.Ocsp;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace GC.MFI.WebApi.Controllers.Modules.GcMfi
{
    [Route("api/GcMfi/GcHealthCare")]
    [ApiController]
    public class GcHealthCareController : ControllerBase
    {
        [HttpGet]
        [Route("GetServiceHistoryList")]
        public IActionResult GetServiceHistoryList()
        {
            var serviceHistory = new[]
            {
                new  {SL = 1, ReferenceNo = "121423", Date = DateTime.Now, Total = 58610, Discount = 610, Payment=5800},
                new  {SL = 2, ReferenceNo = "223425", Date = DateTime.Now, Total = 90254, Discount = 500, Payment=9000},
                new  {SL = 2, ReferenceNo = "343524", Date = DateTime.Now, Total = 78900, Discount = 700, Payment=8500},
                new  {SL = 4, ReferenceNo = "452561", Date = DateTime.Now, Total = 90300, Discount = 300, Payment=9000},
                new  {SL = 5, ReferenceNo = "543525", Date = DateTime.Now, Total = 78000, Discount = 100, Payment=4500},
            };
            return Ok(serviceHistory);
            //var ss = JsonConvert.SerializeObject(serviceHistory);
            //return Ok(ss);
        }
        [HttpGet]
        [Route("GetMedicalHistoryList")]
        public IActionResult GetMedicalHistoryList()
        {
            var medicalHistory = new[]
            {
                new {Index = 1, MedicalHistory = "Good", SurveyDate = DateTime.Now, ViewEdit = "edit Link" },
                new {Index = 2, MedicalHistory = "Goodest", SurveyDate = DateTime.Now, ViewEdit = "edit Link" },
                new {Index = 3, MedicalHistory = "Well", SurveyDate = DateTime.Now, ViewEdit = "edit Link" },
                new {Index = 4, MedicalHistory = "Supper", SurveyDate = DateTime.Now, ViewEdit = "edit Link" },
                new {Index = 5, MedicalHistory = "Excellent", SurveyDate = DateTime.Now, ViewEdit = "edit Link" },
            }.ToList();
            return Ok(medicalHistory);
        }
        [HttpGet]
        [Route("GetBasicCheckupList")]
        public IActionResult GetBasicCheckupList()
        {

            var basicCheckup = new[]
            {
                new {SLNo = 1, Date = DateTime.Now, Height = 145, Wheight = 80, BMI=21.21, Waist=45, Hip=25, WH = 0.74, BTemp = 93.4, SpO2 = 69, BP="128/90", BG = "6(PBS)", BH=11, UG = "-", UP = "-", UU="+-", PR = 56, Arr = "Normal", Cho = 140, Uric=12},
                new {SLNo = 2, Date = DateTime.Now, Height = 155, Wheight = 50, BMI=23.09, Waist=13, Hip=34, WH = 0.75, BTemp = 95.4, SpO2 = 79, BP="110/70", BG = "6(PBS)", BH=09, UG = "-", UP = "-", UU="+-", PR = 77, Arr = "Normal", Cho = 150, Uric=32},
                new {SLNo = 3, Date = DateTime.Now, Height = 135, Wheight = 90, BMI=19.01, Waist=52, Hip=56, WH = 0.76, BTemp = 96.7, SpO2 = 87, BP="125/70", BG = "6(PBS)", BH=14, UG = "-", UP = "-", UU="+-", PR = 98, Arr = "Normal", Cho = 170, Uric=31},
                new {SLNo = 4, Date = DateTime.Now, Height = 125, Wheight = 40, BMI=23.34, Waist=34, Hip=76, WH = 0.77, BTemp = 92.5, SpO2 = 56, BP="145/99", BG = "6(PBS)", BH=15, UG = "-", UP = "-", UU="+-", PR = 69, Arr = "Normal", Cho = 180, Uric=09},
                new {SLNo = 5, Date = DateTime.Now, Height = 115, Wheight = 50, BMI=15.12, Waist=15, Hip=67, WH = 0.78, BTemp = 95.7, SpO2 = 89, BP="160/120", BG = "6(PBS)", BH=10, UG = "-", UP = "-", UU="+-", PR = 98, Arr = "Normal", Cho = 110, Uric=9}
            }.ToList();
            return Ok(basicCheckup);
        }
        [HttpGet]
        [Route("GetTestDocumentList")]
        public IActionResult GetTestDocumentList()
        {
            var testDocument = new[]
            {
                new {USL=1,UploadDate = DateTime.Now, AttachmentTitle = "breakdown", Note="Great Day Gift Ideas", Download = "Download paper", Control = "Stable" },
                new {USL=2,UploadDate = DateTime.Now, AttachmentTitle = "breakdown", Note="Great Day Gift Ideas", Download = "Download paper", Control = "Stable" },
                new {USL=3,UploadDate = DateTime.Now, AttachmentTitle = "breakdown", Note="Great Day Gift Ideas", Download = "Download paper", Control = "Stable" },
                new {USL=4,UploadDate = DateTime.Now, AttachmentTitle = "breakdown", Note="Great Day Gift Ideas", Download = "Download paper", Control = "Stable" },
                new {USL=5,UploadDate = DateTime.Now, AttachmentTitle = "breakdown", Note="Great Day Gift Ideas", Download = "Download paper", Control = "Stable" }
            }.ToList();
            return Ok(testDocument);
        }
        [HttpGet]
        [Route("GetHealthChartList")]
        public IActionResult GetHealthChartList()
        {
            var healthChart = new
            {
                bloodPressureSystolic = new[] { new { date = DateTime.UtcNow, Value = 120 } , new { date = DateTime.UtcNow, Value = 80 } },
                bloodPressureDiastolic = new[] { new { date = DateTime.UtcNow, Value = 130 }, new { date = DateTime.UtcNow, Value = 90 } },
                bloodGlucos = new[] { new { date = DateTime.UtcNow, Value = 12 } },
                bloodHemoglobin = new[] { new { date = DateTime.UtcNow, Value = 88 } },
                pulseRate = new[] { new { date = DateTime.UtcNow, Value = 15 } },
                bmi = new[] { new { date = DateTime.UtcNow, Value = 25 } },
                waist = new[] { new { date = DateTime.UtcNow, Value = 185 } },
                wasteHipRatio = new[] { new { date = DateTime.UtcNow, Value = 175 } },
                temperature = new[] { new { date = DateTime.UtcNow, Value = 245 } },
                bloodChlesterol = new[] { new { date = DateTime.UtcNow, Value = 215 } },
                uricAcid = new[] { new { date = DateTime.UtcNow, Value = 73 } },
                oxygenationOfBlood = new[] { new { date = DateTime.UtcNow, Value = 17 } }

            };
            return Ok(healthChart);
        }
        [HttpGet]
        [Route("GetPathologyList")]
        public IActionResult GetPathologyList()
        {
            var pathology = new[]
            {
                new {PSL =1, ReportID=121, AnalysisDate = DateTime.Now, MicroscopicImages="Image" },
                new {PSL =2, ReportID=123, AnalysisDate = DateTime.Now, MicroscopicImages="Image" },
                new {PSL =3, ReportID=124, AnalysisDate = DateTime.Now, MicroscopicImages="Image" },
                new {PSL =4, ReportID=125, AnalysisDate = DateTime.Now, MicroscopicImages="Image" },
                new {PSL =5, ReportID=126, AnalysisDate = DateTime.Now, MicroscopicImages="Image" }
            }.ToList();
            return Ok(pathology);
        }
        [HttpGet]
        [Route("GetEyeReportList")]
        public IActionResult GetEyeReportList()
        {
            var eyeReport = new[]
            {
                new {ERSL =1, Date=DateTime.Now,  VisionTest="Ok", PreliminaryTest="Ok", Investigation="Good", Refraction ="NULL", FinalExam ="Pass",  GlassPrescription="None", SummaryReport="Everything OK", Prescription="Done"},
                new {ERSL =2, Date=DateTime.Now,  VisionTest="Ok", PreliminaryTest="Ok", Investigation="Good", Refraction ="NULL", FinalExam ="Pass",  GlassPrescription="None", SummaryReport="Everything OK", Prescription="Done"},
                new {ERSL =3, Date=DateTime.Now,  VisionTest="Ok", PreliminaryTest="Ok", Investigation="Good", Refraction ="NULL", FinalExam ="Pass",  GlassPrescription="None", SummaryReport="Everything OK", Prescription="Done"},
                new {ERSL =4, Date=DateTime.Now,  VisionTest="Ok", PreliminaryTest="Ok", Investigation="Good", Refraction ="NULL", FinalExam ="Pass",  GlassPrescription="None", SummaryReport="Everything OK", Prescription="Done"},
                new {ERSL =5, Date=DateTime.Now,  VisionTest="Ok", PreliminaryTest="Ok", Investigation="Good", Refraction ="NULL", FinalExam ="Pass",  GlassPrescription="None", SummaryReport="Everything OK", Prescription="Done"}
            }.ToList();
            return Ok(eyeReport);
        }
        [HttpGet]
        [Route("GetChildrenCheckupList")]
        public IActionResult GetChildrenCheckupList()
        {
            var childrenCheckup = new[]
            {
                new {CCSL =1,Date=DateTime.Now, Height=165, Weight=65,  BMI=22.04, BTemp=96.5, SpO2=99, BP ="120/80", BG="6(PBS)", BH=12, UG = "-", UP = "-", PR = 78, MUAC = "MUAC", PatientColor ="Black", Pres="Pres", Action = "Action"}
            }.ToList();
            return Ok(childrenCheckup);
        }
        [HttpGet]
        [Route("GetConsultationReqList")]
        public IActionResult GetConsultationReqList()
        {
            var consultationReq = new[]
            {
                new {CRSL =1, RequestDate=DateTime.Now, ProblemsDescription ="Problems Description", ConsultancySchedule =DateTime.Now, Priority ="High", DepartmentId=258, Pres="Ok", Action ="Action"},
                new {CRSL =2, RequestDate=DateTime.Now, ProblemsDescription ="Problems Description", ConsultancySchedule =DateTime.Now, Priority ="High", DepartmentId=258, Pres="Ok", Action ="Action"},
                new {CRSL =3, RequestDate=DateTime.Now, ProblemsDescription ="Problems Description", ConsultancySchedule =DateTime.Now, Priority ="High", DepartmentId=258, Pres="Ok", Action ="Action"},
                new {CRSL =4, RequestDate=DateTime.Now, ProblemsDescription ="Problems Description", ConsultancySchedule =DateTime.Now, Priority ="High", DepartmentId=258, Pres="Ok", Action ="Action"},
                new {CRSL =5, RequestDate=DateTime.Now, ProblemsDescription ="Problems Description", ConsultancySchedule =DateTime.Now, Priority ="High", DepartmentId=258, Pres="Ok", Action ="Action"}

            }.ToList();
            return Ok(consultationReq);
        }
        [HttpGet]
        [Route("gphealthmemberprofile")]
        public IActionResult GetMemberProfile()
        {
            var list = new
            {
                PatientID = 134556,
                Email = "portalmember@gHealth.com",
                PhoneNo="01696969669",
                Gender = "Male",
                Age = 25,
                RegDate = "2023-02-08",
                BloodGroup = "B+",
                Address = "Dhaka",
                FullName = "Arefin Shokti",
                UserName = 1234556,
                ServiceSite = "Demonstration",
                DateofBirth = DateTime.Now,
                MaritalStatus = "Married",
                Guardian = "Test Gurdian",
                LastLogin = DateTime.Now,
                Note = "Serious"

            };
            return Ok(list);
        }

       
        [HttpGet]
        [Route("UrinReport")]
        public IActionResult UrinReport()
        {
            var urin = new[]
            {
                new {PSL =1, ReportID=12345, AnalysisDate = DateTime.Now, MicroscopicImages="Image" },
                new {PSL =2, ReportID=67899, AnalysisDate = DateTime.Now, MicroscopicImages="Image" },
                new {PSL =3, ReportID=75257, AnalysisDate = DateTime.Now, MicroscopicImages="Image" },
                new {PSL =4, ReportID=32435, AnalysisDate = DateTime.Now, MicroscopicImages="Image" },
                new {PSL =5, ReportID=64245, AnalysisDate = DateTime.Now, MicroscopicImages="Image" }
            }.ToList();
            return Ok(urin);
        }

        [HttpGet]
        [Route("HematologicalReport")]
        public IActionResult HematologicalReport()
        {
            var hemato = new[]
            {
                new {PSL =1, ReportID=43542, AnalysisDate = DateTime.Now, MicroscopicImages="Image" },
                new {PSL =2, ReportID=23445, AnalysisDate = DateTime.Now, MicroscopicImages="Image" },
                new {PSL =3, ReportID=23423, AnalysisDate = DateTime.Now, MicroscopicImages="Image" },
                new {PSL =4, ReportID=76897, AnalysisDate = DateTime.Now, MicroscopicImages="Image" },
                new {PSL =5, ReportID=68885, AnalysisDate = DateTime.Now, MicroscopicImages="Image" }
            }.ToList();
            return Ok(hemato);
        }

        [HttpGet]
        [Route("BiochemistryReport")]
        public IActionResult BiochemistryReport()
        {
            var bioche = new[]
            {
                new {PSL =1, ReportID=2544545, AnalysisDate = DateTime.Now, MicroscopicImages="Image" },
                new {PSL =2, ReportID=4546666, AnalysisDate = DateTime.Now, MicroscopicImages="Image" },
                new {PSL =3, ReportID=3245778, AnalysisDate = DateTime.Now, MicroscopicImages="Image" },
                new {PSL =4, ReportID=4545677, AnalysisDate = DateTime.Now, MicroscopicImages="Image" },
                new {PSL =5, ReportID=3435657, AnalysisDate = DateTime.Now, MicroscopicImages="Image" }
            }.ToList();
            return Ok(bioche);
        }

        [HttpGet]
        [Route("StoolAnalysisReport")]
        public IActionResult StoolAnalysisReport()
        {
            var stool = new[]
            {
                new {PSL =1, ReportID=98765565, AnalysisDate = DateTime.Now, MicroscopicImages="Image" },
                new {PSL =2, ReportID=56565768, AnalysisDate = DateTime.Now, MicroscopicImages="Image" },
                new {PSL =3, ReportID=23255657, AnalysisDate = DateTime.Now, MicroscopicImages="Image" },
                new {PSL =4, ReportID=34564786, AnalysisDate = DateTime.Now, MicroscopicImages="Image" },
                new {PSL =5, ReportID=00995566, AnalysisDate = DateTime.Now, MicroscopicImages="Image" }
            }.ToList();
            return Ok(stool);
        }

        [HttpGet]
        [Route("MicrobiologyReport")]
        public IActionResult MicrobiologyReport()
        {
            var micbio = new[]
            {
                new {PSL =1, ReportID=2125425, AnalysisDate = DateTime.Now, MicroscopicImages="Image" },
                new {PSL =2, ReportID=2323454, AnalysisDate = DateTime.Now, MicroscopicImages="Image" },
                new {PSL =3, ReportID=3565778, AnalysisDate = DateTime.Now, MicroscopicImages="Image" },
                new {PSL =4, ReportID=8676766, AnalysisDate = DateTime.Now, MicroscopicImages="Image" },
                new {PSL =5, ReportID=6789467, AnalysisDate = DateTime.Now, MicroscopicImages="Image" }
            }.ToList();
            return Ok(micbio);
        }

    }
}
