using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Projet_Heritage.Startup))]
namespace Projet_Heritage
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
