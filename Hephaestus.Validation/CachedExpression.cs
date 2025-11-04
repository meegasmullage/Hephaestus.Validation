using System;

namespace Hephaestus.Validation
{
    internal class CachedExpression<T, TResult> : AbstractCachedExpression
    {
        private readonly Func<T, TResult> _compiledExpression;

        public CachedExpression(Func<T, TResult> compiledExpression)
        {
            _compiledExpression = compiledExpression;
        }

        public override object Invoke(object value) => _compiledExpression.Invoke((T)value);
    }
}
