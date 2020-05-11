using System;
using HtmlAgilityPack;
using PatternsP2PFramwork.models;

namespace PatternsP2PFramwork.Composite
{
    public abstract class Component
    {
        public MyHtmlModel MyHtmlmodel { get; set; }

        public HtmlNode Node { get; set; }

        public Component(MyHtmlModel myHtmlModel)
        {
            MyHtmlmodel = myHtmlModel;
        }

        public virtual void Display()
        {
            if (MyHtmlmodel != null)
            {
                Console.WriteLine(MyHtmlmodel.Name);
                //Console.WriteLine(MyHtmlmodel.HTML);
            }
            
        }

        public virtual void Add(Component component) { }

        public virtual void Remove(Component component) { }
    }
}
