using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SoftwareEngineeringProject.Startup))]
namespace SoftwareEngineeringProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
