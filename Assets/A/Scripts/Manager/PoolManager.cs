using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public Unit[] units;
    public Dictionary<string, Unit> originalPool = new();
    public Dictionary<string, List<Unit>> poolDic = new();
    public void Awake()
    {
        for (int i = 0; i < units.Length; i++)
        {
            originalPool.Add(units[i].name, units[i]);
            poolDic.Add(units[i].name, new());
        }
    }
    public Unit Pop(string key)
    {
        List<Unit> targetPool = poolDic[key];
        if (targetPool.Count <= 0)
        {
            Unit newUnit = Instantiate(originalPool[key]);
            newUnit.name = key;
            targetPool.Add(newUnit);
        }
        Unit returnUnit = targetPool[0];
        targetPool.RemoveAt(0);
        returnUnit.gameObject.SetActive(true);
        returnUnit.transform.SetParent(null);
        return returnUnit;
    }
    public void Push(Unit unit)
    {
        unit.gameObject.SetActive(false);
        unit.transform.SetParent(transform);
        poolDic[unit.name].Add(unit);
    }
}
