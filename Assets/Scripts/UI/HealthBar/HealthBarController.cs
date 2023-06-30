using UnityEngine;

namespace UI.HealthBar
{
    public class HealthBarController : TilableDisplayController
    {

        public HealthBarController(GameObject heartPrefab, GameObject parentGameObj): base(heartPrefab, parentGameObj)
        {

        }

        protected override void SetupUnit(int i)
        {
            Vector3 location = new Vector3(5 + i * 15, -5 + ((i%2)* -5), 0);
            _units[i].transform.SetLocalPositionAndRotation(location, new Quaternion());
        }


    
    }
}
