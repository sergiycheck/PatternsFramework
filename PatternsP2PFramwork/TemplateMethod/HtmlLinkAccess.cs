using System.Linq;
using PatternsP2PFramwork.helpers;

namespace PatternsP2PFramwork.TemplateMethod
{
    public class HtmlLinkAccess:HtmlDataAccess
    {
        public HtmlLinkAccess(string path,string nameOfTheFile) : base(path, nameOfTheFile)
        {
            ElemRetriever = new LinkRetriever();
        }

        public override void GetElements()
        {
            ElemList = ElemRetriever.GetElems(Path,Html).ToList();
        }

        public override void Save(string type = "binary")
        {
            base.Save(type);
        }
    }
}
