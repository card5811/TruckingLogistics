using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TruckingLogistics.WebMVC.Startup))]
namespace TruckingLogistics.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
