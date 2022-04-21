namespace Funda.Core.Options;

public class FundaOptions
{
    public const string Position = "DataProviders:Funda";

    public string ApiEndpoint { get; set; }
    public string ApiKey { get; set; }
    public int PageSize { get; set; } 
    public int RateLimit { get; set; }
    public int RateInterval { get; set; } 
}
