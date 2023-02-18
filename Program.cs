using FluentValidation;
using Microsoft.Extensions.Options;
using OptionsValidation.Extensions;
using OptionsValidation.Options;
using OptionsValidation.Validators;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

// Add services to the container.
builder.Services.AddSingleton<IValidator<AzureAdOptions>, AzureAdOptionsValidator>();

builder.Services
    .AddOptions<AzureAdOptions>()
    .Bind(config.GetSection(AzureAdOptions.SectionName))
    .ValidateFluently()
    .ValidateOnStart();

var app = builder.Build();

app.MapGet("/azureadoptions", (IOptions<AzureAdOptions> options) => options.Value);

app.Run();