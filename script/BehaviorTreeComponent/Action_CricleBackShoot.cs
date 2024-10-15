using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Unity.Mathematics;
using UnityEngine;
using Object = UnityEngine.Object;

public class Action_CricleBackShoot : BehaviorDesigner.Runtime.Tasks.Action
{
    public PlayerDamagerClass bulletPrefab;
    public override TaskStatus OnUpdate()
    {
        StartCoroutine(ShootBullet());
        return TaskStatus.Success;
    }

    private IEnumerator ShootBullet()
    {

        for (int j = 0; j < 36; j++)
        {
            var bullet = Object.Instantiate(bulletPrefab, this.transform.position, Quaternion.AngleAxis(10 * j, Vector3.forward)); //生成子弹
            bullet.moveSpeedAdd = -0.05f;
            bullet.initialVelocity = 7f;
            bullet.lifeTime = 12f;
            yield return new WaitForSeconds(0.15f);
        }
        yield return new WaitForSeconds(0.15f);
    }
}
