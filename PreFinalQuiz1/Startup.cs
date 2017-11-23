using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PreFinalQuiz1.Startup))]
namespace PreFinalQuiz1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
