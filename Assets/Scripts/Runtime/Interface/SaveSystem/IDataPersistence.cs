using Gunfighter.Runtime.SaveSystem;

namespace Gunfighter.Runtime.Interface.SaveSystem
{
    public interface IDataPersistence
    {
        public void Save(ref GameSaveData gameSaveData);
        public void Load(GameSaveData gameSaveData);
    }
}
