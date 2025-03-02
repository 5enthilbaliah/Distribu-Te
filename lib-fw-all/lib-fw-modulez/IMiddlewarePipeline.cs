namespace DistribuTe.Framework.ModuleZ;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

public interface IMiddlewarePipeline
{
    void Setup(WebApplication app, IWebHostEnvironment environment,  IConfiguration configuration);
}