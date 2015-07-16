using System.ServiceModel;
using FLIGServer.Implementations;

namespace FLIGServer.Interfaces
{
    [ServiceContract]
    public interface IFLIGClientListener
    {
        [OperationContract]
        int AddClient(ClientData clientData);

        [OperationContract]
        void RemoveClient(int clientId);
    }
}
