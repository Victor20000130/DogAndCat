using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public int level = 1;
    public int coins = 0;
    public int getCoinVal = 8;
    public int coinsLimit = 100;
    public int levelPerCoinLimit = 50;
    public int levelPerCoin = 4;
    public int levelUpCost = 40;
    public int nextLevelPerCost = 40;
    public int maxLv = 7;
    public int extraSkillColldown = 60;
    public float coinIncreaseInterval;
    public float coinDisplayDuration;
    private float coinIncreasedTime;
    private float displayCoin = 0f;
    public LayerMask extraSkillTarget;
    public Vector2 extraSkillRangeMin;
    public Vector2 extraSkillRangeMax;
    private void Start()
    {
        StartCoroutine(CoinTimer());
    }
    private void Update()
    {
        UIManager.Instance.SpawnEnDisAble();
        if (Time.time > coinIncreasedTime)
        {
            coins += getCoinVal;

            if (coins > coinsLimit)
            {
                coins = coinsLimit;
            }
            extraSkillColldown--;
        }
    }
    private IEnumerator CoinTimer()
    {
        while (true)
        {
            coinIncreasedTime = Time.time + coinIncreaseInterval;
            float endTime = Time.time + coinDisplayDuration;
            while (Time.time < endTime)
            {
                displayCoin = Mathf.Lerp(coins, displayCoin, (endTime - Time.time) / coinDisplayDuration);
                UIManager.Instance.coinText.text = displayCoin.ToString("n0") + " / " + coinsLimit.ToString();
                yield return null;
            }
            yield return new WaitUntil(IsTime);
        }
    }
    private bool IsTime()
    {
        return Time.time > coinIncreasedTime;
    }
    public void LevelUp()
    {
        coins -= levelUpCost;
        levelUpCost += nextLevelPerCost;
        level++;
        coinsLimit += levelPerCoinLimit;
        getCoinVal += levelPerCoin;
        UIManager.Instance.SpawnEnDisAble();
        UIManager.Instance.levelUpText.text = "Lv. " + level.ToString() + "\n" + "Level UP!\n" + levelUpCost.ToString() + " ¿ø";
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector2(-25, -10), new Vector2(50, 20));
    }
    public void ExtraSkill()
    {
        extraSkillColldown = 60;
        UIManager.Instance.SpawnEnDisAble();
        Collider2D[] colls = Physics2D.OverlapBoxAll(new Vector2(-25, -10), new Vector2(50, 20), 0, extraSkillTarget);
        foreach (var coll in colls)
        {
            if (coll.CompareTag("Base")) continue;
            coll.GetComponent<Unit>().TakeDamage(100);
        }
    }
}
