using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    [Serializable]
    public class Pool
    {
        public int size;
        public GameObject prefab;
        public string name => prefab.name;
    }

    public static ObjectPoolManager Instance { get; private set; }

    public List<Pool> pools;
    private Dictionary<string, ObjectPool> _poolDict = new Dictionary<string, ObjectPool>();


    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        foreach (var pool in pools)
        {
            ObjectPool objectPool = new ObjectPool();
            objectPool.Init(pool.prefab, pool.size, transform);
            _poolDict.Add(pool.name, objectPool);
        }
    }
    
    public GameObject Get(string name, Transform parent)
    {
        if (!_poolDict.ContainsKey(name)) return null;

        var obj = _poolDict[name].Get(parent);
        //obj.transform.position = parent.position;
        return obj;
    }

    public void Return(GameObject gameObject)
    {
        //remove (clone) from gameObject name
        string name = gameObject.name.Substring(0, gameObject.name.Length - 7);
        
        if (!_poolDict.ContainsKey(name)) return;

        _poolDict[name].Return(gameObject);
    }

    //public void ReturnAllPool()
    //{
    //    foreach (var pool in _poolDict.Values)
    //    {
    //        while (pool._actives.Count > 0)
    //            pool.Return(pool._actives[pool._actives.Count - 1]);
    //    }
    //}
}

public class ObjectPool
{
    private GameObject _prefab;
    private int _size;
    private Queue<GameObject> _objectPool = new Queue<GameObject>();

    public void Init(GameObject prefab, int size, Transform parent = null)
    {
        _prefab = prefab;
        _size = size;

        GameObject container = new GameObject(_prefab.name);
        container.transform.SetParent(parent);

        for (int i = 0; i < size; i++)
        {
            var obj = GameObject.Instantiate(_prefab, container.transform);
            obj.SetActive(false);
            _objectPool.Enqueue(obj);
        }
    }

    public GameObject Get(Transform parent = null)
    {
        if (_objectPool.Count <= 0) return null;

        GameObject obj = _objectPool.Dequeue();
        obj.transform.position = parent.position;
        obj.SetActive(true);
        return obj;
    }

    public void Return(GameObject obj)
    {
        obj.SetActive(false);
        _objectPool.Enqueue(obj);
    }
}