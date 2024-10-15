using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASC_SpaceGunBullet : EnemyDamagerClass
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
            Destroy(gameObject);

            if (destroyParent)
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
