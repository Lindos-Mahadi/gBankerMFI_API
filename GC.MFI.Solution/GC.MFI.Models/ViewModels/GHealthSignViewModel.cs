using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Models.ViewModels
{
    public class GHealthSignViewModel
    {
        public List<SiteInfo> site_info { get; set; }
        public Account account { get; set; }
        public int success { get; set; }
        public string api_key { get; set; }
    }

    public class SiteInfo
    {
        public string project_id { get; set; }
        public string project_title { get; set; }
        public string site_title { get; set; }
        public string address_1 { get; set; }
        public string address_2 { get; set; }
        public string phone { get; set; }
        public string auto_barcode_generator { get; set; }
        public string logo_image { get; set; }
    }
    public class Account
    {
        public string id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string createdon { get; set; }
        public object verifiedon { get; set; }
        public string lastsignedinon { get; set; }
        public object resetsenton { get; set; }
        public object deletedon { get; set; }
        public object suspendedon { get; set; }
        public string account_id { get; set; }
        public string fullname { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string dateofbirth { get; set; }
        public string gender { get; set; }
        public object skype_id { get; set; }
        public object postalcode { get; set; }
        public string country { get; set; }
        public string language { get; set; }
        public string timezone { get; set; }
        public object citimezone { get; set; }
        public string currency { get; set; }
        public object picture { get; set; }
    }

}
