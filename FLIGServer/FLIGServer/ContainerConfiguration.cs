using Autofac;
using Autofac.Integration.Wcf;
using FLIGCommon.Modules;
using FLIGServer.Modules;

namespace FLIGServer
{
    public class ContainerConfiguration
    {
        public IContainer Create()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<UserDetailsModule>();
            builder.RegisterModule<SettingsProviderModule>();
            builder.RegisterModule<FLIGServiceModule>();

            IContainer rootContainer = builder.Build();
            //AutofacHostFactory.Container = rootContainer;
            Container.Instance = rootContainer;

            return rootContainer;
        }
    }
}
