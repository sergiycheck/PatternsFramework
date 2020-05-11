using System.Collections.Generic;
using HtmlAgilityPack;

namespace PatternsP2PFramwork.helpers
{
    public interface IElemRetriever
    {
        IEnumerable<string> GetElems(string path,HtmlDocument html);
    }
}
