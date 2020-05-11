using System.Threading.Tasks;
using PatternsP2PFramwork.models;
using PatternsP2PFramwork.repository;

namespace PatternsP2PFramwork.helpers
{
    public class HtmlStoreProxy
    {
        private WebCrawler crawler;
        private MyHtmlModelRepository<MyHtmlModel> repository;
        public HtmlStoreProxy()
        {
            repository = new MyHtmlModelRepository<MyHtmlModel>(new MyAppContext());
        }

        public async Task<MyHtmlModel> GetMyHtmlModel(string path)
        {
            MyHtmlModel model = repository.GetHtmlModelByLink(path);
            if (model == null)
            {
                crawler = WebCrawler.GetInstance();
                model = await crawler.GetMyHtmlModel(path);
                await repository.Create(model);
            }

            return model;
        }

    }
}
