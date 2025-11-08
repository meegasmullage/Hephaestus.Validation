using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hephaestus.Validation
{
    public abstract class AbstractRuleCollection
    {
        internal abstract ValueTask<bool> ExecuteAsync(IServiceProvider serviceProvider, object value1, ValidationError error, object value2, CancellationToken cancellationToken = default);
    }
}
