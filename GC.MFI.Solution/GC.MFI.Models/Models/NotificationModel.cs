using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Models.Models
{
    public class NotificationModel
    {
        [JsonProperty("topic")]
        public string Topic { get; set; }

        public FireBaseNotificationModel Notification { get; set; }
    }

    public class FireBaseNotificationModel
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }
    }
}
