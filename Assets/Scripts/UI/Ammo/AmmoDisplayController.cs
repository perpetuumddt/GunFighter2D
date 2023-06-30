using UnityEngine;

namespace Gunfighter.UI.Ammo
{
    public class AmmoDisplayController : TilableDisplayController
    {
    
        protected GameObject ContainerElement;
        public AmmoDisplayController(GameObject unitPrefab, GameObject parentGameObject, GameObject containerElement) : base(unitPrefab, parentGameObject)
        {
            ContainerElement = containerElement;
        }
    
        protected override void SetupUnit(int i)
        {
            Vector3 location = new Vector3(-2.5f, 2f + i * 3, 0);
            Units[i].transform.SetLocalPositionAndRotation(location, new Quaternion());
            Vector3 containerLocation = new Vector3(location.x - 40.5f, location.y - 10f, 0);
            ContainerElement.transform.SetLocalPositionAndRotation(containerLocation, new Quaternion(0, 0, 180, 0));
        }
    }
}
