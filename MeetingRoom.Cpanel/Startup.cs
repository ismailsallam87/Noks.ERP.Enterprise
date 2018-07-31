using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MeetingRoom.Cpanel.Startup))]
namespace MeetingRoom.Cpanel
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
