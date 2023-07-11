using GC.MFI.Models.DbModels;
using GC.MFI.Models.DbModels.BaseModels;
using GC.MFI.Models.Models;
using GC.MFI.Services;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XenterSolution.Models.ViewModels;

namespace GC.MFI.WebApi.Controllers
{
   // [Authorize(Roles = "PortalMember, PortalAdmin")]
    [ApiController]
    public class GCMcfinaLegacyBaseController<TDbModel> : ControllerBase
        where TDbModel : class, ILegacyDbModelBase
    {
        private readonly ILegacyServiceBase<TDbModel> service;
        public GCMcfinaLegacyBaseController(ILegacyServiceBase<TDbModel> service)
        {
            this.service = service;
        }
        #region Common Actions
        [HttpGet]
        [Route("getall")]
        public virtual IEnumerable<TDbModel> GetAll()
        {
            try
            {
                var results = service.GetAll();
                return results;
            }
            catch (Exception ex)
            {
                LogError(ex, null);
                throw;
            }
        }
        [HttpGet]
        [Route("getbyid")]
        public virtual TDbModel GetById(long id)
        {
            try
            {
                var record = service.GetById(id);
                return record;
            }
            catch (Exception ex)
            {
                LogError(ex, null);
                throw;
            }
        }
        #endregion

        #region Error Handling
        protected void LogError(Exception ex, ViewModelBase viewModel)
        {
            //Log error and return customized error request...

        }
        #endregion


        [Route("firebasesendnotification")]
        [HttpPost]
        public IActionResult FirebaseSendNotification(NotificationModel notificationModel, string FirebaseServerKey)
        {

            using (HttpClient client = new HttpClient())
            {

                //  string TransactionID = "6334360607";

                string url = $"https://fcm.googleapis.com/fcm/send";

                Uri baseUri = new Uri(url);


                var requestMessage = new HttpRequestMessage(HttpMethod.Post, baseUri);

                requestMessage.Headers.Add("Content", "application/json");
                //requestMessage.Headers.Add("Authorization", "key=AAAAURtTO1I:APA91bE46TadXT9YXOogb4CPIF_mito-aTxqRN1s0ZlOjIuECLj9YrS5gdj87plXKP1CjRzcB53orr4TRgur5AdEzGYQ8GGe6Mmbjv1DRCZbPUNRH4QgBM8Yu-emjMWnJLnQuC98m1In");
                requestMessage.Headers.Add("Authorization", "bearer " + FirebaseServerKey);


                requestMessage.Content =
                    JsonContent.Create(new
                    {
                        to = "/topics/" + notificationModel.Topic,
                        notification = new
                        {
                            body = notificationModel.Notification.Body,
                            title = notificationModel.Notification.Title
                        }
                    });

                //make the request

                var task = client.SendAsync(requestMessage);

                var response = task.Result;

                response.EnsureSuccessStatusCode();

                string responseBody = response.Content.ReadAsStringAsync().Result;

            }

            //var result = await _notificationService.SendNotification(notificationModel);
            return Ok(notificationModel);
        }


    }
}
