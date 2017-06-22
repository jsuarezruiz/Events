using Microsoft.Owin;
using Owin;
using Xamagram.Azure.Backend;

[assembly: OwinStartup(typeof(Startup))]

namespace Xamagram.Azure.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}