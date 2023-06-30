using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.General.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        [FormerlySerializedAs("_target")] [SerializeField]
        private Transform target;

        [SerializeField]
        private float smoothing = 1f;

        private Vector3 _offset = new Vector3(0, 0, -10);
        private void Start()
        {
            //_target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void LateUpdate()
        {
            if (target == null) return;
            if (transform.position == target.position) return;
            var targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
            //transform.position = _target.position + offset;
        }
    }
}