using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hephaestus.Validation
{
    public class RuleCollection<T1, T2> : AbstractRuleCollection
    {
        private readonly List<AbstractRuleEntry<T1>> _ruleEntries;

        public RuleCollection()
        {
            _ruleEntries = [];
        }

        private async ValueTask<bool> ExecuteAsync(IServiceProvider serviceProvider, T1 value1, ValidationError error, T2 value2, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            foreach (var ruleEntry in _ruleEntries)
            {
                var context = new RuleContext<T1>(serviceProvider, value1);

                if (!await ruleEntry.ExecuteAsync(error, value2, context, cancellationToken))
                {
                    return false;
                }
            }

            return true;
        }

        internal override ValueTask<bool> ExecuteAsync(IServiceProvider serviceProvider, object value1, ValidationError error, object value2, CancellationToken cancellationToken = default)
            => ExecuteAsync(serviceProvider, (T1)value1, error, (T2)value2, cancellationToken);

        public RuleCollection<T1, T2> Rule(Func<RuleContext<T1>, T2, bool> rule)
        {
            _ruleEntries.Add(new DelegateRuleEntry<T1, T2>(rule));

            return this;
        }

        public RuleCollection<T1, T2> RuleAsync(Func<RuleContext<T1>, T2, CancellationToken, Task<bool>> rule)
        {
            _ruleEntries.Add(new AsyncDelegateRuleEntry<T1, T2>(rule));

            return this;
        }

        public RuleCollection<T1, T2> Validator()
        {
            _ruleEntries.Add(new ValidatorRuleEntry<T1, T2>());

            return this;
        }
    }
}
