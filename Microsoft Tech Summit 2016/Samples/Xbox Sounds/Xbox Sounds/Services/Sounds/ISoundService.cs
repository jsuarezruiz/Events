using static Xbox_Sounds.Services.Sounds.SoundService;

namespace Xbox_Sounds.Services.Sounds
{
    public interface ISoundService
    {
        void On();
        void Off();
        void ChangeVolume(double volume);
        void Play(SoundKind soundKind);
    }
}
