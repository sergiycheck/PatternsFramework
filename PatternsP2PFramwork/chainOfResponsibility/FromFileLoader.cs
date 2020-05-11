using System;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using PatternsP2PFramwork.models;

namespace PatternsP2PFramwork.chainOfResponsibility
{
    public class FromFileLoader:AbstractLoader
    {
        private const string PatternFile = @"[A-Z0-9][A-Z0-9. ]*:(\\\\?)?\w*";
        public override object Handle(string path)
        {
            if (Regex.IsMatch(path, PatternFile))
            {
                var doc = new HtmlDocument();
                try
                {
                    doc.Load(path);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                
                }
                
                return new MyHtmlModel()
                {
                    Name = path,
                    HTML = doc.Text
                };
            }

            return base.Handle(path);
        }
    }
}
