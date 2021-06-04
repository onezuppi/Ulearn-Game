using System.IO;
using System.Media;

namespace Game.Domain
{
    public class MusicPlayer
    {
        private const string PathToMusic = @"../../music/a10a0faf6215bb7.wav";
        private readonly SoundPlayer simpleSound;
        public bool IsPlaying { get; private set; }

        public MusicPlayer()
        {
            if (!File.Exists(PathToMusic))
                return;
            simpleSound = new SoundPlayer(PathToMusic);
        }

        public void Start()
        {
            IsPlaying = true;
            simpleSound.PlayLooping();
        }

        public void Stop()
        {
            IsPlaying = false;
            simpleSound.Stop();
        }
    }
}