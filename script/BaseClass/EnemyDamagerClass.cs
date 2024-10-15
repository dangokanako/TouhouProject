using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamagerClass : BulletBaseClass
{

    [Header("撞击后销毁")]
    [SerializeField] public bool destroyOnImpact;
    [Header("伤害类型")]
    /// 1是钝器伤害 2是锐器伤害 3是SC伤害
    [SerializeField] public int damageType;

    [Header("是否能够消弹")]
    public bool canDestroyBullet = true;
    [Header("破坏时播放的动画")]
    public int DesotryAnime;
    [Header("碰撞时播放的音效")]
    public int DesotrySFX;
    [Header("碰撞时是否减少伤害")]
    public bool isReduceDamage = false;

    [Header("碰撞后是否延迟消失")]
    public bool isDelayDestroy = false;


    override protected void Start()
    {
        base.Start();
    }

    override protected void Update()
    {
        base.Update();
    }
    virtual public void WhenTakeDamage(EnemyControl enemy)
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            EnemyControl enemy = collision.GetComponent<EnemyControl>();
            WhenTakeDamage(enemy);
            if (DesotrySFX != 0)
                SFXManger.instance.PlaySFX(DesotrySFX);
            enemy.TakeDamage(damageAmount, true, damageType);
            if (isReduceDamage)
            {
                damageAmount -= 1;
                if (damageAmount <= 0)
                {
                    Destroy(gameObject);
                }
            }

            if (destroyOnImpact)
            {
                DestoryAnimeControl.instance.CreateDestoryAnime(DesotryAnime, this.transform.position);
                if (!isDelayDestroy)
                    Destroy(gameObject);
                else
                {
                    damageAmount = 0;
                    lifeTime = -1;
                }
            }
        }


        if (canDestroyBullet)
        {
            if (collision.tag == "EnemyBullet")
            {
                // 碰撞到敌人子弹时，根据双方子弹攻击力，判断是否销毁子弹
                var EnemyBullet = collision.GetComponent<PlayerDamagerClass>();
                DestoryAnimeControl.instance.CreateDestoryAnime(DesotryAnime, this.transform.position);
                if (EnemyBullet != null && EnemyBullet.damageAmount > 0 && this.damageAmount > 0)
                {
                    var tempatk = EnemyBullet.damageAmount;
                    EnemyBullet.damageAmount -= this.damageAmount;
                    this.damageAmount -= tempatk;

                    if (EnemyBullet.damageAmount <= 0)
                    {
                        EnemyBullet.damageAmount = 0;
                        Destroy(collision.gameObject);
                    }

                    if (this.damageAmount <= 0)
                    {
                        this.damageAmount = 0;
                        Destroy(gameObject);
                    }

                }

            }
        }
    }

    virtual public void OnCollision(Collider collision)
    {
    }

    virtual protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {

            if (DesotrySFX != 0)
                SFXManger.instance.PlaySFX(DesotrySFX);


            EnemyControl enemy = collision.gameObject.GetComponent<EnemyControl>();
            WhenTakeDamage(enemy);
            enemy.TakeDamage(damageAmount, true, damageType);


            // 碰撞伤害系统
            var Rigidbody = this.GetComponent<Rigidbody2D>();
            if (Rigidbody != null)
            {

                // (攻击的质量/4*相对速度)/敌人质量 先测试
                float relativeVelocity = collision.relativeVelocity.magnitude;
                float damage = (Rigidbody.mass / 4 * relativeVelocity) / enemy.GetComponent<Rigidbody2D>().mass;
                if (damage > 1)
                {
                    Debug.Log("相对速度:" + relativeVelocity + "攻击质量:" + Rigidbody.mass + "敌人质量:" + enemy.GetComponent<Rigidbody2D>().mass);
                    Debug.Log("出伤:" + damage);

                    // 撞击伤害上限,只对BOSS有效，橙撞死就撞死吧
                    if (damage > enemy.GetMaxHealth() * 0.05f)
                        if (enemy.GetMaxHealth() > 100)
                            damage = enemy.GetMaxHealth() * 0.05f;

                    enemy.TakeDamage(damage, true, 1);
                }

            }




            if (destroyOnImpact)
            {
                DestoryAnimeControl.instance.CreateDestoryAnime(DesotryAnime, this.transform.position);
                if (!isDelayDestroy)
                    Destroy(gameObject);
                else
                {
                    damageAmount = 0;
                    lifeTime = -1;
                }
            }
        }


        if (canDestroyBullet)
        {
            if (collision.gameObject.tag == "EnemyBullet")
            {
                // 碰撞到敌人子弹时，根据双方子弹攻击力，判断是否销毁子弹
                var EnemyBullet = collision.gameObject.GetComponent<PlayerDamagerClass>();
                DestoryAnimeControl.instance.CreateDestoryAnime(DesotryAnime, this.transform.position);
                if (EnemyBullet != null)
                {
                    var tempatk = EnemyBullet.damageAmount;
                    EnemyBullet.damageAmount -= this.damageAmount;
                    this.damageAmount -= tempatk;

                    if (EnemyBullet.damageAmount <= 0)
                        Destroy(collision.gameObject);

                    if (this.damageAmount <= 0)
                        Destroy(gameObject);
                }

            }
        }
    }

    virtual public void OnTriggerStay2D(Collider2D collision)
    {
    }

}
