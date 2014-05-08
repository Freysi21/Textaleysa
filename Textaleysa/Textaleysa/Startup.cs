using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Textaleysa.Startup))]
namespace Textaleysa
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
