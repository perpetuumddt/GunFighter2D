using UnityEngine;
using UnityEngine.AI;

//***********************************************************************************
// Contributed by author @Lazy_Sloth from unity forum (https://forum.unity.com/)
//***********************************************************************************
namespace Plugins.NavMeshPlus_master.NavMeshComponents.Scripts
{
    public class RotateAgentInstantly: IAgentOverride
    {

        public RotateAgentInstantly(NavMeshAgent agent, AgentOverride2d owner)
        {
            this._agent = agent;
            this._owner = owner;
        }
        private NavMeshAgent _agent;
        private AgentOverride2d _owner;
        private Vector3 _nextWaypoint;

        public void UpdateAgent()
        {
            if (_agent.hasPath && _agent.path.corners.Length > 1)
            {
                if (_nextWaypoint != _agent.path.corners[1])
                {
                    RotateToPoint(_agent.path.corners[1], _agent.transform);
                    _nextWaypoint = _agent.path.corners[1];
                }
            }
        }

        private static void RotateToPoint(Vector3 targetPoint, Transform transform)
        {
            Vector3 targetVector = targetPoint - transform.position;
            float angleDifference = Vector2.SignedAngle(transform.up, targetVector);
            transform.rotation = Quaternion.Euler(0, 0, transform.localEulerAngles.z + angleDifference);
        }
    }
}