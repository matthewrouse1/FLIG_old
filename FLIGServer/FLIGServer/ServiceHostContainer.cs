using GitInterface;
using FLIGCommon;
using Autofac;
using FLIGCommon.Interfaces;
using System;
using System.Collections.Generic;
using FLIGServer.Implementations;
using FLIGServer.Interfaces;

namespace FLIGServer
{
    public class ServiceHostContainer
    {
        private IContainer container;
        private FLIGServerHostContainer fligUserServiceContainer;

        public bool Start(string[] args)
        {
            this.container = new ContainerConfiguration().Create();

            var userDetails = container.Resolve<IUserDetails>(new NamedParameter("container", this.container));

            if (!userDetails.IsUserSetup())
            {
                if (args.Length > 0 && args[0] == "setup")
                {
                    SetupUser(userDetails);
                }
                else
                {
#if DEBUG
                    userDetails.SetUsername("matthewrouse1");
                    userDetails.SetPassword("fakePass");
                    userDetails.SetEmailAddress("matthew.rouse@advancedcomputersoftware.com");
                    userDetails.SetRepoPath(@"C:\repos\alb");
                    userDetails.SetRepoURL(@"git@github.com:AdvancedLegal/alb.git");
#else
                    throw new Exception("Error: No user set up, please re-run in setup mode");
#endif
                }
            }

            fligUserServiceContainer = new FLIGServerHostContainer(this.container);
            fligUserServiceContainer.StartHostedServices();
            
            return true;
        }

        private void SetupUser(IUserDetails userDetailsProvider)
        {
            var userDetailParameters = new List<string>()
                {
                    "Username",
                    "Password",
                    "Email Address",
                    "Local Repository Path",
                    "Remote Repository URL"
                };


            var userDetailList = new List<string>();

            foreach (var userDetail in userDetailParameters)
            {
                Console.WriteLine(string.Format("Enter {0}:", userDetail));
                userDetailList.Add(Console.ReadLine());
            }

            userDetailsProvider.SetUsername(userDetailList[0]);
            userDetailsProvider.SetPassword(userDetailList[1]);
            userDetailsProvider.SetEmailAddress(userDetailList[2]);
            userDetailsProvider.SetRepoPath(userDetailList[3]);
            userDetailsProvider.SetRepoURL(userDetailList[4]);
        }

        public bool Stop()
        {
            this.fligUserServiceContainer.StopHostedServices();

            return true;
        }
    }
}
