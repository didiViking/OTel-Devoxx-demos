using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace OtelDemoDevoxxNet
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddOpenTelemetryTracing(builder =>
                    {
                        builder
                            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("OtelDemoDevoxxNet"))
                            .AddAspNetCoreInstrumentation()
                            .AddConsoleExporter()
                            .AddOtlpExporter(opt => opt.Endpoint = new Uri("http://localhost:4317"));
                    });

                    services.AddOpenTelemetryMetrics(builder =>
                    {
                        builder
                            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("OtelDemoDevoxxNet"))
                            .AddMeter("demo-meter")
                            .AddConsoleExporter()
                            .AddOtlpExporter(opt => opt.Endpoint = new Uri("http://localhost:4317"));
                    });

                    services.AddLogging(logging =>
                    {
                        logging.AddConsole();
                        logging.SetMinimumLevel(LogLevel.Information);
                    });
                })
                .Build();

            var tracerProvider = Sdk.CreateTracerProviderBuilder()
                .AddSource("demo-tracer")
                .Build();

            var meterProvider = Sdk.CreateMeterProviderBuilder()
                .AddMeter("demo-meter")
                .Build();

            var tracer = tracerProvider.GetTracer("demo-tracer");
            var meter = meterProvider.GetMeter("demo-meter");
            var counter = meter.CreateCounter<long>("demo_counter");

            var loggerFactory = host.Services.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger("demo-logger");

            Console.WriteLine("Demo running. Sending traces, metrics, and logs to OTEL Viewer at localhost:4317...");

            for (int i = 0; i < 10; i++)
            {
                using var span = tracer.StartActiveSpan($"operation-{i}");
                counter.Add(i, new("iteration", i));
                logger.LogInformation("Logging iteration {Iteration}", i);
                await Task.Delay(500);
            }

            Console.WriteLine("Demo finished.");
        }
    }
}
