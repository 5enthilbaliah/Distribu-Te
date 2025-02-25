namespace DistribuTe.Utilities.HttpFluent;

public interface IContentMapper
{
    IExecuter WithBody<TBody>(TBody body, bool useCamelCaseSerializer = false)
        where TBody : class;

    IExecuter WithPrimitiveBody<TPrimitive>(TPrimitive body)
        where TPrimitive : struct;

    IExecuter WithFormBody<TForm>(TForm body)
        where TForm : class;
}