using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ass8_GoogleDrive.Startup))]
namespace Ass8_GoogleDrive
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
