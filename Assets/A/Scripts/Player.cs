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
    public float coinIncreaseInterval = 1f;
    public float coinDisplayDuration = 0.3f;
    private float coinIncreasedTime;
    private float displayCoin = 0f;
    public LayerMask extraSkillTarget;
    public Vector2 extraSkillRangeMin;
    public Vector2 extraSkillRangeMax;
    private float glitterDuration = 0.5f;

    private void Start()
    {
        StartCoroutine(CoinTimer());
        StartCoroutine(Glitter());
    }
    private void Update()
    {
        GameManager.Instance.uiManager.SpawnEnDisAble();

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
                GameManager.Instance.uiManager.coinText.text = displayCoin.ToString("n0") + " / " + coinsLimit.ToString();
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
        GameManager.Instance.uiManager.SpawnEnDisAble();
        GameManager.Instance.uiManager.levelupButtonText.text = "Lv. " + level.ToString() + "\n" + "Level UP!\n" + levelUpCost.ToString() + " ¿ø";
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector2(-25, -10), new Vector2(50, 20));
    }
    public void ExtraSkill()
    {
        extraSkillColldown = 60;
        GameManager.Instance.uiManager.SpawnEnDisAble();
        Collider2D[] colls = Physics2D.OverlapBoxAll(new Vector2(-25, -10), new Vector2(50, 20), 0, extraSkillTarget);
        foreach (var coll in colls)
        {
            if (coll.CompareTag("Base")) continue;
            coll.GetComponent<Unit>().TakeDamage(100);
        }
    }
    private IEnumerator Glitter()
    {
        while (true)
        {
            float glitterTime = Time.time + glitterDuration;
            while (Time.time < glitterTime)
            {
                if (coins >= levelUpCost && level != maxLv)
                {
                    GameManager.Instance.uiManager.levelUpButton.image.color = new Color(1, 1, 1, Mathf.PingPong(Time.time * 10, 1));
                }
                if (extraSkillColldown <= 0)
                {
                    GameManager.Instance.uiManager.extraSkillButton.image.color = new Color(1, 1, 1, Mathf.PingPong(Time.time * 10, 1));
                }
                yield return null;
            }
            GameManager.Instance.uiManager.levelUpButton.image.color = new Color(1, 1, 1, 1);
            GameManager.Instance.uiManager.extraSkillButton.image.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
