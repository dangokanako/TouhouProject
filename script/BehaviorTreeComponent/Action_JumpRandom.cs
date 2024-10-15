using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;


public class Action_JumpRandom : Action
{
    public float JumpDitance;
    // public override void OnAwake()
    // {
    //     Owner.SetVariableValue("PlayerTarget", PlayerHealthControl.instance.transform);
    // }


    public override TaskStatus OnUpdate()
    {


        // 随机跳向一个总距离为JumpDitance的位置。
        Vector3 direction = new Vector3(this.transform.position.x + Random.Range(-JumpDitance, JumpDitance), this.transform.position.y + Random.Range(-JumpDitance, JumpDitance), this.transform.position.z);

        transform.DOJump(direction, 1, 1, 0.5f);

        return TaskStatus.Success;
    }

}
