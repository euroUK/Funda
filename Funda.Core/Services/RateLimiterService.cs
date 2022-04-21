namespace Funda.Core.Services
{
    /// <summary>
    /// RateLimiterService is used to limit actions count in period.
    /// </summary>
    public class RateLimiterService
    {
        private readonly Dictionary<string, RateLimiter> _rateLimiters = new();

        /// <summary>
        /// Wait until call satisfies RateLimiter policy
        /// </summary>
        /// <param name="name">Unique name</param>
        public async Task WaitForAny(string name)
        {
            var rateLimiter = _rateLimiters[name];

            await rateLimiter.WaitForRequest();
        }

        /// <summary>
        /// Register unique name with limit parameters
        /// </summary>
        /// <param name="name">Unique name</param>
        /// <param name="limit">Maximum request count</param>
        /// <param name="interval">TIme interval in seconds</param>
        public void Register(string name, int limit, int interval)
        {
            if (!_rateLimiters.ContainsKey(name))
            {
                var limiter = new RateLimiter(name, limit, interval);

                _rateLimiters.Add(name, limiter);
            }
        }

        public void Unregister(string name)
        {
            if (_rateLimiters.ContainsKey(name))
            {
                _rateLimiters.Remove(name);
            }
        }
    }
}
