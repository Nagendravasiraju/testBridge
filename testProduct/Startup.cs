using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(testProduct.Startup))]
namespace testProduct
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
