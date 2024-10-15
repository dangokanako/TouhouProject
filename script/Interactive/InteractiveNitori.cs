using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveNitori : InteractiveClass
{
    override public void Start()
    {
        MapID = 2;

        base.Start();
        // 正常 第一次
        dialogRules.Add(new DialogRule { Condition = () => GlobalControl.instance.GetHasReadDialog(MapID, 6) && !GlobalControl.instance.isYukariLeaving, DialogId = 8 });
        dialogRules.Add(new DialogRule { Condition = () => !GlobalControl.instance.GetHasReadDialog(MapID, 6) && !GlobalControl.instance.isYukariLeaving, DialogId = 6 });

        // 结束时剧情
        dialogRules.Add(new DialogRule { Condition = () => GlobalControl.instance.isNitoriLeaving, DialogId = 7 });
    }

}
