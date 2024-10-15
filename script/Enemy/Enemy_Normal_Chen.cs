using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Normal_Chen : EnemyControl
{
    // 冲撞距离
    public float RushDistanceMax;
    public float RushDistanceMin;
    // 冲撞POWER
    public float RushPower;
    public float RushPowerCount;
    // 是否精准冲撞
    public bool RushAim;

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {

        if (!PlayerHealthControl.instance.isDead && Time.timeScale != 0f)
        {
            // 无敌帧
            InvincibleTime -= Time.deltaTime;

            Vector2 targetSmooth = new Vector2(Mathf.SmoothDamp(this.transform.position.x, MainPlayer.instance.gameObject.transform.position.x, ref xVelocity, smoothTime),
                Mathf.SmoothDamp(this.transform.position.y, MainPlayer.instance.gameObject.transform.position.y, ref yVelocity, smoothTime));

            Vector2 directionToPlayer = ((Vector2)targetSmooth - (Vector2)transform.position).normalized; // 敌人向玩家的方向


            // 冲撞模式
            if (RushPowerCount > RushPower)
            {
                RushPowerCount = -2;
                if (!RushAim)
                    rb.AddForce(directionToPlayer.normalized * 1900, ForceMode2D.Impulse);
                else
                    rb.AddForce((MainPlayer.instance.transform.position - transform.position).normalized * 1900, ForceMode2D.Impulse);
            }
            else
            {
                // 冲刺之后的喘息时间
                if (RushPowerCount < 0)
                {
                    RushPowerCount += Time.deltaTime;
                }
                else
                {
                    // 环绕模式，优先保持在一定距离。如果距离合适，那么冲刺
                    if (Vector3.Distance(PlayerHealthControl.instance.transform.position, transform.position) > RushDistanceMax)
                    {
                        rb.AddForce(12 * directionToPlayer.normalized * moveSpeed, ForceMode2D.Impulse);
                    }
                    else if (Vector3.Distance(PlayerHealthControl.instance.transform.position, transform.position) < RushDistanceMin)
                    {
                        rb.AddForce(12 * directionToPlayer.normalized * moveSpeed * -1.1f, ForceMode2D.Impulse);
                    }
                    else
                    {
                        // 在范围内时
                        RushPowerCount += Time.deltaTime;
                        // 放大缩小的动画环节
                        sprite.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one * activeSize, changeSpeed * 10 * Time.deltaTime);
                    }
                }


                // 防止过远机制
                if (Vector3.Distance(PlayerHealthControl.instance.transform.position, transform.position) > 13f)
                    rb.velocity = (target.position - transform.position).normalized * moveSpeed * 20;

            }


            if (sprite.localScale.x == activeSize)
            {
                if (activeSize == maxSize)
                    activeSize = minSize;
                else
                    activeSize = maxSize;
            }

            if ((rb.velocity.x < 0 && facingDirection == 1) || (rb.velocity.x > 0 && facingDirection == -1))
                if (moveSpeed > 0 && Time.time - lastFlipTime >= flipCooldown)
                    flip();
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

    }
}
