using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UnitStats
{

    public float moveSpeed;
    public float attackPerSec;
    public float attackRange;
    public float damage;
    public float maxHp;
    public float hp;
    public bool singleAttackType;
    public Vector2 offSet;
    public Vector2 offSetSize;
    public int unitCost;

    public float spawnStartTime;
    public float spawnInterval;
}