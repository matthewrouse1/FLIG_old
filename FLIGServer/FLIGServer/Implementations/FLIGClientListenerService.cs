using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FLIGServer.Interfaces;

namespace FLIGServer.Implementations
{
    public class FLIGClientListenerService : IFLIGClientListener
    {
        private List<ClientData> connectedClients = new List<ClientData>();

        public int AddClient(ClientData clientData)
        {
            if (connectedClients.Count(s => s.Name == clientData.Name) > 0)
            {
                return -1;
            }

            clientData.Id = connectedClients.Count + 1;

            connectedClients.Add(clientData);

            return clientData.Id;
        }

        public void RemoveClient(int clientId)
        {
            var foundClient = connectedClients.First(s => s.Id == clientId);
            if (foundClient != null)
                connectedClients.Remove(foundClient);
        }
    }
}
