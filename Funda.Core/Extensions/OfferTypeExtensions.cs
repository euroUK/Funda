using Funda.Core.Models;

namespace Funda.Core.Extensions;

public static class OfferTypeExtensions
{
    public static string ToFundaString(this OfferType value)
    {
        return value switch
        {
            OfferType.Buy => "koop",
            OfferType.Rent => "huur",
            _ => throw new NotImplementedException()
        };
    }
}