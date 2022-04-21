using System.ComponentModel.DataAnnotations;

namespace Funda.Core.Models
{
    public class Agent
    {
        public int AgentId { get; set; }

        [MaxLength(100)]
        public string AgentName { get; set; }

        public List<EstateProperty> Properties { get; set; }

        public Agent(int agentId, string agentName)
        {
            AgentId = agentId;
            AgentName = agentName;
        }
    }
}