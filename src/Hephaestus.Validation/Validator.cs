using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Validation
{
    public sealed class Validator : IValidator
    {
        private readonly IServiceProvider _serviceProvider;

        public Validator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async ValueTask<bool> ValidateAsync<T>(T value, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var validator = _serviceProvider.GetRequiredService<IValidator<T>>();

            return await validator.ValidateAsync(_serviceProvider, value, true, cancellationToken);
        }
    }
}
