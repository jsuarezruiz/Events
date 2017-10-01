using System.Runtime.Serialization;
using WaveEngine.Common.Media;
using WaveEngine.Framework;
using WaveEngine.Framework.Services;
using WaveEngine.Framework.Sound;
using WaveEngine.Hololens.Speech;

namespace HoloPlanets
{
    [DataContract]
    public class VoiceCommands : Component
    {
        private const string MusicTag = "Music";
        private const string TurnMusicOn = "Turn Music On";
        private const string TurnMusicOff = "Turn Music Off";

        private KeywordRecognizerService _keywordService;
        private MusicInfo _music;

        protected override void DefaultValues()
        {
            base.DefaultValues();
        }

        protected override void Initialize()
        {
            base.Initialize();

            _music = new MusicInfo(WaveContent.Assets.Sounds.background_wav);
            _keywordService = WaveServices.GetService<KeywordRecognizerService>();

            if (_keywordService != null)
            {
                _keywordService.Keywords = new string[] { TurnMusicOn, TurnMusicOff };
                _keywordService.Start();
                _keywordService.OnKeywordRecognized += OnKeywordRecognized;
            }
        }

        private void OnKeywordRecognized
            (KeywordRecognizerResult result)
        {
            switch (result.Text)
            {
                case TurnMusicOn:
                    TurnMusic(true);
                    break;
                case TurnMusicOff:
                    TurnMusic(false);
                    break;
            }
        }

        private void TurnMusic(bool state)
        {
            if (state)
            {
                WaveServices.MusicPlayer.Play(_music);
            }
            else
            {
                WaveServices.MusicPlayer.Stop();
            }
        }
    }
}