using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gunfighter.General.Objects_Pool
{
    public class PoolMono<T> where T : MonoBehaviour
    {
        public T Prefab { get; }
        public bool AutoExpand { get; set; }
        public Transform Container { get; }

        private List<T> _pool;

        public PoolMono(T prefab, int count)
        { 
            this.Prefab = prefab;
            this.Container = null;

            this.CreatePool(count);
        }

        public PoolMono(T prefab, int count, Transform container) 
        {
            this.Prefab = prefab;
            this.Container = container;

            this.CreatePool(count);
        }

        private void CreatePool(int count)
        {
            this._pool = new List<T>();

            for(int i =0;i< count;i++) 
            {
                this.CreateObject();
            }
        }

        private T CreateObject(bool isActiveByDefault = false)
        {
            var createdObject = UnityEngine.Object.Instantiate(this.Prefab, this.Container);
            createdObject.gameObject.SetActive(isActiveByDefault);
            this._pool.Add(createdObject);
            return createdObject;
        }

        public bool HasFreeElement(out T element)
        {
            foreach(var mono in _pool)
            {
                if(!mono.gameObject.activeInHierarchy)
                {
                    element = mono;
                    mono.gameObject.SetActive(true);
                    return true;
                }
            }
            element = null;
            return false;
        }

        public T GetFreeElement()
        {
            if(this.HasFreeElement(out var element))
            {
                return element;
            }
            if(this.AutoExpand)
            {
                return this.CreateObject(true);
            }
            throw new Exception($"There is no free elements in pool of type {typeof(T)}");
        }
    }
}
