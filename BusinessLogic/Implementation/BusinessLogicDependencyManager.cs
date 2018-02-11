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
            builder.RegisterType<LoginsRepository>().As<ILoginsRepository>();
            builder.RegisterType<GroupsRepository>().As<IGroupsRepository>();
            builder.RegisterType<InvitationsRepository>().As<IInvitationsRepository>();
            builder.RegisterType<ParticipantsRepository>().As<IParticipantsRepository>();
            builder.RegisterType<LinksRepository>().As<ILinksRepository>();
        }
    }
}
