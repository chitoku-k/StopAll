using NowPlayingLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace StopAll
{
    public class MediaPlayersManager
    {
        private static readonly Dictionary<string, Type> PlayerTypes = new Dictionary<string, Type>
        {
            { "wmplayer", typeof(WindowsMediaPlayer) },
            { "iTunes", typeof(iTunes) },
            { "x-APPLICATION", typeof(XApplication) },
            { "x-APPLISMO", typeof(LismoPort) },
            { "foobar2000", typeof(NowPlayingLib.Foobar2000) }
        };

        public static void PauseAll()
        {
            foreach (var t in Process.GetProcesses().Select(x => x.ProcessName).Intersect(PlayerTypes.Keys).Select(x => PlayerTypes[x]))
            {
                try
                {
                    using (var player = Activator.CreateInstance(t) as MediaPlayerBase)
                    {
                        player.Pause();
                    }
                }
                catch { }
            }
        }
    }
}
