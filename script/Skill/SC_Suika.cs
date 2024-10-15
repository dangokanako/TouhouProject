using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Suika : EnemyDamagerClass
{
    public float throwPower;
    public  float rotateSpeed_SC_Suika;
    protected override void Start()
    {
        rb.velocity = new Vector2(Random.Range(-throwPower, throwPower), Random.Range(-throwPower, throwPower));
        base.Start();
    }
    protected override void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + (rotateSpeed_SC_Suika * 360f * Time.deltaTime * Mathf.Sign(rb.velocity.x)));
        base.Update();
    }

    override protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyControl>().TakeDamage(damageAmount, true, damageType);

            // 碰撞后伤害减少。
            this.damageAmount -= 1;
            if (this.damageAmount <= 0)
            {
                DestoryAnimeControl.instance.CreateDestoryAnime(DesotryAnime, this.transform.position);
                Destroy(gameObject);
            }


            if (destroyOnImpact)
            {
                DestoryAnimeControl.instance.CreateDestoryAnime(DesotryAnime, this.transform.position);
                Destroy(gameObject);
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


}
