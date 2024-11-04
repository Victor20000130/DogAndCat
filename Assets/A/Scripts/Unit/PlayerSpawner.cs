using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerSpawner : EnemySpawner
{
    public void Spawn(int num)
    {
        var obj = poolManager.Pop(unitPrefabs[num].name);

        PutStats(obj, unitStats[num]);
        PlayerUnitListUp(obj);
        AllUnitListUp(obj);
        GameManager.Instance.player.coins -= obj.unitCost;
        UIManager.Instance.SpawnEnDisAble();
    }
    protected override void PutStats(Unit unit, UnitStats stats)
    {
        base.PutStats(unit, stats);
    }
}
