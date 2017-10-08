using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(knipsTest.Startup))]
namespace knipsTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
