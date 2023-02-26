using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Collections.Generic;

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
                new  {SL = 1, ReferenceNo = "1", Date = DateTime.Now, Total = 5000, Discount = 100},
                new  {SL = 2, ReferenceNo = "2", Date = DateTime.Now, Total = 5000, Discount = 100},
                new  {SL = 2, ReferenceNo = "1", Date = DateTime.Now, Total = 5000, Discount = 100},
                new  {SL = 4, ReferenceNo = "2", Date = DateTime.Now, Total = 5000, Discount = 100},
                new  {SL = 5, ReferenceNo = "1", Date = DateTime.Now, Total = 5000, Discount = 100},
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
                new {SLNo = 1, Date = DateTime.Now, Height = 165, Wheight = 60, BMI=22.04, Waist=75, Hip=95, WH = 0.79, BTemp = 96.5, SpO2 = 99, BP="120/80", BG = "6(PBS)", BH=12, UG = "-", UP = "-", UU="+-", PR = 78, Arr = "Normal", Cho = 150, Uric=5},
                new {SLNo = 2, Date = DateTime.Now, Height = 165, Wheight = 60, BMI=22.04, Waist=75, Hip=95, WH = 0.79, BTemp = 96.5, SpO2 = 99, BP="120/80", BG = "6(PBS)", BH=12, UG = "-", UP = "-", UU="+-", PR = 78, Arr = "Normal", Cho = 150, Uric=5},
                new {SLNo = 3, Date = DateTime.Now, Height = 165, Wheight = 60, BMI=22.04, Waist=75, Hip=95, WH = 0.79, BTemp = 96.5, SpO2 = 99, BP="120/80", BG = "6(PBS)", BH=12, UG = "-", UP = "-", UU="+-", PR = 78, Arr = "Normal", Cho = 150, Uric=5},
                new {SLNo = 4, Date = DateTime.Now, Height = 165, Wheight = 60, BMI=22.04, Waist=75, Hip=95, WH = 0.79, BTemp = 96.5, SpO2 = 99, BP="120/80", BG = "6(PBS)", BH=12, UG = "-", UP = "-", UU="+-", PR = 78, Arr = "Normal", Cho = 150, Uric=5},
                new {SLNo = 5, Date = DateTime.Now, Height = 165, Wheight = 60, BMI=22.04, Waist=75, Hip=95, WH = 0.79, BTemp = 96.5, SpO2 = 99, BP="120/80", BG = "6(PBS)", BH=12, UG = "-", UP = "-", UU="+-", PR = 78, Arr = "Normal", Cho = 150, Uric=5}
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
            var healthChart = new[]
            {
                new {HCSL =1, BloodPressureSystolic = 150, BloodPressureDiastolic=100,BloodGlucos = 100,BloodHemoglobin=15, PulseRate = 100, BMI=30, Waist = 100, WasteHipRatio = 0.75, Temperature = 125, BloodChlesterol = 200,UricAcid = 6, OxygenationOfBlood=125},
                new {HCSL =2, BloodPressureSystolic = 150, BloodPressureDiastolic=100,BloodGlucos = 100,BloodHemoglobin=15, PulseRate = 100, BMI=30, Waist = 100, WasteHipRatio = 0.75, Temperature = 125, BloodChlesterol = 200,UricAcid = 6, OxygenationOfBlood=125},
                new {HCSL =3, BloodPressureSystolic = 150, BloodPressureDiastolic=100,BloodGlucos = 100,BloodHemoglobin=15, PulseRate = 100, BMI=30, Waist = 100, WasteHipRatio = 0.75, Temperature = 125, BloodChlesterol = 200,UricAcid = 6, OxygenationOfBlood=125},
                new {HCSL =4, BloodPressureSystolic = 150, BloodPressureDiastolic=100,BloodGlucos = 100,BloodHemoglobin=15, PulseRate = 100, BMI=30, Waist = 100, WasteHipRatio = 0.75, Temperature = 125, BloodChlesterol = 200,UricAcid = 6, OxygenationOfBlood=125},
                new {HCSL =5, BloodPressureSystolic = 150, BloodPressureDiastolic=100,BloodGlucos = 100,BloodHemoglobin=15, PulseRate = 100, BMI=30, Waist = 100, WasteHipRatio = 0.75, Temperature = 125, BloodChlesterol = 200,UricAcid = 6, OxygenationOfBlood=125}
            }.ToList();
            return Ok(healthChart);
        }
        [HttpGet]
        [Route("GetPathologyList")]
        public IActionResult GetPathologyList()
        {
            var pathology = new[]
            {
                new {PSL =1, ReportID=12345, AnalysisDate = DateTime.Now, MicroscopicImages="Image" },
                new {PSL =2, ReportID=12345, AnalysisDate = DateTime.Now, MicroscopicImages="Image" },
                new {PSL =3, ReportID=12345, AnalysisDate = DateTime.Now, MicroscopicImages="Image" },
                new {PSL =4, ReportID=12345, AnalysisDate = DateTime.Now, MicroscopicImages="Image" },
                new {PSL =5, ReportID=12345, AnalysisDate = DateTime.Now, MicroscopicImages="Image" }
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
                new {CRSL =1, RequestDate=DateTime.Now, ProblemsDescription ="Problems Description", ConsultancySchedule =DateTime.Now, Priority ="High", DepartmentId=258, Pres="Ok", Action ="Action"}
            }.ToList();
            return Ok(consultationReq);
        }
    }
}
