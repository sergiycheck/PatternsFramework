using System;
using System.Runtime.Serialization;
using PatternsP2PFramwork.models;

namespace PatternsP2PFramwork.Memento
{
    //Memento
    [Serializable]
    [DataContract]
    public class SnapShotOfMyHtmlModel
    {
        public SnapShotOfMyHtmlModel()
        {

        }
        public SnapShotOfMyHtmlModel(MyHtmlModel model, string name)
        {
            MyHtmlModel = model;
            StateName = name;
        }
        [DataMember]
        public MyHtmlModel MyHtmlModel { get; private set; }
        [DataMember]
        public string StateName { get; private set; }


    }
}
