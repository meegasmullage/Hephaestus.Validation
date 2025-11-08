using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hephaestus.Validation
{
    internal class AsyncDelegateRuleEntry<T1, T2> : AbstractRuleEntry<T1>
    {
        private readonly Func<RuleContext<T1>, T2, CancellationToken, Task<bool>> _rule;

        public AsyncDelegateRuleEntry(Func<RuleContext<T1>, T2, CancellationToken, Task<bool>> rule)
        {
            _rule = rule;
        }

        private async ValueTask<bool> ExecuteAsync(ValidationError error, T2 value, RuleContext<T1> context, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (!await _rule.Invoke(context, value, cancellationToken))
            {
                error.Errors = [.. error.Errors ?? [], .. context.Errors];

                return false;
            }

            return true;
        }

        internal override ValueTask<bool> ExecuteAsync(ValidationError error, object value, RuleContext<T1> context, CancellationToken cancellationToken = default)
            => ExecuteAsync(error, (T2)value, context, cancellationToken);
    }
}
