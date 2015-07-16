using Autofac;
using FLIGServer.Implementations;
using FLIGServer.Interfaces;

namespace FLIGServer.Modules
{
    public class FLIGServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FLIGClientListenerService>().As<IFLIGClientListener>();
        }
    }
}
