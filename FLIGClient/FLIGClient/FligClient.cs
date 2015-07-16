using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using FLIGServer.Interfaces;

namespace FLIGClient
{
    public class FligClient : ClientBase<IFLIGClientListener>, IFLIGClientListener
    {
        public FligClient(BasicHttpBinding binding, EndpointAddress endPoint) : base(binding, endPoint) { }

        public int AddClient(FLIGServer.Implementations.ClientData clientData)
        {
            return Channel.AddClient(clientData);
        }

        public void RemoveClient(int clientId)
        {
            Channel.RemoveClient(clientId);
        }
    }
}
