using Autofac;
using DataAccess.Implementation;
using DataAccess.Interfaces;

namespace BusinessLogic.Implementation
{
    public class BusinessLogicDependencyManager
    {
        public static void RegisterDependencies(ContainerBuilder builder)
        {
            builder.RegisterType<UsersRepository>().As<IUsersRepository>();
        }
    }
}
