using Hephaestus.Validation.Sandbox.Models;

namespace Hephaestus.Validation.Sandbox.Validators
{
    public class SampleValidatorD : AbstractValidator<SampleD>
    {
        public SampleValidatorD()
        {
            RuleFor(p => p.Field01)
                .Rule((context, value) =>
                {
                    if (value < 0)
                    {
                        context.Errors.Add("Value cannot be less than zero");

                        return false;
                    }

                    return true;
                })
                .Rule((context, value) =>
                {
                    if (value > 100)
                    {
                        context.Errors.Add($"Value cannot be greater than 100. [Value={value}]");

                        return false;
                    }

                    return true;
                });
        }
    }
}
