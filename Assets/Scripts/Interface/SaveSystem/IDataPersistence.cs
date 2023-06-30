using SaveSystem;

namespace Interface.SaveSystem
{
    public interface IDataPersistence
    {
        public void Save(ref GameSaveData gameSaveData);
        public void Load(GameSaveData gameSaveData);
    }
}
