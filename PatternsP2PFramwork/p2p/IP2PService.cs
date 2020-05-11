using System.ServiceModel;
using PatternsP2PFramwork.models;

namespace PatternsP2PFramwork.p2p
{
    [ServiceContract]
    public interface IP2PService
    {
        [OperationContract]
        string GetName();

        [OperationContract(IsOneWay = true)]
        void SendMessage(string message, string from);

        [OperationContract(IsOneWay = true)]

        void SendMyHtmlModel(MyHtmlModel model);
    }
}