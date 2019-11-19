using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Examen_Practico.Startup))]
namespace Examen_Practico
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
