using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASC_Normal_Blade : EnemyDamagerClass
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
}
