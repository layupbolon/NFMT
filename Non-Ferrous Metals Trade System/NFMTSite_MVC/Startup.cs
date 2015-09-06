using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NFMTSite_MVC.Startup))]
namespace NFMTSite_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
