using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Gunfighter.Runtime.Entity.Weapon.RangedWeapons
{
    public class WeaponRotation : MonoBehaviour
    {
        [SerializeField]
        private Camera mainCam;
        [SerializeField]
        private Transform shotPoint;
        [SerializeField]
        private SpriteRenderer weaponSprite;

        private bool isFlipped = false;

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
                weaponSprite.transform.localPosition = new Vector3(-.25f, -.17f);
            }
            else if (rotation.x > 0.1)
            {
                weaponSprite.transform.localPosition = new Vector3(.25f, -.17f);
            }
        }

        private void WeaponFlip()
        {
            isFlipped = !isFlipped;
            weaponSprite.transform.Rotate(180, 0, 0);
        }
    }
}
