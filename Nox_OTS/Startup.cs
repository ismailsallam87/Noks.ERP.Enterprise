using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Nox_OTS.Startup))]
namespace Nox_OTS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
           // app.MapSignalR();
        }
    }
}
