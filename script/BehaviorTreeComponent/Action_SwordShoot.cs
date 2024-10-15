using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Unity.Mathematics;
using UnityEngine;
using Object = UnityEngine.Object;

public class Action_SwordShoot : BehaviorDesigner.Runtime.Tasks.Action
{
    public PlayerDamagerClass bulletPrefab;
    public override TaskStatus OnUpdate()
    {
        if (UnityEngine.Random.value < 0.2f)
        {
            BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("youmu_small_1_0", "『巫女！哪里逃！』"));
            BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『谁要逃跑啦！』"));
        }
        StartCoroutine(ShootBullet());
        return TaskStatus.Success;
    }

    private IEnumerator ShootBullet()
    {
        Vector2 direction = MainPlayer.instance.transform.position - this.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // 计算旋转角度

        for (int j = 0; j < 10; j++)
        {
            // 计算子弹的旋转角度
            Quaternion bulletRotation = Quaternion.Euler(0, 0, angle + (-10 + 2 * j));
            // 实例化子弹
            var bullet = Object.Instantiate(bulletPrefab, this.transform.position, bulletRotation);
            bullet.moveSpeedAdd = 0.04f - math.abs(j - 5) * 0.004f;
        }
        yield return new WaitForSeconds(0.25f);

        for (int j = 0; j < 10; j++)
        {
            // 计算子弹的旋转角度
            Quaternion bulletRotation = Quaternion.Euler(0, 0, angle + (-10 + 2 * j));
            // 实例化子弹
            var bullet = Object.Instantiate(bulletPrefab, this.transform.position, bulletRotation);
            bullet.moveSpeedAdd = 0.04f - math.abs(j - 5) * 0.004f;
        }
        yield return new WaitForSeconds(0.25f);

        for (int j = 0; j < 10; j++)
        {
            // 计算子弹的旋转角度
            Quaternion bulletRotation = Quaternion.Euler(0, 0, angle + (-10 + 2 * j));
            // 实例化子弹
            var bullet = Object.Instantiate(bulletPrefab, this.transform.position, bulletRotation);
            bullet.moveSpeedAdd = 0.04f - math.abs(j - 5) * 0.004f;
        }
        yield return new WaitForSeconds(0.25f);

    }
}
