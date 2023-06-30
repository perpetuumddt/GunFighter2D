using UnityEngine;

namespace General.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField]
        private Transform _target;

        [SerializeField]
        private float smoothing = 1f;

        private Vector3 offset = new Vector3(0, 0, -10);
        private void Start()
        {
            //_target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void LateUpdate()
        {
            if (_target == null) return;
            if (transform.position == _target.position) return;
            var _targetPosition = new Vector3(_target.position.x, _target.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, _targetPosition, smoothing);
            //transform.position = _target.position + offset;
        }
    }
}