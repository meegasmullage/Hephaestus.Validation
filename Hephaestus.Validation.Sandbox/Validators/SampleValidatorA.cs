using System;
using System.Threading;
using System.Threading.Tasks;
using Hephaestus.Validation.Sandbox.Models;
using Hephaestus.Validation.Sandbox.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Validation.Sandbox.Validators
{
    public class SampleValidatorA : AbstractValidator<SampleA>
    {
        public SampleValidatorA()
        {
            RuleFor(p => p.Field01)
                .Rule((context, value) =>
                {
                    if (value < 0)
                    {
                        context.Errors.Add("Value cannot be less than zero");

                        return false;
                    }

                    return true;
                })
                .RuleAsync(async (context, value, cancellationToken) =>
                {
                    await Task.CompletedTask;

                    if (value > 100)
                    {
                        context.Errors.Add("Value cannot be greater than 100");

                        return false;
                    }

                    return true;
                });

            RuleFor(p => p.Field03)
                .Rule((context, value) =>
                {
                    //Can access the DI service
                    var sampleService = context.ServiceProvider.GetRequiredService<ISampleService>();

                    Console.WriteLine("Timestamp: {0}", sampleService.GetTimestamp());

                    if (value == null)
                    {
                        context.Errors.Add("Value cannot null");

                        return false;
                    }

                    return true;
                })
                .Validator();
        }
    }
}
