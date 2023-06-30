using UnityEngine;
using UnityEngine.AI;

namespace Plugins.NavMeshPlus_master.NavMeshComponents.Scripts
{
    public interface IAgentOverride
    {
        void UpdateAgent();
    }

    public class AgentDefaultOverride : IAgentOverride
    {
        public void UpdateAgent()
        {
        }
    }
    public class AgentOverride2d: MonoBehaviour
    {
        public NavMeshAgent Agent { get; private set; }
        public IAgentOverride AgentOverride { get; set; }
        private void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();
        }
        private void Start()
        {
            Agent.updateRotation = false;
            Agent.updateUpAxis = false;
        }

        private void Update()
        {
            AgentOverride?.UpdateAgent();
        }
    }
}
