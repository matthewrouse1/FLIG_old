using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using FLIGCommon.Implementations;
using FLIGCommon.Interfaces;

namespace FLIGCommon.Modules
{
    public class UserDetailsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserDetails>().As<IUserDetails>();
        }
    }
}
