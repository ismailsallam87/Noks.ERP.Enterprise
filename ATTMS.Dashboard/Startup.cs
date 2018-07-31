using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ATTMS.Dashboard.Startup))]
namespace ATTMS.Dashboard
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
