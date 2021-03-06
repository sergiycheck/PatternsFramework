﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using PatternsP2PFramwork.chainOfResponsibility;
using PatternsP2PFramwork.Composite;
using PatternsP2PFramwork.helpers;
using PatternsP2PFramwork.Memento;
using PatternsP2PFramwork.models;
using PatternsP2PFramwork.p2p;
using PatternsP2PFramwork.TemplateMethod;

namespace PatternsP2PFramwork
{
    class Program
    {

        static void Main(string[] args)
        {
            MainAsync(args).Wait();
        }

        static async Task<MyHtmlModel> Proxy(string path)
        {

            HtmlStoreProxy proxyOb = new HtmlStoreProxy();
            var model = await proxyOb.GetMyHtmlModel(path);
            //Console.WriteLine(model.HTML);
            return model;
        }

        static void ChainOfResponsibility()
        {
            var fileLoad = new FromFileLoader();
            var webload = new FromWebLoader();
            fileLoad.SetNext(webload);
            Console.WriteLine("Chain: File->Web");
            Client.ClientLoader(fileLoad);
        }

        static void Strategy(string path)
        {
            //crawler.Retriever = new LinkRetriever();
            //var elems1 = await crawler.Retriever.GetElems(crawler.Path, html);
            //Console.WriteLine("-------------Links-------------");
            //crawler.Print(elems1);
            //crawler.Retriever = new ButtonRetriever();
            //Console.WriteLine("-------------Buttons html-------------");
            //var elems2 = await crawler.Retriever.GetElems(crawler.Path, html);
            //crawler.Print(elems2);
        }

        static void Memento(string path)
        {
            CareTaker careTaker = CareTaker.RestoreFromFile();
            Originator originator = null;
            WebCrawler webcrawler = WebCrawler.GetInstance();
            if (careTaker != null)
            {
                originator = careTaker.Originator;
            }
            if (careTaker == null)
            {
                var model = webcrawler.LoadFromFile(path);
                originator = new Originator(model);
                careTaker = new CareTaker(originator);
            }

            //careTaker.Restore("nkbrrk");
            //Console.WriteLine($"Originator stateName {originator.StateName}");

            careTaker.Compare("citmxv");

            //originator.MakeChanges(webcrawler);
            //await careTaker.Save();

            //Console.WriteLine("History");
            //careTaker.ShowHistory();

        }

        static void TemplateMethod(string path)
        {
            HtmlDataAccess data = new HtmlButtonAccess(path, "buttons");
            data.Process();
            data = new HtmlLinkAccess(path, "links");
            data.Process("json");
        }

        static async void Composite(string path)
        {
            WebCrawler crawler = WebCrawler.GetInstance();

            HtmlStoreProxy proxy = new HtmlStoreProxy();

            MyHtmlModel model = await proxy.GetMyHtmlModel(path);

            HtmlDocument htmlDoc = new HtmlDocument();

            htmlDoc.LoadHtml(model.HTML);


            HtmlNode html = crawler.GetFirstNode("html", htmlDoc);

            var bodyNode = html.SelectSingleNode("//body");

            var navigationNode = crawler.GetNode
            (
                "class",
                "navbar navbar-expand-lg navbar-light fixed-top",
                htmlDoc
            );
            var headerNode = crawler.GetNode
            (
                "class",
                "masthead",
                htmlDoc
            );
            var aboutSectionNode = crawler.GetNode
            (
                "id",
                "about",
                htmlDoc
            );
            var signupSection = crawler.GetNode
            (
                "id",
                "signup",
                htmlDoc
            );

            var contentOfsignupSection = crawler.GetFirstNode("body/section[3]/div[1]", htmlDoc);

            signupSection.RemoveAllChildren();


            html.RemoveChild(bodyNode);//html tag is without body tag

            Console.WriteLine(html.OuterHtml);

            ////////////////Composite realization

            Composite.Component htmlComposite = new CompositeComponent
                (
                new MyHtmlModel() { HTML = html.OuterHtml, Name = "WithoutBody" }
                );
            htmlComposite.Node = html;


            bodyNode.RemoveAllChildren();//body tag is empty

            //composite components
            Composite.Component EmptyBody = new CompositeComponent
                (
                new MyHtmlModel() { HTML = bodyNode.OuterHtml, Name = "EmptyBody" }
                );
            EmptyBody.Node = bodyNode;

            //leaf
            HtmlElem navigation = new HtmlElem(new MyHtmlModel()
            {
                HTML = navigationNode.OuterHtml,
                Name = "NavBar"
            });
            navigation.Node = navigationNode;

            //leaf
            HtmlElem header = new HtmlElem(new MyHtmlModel()
            {
                HTML = headerNode.OuterHtml,
                Name = "Header"
            });
            header.Node = headerNode;

            //leaf
            HtmlElem AboutSection = new HtmlElem(new MyHtmlModel()
            {
                HTML = aboutSectionNode.OuterHtml,
                Name = "NavBar"
            });
            AboutSection.Node = aboutSectionNode;

            //append children to body
            EmptyBody.Add(navigation);
            EmptyBody.Node.AppendChild(navigation.Node);

            EmptyBody.Add(header);
            EmptyBody.Node.AppendChild(header.Node);

            EmptyBody.Add(AboutSection);
            EmptyBody.Node.AppendChild(AboutSection.Node);

            htmlComposite.Add(EmptyBody);
            htmlComposite.Node.AppendChild(EmptyBody.Node);

            htmlComposite.Display();//displaying tree

            Console.WriteLine("--------------------htmlComposite html----------------");

            Console.WriteLine(htmlComposite.Node.OuterHtml);//displaying node html

            EmptyBody.Remove(AboutSection);//remove child
            EmptyBody.Node.RemoveChild(AboutSection.Node);//remove node

            Console.WriteLine("--------------------htmlComposite html after remove----------------");

            Console.WriteLine(htmlComposite.Node.OuterHtml);//displaying body without child

            EmptyBody.Add(AboutSection);
            EmptyBody.Node.AppendChild(AboutSection.Node);

            //add one more composite element
            Composite.Component signUpSectionComposite = new CompositeComponent
                (new MyHtmlModel()
                {
                    HTML = signupSection.OuterHtml,
                    Name = "EmptySignUpSection"
                });
            signUpSectionComposite.Node = signupSection;

            //leaf
            HtmlElem signUpSectionContainer = new HtmlElem
                (
                new MyHtmlModel()
                {
                    HTML = contentOfsignupSection.OuterHtml,
                    Name = "SignUpSectionContainer"
                }
                );
            signUpSectionContainer.Node = contentOfsignupSection;

            signUpSectionComposite.Add(signUpSectionContainer);
            signUpSectionComposite.Node.AppendChild(signUpSectionContainer.Node);

            EmptyBody.Add(signUpSectionComposite);
            EmptyBody.Node.AppendChild(signUpSectionComposite.Node);

            htmlComposite.Display();

            Console.WriteLine("--------------------htmlComposite html----------------");

            Console.WriteLine(htmlComposite.Node.OuterHtml);

            //htmlComposite.MyHtmlmodel.HTML=htmlComposite.Node.OuterHtml;//saving all html 

            MyHtmlModel final = new MyHtmlModel()
            {
                HTML = htmlComposite.MyHtmlmodel.HTML,
                Name = "ResultOfComposition"
            };
            try
            {
                GenericSerializer serializer = new GenericSerializer("res", @"E:\studying\education\6сем\трпз\lab5\Saves\");
                serializer.BinarySerializing(FileMode.Create, final);
                serializer.DataContractSerialization(FileMode.Create, typeof(MyHtmlModel), final);
            }
            catch (Exception e)
            {

            }
            EmptyBody.Remove(header);//remove child
            EmptyBody.Node.RemoveChild(header.Node);//remove node
            htmlComposite.Display();

        }

        static async Task PeerToPeer()
        {
            PeerToPeerImplementation manager = new PeerToPeerImplementation();
            int selection = 0;
            Console.WriteLine($"Your name is:{manager.userName}");
            Console.WriteLine("Enter 1 to refresh");
            Console.WriteLine("Enter 2 to show all peers");
            Console.WriteLine("Enter 3 to send message");
            Console.WriteLine("Enter 4 to stop host");
            Console.WriteLine("Enter 6 to send html");
            Console.WriteLine("Enter 7 see menu");

            while (selection != 5)
            {
                switch (selection)
                {
                    case 1:
                        Console.WriteLine("Refreshing");
                        manager.Refresh().Wait();
                        
                        break;
                    case 2:
                        Console.WriteLine("All peers");
                        manager.DisplayPeers();
                        break;
                    case 3:
                        Console.WriteLine("Enter peer name");
                        string name = Console.ReadLine();
                        manager.SendMessape(name);
                        break;
                    case 4:
                        Console.WriteLine("Stopping host ...");
                        manager.Stop();
                        break;
                    case 6:
                        Console.WriteLine("Finding html model");
                        MyHtmlModel model = await Proxy(path3);
                        Console.WriteLine("Enter peer name");
                        string name1 = Console.ReadLine();
                        Console.WriteLine("Sending html model");
                        manager.SendHtmlModel(name1,model);
                        break;
                    case 7:
                        Console.WriteLine("Enter 1 to refresh");
                        Console.WriteLine("Enter 2 to show all peers");
                        Console.WriteLine("Enter 3 to send message");
                        Console.WriteLine("Enter 4 to stop host");
                        Console.WriteLine("Enter 6 to send html");
                        Console.WriteLine("Enter 7 see menu");
                        break;
                        


                }
                Console.WriteLine("Please select an action");
                string act = Console.ReadLine();
                selection = int.Parse(act);
            }

        }

        static string path1 = @"https://getbootstrap.com/docs/4.4/examples/album/";
        static string path2 = @"https://codebeautify.org/all-number-converter";
        static string path3 = @"E:/filesFromCDisk/templates/startbootstrap-grayscale-gh-pages/index.html";
        static string path4 = @"E:\filesFromCDisk\templates\microsoft\Microsoft Docs.html";

        static async Task MainAsync(string[] args)
        {
            



            await PeerToPeer();

            //List<string> paths = new List<string>() { path1, path2, path3, path4 };
            //foreach (var path in paths)
            //{
            //    await Proxy(path);
            //}
            

            //Composite
            //Composite(path);
            //TemplateMethod
            //TemplateMethod(path);

            //memento
            //Memento(path);

            //proxy
            //await Proxy(path);

            //chain of responsibility
            //ChainOfResponsibility();

            //strategy
            //Strategy(path);


        }
    }
}
