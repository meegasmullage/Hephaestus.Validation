namespace Hephaestus.Validation
{
    internal record struct ValidatorEntry(string Target, AbstractCachedExpression Expression, AbstractRuleCollection Rules);
}
