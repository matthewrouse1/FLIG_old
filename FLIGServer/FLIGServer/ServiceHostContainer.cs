using GitInterface;
using FLIGCommon;
using Autofac;
using FLIGCommon.Interfaces;

namespace FLIGServer
{
    public class ServiceHostContainer
    {
        private IContainer container;

        public bool Start()
        {
            this.container = new FLIGCommon.ContainerConfiguration().Create();
            var userDetails = container.Resolve<IUserDetails>();
            
            

            //var container = new ContainerConfiguration().Create();
            //var DataAccess = container.ComponentRegistry.Registrations.

            return true;
        }

        public bool Stop()
        {
            return true;
        }
    }
}
