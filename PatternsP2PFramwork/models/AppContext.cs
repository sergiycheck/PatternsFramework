using System.Data.Entity;

namespace PatternsP2PFramwork.models
{
    public class MyAppContext:DbContext
    {
        public DbSet<MyHtmlModel> HtmlModels { get; set; }

        public MyAppContext() :base("DatabaseHtml") { }

    }
}
