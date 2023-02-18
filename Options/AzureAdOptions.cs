namespace OptionsValidation.Options;

public class AzureAdOptions
{
    public const string SectionName = "AzureAd";
    
    public required Guid ClientId { get; init; }
    public required string TenantId { get; init; }
    public required string Instance { get; init; }
}