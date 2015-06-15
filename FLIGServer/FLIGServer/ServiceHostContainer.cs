using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FLIGServer
{
    public class ServiceHostContainer
    {
        public bool Start()
        {
            var container = new ContainerConfiguration().Create();
            //var DataAccess = container.ComponentRegistry.Registrations.

            return true;
        }

        public bool Stop()
        {
            return true;
        }
    }
}
