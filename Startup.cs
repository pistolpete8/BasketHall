using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KosSala.Startup))]
namespace KosSala
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
