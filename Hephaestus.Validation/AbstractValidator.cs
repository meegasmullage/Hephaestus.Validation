using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Hephaestus.Validation
{
    public abstract class AbstractValidator<T> : IValidator<T>
    {
        private readonly Dictionary<string, ValidatorEntry> _validatorEntries;

        private readonly List<ValidationError> _validationErrors;

        public AbstractValidator()
        {
            _validatorEntries = [];

            _validationErrors = [];
        }

        public IEnumerable<ValidationError> Errors => _validationErrors;

        public RuleCollection<T, TResult> RuleFor<TResult>(Expression<Func<T, TResult>> expression, string name = null)
        {
            var memberExpression = expression.Body is UnaryExpression unaryExpression ?
                (MemberExpression)unaryExpression.Operand : (MemberExpression)expression.Body;

            var target = name ?? memberExpression.Member.Name;
            var compiledExpression = expression.Compile();

            var ruleCollection = new RuleCollection<T, TResult>();

            _validatorEntries.Add(target, new ValidatorEntry(target, new CachedExpression<T, TResult>(compiledExpression), ruleCollection));

            return ruleCollection;
        }

        public async ValueTask<bool> ValidateAsync(IServiceProvider serviceProvider, T value, bool throwOnError = false, CancellationToken cancellationToken = default)
        {
            _validationErrors.Clear();

            cancellationToken.ThrowIfCancellationRequested();

            foreach (var validatorEntry in _validatorEntries.Values)
            {
                var validationError = new ValidationError
                {
                    Target = validatorEntry.Target
                };

                var value2 = validatorEntry.Expression.Invoke(value);

                if (!await validatorEntry.Rules.ExecuteAsync(serviceProvider, value, validationError, value2, cancellationToken))
                {
                    _validationErrors.Add(validationError);

                    if (throwOnError)
                    {
                        throw new ValidationFailedException([.. _validationErrors]);
                    }

                    return false;
                }
            }

            return true;
        }
    }
}
