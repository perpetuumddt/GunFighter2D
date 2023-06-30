using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class AmmoDisplayController : TilableDisplayController
{
    
    protected GameObject _containerElement;
    public AmmoDisplayController(GameObject unitPrefab, GameObject containerElement) : base(unitPrefab)
    {
        _containerElement = containerElement;
    }
    
    protected override void SetupUnit(int i)
    {
        Vector3 location = new Vector3(-2.5f, 2f + i * 3, 0);
        _units[i].transform.SetLocalPositionAndRotation(location, new Quaternion());
        Vector3 containerLocation = new Vector3(location.x - 40.5f, location.y - 10f, 0);
        _containerElement.transform.SetLocalPositionAndRotation(containerLocation, new Quaternion(0, 0, 180, 0));
    }
}
