using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hephaestus.Validation
{
    internal class DelegateRuleEntry<T1, T2> : AbstractRuleEntry<T1>
    {
        private readonly Func<RuleContext<T1>, T2, bool> _rule;

        public DelegateRuleEntry(Func<RuleContext<T1>, T2, bool> rule)
        {
            _rule = rule;
        }

        private ValueTask<bool> ExecuteAsync(ValidationError error, T2 value, RuleContext<T1> context, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (!_rule.Invoke(context, value))
            {
                error.Errors = [.. error.Errors ?? [], .. context.Errors];

                return new ValueTask<bool>(false);
            }

            return new ValueTask<bool>(true);
        }

        internal override ValueTask<bool> ExecuteAsync(ValidationError error, object value, RuleContext<T1> context, CancellationToken cancellationToken = default)
            => ExecuteAsync(error, (T2)value, context, cancellationToken);
    }
}
