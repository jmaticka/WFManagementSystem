using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WFManagementSystem.Startup))]
namespace WFManagementSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
        
    }
}
