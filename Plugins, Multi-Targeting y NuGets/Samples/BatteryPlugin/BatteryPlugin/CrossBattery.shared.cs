using Plugin.BatteryPlugin.Abstractions;
using System;

namespace Plugin.BatteryPlugin
{
    /// <summary>
    /// Cross platform Battery implemenations
    /// </summary>
    public class CrossBattery
    {
        static Lazy<IBattery> Implementation = new Lazy<IBattery>(() => CreateBattery(), System.Threading.LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
		/// Gets if the plugin is supported on the current platform.
		/// </summary>
		public static bool IsSupported => Implementation.Value == null ? false : true;

        /// <summary>
        /// Current plugin implementation to use
        /// </summary>
        public static IBattery Current
        {
            get
            {
                var ret = implementation.Value;

                if (ret == null)
                {
                    throw NotImplementedInReferenceAssembly();
                }

                return ret;
            }
        }

        static IBattery CreateBattery()
        {
#if NETSTANDARD1_0 || NETSTANDARD2_0
            return null;
#else
            return new BatteryImplementation();
#endif
        }

        internal static Exception NotImplementedInReferenceAssembly()
        {
            return new NotImplementedException("This functionality is not implemented in the portable version of this assembly.  You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
        }



        /// <summary>
        /// Dispose of everything 
        /// </summary>
        public static void Dispose()
        {
            if (Implementation != null && Implementation.IsValueCreated)
            {
                Implementation.Value.Dispose();
                Implementation = new Lazy<IBattery>(() => CreateBattery(), System.Threading.LazyThreadSafetyMode.PublicationOnly);
            }
        }
    }
}
