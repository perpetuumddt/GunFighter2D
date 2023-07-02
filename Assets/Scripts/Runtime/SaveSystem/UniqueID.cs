using System;
using Gunfighter.Runtime.General;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.Runtime.SaveSystem
{
    [System.Serializable]
    [ExecuteInEditMode]
    public class UniqueID : MonoBehaviour
    {
        [FormerlySerializedAs("_id")] [SerializeField, ReadOnly] private string id;
        [SerializeField] private static SerializableDictionary<string, GameObject> _idDatabase 
            = new SerializableDictionary<string,GameObject>();
        public string ID => id;

        private void Awake()
        {
            if (_idDatabase == null)
                _idDatabase = new SerializableDictionary<string, GameObject>();
            if (_idDatabase.ContainsKey(id)) Generate();
            else _idDatabase.Add(id, this.gameObject);
        }

        private void OnDestroy()
        {
            if (_idDatabase.ContainsKey(id)) _idDatabase.Remove(id);
        }

        private void Generate()
        {
            id = Guid.NewGuid().ToString();
            _idDatabase.Add(id, this.gameObject);
        }
    }
}

