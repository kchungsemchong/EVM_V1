using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EVM.Startup))]
namespace EVM
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
