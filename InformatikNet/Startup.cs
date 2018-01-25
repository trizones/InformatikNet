using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InformatikNet.Startup))]
namespace InformatikNet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
