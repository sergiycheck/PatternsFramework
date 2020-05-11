namespace PatternsP2PFramwork.chainOfResponsibility
{
    public interface ILoader
    {
        ILoader SetNext(ILoader loader);
        object Handle(string path);
    }
}
