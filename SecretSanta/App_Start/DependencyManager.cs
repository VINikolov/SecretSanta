using Autofac;
using BusinessLogic.Implementation;
using BusinessLogic.Interfaces;

namespace SecretSanta
{
    public static class DependencyManager
    {
        public static void RegisterDependencies(ContainerBuilder builder)
        {
            builder.RegisterType<UsersManager>().As<IUsersManager>();
            builder.RegisterType<LoginsManager>().As<ILoginsManager>();
            builder.RegisterType<GroupsManager>().As<IGroupsManager>();
            builder.RegisterType<InvitationsManager>().As<IInvitationsManager>();
            builder.RegisterType<ParticipantsManager>().As<IParticipantsManager>();
        }
    }
}