using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boss_Marisa : Enemy_Boss
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

        if (GlobalControl.instance.isGetMarisa == false)
            BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『要不要回一趟红魔馆，看看魔理沙。不过道具没办法拿回去。』"));


        GlobalControl.instance.isGetMarisa = true;

        SFXManger.instance.PlayBgmBattleGroup();
    }

    // 返回位置
    public Vector3 getPosition()
    {
        return this.transform.position;
    }
    protected override void FixedUpdate()
    {
        // 如果当前HP小于最大HP的2/3，那么伤害抗性提升到100，5秒种后恢复
        if (health < maxHealth * 2 / 3 && !firstStage)
        {
            firstStage = true;
            bluntResistance = 90;
            sharpResistance = 90;
            SCResistance = 90;

            // 血条变成蓝色
            UIControl.instance.HealthText_Boss.color = Color.blue;

            // 显示“增强模式！”
            DamageNumberControl.instance.ShowText("增强模式！", this.getPosition(), Color.red, 1.5f);

            // 降低移动速度
            moveSpeedReal = 0.5f * moveSpeed;

            StartCoroutine(RecoverResistance());
        }

        // 如果当前HP小于最大HP的1/3，那么进入第二次增强模式
        if (health < maxHealth / 3 && !secondStage)
        {
            secondStage = true;
            bluntResistance = 90;
            sharpResistance = 90;
            SCResistance = 90;

            // 血条变成绿色
            UIControl.instance.HealthText_Boss.color = Color.green;

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
            if (secondStage)
            {
                moveSpeedReal = 0.4f * moveSpeed;
                for (int i = 0; i < 2; i++)
                {
                    // 产生1-10的随机数
                    int random = Random.Range(1, 11);

                    for (int j = 0; j < 36; j++)
                    {
                        var bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.AngleAxis(10 * j, Vector3.forward)); //生成子弹
                    }
                    yield return new WaitForSeconds(0.3f);
                }
                moveSpeedReal = moveSpeed;
                yield return new WaitForSeconds(3f);
            }
            else if (firstStage)
            {
                int random = Random.Range(1, 21);
                moveSpeedReal = 0.4f * moveSpeed;
                for (int j = 0; j < 36; j++)
                {

                    var bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.AngleAxis(20 * j + random, Vector3.forward)); //生成子弹
                    bullet.initialVelocity = 4f;
                    yield return new WaitForSeconds(0.05f);
                }
                moveSpeedReal = moveSpeed;
                yield return new WaitForSeconds(4f);
            }
            else
            {

                moveSpeedReal = 0.4f * moveSpeed;
                for (int i = 0; i < 10; i++)
                {
                    // 创建子弹
                    var bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);

                    // 把子弹旋转朝向玩家
                    // float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
                    // bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));


                    bullet.initialDirection = MainPlayer.instance.transform.position - bullet.transform.position;
                    float angle = Mathf.Atan2(bullet.initialDirection.y, bullet.initialDirection.x) * Mathf.Rad2Deg;
                    bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

                    // 随机增加或者减少速度
                    bullet.initialVelocity *= Random.Range(1f, 2.5f);
                    // 随机增加或者减少大小
                    bullet.transform.localScale *= Random.Range(0.5f, 1.2f);
                    // 随机改变方向
                    bullet.transform.Rotate(Vector3.forward * Random.Range(-10f, 10f));

                }
                moveSpeedReal = moveSpeed;
                yield return new WaitForSeconds(4f);
            }

        }
    }
}
