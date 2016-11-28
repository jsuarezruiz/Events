namespace RecetarioUniversal.Services.Speech
{
    using System;
    using System.Linq;
    using System.Diagnostics;
    using System.Globalization;
    using System.Threading.Tasks;
    using Windows.Media.SpeechSynthesis;
    using Windows.UI.Xaml.Controls;

    public class SpeechService : ISpeechService
    {
        //Consts 
        const VoiceGender Gender = VoiceGender.Female;

        //Variables
        static private SpeechSynthesizer _speech = null;
        MediaElement _mediaElement;

        /// <summary>
        /// Provides access to the functionality of an installed a speech synthesis engine.
        /// </summary>
        static private SpeechSynthesizer Speech
        {
            get { return _speech ?? (_speech = InitializeSpeech()); }
        }

        /// <summary>
        /// Initialize the SpeechSynthesizer.
        /// </summary>
        /// <returns></returns>
        static private SpeechSynthesizer InitializeSpeech()
        {
            // Initialize a new instance of the SpeechSynthesizer.
            var speech = new SpeechSynthesizer();
            var language = CultureInfo.CurrentCulture.ToString();
            var voices = SpeechSynthesizer.AllVoices.Where(v => v.Language == language).OrderByDescending(v => v.Gender);
            speech.Voice = voices.FirstOrDefault(v => v.Gender == Gender);

            return speech;
        }

        /// <summary>
        /// Synthetize a text into a speech and pronounces it.
        /// </summary>
        /// <param name="message">The message to be pronounced.</param>
        public async Task TextToSpeech(string message)
        {
            try
            {
                if (!string.IsNullOrEmpty(message))
                {
                    // Speak a string.
                    var result = await Speech.SynthesizeTextToStreamAsync(message);
                    _mediaElement = new MediaElement();
                    _mediaElement.SetSource(result, result.ContentType);
                    _mediaElement.Play();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SpeechServices", ex);
            }
        }

        /// <summary>
        /// Stop the current speech running.
        /// </summary>
        public void StopSpeech()
        {
            if (_speech != null && _mediaElement != null)
                _mediaElement.Stop();
        }
    }
}
