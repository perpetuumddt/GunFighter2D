using UnityEngine;

namespace Plugins.NavMeshPlus_master.NavMeshComponents.Scripts
{
    class AgentRotateSmooth2d: MonoBehaviour
    {
        public float angularSpeed;
        private AgentOverride2d _override2D;

        private void Start()
        {
            _override2D = GetComponent<AgentOverride2d>();
            _override2D.AgentOverride = new RotateAgentSmoothly(_override2D.Agent, _override2D, angularSpeed);
        }
    }
}
