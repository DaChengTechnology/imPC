using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMPLib;
using AMRPlayer;

namespace ChangLiao.Util
{
    /// <summary>
    /// 声音播放类库
    /// </summary>
    class AudioPlayer
    {
        static AudioPlayer instance;
        private static readonly object padlock = new object();
        private WindowsMediaPlayer  player;
        private AMRPlayer.AMRPlayer amr;

        public static AudioPlayer shard
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new AudioPlayer();
                    }
                    return instance;
                }
            }
        }

        private AudioPlayer()
        {
            player = new WindowsMediaPlayer();
            amr = new AMRPlayer.AMRPlayer();
        }
        /// <summary>
        /// 播放提示音
        /// </summary>
        public void playTips()
        {
            try
            {
                if (amr.IsPlaying())
                {
                    return;
                }
                if (player.playState != WMPPlayState.wmppsPlaying)
                {
                    player.URL = Directory.GetCurrentDirectory() + "\\ding.mp3";
                    player.controls.play();
                }
            }
            catch
            {

            }
        }
        /// <summary>
        /// 播放amr
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public void playAMR(string filePath)
        {
            try
            {
                if (player.playState == WMPPlayState.wmppsPlaying)
                {
                    player.controls.stop();
                }
                if (!amr.IsPlaying())
                {
                    amr.Stop();
                }
                uint durent;
                amr.Play(filePath, out durent);
            }
            catch
            {

            }
        }
    }
}
