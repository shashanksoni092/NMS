using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NMS.Startup))]
namespace NMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
