using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : Singleton<DataPersistenceManager>
{
    private GameSaveData _gameSaveData;
    private List<IDataPersistence> _dataPersistenceObjects;

    private void Start()
    {
        _dataPersistenceObjects = FindAllDataPersistenceObjects();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    
    public void NewGame()
    {
        _gameSaveData = new GameSaveData();
    }
    
    public void LoadGame()
    {


        if (this._gameSaveData == null)
        {
            Debug.Log("No data was found, initializing new game");
            NewGame();
        }

        foreach (IDataPersistence dataPersistenceObject in _dataPersistenceObjects)
        {
            dataPersistenceObject.Load(_gameSaveData);
        }
        
        Debug.Log("Loaded exp " + _gameSaveData.PlayerExperience);
    }
    
    public void SaveGame()
    {
        foreach (IDataPersistence dataPersistenceObject in _dataPersistenceObjects)
        {
            dataPersistenceObject.Save(ref _gameSaveData);
        }
        Debug.Log("Saved  exp " + _gameSaveData.PlayerExperience);
    }
}
