using System.Threading;
using System.Threading.Tasks;

namespace Hephaestus.Validation
{
    internal abstract class AbstractRuleEntry<T>
    {
        internal abstract ValueTask<bool> ExecuteAsync(ValidationError error, object value, RuleContext<T> context, CancellationToken cancellationToken = default);
    }
}
