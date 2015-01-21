using NowPlayingLib.Interop;
using SKYPE4COMLib;
using System.Diagnostics;
using System.Linq;

namespace StopAll
{
    public class SkypeManager
    {
        public static void SetMute()
        {
            if (Process.GetProcesses().Any(x => x.ProcessName == "Skype"))
            {
                using (var app = ComWrapper.Create<ISkype>(new Skype()))
                {
                    app.Object.Mute = true;
                }
            }
        }
    }
}
