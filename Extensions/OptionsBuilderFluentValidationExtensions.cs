using FluentValidation;
using Microsoft.Extensions.Options;

namespace OptionsValidation.Extensions;

public static class OptionsBuilderFluentValidationExtensions
{
    public static OptionsBuilder<TOptions> ValidateFluently<TOptions>(this OptionsBuilder<TOptions> optionsBuilder, 
        string? name = null) 
        where TOptions : class
    {
        optionsBuilder.Services.AddSingleton<IValidateOptions<TOptions>>(
            s => new FluentValidationOptions<TOptions>(
                name, 
                s.GetRequiredService<IValidator<TOptions>>()));
        return optionsBuilder;
    }
}

public class FluentValidationOptions<TOptions> : IValidateOptions<TOptions>
    where TOptions : class
{
    public string? Name { get; }
    private readonly IValidator<TOptions> _validator;

    public FluentValidationOptions(string? name, IValidator<TOptions> validator)
    {
        Name = name;
        _validator = validator;
    }

    public ValidateOptionsResult Validate(string? name, TOptions options)
    {
        if (Name != null && Name != name)
        {
            return ValidateOptionsResult.Skip;
        }

        ArgumentNullException.ThrowIfNull(options);
        
        var validationResult = _validator.Validate(options);
        
        if (validationResult.IsValid)
        {
            return ValidateOptionsResult.Success;
        }
        
        var errors = validationResult.Errors.Select(e => 
            $"Options validation failed for '{e.PropertyName}' with error: '{e.ErrorMessage}'").ToList();
        
        return ValidateOptionsResult.Fail(errors);
    }
}