using Funda.Core.Models;

namespace Funda.Core.Models
{
    /// <summary>
    /// View model for statistics UI
    /// </summary>
    public class AgentStatistics
    {
        public Agent Agent { get; set; }
        public int TotalPositions { get; set; }
    }
}