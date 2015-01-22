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
            { "wmplayer",      () => new WindowsMediaPlayer() },
            { "iTunes",        () => new iTunes() },
            { "x-APPLICATION", () => new XApplication() },
            { "x-APPLISMO",    () => new LismoPort() },
            { "foobar2000",    () => new NowPlayingLib.Foobar2000() }
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
