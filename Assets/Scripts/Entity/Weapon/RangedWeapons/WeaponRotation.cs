using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Gunfighter.Entity.Weapon.RangedWeapons
{
    public class WeaponRotation : MonoBehaviour
    {
        [FormerlySerializedAs("_mainCam")] [SerializeField]
        private Camera mainCam;
        [FormerlySerializedAs("_shotPoint")] [SerializeField]
        private Transform shotPoint;
        [FormerlySerializedAs("_weaponSprite")] [SerializeField]
        private SpriteRenderer weaponSprite;

        private void Update()
        {
            Vector3 mousepos = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mousepos.z = 0f;
            Vector2 rotation = mousepos - transform.position;
            float rotationZ = Mathf.Atan2(rotation.y, rotation.x)*Mathf.Rad2Deg; //rotation in degrees

            RotationLogic(rotation,rotationZ);
        }

        private void RotationLogic(Vector2 rotation, float rotationZ)
        {
            shotPoint.transform.rotation = Quaternion.Euler(0, 0, rotationZ + 270);
            weaponSprite.transform.rotation = Quaternion.Euler(0, 0, rotationZ + 180);

            if (rotation.x < 0.1)
            {
                WeaponFlip();
            }
        }

        private void WeaponFlip()
        {
            weaponSprite.transform.Rotate(180, 0, 0);
        }
    }
}
