using System;
using System.Runtime.Serialization;

namespace PatternsP2PFramwork.models
{
    [Serializable]
    [DataContract]
    public class MyHtmlModel
    {
        public MyHtmlModel()
        {

        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string HTML { get; set; }
    }
}
