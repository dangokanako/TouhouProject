using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using Unity.Mathematics;
using UnityEngine;

public class Enemy_Boss_Youmu : Enemy_Boss
{
    [SerializeField] private BehaviorTree bt;
    public PlayerDamagerClass bulletPrefab;
    protected override void Start()
    {
        base.Start();
        // 转为在行为树里进行普通攻击
        // StartCoroutine(ShootBullet());
        // bt = GetComponentInChildren<BehaviorTree>();
    }

    override protected void enemyDead()
    {
        base.enemyDead();
        GlobalControl.instance.isGetRedi = true;
        SFXManger.instance.PlayBgmBattleGroup();
        TalkDialogYoumu.GetText(19);

    }

    // 返回位置
    public Vector3 getPosition()
    {
        return this.transform.position;
    }
    protected override void FixedUpdate()
    {

        //        Debug.Log("DEBUG:" + bt.GetVariable("DistanceToPlayer"));
        //        Vector3.Distance(PlayerHealthControl.instance.transform.position, transform.position)
        // bt.SetVariableValue("DistanceToPlayer", Vector3.Distance(PlayerHealthControl.instance.transform.position, transform.position));

        // 如果当前HP小于最大HP的一半，那么伤害抗性提升到90，5秒种后恢复
        if (health < maxHealth / 2 && !firstStage)
        {
            BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("youmu_small_1_0", "『做的不错嘛，来吧，接下来是更加疾风骤雨的攻击！』"));
            bt.SendEvent("firstStage");
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



        // 原UPDATE
        if (!PlayerHealthControl.instance.isDead && Time.timeScale != 0f)
        {
            // 无敌帧
            InvincibleTime -= Time.deltaTime;

            // 非环绕模式，处理简单
            if (!isSurround)
            {
                rb.AddForce(12 * new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y).normalized * moveSpeedReal, ForceMode2D.Impulse);
            }
            else
            {
                Vector2 targetSmooth = new Vector2(Mathf.SmoothDamp(this.transform.position.x, MainPlayer.instance.gameObject.transform.position.x, ref xVelocity, smoothTime),
                    Mathf.SmoothDamp(this.transform.position.y, MainPlayer.instance.gameObject.transform.position.y, ref yVelocity, smoothTime));

                Vector2 directionToPlayer = ((Vector2)targetSmooth - (Vector2)transform.position).normalized; // 敌人向玩家的方向

                // 环绕模式，优先保持在一定距离。如果距离合适，那么环绕玩家运动
                if (Vector3.Distance(PlayerHealthControl.instance.transform.position, transform.position) > surroundDistanceMax)
                {
                    rb.AddForce(12 * directionToPlayer.normalized * moveSpeedReal, ForceMode2D.Impulse);
                    // rb.velocity = directionToPlayer.normalized * moveSpeed;
                }
                else if (Vector3.Distance(PlayerHealthControl.instance.transform.position, transform.position) < surroundDistanceMin)
                {
                    if (isSurroundBack)
                    {
                        rb.AddForce(12 * directionToPlayer.normalized * moveSpeedReal * -1.1f, ForceMode2D.Impulse);
                    }
                }
                else
                {
                    if (isSurroundMove)
                    {
                        Vector3 orbitDirectionVector = Quaternion.Euler(0, 0, 90) * directionToPlayer;
                        rb.AddForce(12 * orbitDirectionVector.normalized * moveSpeedReal, ForceMode2D.Impulse);
                    }
                }
            }

            // 防止过远机制
            if (Vector3.Distance(PlayerHealthControl.instance.transform.position, transform.position) > 13f)
                rb.velocity = (target.position - transform.position).normalized * moveSpeedReal * 20;


            // 放大缩小的动画环节……
            sprite.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one * activeSize, changeSpeed * Time.deltaTime);

            if (sprite.localScale.x == activeSize)
            {
                if (activeSize == maxSize)
                    activeSize = minSize;
                else
                    activeSize = maxSize;
            }

            if ((rb.velocity.x < 0 && facingDirection == 1) || (rb.velocity.x > 0 && facingDirection == -1))
                if (moveSpeedReal > 0 && Time.time - lastFlipTime >= flipCooldown)
                    flip();
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private IEnumerator RecoverResistance()
    {
        yield return new WaitForSeconds(3f);
        bluntResistance = 0;
        sharpResistance = 25;
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

            moveSpeedReal = 0.4f * moveSpeed;
            // 计算子弹的方向
            Vector2 direction = MainPlayer.instance.transform.position - this.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // 计算旋转角度
            direction.Normalize();


            for (int j = 0; j < 36; j++)
            {
                // var bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.AngleAxis(10 * j, Vector3.forward)); //生成子弹
                var bullet = EnemyBulletControl.instance.GetBulletAndInitialize(bulletPrefab, this.transform.position, Quaternion.AngleAxis(10 * j, Vector3.forward));
            }


            for (int i = 0; i < 7; i++)
            {
                var randomAngle = UnityEngine.Random.Range(0, 360);
                for (int j = 0; j < 10; j++)
                {
                    // 实例化子弹
                    // var bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.Euler(0, 0, randomAngle + (-10 + 2 * j)));
                    var bullet = EnemyBulletControl.instance.GetBulletAndInitialize(bulletPrefab, this.transform.position, Quaternion.Euler(0, 0, randomAngle + (-10 + 2 * j)));
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
                // var bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.AngleAxis(10 * j, Vector3.forward)); //生成子弹
                var bullet = EnemyBulletControl.instance.GetBulletAndInitialize(bulletPrefab, this.transform.position, Quaternion.AngleAxis(10 * j, Vector3.forward));

            }


            for (int i = 0; i < 7; i++)
            {
                var randomAngle = UnityEngine.Random.Range(0, 360);
                for (int j = 0; j < 10; j++)
                {
                    // 实例化子弹
                    // var bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.Euler(0, 0, randomAngle + (-10 + 2 * j)));
                    var bullet = EnemyBulletControl.instance.GetBulletAndInitialize(bulletPrefab, this.transform.position, Quaternion.Euler(0, 0, randomAngle + (-10 + 2 * j)));
                    bullet.moveSpeedAdd = 0.04f - math.abs(j - 5) * 0.005f;

                }
                yield return new WaitForSeconds(0.15f);
            }

            for (int j = 0; j < 36; j++)
            {
                // var bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.AngleAxis(10 * j, Vector3.forward)); //生成子弹
                var bullet = EnemyBulletControl.instance.GetBulletAndInitialize(bulletPrefab, this.transform.position, Quaternion.AngleAxis(10 * j, Vector3.forward));
            }


            moveSpeedReal = moveSpeed;
            yield return new WaitForSeconds(5f);

        }
    }
}
