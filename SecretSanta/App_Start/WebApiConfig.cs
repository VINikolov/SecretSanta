using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using BusinessLogic.Implementation;
using Models.DataTransferModels;
using SecretSanta.Controllers;
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
            DependencyManager.RegisterDependencies(builder);

            builder.RegisterType<GlobalErrorHandler>().AsWebApiExceptionFilterFor<ApiController>();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(config);

            builder.RegisterType<AuthenticationFilterAttribute>().AsWebApiActionFilterFor<LoginsController>(x => x.Logout(default(string)));
            builder.RegisterType<AuthenticationFilterAttribute>().AsWebApiActionFilterFor<UsersController>(x => x.GetPagedUsers(default(int), default(int), default(string), default(string)));
            builder.RegisterType<AuthenticationFilterAttribute>().AsWebApiActionFilterFor<UsersController>(x => x.GetUser(default(string)));
            builder.RegisterType<AuthenticationFilterAttribute>().AsWebApiActionFilterFor<UsersController>(x => x.GetGroupsForUser(default(string), default(int), default(int)));
            builder.RegisterType<AuthenticationFilterAttribute>().AsWebApiActionFilterFor<GroupsController>().InstancePerRequest();
            builder.RegisterType<AuthenticationFilterAttribute>().AsWebApiActionFilterFor<InvitationsController>().InstancePerRequest();
            builder.RegisterType<AuthenticationFilterAttribute>().AsWebApiActionFilterFor<LinksController>().InstancePerRequest();

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
