using System.Collections.Generic;
using UnityEngine;

namespace ExerciseOne
{
    public class ObjectPool<T> where T : Component
    {
        private Queue<T> pool = null;
        private T prefab = null;
        private Transform parent = null;
        private List<T> activeObjects = null;

        public ObjectPool(T _prefab, int _initialSize, Transform _parent = null)
        {
            prefab = _prefab;
            parent = _parent;
            pool = new Queue<T>();
            activeObjects = new List<T>();

            CreateInstances(_initialSize);
        }

        public T GetObject()
        {
            T instance;

            if (pool.Count > 0)
            {
                instance = pool.Dequeue();
            }
            else
            {
                instance = CreateNewInstance();
            }

            instance.gameObject.SetActive(true);
            activeObjects.Add(instance);
            
            return instance;
        }

        public void ReturnObject(T _instance)
        {
            _instance.gameObject.SetActive(false);
            pool.Enqueue(_instance);
            activeObjects.Remove(_instance);
        }

        public void CreateInstances(int _numberOfAdditionalObjects)
        {
            for (int i = 0; i < _numberOfAdditionalObjects; i++)
            {
                T instance = CreateNewInstance();

                pool.Enqueue(instance);
            }
        }

        public List<T> GetAllActiveObjects()
        {
            return activeObjects;
        }

        public T GetARandomActiveObject()
        {
            return activeObjects[Random.Range(0, activeObjects.Count)];
        }

        private T CreateNewInstance()
        {
            T instance = Object.Instantiate(prefab, parent);
            instance.gameObject.name = prefab.name + " #" + pool.Count;
            instance.gameObject.SetActive(false);
            return instance;
        }
    }
}