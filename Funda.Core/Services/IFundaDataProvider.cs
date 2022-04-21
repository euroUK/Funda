using Funda.Core.ApiModels;
using Funda.Core.Models;

namespace Funda.Core.Services;

public interface IFundaDataProvider
{
    Task<IEnumerable<EstatePropertyModel>> GetEstatePropertiesAsync(
        OfferType offerType,
        string searchQuery,
        DateTime? startDate = null,
        CancellationToken cancellationToken = default);
}