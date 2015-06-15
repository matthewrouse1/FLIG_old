using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.Wcf;
using FLIGServer.Implementations;
using FLIGServer.Interfaces;

namespace FLIGServer
{
    public class ContainerConfiguration
    {
        public IContainer Create()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<DataAccess>().As<IDataAccess>().InstancePerLifetimeScope();

            var container = builder.Build();
            //AutofacHostFactory.Container = container;

            return container;
        }
    }
}
