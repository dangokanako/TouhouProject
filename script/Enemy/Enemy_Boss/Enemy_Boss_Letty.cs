using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemy_Boss_Letty : Enemy_Boss
{
    public PlayerDamagerClass bulletPrefab;
    protected override void Start()
    {
        base.Start();
        StartCoroutine(ShootBullet());
    }

    override protected void enemyDead()
    {
        base.enemyDead();
        GlobalControl.instance.isGetRedi = true;
        SFXManger.instance.PlayBgmBattleGroup();

    }

    // 返回位置
    public Vector3 getPosition()
    {
        return this.transform.position;
    }
    protected override void FixedUpdate()
    {
        // 如果当前HP小于最大HP的一半，那么伤害抗性提升到90，5秒种后恢复
        if (health < maxHealth / 2 && !firstStage)
        {
            firstStage = true;
            bluntResistance = 80;
            sharpResistance = 80;
            SCResistance = 80;

            // 血条变成蓝色
            UIControl.instance.HealthText_Boss.color = Color.blue;

            // 显示“增强模式！”
            DamageNumberControl.instance.ShowText("增强模式！", this.getPosition(), Color.red, 1.5f);

            // 降低移动速度
            this.moveSpeed *= 0.5f;

            StartCoroutine(RecoverResistance());
        }

        // 如果当前HP小于最大HP的四分之一，那么进入第二次增强模式
        if (health < maxHealth / 4 && !secondStage)
        {
            secondStage = true;
            bluntResistance = 80;
            sharpResistance = 80;
            SCResistance = 80;

            // 血条变成绿色
            UIControl.instance.HealthText_Boss.color = Color.green;

            DamageNumberControl.instance.ShowText("增强模式！", this.getPosition(), Color.red, 1.5f);

            // 降低移动速度
            this.moveSpeed *= 0.5f;

            StartCoroutine(RecoverResistance());
        }

        base.FixedUpdate();
    }

    private IEnumerator RecoverResistance()
    {
        yield return new WaitForSeconds(3f);
        bluntResistance = 0;
        sharpResistance = 0;
        SCResistance = 0;

        // 显示“退出增强模式”
        DamageNumberControl.instance.ShowText("退出增强模式", this.getPosition(), Color.red, 1.5f);

        // 血条变回颜色 #9C0700
        UIControl.instance.HealthText_Boss.color = new Color(0.61f, 0.03f, 0f);

        // 恢复移动速度
        this.moveSpeed *= 2f;

    }

    private IEnumerator ShootBullet()
    {

        while (true)
        {
            // if (firstStage)
            // {


            //     yield return new WaitForSeconds(3f);
            // }
            // else
            // {

            moveSpeedReal = 0.4f * moveSpeed;
            // 计算子弹的方向
            Vector2 direction = MainPlayer.instance.transform.position - this.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // 计算旋转角度
            direction.Normalize();


            for (int j = 0; j < 36; j++)
            {
                var bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.AngleAxis(10 * j, Vector3.forward)); //生成子弹
            }


            for (int i = 0; i < 7; i++)
            {
                var randomAngle = UnityEngine.Random.Range(0, 360);
                for (int j = 0; j < 10; j++)
                {
                    // 实例化子弹
                    var bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.Euler(0, 0, randomAngle + (-10 + 2 * j)));
                    bullet.moveSpeedAdd = 0.04f - math.abs(j - 5) * 0.005f;
                }
                yield return new WaitForSeconds(0.15f);
            }

            for (int j = 0; j < 10; j++)
            {
                // 计算子弹的旋转角度
                Quaternion bulletRotation = Quaternion.Euler(0, 0, angle + (-10 + 2 * j));
                // 实例化子弹
                var bullet = Instantiate(bulletPrefab, this.transform.position, bulletRotation);
                bullet.moveSpeedAdd = 0.04f - math.abs(j - 5) * 0.005f;
            }

            for (int j = 0; j < 36; j++)
            {
                var bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.AngleAxis(10 * j, Vector3.forward)); //生成子弹
            }


            for (int i = 0; i < 7; i++)
            {
                var randomAngle = UnityEngine.Random.Range(0, 360);
                for (int j = 0; j < 10; j++)
                {
                    // 实例化子弹
                    var bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.Euler(0, 0, randomAngle + (-10 + 2 * j)));
                    bullet.moveSpeedAdd = 0.04f - math.abs(j - 5) * 0.005f;
                }
                yield return new WaitForSeconds(0.15f);
            }

            for (int j = 0; j < 36; j++)
            {
                var bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.AngleAxis(10 * j, Vector3.forward)); //生成子弹
            }


            moveSpeedReal = moveSpeed;
            yield return new WaitForSeconds(5f);
            //     yield return new WaitForSeconds(3f);
            // }

        }
    }
}
