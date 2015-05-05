
using System.Threading.Tasks;
namespace RecetarioUniversal.Services.Speech
{
    public interface ISpeechService
    {
        Task TextToSpeech(string message);
    }
}
