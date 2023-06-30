using System.Collections.Generic;
using System.Linq;
using General;
using Interface.SaveSystem;
using UnityEngine;

namespace SaveSystem
{
    public class DataPersistenceManager : Singleton<DataPersistenceManager>
    {
        [Header("File Storage Config")] [SerializeField]
        private string _fileName;
    
        private GameSaveData _gameSaveData;
        private List<IDataPersistence> _dataPersistenceObjects;
        private FileDataHandler _fileDataHandler;

        private void Start()
        {
            _fileDataHandler = new FileDataHandler(Application.persistentDataPath, _fileName);
            _dataPersistenceObjects = FindAllDataPersistenceObjects();
        }

        private List<IDataPersistence> FindAllDataPersistenceObjects()
        {
            IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
            return new List<IDataPersistence>(dataPersistenceObjects);
        }

        [ContextMenu("Data Persistence Manager/New game")]    
        public void NewGame()
        {
            _gameSaveData = new GameSaveData();
            UpdatePersistentObjects();
        }

        private void UpdatePersistentObjects()
        {
            foreach (IDataPersistence dataPersistenceObject in _dataPersistenceObjects)
            {
                dataPersistenceObject.Load(_gameSaveData);
            }
        }
    
        [ContextMenu("Data Persistence Manager/Load game")]
        public void LoadGame()
        {
            this._gameSaveData = _fileDataHandler.Load();

            if (this._gameSaveData == null)
            {
                Debug.Log("No data was found, initializing new game");
                NewGame();
            }

            UpdatePersistentObjects();
        
        }
    
        [ContextMenu("Data Persistence Manager/Save game")]
        public void SaveGame()
        {
            if (_gameSaveData == null) _gameSaveData = new GameSaveData();
            foreach (IDataPersistence dataPersistenceObject in _dataPersistenceObjects)
            {
                dataPersistenceObject.Save(ref _gameSaveData);
            }
        
            _fileDataHandler.Save(_gameSaveData);
        }
    }
}
