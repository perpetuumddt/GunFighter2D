using System;
using System.IO;
using UnityEngine;

namespace Gunfighter.SaveSystem
{
    public class FileDataHandler
    {
        private string _dataDirPath;
        private string _dataFileName;

        public string FullFilePath => Path.Combine(_dataDirPath, _dataFileName);
    
        public FileDataHandler(string dataDirPath, string dataFileName)
        {
            this._dataDirPath = dataDirPath;
            this._dataFileName = dataFileName;
        }

        public GameSaveData Load()
        {
            GameSaveData loadedData = null;
            if (!File.Exists(FullFilePath)) return loadedData;
            try
            {
                string dataToLoad;
            
                using (FileStream stream = new FileStream(FullFilePath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                loadedData = JsonUtility.FromJson<GameSaveData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error occured reading from a file: " + FullFilePath + "\n " + e);
            }
            return loadedData;
        }

        public void Save(GameSaveData gameSaveData)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(FullFilePath));
                string dataToStore = JsonUtility.ToJson(gameSaveData, true);

                using (FileStream stream = new FileStream(FullFilePath, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Write(dataToStore);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Error occured when saving to a file: " + FullFilePath + "\n " + e);
            }
        }
    }
}
