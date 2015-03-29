using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HFi.Startup))]
namespace HFi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
