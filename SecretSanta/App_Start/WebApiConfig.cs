using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using BusinessLogic.Implementation;
using BusinessLogic.Interfaces;
using SecretSanta.CrossDomain;

namespace SecretSanta
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();
            //register dependencies
            BusinessLogicDependencyManager.RegisterDependencies(builder);
            builder.RegisterType<UsersManager>().As<IUsersManager>();
            builder.RegisterType<LoginsManager>().As<ILoginsManager>();

            builder.RegisterType<GlobalErrorHandler>().AsWebApiExceptionFilterFor<ApiController>();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(config);

            IContainer container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
