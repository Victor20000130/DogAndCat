using Assets.FantasyMonsters.Common.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Unit : MonoBehaviour
{
    private PoolManager poolManager;
    private Monster monster;
    public float moveSpeed;
    public float attackPerSec;
    public float damage;
    public float maxHp;
    public float hp;
    public bool singleAttackType;
    private bool isBattle;
    public int unitCost;
    public LayerMask targetLayer;
    public Vector2 offset;
    public Vector2 offsetSize;
    public BoxCollider2D colloffset;
    public Slider hpBar;
    private BoxCollider2D hurtBox;
    public TMP_Text tMP_Text;
    public GameObject dieEffect;
    public float hpAmount { get { return hp / maxHp; } }
    private void Awake()
    {
        hurtBox = GetComponent<BoxCollider2D>();
        poolManager = FindAnyObjectByType<PoolManager>();
    }
    private void OnEnable()
    {
        hp = maxHp;
        isBattle = false;
        hurtBox.enabled = true;
        if (gameObject.CompareTag("Unit"))
        {
            monster = GetComponent<Monster>();
            monster.SetState(MonsterState.Run);
            dieEffect.gameObject.SetActive(false);
            dieEffect.transform.position = transform.position;
        }
    }
    private float preDamageTime = 0;
    private void Update()
    {
        Move();
        hpBar.value = hpAmount;
        if (hurtBox.enabled == true)
        {
            if (preDamageTime + attackPerSec < Time.time)
            {
                Battle();
                preDamageTime = Time.time;
            }
        }
    }
    private void Battle()
    {
        Collider2D[] colls = Physics2D.OverlapBoxAll((Vector2)transform.position + offset, offsetSize, 0, targetLayer);
        if (colls.Length == 0)
        {
            isBattle = false;
            return;
        }
        else { isBattle = true; }
        if (singleAttackType)
        {
            Unit target = null;
            foreach (var coll in colls)
            {
                if (target == null)
                {
                    target = coll.GetComponent<Unit>();
                }
                else
                {
                    var currentEnemy = coll.GetComponent<Unit>();
                    if (target.hp > currentEnemy.hp)
                    {
                        target = currentEnemy;
                    }
                }
            }
            target.TakeDamage(damage);
        }
        else
        {
            foreach (var coll in colls)
            {
                coll.GetComponent<Unit>().TakeDamage(damage);
            }
        }
        Attack();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube((Vector2)transform.position + offset, offsetSize);
    }
    private void Move()
    {
        if (isBattle == true) return;
        transform.Translate(new Vector2(-0.15f, 0) * moveSpeed * Time.deltaTime);
    }
    public void Attack()
    {
        monster.Attack();
    }
    private void Die()
    {
        if (hp <= 0)
        {
            hurtBox.enabled = false;
            isBattle = true;
            if (gameObject.CompareTag("Unit"))
            {
                monster.SetState(MonsterState.Death);
                dieEffect.SetActive(true);

                StartCoroutine(Despawn(this, 2f));
            }
            if (gameObject.CompareTag("Base"))
            {
                StartCoroutine(EndGame());
            }
        }
    }
    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (transform.CompareTag("Base"))
        {
            tMP_Text.text = hp.ToString() + " / " + maxHp.ToString();
        }
        Die();
    }
    public IEnumerator Despawn(Unit unit, float t)
    {
        float dieTime = Time.time + t;
        while (Time.time <= dieTime)
        {
            dieEffect.transform.Translate(new Vector2(0, 1f) * 10 * Time.deltaTime);
            yield return null;
        }
        poolManager.Push(unit);
    }
    public IEnumerator EndGame()
    {
        while (true)
        {
            if (gameObject.layer == 3)
            {
                GameManager.Instance.uiManager.battleResult[0].SetActive(true);
            }
            if (gameObject.layer == 6)
            {
                GameManager.Instance.uiManager.battleResult[1].SetActive(true);
            }
            yield return new WaitForSeconds(3f);
            GameManager.Instance.uiManager.OnRestart();
            GameManager.Instance.GameOver();

        }
    }
}