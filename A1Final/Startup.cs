using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(A1Final.Startup))]
namespace A1Final
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
