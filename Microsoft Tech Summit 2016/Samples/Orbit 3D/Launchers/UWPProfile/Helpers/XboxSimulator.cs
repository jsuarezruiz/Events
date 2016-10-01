namespace Orbit.Helpers
{
    public static class XboxSimulator
    {
        private static bool _isActivate;

        public static bool IsActivate { get { return _isActivate; } }

        public static void Activate()
        {
            _isActivate = true;
        }

        public static void Deactivate()
        {
            _isActivate = false;
        }
    }
}
