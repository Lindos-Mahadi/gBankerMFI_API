using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Utility.Helpers
{
    public class Base64File
    {
        public string MimeType { get; set; }
        public string Data { get; set; }
        public byte[] DataBytes { get; set; }
    }
    public static class ImageHelper
    {
        public static Base64File GetFileDetails(string url)
        {
            string[] file = url.Split(new Char[] { ':', ';', ',' });
            string mimeType = file[1];
            string data = file[3];
            byte[] dataBytes = Convert.FromBase64String(data);
            return new Base64File
            {
                Data = data,
                MimeType = mimeType,
                DataBytes = dataBytes
            };
        }
    }
}
