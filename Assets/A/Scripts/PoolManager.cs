using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : SingletonManager<PoolManager>
{
    // 풀링할 오브젝트들을 담을 Dictionary
    Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();

    // 풀 생성 메서드
    public void CreatePool(GameObject prefab, int size)
    {
        string poolKey = prefab.name;
        GameObject poolHolder = new GameObject($"Pool - {poolKey}");
        poolHolder.transform.parent = transform;

        if (!poolDictionary.ContainsKey(poolKey))
            poolDictionary.Add(poolKey, new Queue<GameObject>());

        for (int i = 0; i < size; i++)
        {
            GameObject obj = Instantiate(prefab, poolHolder.transform);
            obj.SetActive(false);
            poolDictionary[poolKey].Enqueue(obj);
        }
    }

    // 오브젝트 가져오기
    public GameObject GetObject(string poolKey, Vector3 position, Quaternion rotation)
    {
        if (poolDictionary.ContainsKey(poolKey))
        {
            if (poolDictionary[poolKey].Count == 0)
                return null;

            GameObject obj = poolDictionary[poolKey].Dequeue();
            obj.SetActive(true);
            obj.transform.position = position;
            obj.transform.rotation = rotation;

            return obj;
        }
        return null;
    }

    // 오브젝트 반환
    public void ReturnObject(string poolKey, GameObject obj)
    {
        if (poolDictionary.ContainsKey(poolKey))
        {
            obj.SetActive(false);
            poolDictionary[poolKey].Enqueue(obj);
        }
    }
}
