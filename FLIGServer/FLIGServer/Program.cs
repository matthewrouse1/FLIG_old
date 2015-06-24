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
            var secure = new SecureString();
            foreach (var c in "welcome4".ToCharArray())
            {
                secure.AppendChar(c);
            }
            Console.WriteLine(secure);
            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                Console.WriteLine(Marshal.PtrToStringUni(unmanagedString));
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }

            HostFactory.Run(c =>
            {
                c.Service<ServiceHostContainer>(s =>
                {
                    s.ConstructUsing(name => new ServiceHostContainer());
                    s.WhenStarted((service, control) => service.Start());
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
