using System;

namespace Plugin.BatteryPlugin.Abstractions
{
    public abstract class BaseBatteryImplementation : IBattery, IDisposable
    {
        private bool disposed = false;

        ~BaseBatteryImplementation()
        {
            Dispose(false);
        }

        public virtual int GetBatteryStatus()
        {
            throw new NotImplementedException();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Dispose only
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
