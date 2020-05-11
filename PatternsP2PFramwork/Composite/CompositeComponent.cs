using System;
using System.Collections.Generic;
using PatternsP2PFramwork.models;

namespace PatternsP2PFramwork.Composite
{
    public class CompositeComponent:Component
    {
        List<Component> components = new List<Component>();
        public CompositeComponent(MyHtmlModel myHtmlModel) : base(myHtmlModel) {}

        public override void Add(Component component)
        {
            components.Add(component);
        }

        public override void Remove(Component component)
        {
            components.Remove(component);
        }

        public override void Display()
        {
            Console.WriteLine($"Node name {this.MyHtmlmodel.Name}");
            if (components.Count > 0)
            {
                Console.WriteLine("Children nodes names");
                foreach (var comp in components)
                {
                    comp.Display();
                }
            }
            
        }
    }
}
