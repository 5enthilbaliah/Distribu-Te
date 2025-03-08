namespace DistribuTe.Framework.OData.ErrorHandling;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using ModuleZ;

public class ErrorHandlePipeline : IMiddlewarePipeline
{
    public void Setup(WebApplication app, IWebHostEnvironment environment, IConfiguration configuration)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/error-development");
        }
        else
        {
            app.UseExceptionHandler("/error");
        }
    }
}