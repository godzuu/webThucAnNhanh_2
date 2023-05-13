using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(webThucAnNhanh.Startup))]
namespace webThucAnNhanh
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
