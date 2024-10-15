using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boss_Mystia : Enemy_Boss
{
    public PlayerDamagerClass bulletPrefab;
    protected override void Start()
    {
        base.Start();
        StartCoroutine(ShootBullet());
    }



    // 返回位置
    public Vector3 getPosition()
    {
        return this.transform.position;
    }
    protected override void FixedUpdate()
    {
        // 如果当前HP小于最大HP的一半，那么伤害抗性提升到80，若干秒种后恢复
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
            if (firstStage)
            {
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        // 创建子弹
                        var bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
                        // bullet朝向玩家方向
                        Vector2 direction = MainPlayer.instance.transform.position - this.transform.position;
                        direction.Normalize();

                        // 计算旋转角度
                        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                        // 设置子弹的旋转
                        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + (j * 15 - 15)));
                    }
                    yield return new WaitForSeconds(0.15f);
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    // 创建子弹
                    var bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
                    // bullet朝向玩家方向
                    Vector2 direction = MainPlayer.instance.transform.position - this.transform.position;
                    direction.Normalize();

                    // 计算旋转角度
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    // 设置子弹的旋转
                    bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));


                    yield return new WaitForSeconds(0.15f);
                }
            }

            yield return new WaitForSeconds(5f);
        }
    }
}
