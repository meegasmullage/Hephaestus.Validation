using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Hephaestus.Validation.Sandbox.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Hephaestus.Validation.Sandbox
{
    public class ExampleService : IHostedService
    {
        private readonly ILogger<ExampleService> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly System.Timers.Timer _timer;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public ExampleService(ILogger<ExampleService> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            _timer = new System.Timers.Timer
            {
                Interval = 1000 * 5,
                Enabled = false
            };

            _timer.Elapsed += TimerElapsed;
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                _timer.Enabled = false;

                Task.Run(async () =>
                {
                    using (var cancellationTokenSource = new CancellationTokenSource())
                    {
                        using (var scope = _serviceScopeFactory.CreateScope())
                        {
                            var validator = scope.ServiceProvider.GetRequiredService<IValidator>();

                            var value = new SampleA
                            {
                                Field01 = 1,
                                Field02 = 2,
                                Field03 = new SampleB
                                {
                                    Field01 = 3,
                                    Field02 = 4,
                                    Field03 = new SampleC
                                    {
                                        Field01 = 5,
                                        Field02 = 6,
                                        Field03 = new SampleD
                                        {
                                            Field01 = Random.Shared.Next(),
                                        }
                                    }
                                }
                            };

                            try
                            {
                                _ = await validator.ValidateAsync(value, cancellationTokenSource.Token);
                            }
                            catch (ValidationFailedException ex)
                            {
                                Console.WriteLine();
                                Console.WriteLine(JsonSerializer.Serialize(ex.Errors, _jsonSerializerOptions));
                            }
                        }
                    }
                }).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while running the service");
            }
            finally
            {
                _timer.Enabled = true;
            }
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _timer.Enabled = true;

            await Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer.Enabled = false;

            return Task.CompletedTask;
        }
    }
}
