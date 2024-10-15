using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamagerClass : BulletBaseClass
{
    [Header("撞击后销毁")]
    [SerializeField] public bool destroyOnImpact;


    override protected void Start()
    {
        base.Start();
    }

    override protected void Update()
    {
        base.Update();

    }

    void FixedUpdate()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerHealthControl.instance.TakeDamage(damageAmount, 2);

            if (destroyOnImpact)
            {
                DestoryAnimeControl.instance.CreateDestoryAnime(1, this.transform.position);
                // EnemyBulletControl.instance.ReturnBullet(this);
                Destroy(gameObject);
            }
        }
    }

    virtual protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerHealthControl.instance.TakeDamage(damageAmount, 2);

            if (destroyOnImpact)
            {
                DestoryAnimeControl.instance.CreateDestoryAnime(1, this.transform.position);
                // EnemyBulletControl.instance.ReturnBullet(this);
                Destroy(gameObject);
            }
        }
    }

    public void CopyFrom(PlayerDamagerClass other)
    {
        // 复制父类的属性
        base.CopyFrom(other);
        // 复制自己的属性
        destroyOnImpact = other.destroyOnImpact;
    }
}
