using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UserForm.Startup))]
namespace UserForm
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
