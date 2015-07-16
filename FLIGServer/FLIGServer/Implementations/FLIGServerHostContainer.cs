using System;
using System.Collections.Generic;
using System.ServiceModel;
using Autofac;
using FLIGServer.Interfaces;
using System.Xml;
using Autofac.Core;
using Autofac.Integration.Wcf;
using System.ServiceModel.Description;
using System.Linq;

namespace FLIGServer.Implementations
{
    public class FLIGServerHostContainer
    {
        private readonly string serviceHostAddress;
        private List<ServiceHost> hostedServices = new List<ServiceHost>();
        private const string BaseAddressFormat = "http://{0}/{1}";
        private readonly IContainer container;

        public FLIGServerHostContainer(IContainer container, string serviceHost = "localhost:4554")
        {
            this.serviceHostAddress = serviceHost;
            this.container = container;
        }

        public void StartHostedServices()
        {
            var baseAddress = new Uri(string.Format(BaseAddressFormat, this.serviceHostAddress, "ClientListener.svc"));

            var host = new ServiceHost(typeof(FLIGClientListenerService), baseAddress);
            host.AddServiceEndpoint(typeof(IFLIGClientListener), GetBinding(), string.Empty);

            IComponentRegistration registration;
            if (!this.container.ComponentRegistry.TryGetRegistration(new TypedService(typeof(IFLIGClientListener)), out registration))
            {
                throw new Exception("The IFLIGClientListener service contract has not been registered in the container");
            }

            //host.AddDependencyInjectionBehavior<IFLIGClientListener>(this.container);

            host.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true, HttpGetUrl = baseAddress });

            var serviceDebugBehaviour = (ServiceDebugBehavior)host.Description.Behaviors.FirstOrDefault(b => b is ServiceDebugBehavior);
            if (serviceDebugBehaviour != null)
            {
                serviceDebugBehaviour.IncludeExceptionDetailInFaults = true;
            }

            Console.WriteLine("FLIG Client Listener started on {0}", baseAddress);

            host.Open();

            this.hostedServices.Add(host);
        }

        private static BasicHttpBinding GetBinding()
        {
            var binding = new BasicHttpBinding(BasicHttpSecurityMode.Message)
            {
                Name = "basicHttpBindingIRISClient1",
                MaxBufferSize = 2147483647,
                MaxReceivedMessageSize = 2147483647,
                ReaderQuotas = { MaxStringContentLength = 2147483647, MaxDepth = 32, MaxBytesPerRead = 2147483647 }
            };
            binding.ReaderQuotas.MaxBytesPerRead = 4096;
            binding.ReaderQuotas.MaxNameTableCharCount = 2147483647;
            binding.Security.Mode = BasicHttpSecurityMode.None;
            return binding;
        }

        internal void StopHostedServices()
        {
            foreach (var host in this.hostedServices)
            {
                host.Close();
            }
        }
    }
}
