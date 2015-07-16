using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLIGServer.Implementations
{
    public class ClientData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime LastContact { get; set; }
        public int FailedConnectionAttempts { get; set; }

    }
}
