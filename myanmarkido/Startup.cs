using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(myanmarkido.Startup))]
namespace myanmarkido
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
