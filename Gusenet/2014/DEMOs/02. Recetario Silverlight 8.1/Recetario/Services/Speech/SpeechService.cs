using System;
using System.Linq;
using Windows.Phone.Speech.Synthesis;

namespace Recetario.Services.Speech
{
    public class SpeechService : ISpeechService
    {
        public async void TextToSpeech(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                var synth = new SpeechSynthesizer();

                IOrderedEnumerable<VoiceInformation> voices =
                    InstalledVoices.All.Where(v => v.Language == "es-ES").OrderByDescending(v => v.Gender);

                const VoiceGender gender = VoiceGender.Female;

                synth.SetVoice(voices.FirstOrDefault(v => v.Gender == gender));

                await synth.SpeakTextAsync(text);
            }
        }
    }
}