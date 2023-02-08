using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GC.MFI.Utility.Helpers
{
    public static class FileDecodeHelper
    {
        public static string Base64(string type ,byte[] file)
        {
            string ToBase64 = $"data:{type};base64," + Convert.ToBase64String(file);
            return ToBase64;
        }
    }
}
