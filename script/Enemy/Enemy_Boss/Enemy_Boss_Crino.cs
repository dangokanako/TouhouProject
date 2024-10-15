using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemy_Boss_Crino : Enemy_Boss
{
    public PlayerDamagerClass bulletPrefab;
    public PlayerDamagerClass bulletPrefab_2;
    protected override void Start()
    {
        base.Start();
        StartCoroutine(ShootBullet());
    }


    override protected void enemyDead()
    {
        base.enemyDead();
        GlobalControl.instance.isGetCrino = true;
        SFXManger.instance.PlayBgmBattleGroup();

    }
    // 返回位置
    public Vector3 getPosition()
    {
        return this.transform.position;
    }
    protected override void FixedUpdate()
    {
        // 如果当前HP小于最大HP的一半，那么伤害抗性提升到90，若干秒种后恢复
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
            moveSpeedReal = 0.5f * moveSpeed;


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
        moveSpeedReal = moveSpeed;

    }

    private IEnumerator ShootBullet()
    {
        while (true)
        {
            if (!firstStage)
            {
                moveSpeedReal = 0.4f * moveSpeed;
                for (int i = 0; i < 14; i++)
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
                moveSpeedReal = moveSpeed;
                yield return new WaitForSeconds(3f);

            }
            else
            {
                moveSpeedReal = 0.4f * moveSpeed;
                for (int i = 0, j = 0; i < 1440; i += 11, j++)
                {
                    // 创建子弹
                    var bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.Euler(0f, 0f, i));
                    bullet.moveSpeedAdd = -0.005f;
                    // bullet.moveSpeedAdd = 0.01f * (i % 5);
                    bullet.rb.AddForce(35 * bullet.transform.up);
                    yield return new WaitForSeconds(0.01f);


                    if (j % 5 == 0)
                    {
                        var bullet2 = Instantiate(bulletPrefab_2, this.transform.position, Quaternion.Euler(0f, 0, UnityEngine.Random.Range(0, 360)));
                        bullet2.moveSpeedAdd = -0.01f;
                        bullet2.rb.AddForce(50 * bullet2.transform.right);
                    }
                }
                moveSpeedReal = moveSpeed;

                yield return new WaitForSeconds(3f);
            }

        }
    }
}
