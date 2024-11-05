using Assets.FantasyMonsters.Common.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRan = UnityEngine.Random;
public class EnemySpawner : MonoBehaviour
{
    public Unit[] unitPrefabs;
    public UnitStats[] unitStats;
    protected PoolManager poolManager;
    public bool isEnemySpawner;
    public float rangeMin;
    public float rangeMax;
    public float spawnXAxis;
    private void Awake()
    {
        poolManager = FindAnyObjectByType<PoolManager>();
    }
    private void Update()
    {
        if (isEnemySpawner)
        {
            EnemySpawn();
        }
    }
    protected virtual void EnemySpawn()
    {
        for (int i = 0; i < unitPrefabs.Length; i++)
        {
            if (unitStats[i].spawnStartTime <= Time.timeSinceLevelLoad)
            {
                var obj = poolManager.Pop(unitPrefabs[i].name);
                PutStats(obj, unitStats[i]);
                EnemiseListUp(obj);
                AllUnitListUp(obj);
            }
        }
    }
    protected virtual void PutStats(Unit unit, UnitStats stats)
    {
        unit.moveSpeed = stats.moveSpeed;
        unit.attackPerSec = stats.attackPerSec;
        unit.damage = stats.damage;
        unit.maxHp = stats.maxHp;
        unit.hp = stats.hp;
        unit.singleAttackType = stats.singleAttackType;
        stats.spawnStartTime += stats.spawnInterval;
        float rand = UniRan.Range(rangeMin, rangeMax);
        unit.transform.position = new Vector2(spawnXAxis, rand);
        unit.unitCost = stats.unitCost;
    }
    private void EnemiseListUp(Unit unit)
    {
        GameManager.Instance.enemise.Add(unit);
    }
    protected void PlayerUnitListUp(Unit unit)
    {
        GameManager.Instance.p_unit.Add(unit);
    }
    protected void AllUnitListUp(Unit unit)
    {
        GameManager.Instance.units.Add(unit);
    }
}


