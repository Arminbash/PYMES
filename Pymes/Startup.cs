using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Pymes.Startup))]
namespace Pymes
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
