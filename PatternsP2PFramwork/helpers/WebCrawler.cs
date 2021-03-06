﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HtmlAgilityPack;
using PatternsP2PFramwork.models;

namespace PatternsP2PFramwork.helpers
{
    public class WebCrawler
    {
        public string Path { get; set; }
        private HtmlWeb web;
        private static WebCrawler instance;
        private static object syncRoot = new Object();
        public IElemRetriever Retriever { get; set; }

        private WebCrawler()
        {
            
            web = new HtmlWeb();
        }

        public async Task<HtmlDocument> Download(string path)
        {
            //this method downloads html from web or disc space
            this.Path = path;
            try
            {
                var doc = await web.LoadFromWebAsync(Path);
                if (doc != null)
                {
                    return doc;
                }

            }
            catch (Exception e)
            {
                //Console.WriteLine(e);

            }

            try
            {
                var docf = new HtmlDocument();
                docf.Load(path);
                if(docf!=null)
                    return docf;
                return null;
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);

            }


            return null;
        }

        public MyHtmlModel LoadFromFile(string path)
        {
            try
            {
                var docf = new HtmlDocument();
                docf.Load(path);
                if (docf != null)
                    return new MyHtmlModel()
                    {
                        HTML = docf.Text,
                        Name = path
                    };
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);

            }

            return null;
        }
        /// <summary>
        /// get html node with specified attribute
        /// </summary>
        /// <param name="attribute"></param>any html attribute (id,class,value ...)
        /// <param name="value"></param>attribute value
        /// <param name="doc"></param>html document to find in 
        /// <returns></returns>
        public HtmlNode GetNode(string attribute, string value,HtmlDocument doc)
        {
            return doc.DocumentNode.SelectSingleNode($"//*[@{attribute}=\"{value}\"]");
        }
        /// <summary>
        /// gets the first tag with matching html tag name
        /// </summary>
        /// <param name="name"></param>name of the tag
        /// <param name="doc"></param>html document to find in 
        /// <returns></returns>
        public HtmlNode GetFirstNode(string name, HtmlDocument doc)
        {
            return doc.DocumentNode.SelectSingleNode($"//{name}");
        }


        public async Task<MyHtmlModel> GetMyHtmlModel(string path)
        {
            HtmlDocument html = await Download(path);
            return new MyHtmlModel()
            {
                HTML = html.Text,
                Name = path
            };

        }

        public void Print(IEnumerable<string> elems)
        {
            List<string> elsList = new List<string>(elems);
            foreach (var VARIABLE in elsList)
            {
                Console.WriteLine(VARIABLE);
            }
        }


        public static WebCrawler GetInstance()
        {
            Console.WriteLine($"GetInstance {DateTime.Now.TimeOfDay}");
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                        instance = new WebCrawler();
                }
            }
            return instance;
        }

    }
}
