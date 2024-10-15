using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Unity.Mathematics;
public class Action_Youmu_Normal1 : Action
{
    public PlayerDamagerClass bulletPrefab;
    public EnemyControl enemyControl;
    public override void OnStart()
    {
        enemyControl = GetComponent<EnemyControl>();
    }
    public override TaskStatus OnUpdate()
    {
        StartCoroutine(ShootBullet());
        return TaskStatus.Success;

    }

    private IEnumerator ShootBullet()
    {
        enemyControl.SetSpeed(0.3f);

        // 计算子弹的方向
        Vector2 direction = MainPlayer.instance.transform.position - this.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // 计算旋转角度
        direction.Normalize();


        for (int j = 0; j < 36; j++)
        {
            var bullet = Object.Instantiate(bulletPrefab, this.transform.position, Quaternion.AngleAxis(10 * j, Vector3.forward)); //生成子弹
            // var bullet = EnemyBulletControl.instance.GetBulletAndInitialize(bulletPrefab, this.transform.position, Quaternion.AngleAxis(10 * j, Vector3.forward));
        }


        for (int i = 0; i < 7; i++)
        {
            var randomAngle = UnityEngine.Random.Range(0, 360);
            for (int j = 0; j < 10; j++)
            {
                // 实例化子弹
                var bullet = Object.Instantiate(bulletPrefab, this.transform.position, Quaternion.Euler(0, 0, randomAngle + (-10 + 2 * j)));
                // var bullet = EnemyBulletControl.instance.GetBulletAndInitialize(bulletPrefab, this.transform.position, Quaternion.Euler(0, 0, randomAngle + (-10 + 2 * j)));
                bullet.moveSpeedAdd = 0.04f - math.abs(j - 5) * 0.005f;
            }
            yield return new WaitForSeconds(0.15f);
        }

        for (int j = 0; j < 10; j++)
        {
            // 计算子弹的旋转角度
            Quaternion bulletRotation = Quaternion.Euler(0, 0, angle + (-10 + 2 * j));
            // 实例化子弹
            var bullet = Object.Instantiate(bulletPrefab, this.transform.position, bulletRotation); //生成子弹
            // var bullet = EnemyBulletControl.instance.GetBulletAndInitialize(bulletPrefab, this.transform.position, bulletRotation);

            bullet.moveSpeedAdd = 0.04f - math.abs(j - 5) * 0.005f;
        }

        for (int j = 0; j < 36; j++)
        {
            var bullet = Object.Instantiate(bulletPrefab, this.transform.position, Quaternion.AngleAxis(10 * j, Vector3.forward)); //生成子弹
            // var bullet = EnemyBulletControl.instance.GetBulletAndInitialize(bulletPrefab, this.transform.position, Quaternion.AngleAxis(10 * j, Vector3.forward));

        }


        for (int i = 0; i < 7; i++)
        {
            var randomAngle = UnityEngine.Random.Range(0, 360);
            for (int j = 0; j < 10; j++)
            {
                // 实例化子弹
                var bullet = Object.Instantiate(bulletPrefab, this.transform.position, Quaternion.Euler(0, 0, randomAngle + (-10 + 2 * j)));
                // var bullet = EnemyBulletControl.instance.GetBulletAndInitialize(bulletPrefab, this.transform.position, Quaternion.Euler(0, 0, randomAngle + (-10 + 2 * j)));
                bullet.moveSpeedAdd = 0.04f - math.abs(j - 5) * 0.005f;

            }
            yield return new WaitForSeconds(0.15f);
        }

        for (int j = 0; j < 36; j++)
        {
            var bullet = Object.Instantiate(bulletPrefab, this.transform.position, Quaternion.AngleAxis(10 * j, Vector3.forward)); //生成子弹

            // var bullet = EnemyBulletControl.instance.GetBulletAndInitialize(bulletPrefab, this.transform.position, Quaternion.AngleAxis(10 * j, Vector3.forward));
        }

        enemyControl.SetSpeed(1f);
        yield return new WaitForSeconds(5f);
    }

}
