using System.Collections;
using UnityEngine;
using UnityEngine.AI;

//***********************************************************************************
// Contributed by author @Lazy_Sloth from unity forum (https://forum.unity.com/)
//***********************************************************************************
namespace Plugins.NavMeshPlus_master.NavMeshComponents.Scripts
{
    public class RotateAgentSmoothly: IAgentOverride
    {
        public RotateAgentSmoothly(NavMeshAgent agent, AgentOverride2d owner, float rotateSpeed)
        {
            this._agent = agent;
            this._owner = owner;
            this.RotateSpeed = rotateSpeed;
        }

        private NavMeshAgent _agent;
        private AgentOverride2d _owner;
        private Vector2 _nextWaypoint;
        private float _angleDifference;
        private float _targetAngle;
        public float RotateSpeed;

        public void UpdateAgent()
        {
            if (_agent.hasPath && _agent.path.corners.Length > 1)
            {
                if (_nextWaypoint != (Vector2)_agent.path.corners[1])
                {
                    _owner.StartCoroutine(_RotateCoroutine());
                    _nextWaypoint = _agent.path.corners[1];
                }
            }
        }
        protected IEnumerator _RotateCoroutine()
        {
            yield return RotateToWaypoints(_agent.transform);
        }
        protected IEnumerator RotateToWaypoints(Transform transform)
        {
            Vector2 targetVector = _agent.path.corners[1] - transform.position;
            _angleDifference = Vector2.SignedAngle(transform.up, targetVector);
            _targetAngle = transform.localEulerAngles.z + _angleDifference;

            if (_targetAngle >= 360) { _targetAngle -= 360; }
            else if (_targetAngle < 0) { _targetAngle += 360; }

            while (transform.localEulerAngles.z < _targetAngle - 0.1f || transform.localEulerAngles.z > _targetAngle + 0.1f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, _targetAngle), RotateSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}