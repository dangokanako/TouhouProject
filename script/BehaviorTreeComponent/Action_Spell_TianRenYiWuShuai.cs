using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.UIElements;
public class Action_Spell_TianRenYiWuShuai : Action
{
    public PlayerDamagerClass bulletPrefab;
    public override TaskStatus OnUpdate()
    {
        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("youmu_small_1_0", "天上剑「天人之五衰」！"));
        StartCoroutine(ShootBullet());
        return TaskStatus.Success;
    }

    private IEnumerator ShootBullet()
    {
        var target = PlayerHealthControl.instance.transform.position;
        target = new Vector2(target.x + Random.Range(-1f, 1f), target.y + Random.Range(-1f, 1f)); ;

        var direction = target - this.transform.position;
        // direction2为direction旋转120度
        // 创建一个表示 120 度旋转的四元数
        Quaternion rotation2 = Quaternion.Euler(0, 0, 120);
        Quaternion rotation3 = Quaternion.Euler(0, 0, -120);
        // 使用四元数旋转 direction 向量
        var direction2 = rotation2 * direction;
        var direction3 = rotation3 * direction;


        List<PlayerDamagerClass> list = new List<PlayerDamagerClass>();
        for (int i = 0; i < 50; i++)
        {
            var bulletPosition = this.transform.position + (0.7f + 0.15f * i) * direction.normalized;
            var bulletPosition2 = this.transform.position + (0.7f + 0.15f * i) * direction2.normalized;
            var bulletPosition3 = this.transform.position + (0.7f + 0.15f * i) * direction3.normalized;

            bulletPosition = new Vector2(bulletPosition.x + Random.Range(-0.3f, 0.3f), bulletPosition.y + Random.Range(-0.3f, 0.3f));
            bulletPosition2 = new Vector2(bulletPosition2.x + Random.Range(-0.3f, 0.3f), bulletPosition2.y + Random.Range(-0.3f, 0.3f));
            bulletPosition3 = new Vector2(bulletPosition3.x + Random.Range(-0.3f, 0.3f), bulletPosition3.y + Random.Range(-0.3f, 0.3f));

            var bullet = Object.Instantiate(bulletPrefab, bulletPosition, Quaternion.Euler(0, 0, Random.Range(0, 360))); //生成子弹
            var bullet2 = Object.Instantiate(bulletPrefab, bulletPosition2, Quaternion.Euler(0, 0, Random.Range(0, 360))); //生成子弹
            var bullet3 = Object.Instantiate(bulletPrefab, bulletPosition3, Quaternion.Euler(0, 0, Random.Range(0, 360))); //生成子弹

            bullet.lifeTime = 9f;
            bullet2.lifeTime = 9f;
            bullet3.lifeTime = 9f;
            bullet.initialVelocity = 0f;
            bullet2.initialVelocity = 0f;
            bullet3.initialVelocity = 0f;

            list.Add(bullet);
            list.Add(bullet2);
            list.Add(bullet3);

            yield return new WaitForSeconds(0.018f);
        }

        yield return new WaitForSeconds(2f);

        for (int i = 0; i < 150; i++)
        {
            list[i].moveSpeedAdd = 0.09f;
            yield return new WaitForSeconds(0.03f);
        }

        yield return new WaitForSeconds(0.15f);
    }
}
