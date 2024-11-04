using Assets.FantasyMonsters.Common.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Unit : MonoBehaviour
{
    private Monster monster;
    public string Name;
    public float moveSpeed;
    public float attackPerSec;
    public float attackRange;
    public float damage;
    public float maxHp;
    public float hp;
    public bool singleAttackType;
    private bool isBattle = false;

    public int unitCost;

    public LayerMask targetLayer;
    public Vector2 offset;
    public Vector2 offsetSize;

    public BoxCollider2D colloffset;

    public float hpAmount { get { return hp / maxHp; } }
    public Slider hpBar;

    private BoxCollider2D hitBox;

    public TMP_Text tMP_Text;

    private void Awake()
    {

        hitBox = GetComponent<BoxCollider2D>();
        if (gameObject.CompareTag("Unit"))
        {
            monster = GetComponent<Monster>();
            monster.SetState(MonsterState.Run);
        }
    }

    private void Start()
    {

    }


    private float preDamageTime = 0;
    private void Update()
    {


        Move();

        if (hitBox.enabled == true)
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

        //List<Unit> targets = null;

        if (colls.Length == 0)
        {
            isBattle = false;
            return;
        }
        else { isBattle = true; }
        #region 강사님 1안
        //else
        //{
        //    targets = new List<Collider2D>(colls).ConvertAll((x) => x.GetComponent<Unit>());
        //}


        //if (singleAttackType)
        //{
        //    FindTarget(colls);
        //}
        //else
        //{
        //if (targets != null)
        //{
        //    targets.Sort((a, b) =>
        //    {
        //        return (int)Mathf.Sign(b.hp - a.hp);
        //    });
        //}

        //if (singleAttackType && colls.Length != 0)
        //{
        //    targets[0].TakeDamage(damage);
        //}
        //else
        //{
        //    if (colls.Length == 0) return;
        //    foreach (var target in targets)
        //    {
        //        target.TakeDamage(damage);
        //    }
        //}
        #endregion
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

        #region 내가한거
        //foreach (var coll in colls)
        //{
        //    if (coll.TryGetComponent<Unit>(out Unit unit))
        //    {
        //        isBattle = true;
        //        if (targetList.Contains(unit) == false)
        //        { targetList.Add(unit); }
        //        Attack();
        //        if (unit.hp - damage <= 0)
        //        {
        //            targetList.Remove(unit);
        //        }
        //        unit.TakeDamage(damage, unit);
        //    }
        //}
        //print($"{name} : {targetList.Count}");
        //}
        #endregion
    }

    #region 내가한거
    //float targetHp;
    //float nextTargetHp;
    //private void FindTarget(Collider2D[] colls)
    //{
    //    foreach (var coll in colls)
    //    {
    //        if (coll.TryGetComponent<Unit>(out Unit unit))
    //        {

    //            if (unit.hp > targetHp)
    //            {
    //                targetHp = unit.hp;
    //            }
    //        }
    //    }
    //}
    #endregion
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
            hitBox.enabled = false;
            if (gameObject.CompareTag("Unit"))
                monster.SetState(MonsterState.Death);
            gameObject.SetActive(false);
            if (gameObject.CompareTag("Base"))
            {
                if (gameObject.layer == 3)
                {
                    //player win
                    UIManager.Instance.battleResult[0].SetActive(true);
                }
                if (gameObject.layer == 6)
                {
                    //player lose
                    UIManager.Instance.battleResult[1].SetActive(true);
                }
                Time.timeScale = 0;
                new WaitForSeconds(5f);
                GameManager.Instance.GameOver();
            }

        }

    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        hpBar.value = hpAmount;
        if (transform.CompareTag("Base"))
        {
            tMP_Text.text = hp.ToString() + " / " + maxHp.ToString();
        }
        Die();
    }

}
