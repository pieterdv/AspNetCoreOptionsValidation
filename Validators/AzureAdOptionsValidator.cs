using FluentValidation;
using OptionsValidation.Options;

namespace OptionsValidation.Validators;

public class AzureAdOptionsValidator : AbstractValidator<AzureAdOptions>
{
    public AzureAdOptionsValidator()
    {
        RuleFor(x => x.ClientId).NotEmpty();
        RuleFor(x => x.TenantId).NotEmpty();
        RuleFor(x => x.Instance).NotEmpty();
    }
}