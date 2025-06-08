namespace DistribuTe.Aggregates.Projects.gRPC.Services;

using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

public class ProjectCategoryService : ProjectCategoryFinder.ProjectCategoryFinderBase
{
    public override Task<BoolValue> EntityExists(Int32Value request, ServerCallContext context)
    {
        return base.EntityExists(request, context);
    }
}