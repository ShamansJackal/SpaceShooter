using System.Collections.Generic;
using UnityEngine;

namespace Pool
{
    public class ObjectPooler : MonoBehaviour
    {
        [System.Serializable]
        public class Pool
        {
            public string tag;
            public GameObject prefab;
            public int size;
        }

        public static ObjectPooler instance;

        public void Awake()
        {
            instance = this;
        }

        public List<Pool> pools;
        public Dictionary<string, Queue<GameObject>> PoolDictionary;

        public void Start()
        {
            PoolDictionary = new Dictionary<string, Queue<GameObject>>();

            foreach(var pool in pools)
            {
                Queue<GameObject> queue = new Queue<GameObject>();

                for (int i = 0; i < pool.size; i++)
                {
                    var obj = Instantiate(pool.prefab, transform);
                    obj.SetActive(false);
                    queue.Enqueue(obj);
                }

                PoolDictionary.Add(pool.tag, queue);
            }
        }

        public GameObject SpawnFromPool(string name, Vector3 position, Quaternion rotation)
        {
            if(!PoolDictionary.ContainsKey(name)) { Debug.LogWarning("No such pool"); return null; }

            var obj = PoolDictionary[name].Dequeue();

            obj.transform.SetPositionAndRotation(position, rotation);
            obj.SetActive(true);
            PoolDictionary[name].Enqueue(obj);

            IPoolebObject poolebObject = obj.GetComponent<IPoolebObject>();

            if (poolebObject != null) poolebObject.OnSpawn();

            return obj;
        }
    }
}
