using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Validation
{
    internal class ValidatorRuleEntry<T1, T2> : AbstractRuleEntry<T1>
    {
        protected static async ValueTask<bool> Execute(ValidationError error, T2 value, RuleContext<T1> context, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var validator = context.ServiceProvider.GetRequiredService<IValidator<T2>>();

            if (!await validator.ValidateAsync(context.ServiceProvider, value, cancellationToken:cancellationToken))
            {
                error.Children = [.. error.Children ?? [], .. validator.Errors];

                return false;
            }

            return true;
        }

        internal override ValueTask<bool> ExecuteAsync(ValidationError error, object value, RuleContext<T1> context, CancellationToken cancellationToken = default)
            => Execute(error, (T2)value, context, cancellationToken);
    }
}
