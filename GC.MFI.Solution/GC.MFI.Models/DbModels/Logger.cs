using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Models.DbModels
{
    public class Logger : DbModelBase
    {
        public string ControllerName { get; set; }
        public string MethodName { get; set; }
        public string EntryLevel { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
    }
}
