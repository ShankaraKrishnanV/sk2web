using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Brain_IQ.Startup))]
namespace Brain_IQ
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
