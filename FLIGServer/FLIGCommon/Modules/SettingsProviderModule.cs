using Autofac;
using FLIGCommon.Implementations;
using FLIGCommon.Interfaces;

namespace FLIGCommon.Modules
{
    public class SettingsProviderModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SettingsProvider>().As<ISettingsProvider>();
        }
    }
}
