using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_FengmozhenDamage : EnemyDamagerClass
{
    // 以免接触式攻击疯狂弹数字，需要一个攻击间隔
    public float damageConuter;
    public float timeBetweenDamage;
    public List<EnemyControl> enemiesInRange = new List<EnemyControl>();
    // 减攻效果
    public float DEFreduce;
    // SP消耗
    public float SPConsume;

    public float SCRange;

    override protected void Start()
    {
    }

    // 有敌人进入碰撞范围则加入List，之后每单位时间进行伤害结算

    override protected void Update()
    {
        if (GlobalControl.instance.isBattle)
        {
            damageConuter -= Time.deltaTime;
            if (damageConuter <= 0)
            {
                damageConuter = timeBetweenDamage;
                if (PlayerHealthControl.instance.UseSP(SPConsume, false, false))
                {
                    this.transform.localScale = new Vector3(SCRange, SCRange, 1);

                    for (int i = 0; i < enemiesInRange.Count; i++)
                    {
                        if (enemiesInRange[i] != null)
                        {
                            enemiesInRange[i].ReduceAtk(DEFreduce);
                            enemiesInRange[i].TakeDamage(damageAmount, damageType);
                            SFXManger.instance.PlaySFX(19);
                            transform.Rotate(0, 0, 0.4f);
                        }
                        else
                        {
                            Debug.Log("未知敌人");
                            enemiesInRange.RemoveAt(i);
                            i--;
                        }
                    }
                }
                else
                {
                    // 图片消失
                    this.transform.localScale = new Vector3(0, 0, 1);
                }
            }
            // 自旋转
            transform.Rotate(0, 0, 0.1f * (2 - timeBetweenDamage));
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
