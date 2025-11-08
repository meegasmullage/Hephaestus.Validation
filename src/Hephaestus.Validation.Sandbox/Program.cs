using Hephaestus.Validation.Sandbox.Models;
using Hephaestus.Validation.Sandbox.Services;
using Hephaestus.Validation.Sandbox.Validators;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hephaestus.Validation.Sandbox
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddValidation();

                    services.AddScoped<ISampleService, SampleService>();

                    services.AddSingleton<IValidator<SampleA>, SampleValidatorA>();
                    services.AddSingleton<IValidator<SampleB>, SampleValidatorB>();
                    services.AddSingleton<IValidator<SampleC>, SampleValidatorC>();
                    services.AddSingleton<IValidator<SampleD>, SampleValidatorD>();

                    services.AddHostedService<ExampleService>();
                });

            builder
                .UseConsoleLifetime()
                .Build()
                .Run();
        }
    }
}
