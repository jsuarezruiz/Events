using Windows.UI.Xaml;

namespace Xbox_Sounds.Services.Sounds
{
    public class SoundService : ISoundService
    {
        public enum SoundKind
        {
            Focus,
            GoBack,
            Hide,
            Invoke,
            MoveNext,
            MovePrevious,
            Show
        };

        public void On()
        {
            ElementSoundPlayer.State = ElementSoundPlayerState.On;
        }

        public void Off()
        {
            ElementSoundPlayer.State = ElementSoundPlayerState.Off;
        }

        public void ChangeVolume(double volume)
        {
            if (volume < 0.0f)
                volume = 0.0f;

            if (volume > 1.0f)
                volume = 1.0f;

            ElementSoundPlayer.Volume = volume;
        }

        public void Play(SoundKind soundKind)
        {
            switch(soundKind)
            {
                case SoundKind.Focus:
                    ElementSoundPlayer.Play(ElementSoundKind.Focus);
                    break;
                case SoundKind.GoBack:
                    ElementSoundPlayer.Play(ElementSoundKind.GoBack);
                    break;
                case SoundKind.Hide:
                    ElementSoundPlayer.Play(ElementSoundKind.Hide);
                    break;
                case SoundKind.Invoke:
                    ElementSoundPlayer.Play(ElementSoundKind.Invoke);
                    break;
                case SoundKind.MoveNext:
                    ElementSoundPlayer.Play(ElementSoundKind.MoveNext);
                    break;
                case SoundKind.MovePrevious:
                    ElementSoundPlayer.Play(ElementSoundKind.MovePrevious);
                    break;
                case SoundKind.Show:
                    ElementSoundPlayer.Play(ElementSoundKind.Show);
                    break;
            }
        }
    }
}
