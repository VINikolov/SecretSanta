using System.Web.Http;

namespace SecretSanta
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AutoMapperConfiguration.Register();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
