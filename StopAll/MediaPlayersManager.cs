using NowPlayingLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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
            foreach (var func in Process.GetProcesses().Select(x => x.ProcessName).Intersect(PlayerConstructors.Keys).Select(x => PlayerConstructors[x]))
            {
                try
                {
                    using (var player = func())
                    {
                        player.Pause();
                    }
                }
                catch { }
            }
        }
    }
}
