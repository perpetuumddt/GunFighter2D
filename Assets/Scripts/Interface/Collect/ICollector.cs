using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollector
{
    void Collect(ICollectable collectable);
}
