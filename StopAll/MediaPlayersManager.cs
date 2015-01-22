using NowPlayingLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace StopAll
{
    public class MediaPlayersManager
    {
        private static readonly Dictionary<string, Func<MediaPlayerBase>> PlayerConstructors = new Dictionary<string, Func<MediaPlayerBase>>
        {
            { WindowsMediaPlayer.ProcessName,       () => new WindowsMediaPlayer() },
            { iTunes.ProcessName,                   () => new iTunes() },
            { XApplication.ProcessName,             () => new XApplication() },
            { LismoPort.ProcessName,                () => new LismoPort() },
            { NowPlayingLib.Foobar2000.ProcessName, () => new NowPlayingLib.Foobar2000() }
        };

        public static void PauseAll()
        {
            Parallel.ForEach(Process.GetProcesses().Select(x => x.ProcessName).Intersect(PlayerConstructors.Keys), processName =>
            {
                try
                {
                    using (var player = PlayerConstructors[processName]())
                    {
                        player.Pause();
                    }
                }
                catch { }
            });
        }
    }
}
