using RestWithASPNETUdemy.Services.Implementations;
using OpenTelemetry.Trace;
using OpenTelemetry.Resources;
using OpenTelemetry.Instrumentation.AspNetCore;
using OpenTelemetry.Extensions.Hosting;
using OpenTelemetry;
using System.Diagnostics;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NLog;
using OpenTelemetry.Logs;
using System.Reflection;
using System.Reflection.PortableExecutable;

//www.aspecto.io/blog/getting-started-with-opentelemetry-dotnet/ and opentelemetry.io/docs/instrumentation/net/getting-started/
internal class Program
{
    private static void Main(string[] args)
    {
       
        var builder = WebApplication.CreateBuilder(args);

        // Configure the Otel
        var serviceName = "Back.Otel";
        var serviceVersion = "1.0";

         builder.Services.AddOpenTelemetry()
        .WithTracing(tracerProviderBuilder =>
            tracerProviderBuilder.AddOtlpExporter(opt =>
            {
                opt.Endpoint = new Uri("http://localhost:4317/v1/traces");
                opt.Headers = $"Authorization={Environment.GetEnvironmentVariable("0bf164a69482b4413540083180aca560ce84NRAL")}";
            })
             .AddSource(serviceName)
             .SetResourceBuilder(
            ResourceBuilder.CreateDefault()
        .AddService(serviceName: serviceName, serviceVersion: serviceVersion))
        .AddHttpClientInstrumentation()
        .AddAspNetCoreInstrumentation()
           );

        //void ConfigureResource(ResourceBuilder r) => r.AddService(
        //       serviceName: builder.Configuration.GetValue("ServiceName", "BackOtel"),
        //       serviceVersion: Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "unknown",
        //       serviceInstanceId: Environment.MachineName);

        //var resourceBuilder = ResourceBuilder.CreateDefault();
        //ConfigureResource(resourceBuilder);

        //builder.Services.AddOpenTelemetry()
        //            .ConfigureResource(ConfigureResource)
        //            .WithTracing(tracerProviderBuilder =>
        //            {
        //                tracerProviderBuilder
        //                    .SetResourceBuilder(resourceBuilder)
        //                    .AddHttpClientInstrumentation()                             
        //                    .AddOtlpExporter(opt =>
        //                    {
        //                        opt.Endpoint = new Uri("http://localhost:4317/v1/traces");
        //                        opt.Headers = $"Authorization={Environment.GetEnvironmentVariable("0bf164a69482b4413540083180aca560ce84NRAL")}";
        //                    });
        //            }).StartWithHost();

        // Add services to the container.

        builder.Services.AddControllers();

        //Dependency injection
        builder.Services.AddScoped<IPersonService, PersonServiceImplementation>();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddCors(options =>
            options.AddPolicy("DefaultPolicy", x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }       

        app.UseHttpsRedirection();

        app.UseAuthorization();
        
        app.UseCors("DefaultPolicy");

        app.MapControllers(); 

        app.Run();

       
    }
}
