using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence
{
    public void Save(ref GameSaveData gameSaveData);
    public void Load(GameSaveData gameSaveData);
}
