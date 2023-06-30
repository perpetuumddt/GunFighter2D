using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : TilableDisplayController
{

    public HealthBarController(GameObject heartPrefab): base(heartPrefab)
    {

    }

    protected override void SetupUnit(int i)
    {
        Vector3 location = new Vector3(5 + i * 15, -5 + ((i%2)* -5), 0);
        _units[i].transform.SetLocalPositionAndRotation(location, new Quaternion());
    }


    
}
