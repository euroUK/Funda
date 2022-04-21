using Funda.Core.ApiModels;
using Funda.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Funda.Core.Services
{
    /// <summary>
    /// Service is used to get real estate objects and agents from data provider and store it into database.
    /// </summary>
    public class FundaDataLoaderService
    {
        private readonly PropertyDbContext _dbContext;
        private readonly IFundaDataProvider _provider;

        public FundaDataLoaderService(PropertyDbContext dbContext, IFundaDataProvider provider)
        {
            _dbContext = dbContext;
            _provider = provider;
        }

        /// <summary>
        /// Query data for each city and each option available and store in database
        /// </summary>
        /// <param name="offerType">Offer type</param>
        /// <param name="fromDate">Start from, optional</param>
        /// <returns>Messages for UI</returns>
        public async IAsyncEnumerable<string> LoadData(OfferType offerType, DateTime? fromDate)
        {
            yield return "Loading from Funda started";

            var tagTypes = GetAllTagTypes();
            var cities = tagTypes.Where(x => x.ParentTagId == -1).ToList();

            foreach (var city in cities)
            {
                foreach (var tagType in tagTypes.Where(x => x.ParentTagId != -1).OrderBy(x => x.Code))
                {
                    var (searchQuery, count) = await LoadDataBySearchQuery(offerType, city, tagType, fromDate);

                    yield return $"Loaded {count} items from '{searchQuery}'";
                }
            }

            yield return "Loading from Funda finished";
        }

        private async Task<(string searchQuery, int count)> LoadDataBySearchQuery(
            OfferType offerType,
            TagType city,
            TagType option,
            DateTime? selectedDate)
        {
            var searchQuery = GetQueryTag(city, option);

            var models = await _provider.GetEstatePropertiesAsync(offerType, searchQuery, selectedDate);

            SaveAgents(models);
            SaveEstatePropertiesWithTags(models, option);

            await _dbContext.SaveChangesAsync();

            return (searchQuery, models.Count());
        }

        private void SaveAgents(IEnumerable<EstatePropertyModel> models)
        {
            foreach (var model in models.DistinctBy(x => x.MakelaarId))
            {
                if (!_dbContext.Agents.Any(x => x.AgentId == model.MakelaarId))
                {
                    _dbContext.Agents.Add(new Agent(model.MakelaarId, model.MakelaarNaam));
                }
            }
        }

        private void SaveEstatePropertiesWithTags(IEnumerable<EstatePropertyModel> models, TagType tagType)
        {
            foreach (var model in models)
            {
                var estateProperty = _dbContext.EstateProperties
                    .Include(x => x.Tags)
                    .FirstOrDefault(x => x.GlobalId == model.GlobalId);

                if (estateProperty == null)
                {
                    estateProperty = model.ToEstateProperty();

                    _dbContext.EstateProperties.Add(estateProperty);
                }

                if (!estateProperty.Tags.Any(x => x.TagType.Id == tagType.Id))
                {
                    estateProperty.Tags.Add(new PropertyTag { TagType = tagType });
                }
            }
        }

        private static string GetQueryTag(TagType city, TagType tagType)
        {
            return string.IsNullOrEmpty(tagType.Code)
                ? city.Code
                : $"{city.Code}/{tagType.Code}";
        }

        private List<TagType> GetAllTagTypes()
        {
            return _dbContext.TagTypes
                .Include(x => x.ParentTag)
                .Where(x => x.Id > 0)
                .OrderBy(x => x.Id)
                .ToList();
        }
    }
}
