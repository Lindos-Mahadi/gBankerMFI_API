using XenterSolution.Models.ViewModels;

namespace GC.MFI.Models.ViewModels
{
    public class LoggerViewModel : ViewModelBase
    {
        public string ControllerName { get; set; }
        public string MethodName { get; set; }
        public string EntryLevel { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
    }
}
