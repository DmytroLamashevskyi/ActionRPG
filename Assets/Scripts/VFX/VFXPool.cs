using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXPool : MonoBehaviour
{
    public static VFXPool Instance;

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    private Dictionary<string, Queue<GameObject>> _poolDictionary;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        _poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in pools)
        {
            var objectPool = new Queue<GameObject>();

            for(int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            _poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if(!_poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Пул с тегом " + tag + " не найден.");
            return null;
        }

        GameObject objectToSpawn = _poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        _poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
