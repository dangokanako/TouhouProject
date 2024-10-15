using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Conditional_IsDistanceToPlayerLessThan : Conditional
{
    public SharedFloat threshold; // 距离阈值

    public override TaskStatus OnUpdate()
    {
        Owner.SetVariableValue("DistanceToPlayer", Vector3.Distance(PlayerHealthControl.instance.transform.position, transform.position));

        if ((float)Owner.GetVariable("DistanceToPlayer").GetValue() < threshold.Value)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}
