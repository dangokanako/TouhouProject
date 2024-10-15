using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class Action_MoveToPlayer : Action
{
    // [SerializeField] protected float moveSpeed;
    // // 实际移动速度
    [SerializeField] private float moveSpeedReal;
    [SerializeField] private Transform target;
    [SerializeField] private Rigidbody2D rb;

    public override void OnAwake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = PlayerHealthControl.instance.transform;
    }

    public override void OnStart()
    {
        moveSpeedReal = (float)Owner.GetVariable("moveSpeed").GetValue();
    }

    public override TaskStatus OnUpdate()
    {
        rb.AddForce(12 * new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y).normalized * moveSpeedReal, ForceMode2D.Impulse);

        return TaskStatus.Success;
    }
}
