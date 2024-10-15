using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Action_Flip : Action
{
    [SerializeField] private Rigidbody2D rb;
    public override void OnAwake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    public override TaskStatus OnUpdate()
    {
        if ((rb.velocity.x < 0 && facingDirection == 1) || (rb.velocity.x > 0 && facingDirection == -1))
            if (Time.time - lastFlipTime >= flipCooldown)
                flip();

        return TaskStatus.Running;
    }

    // 改变方向的冷却时间
    private float flipCooldown = 0.5f;
    // 上一次改变方向的时间
    private float lastFlipTime;
    // 当前朝向
    private int facingDirection = 1;



    virtual protected void flip()
    {
        if (Time.time - lastFlipTime < flipCooldown)
        {
            // 如果当前时间与上一次改变方向的时间之差小于冷却时间，那么不执行改变方向的操作
            return;
        }

        facingDirection *= -1;
        transform.Rotate(0, 180, 0);

        lastFlipTime = Time.time;
    }
}
