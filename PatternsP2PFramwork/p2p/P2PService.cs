using System;
using System.ServiceModel;
using PatternsP2PFramwork.models;

namespace PatternsP2PFramwork.p2p
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class P2PService : IP2PService
    {
        private string username;
        public MyHtmlModel Model { get; set; }

        public P2PService(string username)
        {
            
            this.username = username;
        }

        public string GetName()
        {
            return username;
        }

        public void SendMessage(string message, string from)
        {
            //MessageBox.Show(message, from);
            Console.WriteLine($"{message} {from}");
        }

        public void SendMyHtmlModel(MyHtmlModel model)
        {
            Model = model;
            Console.WriteLine(Model.HTML);
        }
    }
}