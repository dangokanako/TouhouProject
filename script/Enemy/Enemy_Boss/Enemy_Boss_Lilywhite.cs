using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boss_Lilywhite : Enemy_Boss
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
            if (firstStage)
            {
                for (int i = 0; i < 3; i++)
                {
                    // 产生1-10的随机数
                    int random = Random.Range(1, 11);

                    for (int j = 0; j < 36; j++)
                    {
                        var bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.AngleAxis(10 * j + random, Vector3.forward)); //生成子弹
                    }
                    yield return new WaitForSeconds(0.3f);
                }

                yield return new WaitForSeconds(3f);
            }
            else
            {
                for (int i = 0; i < (firstStage ? 6 : 3); i++)
                {

                    // 创建子弹
                    var bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);

                    // 计算子弹的方向
                    Vector2 direction = MainPlayer.instance.transform.position - this.transform.position;
                    direction.Normalize();
                    direction = new Vector2(direction.x + UnityEngine.Random.Range(-0.3f, 0.3f), direction.y + UnityEngine.Random.Range(-0.3f, 0.3f));

                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // 计算旋转角度
                    bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle + 90);


                    direction.Normalize();
                    // 施加一个力使子弹朝向玩家的方向移动
                    bullet.rb.velocity = direction * 2f * UnityEngine.Random.Range(0.9f, 1.1f);
                }

                yield return new WaitForSeconds(3f);

                for (int i = 0; i < (firstStage ? 10 : 5); i++)
                {
                    // 创建子弹
                    var bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);

                    // 计算子弹的方向
                    Vector2 direction = MainPlayer.instance.transform.position - this.transform.position;
                    direction.Normalize();
                    direction = new Vector2(direction.x + UnityEngine.Random.Range(-0.3f, 0.3f), direction.y + UnityEngine.Random.Range(-0.3f, 0.3f));

                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // 计算旋转角度
                    bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle + 90);


                    direction.Normalize();
                    // 施加一个力使子弹朝向玩家的方向移动
                    bullet.rb.velocity = direction * 2f * UnityEngine.Random.Range(0.9f, 1.1f);
                }

                yield return new WaitForSeconds(3f);

                for (int i = 0; i < (firstStage ? 14 : 7); i++)
                {

                    // 创建子弹
                    var bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);

                    // 计算子弹的方向
                    Vector2 direction = MainPlayer.instance.transform.position - this.transform.position;
                    direction.Normalize();
                    direction = new Vector2(direction.x + UnityEngine.Random.Range(-0.3f, 0.3f), direction.y + UnityEngine.Random.Range(-0.3f, 0.3f));

                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // 计算旋转角度
                    bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle + 90);


                    direction.Normalize();
                    // 施加一个力使子弹朝向玩家的方向移动
                    bullet.rb.velocity = direction * 2f * UnityEngine.Random.Range(0.9f, 1.1f);
                }

                yield return new WaitForSeconds(3f);
            }

        }
    }
}
