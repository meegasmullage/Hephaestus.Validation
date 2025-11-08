using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hephaestus.Validation
{
    public interface IValidator<T>
    {
        IEnumerable<ValidationError> Errors { get; }

        ValueTask<bool> ValidateAsync(IServiceProvider serviceProvider, T value, bool throwOnError = false, CancellationToken cancellationToken = default);
    }
}
