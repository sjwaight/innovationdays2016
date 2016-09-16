using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Demo.Innovationdays16.Web.Startup))]
namespace Demo.Innovationdays16.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
