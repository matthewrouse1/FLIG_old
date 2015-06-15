using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FLIGServer.Interfaces;

namespace FLIGServer.Implementations
{
    public class DataAccess : IDataAccess
    {
        public void Set(string data)
        {
            Console.WriteLine("got here ok ");
        }
    }
}
