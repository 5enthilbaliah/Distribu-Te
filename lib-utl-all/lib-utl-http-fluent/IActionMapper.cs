namespace DistribuTe.Utilities.HttpFluent;

public interface IActionMapper
{
    IExecuter Get();
    IContentMapper Post();
    IContentMapper Put();
    IContentMapper Patch();
    IExecuter Delete();
}