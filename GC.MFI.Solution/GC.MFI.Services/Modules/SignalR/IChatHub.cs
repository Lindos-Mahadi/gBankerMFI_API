using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Services.Modules.SignalR
{
    public interface IChatHub
    {
        Task SendMessage(string user, string message);
    }
}
