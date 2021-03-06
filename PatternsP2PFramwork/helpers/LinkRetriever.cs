﻿using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace PatternsP2PFramwork.helpers
{
    class LinkRetriever : IElemRetriever
    {
        public IEnumerable<string> GetElems(string path, HtmlDocument html)
        {
            try
            {
                HtmlWeb web = new HtmlWeb();
                var linkList = html.DocumentNode
                    .Descendants("a")
                    .Select(a => a.GetAttributeValue("href", null))
                    .Where(u => !string.IsNullOrEmpty(u))
                    .Distinct();
                return linkList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }

            return null;

        }
    }
}
