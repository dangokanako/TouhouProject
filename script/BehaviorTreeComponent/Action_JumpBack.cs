using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;


public class Action_JumpBack : Action
{
    public float JumpDitance;
    public override void OnAwake()
    {
        Owner.SetVariableValue("PlayerTarget", PlayerHealthControl.instance.transform);
    }


    public override TaskStatus OnUpdate()
    {
        // 计算到玩家的方向，之后向后计算距离进行跳跃
        Vector3 direction = (transform.position - PlayerHealthControl.instance.transform.position).normalized;
        Vector3 targetPosition = transform.position + direction * JumpDitance;

        transform.DOJump(targetPosition, 1, 1, 0.5f);

        return TaskStatus.Success;
    }

}
