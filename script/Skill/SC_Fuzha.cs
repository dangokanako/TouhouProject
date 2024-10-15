using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class SC_Fuzha : EnemyDamagerClass
{
    // 减攻效果
    public float SPEDreduce;

    override public void WhenTakeDamage(EnemyControl enemy)
    {
        enemy.ReduceSPD(SPEDreduce);
    }

    override protected void Update()
    {
        // 自旋转部分。如果是自旋转，那么调整自身旋转角度
        if (shouldSpin)
        {
            transform.Rotate(Vector3.forward * 360 * Time.deltaTime);
        }


        // 放大缩小的动画部分
        if (!closeGrowAnime)
            transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, growSpeed * Time.deltaTime);


        // 生命时间到了自动销毁的部分
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            if (!closeGrowAnime)
            {
                // 开始改变为0
                targetSize = Vector3.zero;

                if (transform.localScale.x == 0f)
                {
                    Destroy(gameObject);
                    DestoryAnimeControl.instance.CreateDestoryAnime(6, this.transform.position);
                }
            }
            else
            {
                // 重新打开动画，播放消失动画。
                closeGrowAnime = false;
            }
        }

    }
}
