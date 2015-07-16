using Topshelf;

using System.Security;
using System;
using System.Runtime.InteropServices;

namespace FLIGServer
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(c =>
            {
                c.Service<ServiceHostContainer>(s =>
                {
                    s.ConstructUsing(name => new ServiceHostContainer());
                    s.WhenStarted((service, control) => service.Start(args));
                    s.WhenStopped((service, control) => service.Stop());
                });

                c.RunAsLocalSystem();
                c.SetDescription("File Locking in git Service Host");
                c.SetDisplayName("FLIG Service Host");
                c.SetServiceName("FLIGServer");
            });
        }
    }
}
