using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerSpawner : EnemySpawner
{



    public void Spawn(int num)
    {
        Unit unit = Instantiate(unitPrefabs[num]);
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
