using Autofac;
using Autofac.Integration.Wcf;
using FLIGCommon.Modules;

namespace FLIGCommon
{
    public class ContainerConfiguration
    {
        public IContainer Create()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<UserDetailsModule>();
            builder.RegisterModule<SettingsProviderModule>();

            IContainer rootContainer = builder.Build();
            //AutofacHostFactory.Container = rootContainer;
            Container.Instance = rootContainer;

            return rootContainer;
        }
    }
}
