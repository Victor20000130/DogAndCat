using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerSpawner : EnemySpawner
{



    private void Start()
    {
        for (int i = 0; i < unitPrefabs.Length; i++)
        {
            PoolManager.Instance.CreatePool(unitPrefabs[i].gameObject, 20);
        }
    }

    public void Spawn(int num)
    {
        GameObject unitObj = PoolManager.Instance.GetObject(unitPrefabs[num].name, unitPrefabs[num].transform.position, unitPrefabs[num].transform.rotation);
        Unit unit = unitObj.GetComponent<Unit>();
        PutStats(unit, unitStats[num]);
        PlayerUnitListUp(unit);
        AllUnitListUp(unit);
        GameManager.Instance.player.coins -= unit.unitCost;
        UIManager.Instance.SpawnEnDisAble();
    }

    protected override void PutStats(Unit unit, UnitStats stats)
    {
        base.PutStats(unit, stats);
    }

}
