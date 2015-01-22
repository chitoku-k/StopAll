using System;

namespace StopAll
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            MediaPlayersManager.PauseAll();
            SkypeManager.SetMute();
        }
    }
}
