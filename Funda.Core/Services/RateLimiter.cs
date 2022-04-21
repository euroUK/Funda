using System.Collections.Concurrent;

namespace Funda.Core.Services
{
    /// <summary>
    /// Rate limiter class that verify rate limit policy
    /// </summary>
    public class RateLimiter
    {
        private readonly SemaphoreSlim _pool;
        public string Name { get; private set; }

        private ConcurrentQueue<DateTime> _requestsQueue;
        private readonly int _limit;
        private readonly int _interval;
        private readonly System.Timers.Timer _timer;

        ~RateLimiter()
        {
            _timer.Stop();
            _requestsQueue.Clear();
        }

        public RateLimiter(string name, int limit, int interval)
        {
            _limit = limit;
            _interval = interval;
            Name = name;
            _requestsQueue = new ConcurrentQueue<DateTime>();
            _pool = new SemaphoreSlim(_limit, _limit);
            _timer = new System.Timers.Timer();
            _timer.Interval = 50; 
            _timer.Elapsed += CheckQueue;
            _timer.Start();
        }

        /// <summary>
        /// Waits for semaphore and stores call time in queue
        /// </summary>
        public async Task WaitForRequest()
        {
            await _pool.WaitAsync();
            _requestsQueue.Enqueue(DateTime.UtcNow);
        }

        /// <summary>
        /// Each interval queue is checked if there are old calls that need t be cleared and semaphore released
        /// </summary>
        private void CheckQueue(object? sender, System.Timers.ElapsedEventArgs e)
        {
            DateTime item;
            var now = DateTime.UtcNow;
            var stop = false;
            while (_requestsQueue.TryPeek(out item) && !stop)
            {
                if (item.AddMilliseconds(_interval) < now)
                {
                    if (_requestsQueue.TryDequeue(out item))
                    {
                        _pool.Release();
                    }
                }
                else
                {
                    stop = true;
                }
            }
        }
    }
}
