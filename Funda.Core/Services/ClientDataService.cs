using Funda.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Funda.Core.Services
{
    /// <summary>
    /// The service provides data to the UI client
    /// </summary>
    public class ClientDataService
    {
        private readonly PropertyDbContext _dbContext;

        public ClientDataService(PropertyDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <summary>
        /// Get cities tag types
        /// </summary>
        /// <returns>IEnumerable<TagType></returns>
        public IEnumerable<TagType> GetCitiesTags()
        {
            return _dbContext.TagTypes
                .Include(x => x.ParentTag)
                .Where(x => x.ParentTagId == -1)
                .ToArray();
        }
        /// <summary>
        /// Get options tag types
        /// </summary>
        /// <returns>IEnumerable<TagType></returns>
        public IEnumerable<TagType> GetOptionsTags()
        {
            return _dbContext.TagTypes
                .Include(x => x.ParentTag)
                .Where(x => x.ParentTagId == 1 || x.Id == 1)
                .ToArray();
        }

        /// <summary>
        /// Get all real estate properties
        /// </summary>
        /// <returns>IEnumerable<EstateProperty></returns>
        public IEnumerable<EstateProperty> GetAllProperties()
        {
            return _dbContext.EstateProperties
                .Include(x => x.Agent)
                .ToArray();
        }

        /// <summary>
        /// Calculate statistics for provided parameters
        /// </summary>
        /// <param name="city">City tag</param>
        /// <param name="offerType">Offer type</param>
        /// <param name="options">Options tag</param>
        /// <param name="size">Result size</param>
        /// <returns>Enumerable of statistics view model</returns>
        public IEnumerable<AgentStatistics> GetStatistics(
            TagType city,
            OfferType offerType,
            TagType options,
            int size)
        {
            if (null == city || null == options)
                return Enumerable.Empty<AgentStatistics>();
            return _dbContext.EstateProperties
                .Include(x => x.Agent)
                .Where(
                    x => (city.Id == -1 || x.City == city.Name) &&
                         x.OfferType == offerType &&
                         x.Tags.Any(y => y.TagType.Id == options.Id)
                )
                .ToLookup(x => x.Agent)
                .Select(x => new AgentStatistics { Agent = x.Key, TotalPositions = x.Count() })
                .OrderByDescending(x => x.TotalPositions)
                .Take(size)
                .ToArray();
        }
    }
}