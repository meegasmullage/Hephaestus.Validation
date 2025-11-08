using Hephaestus.Validation.Sandbox.Models;

namespace Hephaestus.Validation.Sandbox.Validators
{
    public class SampleValidatorC : AbstractValidator<SampleC>
    {
        public SampleValidatorC()
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
                        context.Errors.Add("Value cannot be greater than 100");

                        return false;
                    }

                    return true;
                });

            RuleFor(p => p.Field03)
                .Rule((context, value) =>
                {
                    if (default(SampleD).Equals(value))
                    {
                        context.Errors.Add("Value cannot be default");

                        return false;
                    }

                    return true;
                }).Validator();

        }
    }
}
