using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASC_Youmu_Blade1 : EnemyDamagerClass
{
    // 重写了update，去掉了大小改变
    override protected void Start()
    {
    }
    override protected void Update()
    {

        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
        {
            if (transform.localScale.x == 0f)
            {
                Destroy(gameObject);

                if (destroyParent)
                {
                    Destroy(transform.parent.gameObject);
                }
            }
        }
    }

    private HashSet<EnemyControl> damagedEnemies = new HashSet<EnemyControl>();

    override public void OnTriggerStay2D(Collider2D collision)
    {
        if (lifeTime < 0.4f)
        {
            if (collision.tag == "Enemy")
            {
                var enemy = collision.GetComponent<EnemyControl>();
                if (!damagedEnemies.Contains(enemy))
                {
                    // 摇晃屏幕
                    CameraControl.instance.DoShake(0.3f, 0.2f);

                    // 播放音效
                    SFXManger.instance.PlaySFX(11);

                    enemy.TakeDamage(17f, true, damageType, 48);
                    damagedEnemies.Add(enemy);
                }
            }
        }
    }
}
