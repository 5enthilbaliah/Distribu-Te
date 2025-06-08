namespace DistribuTe.Aggregates.Projects.gRPC;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Services;

public static class GrpcSetup
{
    public static WebApplication SetupGrpc(this WebApplication app,
        IWebHostEnvironment environment, IConfiguration configuration)
    {
        // app.MapGrpcService<GreeterService>();
        app.MapGet("/",
            () =>
                "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

        return app;
    }
}