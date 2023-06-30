using UnityEngine;

namespace Plugins.NavMeshPlus_master.NavMeshComponents.Scripts
{
    public class AgentRotate2d: MonoBehaviour
    {
        private AgentOverride2d _override2D;
        private void Start()
        {
            _override2D = GetComponent<AgentOverride2d>();
            _override2D.AgentOverride = new RotateAgentInstantly(_override2D.Agent, _override2D);
        }

    }
}
