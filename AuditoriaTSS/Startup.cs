using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AimgosWeb.Startup))]
namespace AimgosWeb
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
