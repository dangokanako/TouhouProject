using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Youmu2 : EnemyDamagerClass
{
    // 以免接触式攻击疯狂弹数字，需要一个攻击间隔
    public float damageConuter;
    public float timeBetweenDamage;
    private List<EnemyControl> enemiesInRange = new List<EnemyControl>();
    public float SPEDreduce;


    // 有敌人进入碰撞范围则加入List，之后每单位时间进行伤害结算

    override protected void Update()
    {
        if (GlobalControl.instance.isBattle)
        {
            damageConuter -= Time.deltaTime;
            if (damageConuter <= 0)
            {
                damageConuter = timeBetweenDamage;

                for (int i = 0; i < enemiesInRange.Count; i++)
                {
                    if (enemiesInRange[i] != null)
                    {
                        enemiesInRange[i].ReduceSPD(SPEDreduce);
                        enemiesInRange[i].TakeDamage(damageAmount, damageType);
                    }
                    else
                    {
                        Debug.Log("未知敌人");
                        enemiesInRange.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // AOE范围持续伤害
        if (collider.tag == "Enemy")
        {
            enemiesInRange.Add(collider.GetComponent<EnemyControl>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            enemiesInRange.Remove(collision.GetComponent<EnemyControl>());
        }
    }
}
