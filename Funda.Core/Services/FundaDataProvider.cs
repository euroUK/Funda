using Funda.Core.ApiModels;
using Funda.Core.Convertors;
using Funda.Core.Models;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json;
using Funda.Core.Extensions;
using Funda.Core.Options;
using Microsoft.Extensions.Options;

namespace Funda.Core.Services
{
    /// <summary>
    /// This provider performs API calls and returns EstatePropertyModel object
    /// </summary>
    public class FundaDataProvider: IFundaDataProvider
    {
        private readonly FundaOptions _options;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly RateLimiterService _rateLimiterService;
        private readonly ILogger<FundaDataProvider> _logger;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public FundaDataProvider(
            IOptions<FundaOptions> options,
            IHttpClientFactory httpClientFactory,
            RateLimiterService rateLimiterService,
            ILogger<FundaDataProvider> logger)
        {
            _options = options.Value;
            _httpClientFactory = httpClientFactory;
            _rateLimiterService = rateLimiterService;
            _logger = logger;

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                Converters = { new UnixEpochOffsetConverter(), new UnixEpochOffsetArrayConverter() },
            };

            _rateLimiterService.Register(GetType().FullName!, _options.RateLimit, _options.RateInterval);
        }

        /// <summary>
        /// Async Get Lit of EstatePropertyModel from Funda API
        /// </summary>
        /// <param name="offerType">Offer type</param>
        /// <param name="searchQuery">Funda search query</param>
        /// <param name="startDate">Start date, optional</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>IEnumerable<EstatePropertyModel></returns>
        public async Task<IEnumerable<EstatePropertyModel>> GetEstatePropertiesAsync(
            OfferType offerType,
            string searchQuery,
            DateTime? startDate = null,
            CancellationToken cancellationToken = default)
        {
            var result = new List<EstatePropertyModel>();

            var httpClient = GetHttpClient();

            var pageNumber = 1;
            var totalPages = 1;

            while (pageNumber <= totalPages && (!cancellationToken.IsCancellationRequested))
            {
                var data = await GetData(httpClient, offerType, searchQuery, pageNumber, startDate, cancellationToken);

                totalPages = data.totalPages;
                result.AddRange(data.properties);

                pageNumber++;
            }

            return result.DistinctBy(x => x.GlobalId);
        }

        private async Task<(int totalPages, IEnumerable<EstatePropertyModel> properties)> GetData(
            HttpClient httpClient,
            OfferType offerType,
            string searchQuery,
            int pageNumber,
            DateTime? startDate,
            CancellationToken cancellationToken)
        {
            await _rateLimiterService.WaitForAny(GetType().FullName!);

            try
            {
                var requestUri = GetRequestUri(offerType, searchQuery, _options.PageSize, pageNumber, startDate);

                var response = await httpClient.GetFromJsonAsync<FundaResponse>(
                    requestUri, _jsonSerializerOptions, cancellationToken
                );

                if (response != null)
                {
                    return (response.Paging.AantalPaginas, response.Objects);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return (pageNumber, Enumerable.Empty<EstatePropertyModel>());
        }

        private HttpClient GetHttpClient()
        {
            var client = _httpClientFactory.CreateClient();

            client.BaseAddress = new Uri($"{_options.ApiEndpoint}/{_options.ApiKey}");
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
        }

        private static string GetRequestUri(
            OfferType offerType,
            string searchQuery,
            int pageSize,
            int pageNumber,
            DateTime? startDate)
        {
            var result = $"?type={offerType.ToFundaString()}&zo=/{searchQuery}/&page={pageNumber}&pagesize={pageSize}";

            if (startDate != null)
            {
                result += $"&since={startDate.Value:yyyy-MM-ddTHH:mm:ss}";
            }

            return result;
        }
    }
}