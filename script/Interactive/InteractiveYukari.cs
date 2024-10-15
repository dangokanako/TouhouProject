using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveYukari : InteractiveClass
{
    override public void Start()
    {
        MapID = 2;

        base.Start();

        // 正常 循环2
        dialogRules.Add(new DialogRule { Condition = () => !GlobalControl.instance.GetHasReadDialog(MapID, 5) && GlobalControl.instance.GetHasReadDialog(MapID, 4) && !GlobalControl.instance.isYukariLeaving, DialogId = 5 });
        // 正常 循环1
        dialogRules.Add(new DialogRule { Condition = () => !GlobalControl.instance.GetHasReadDialog(MapID, 4) && GlobalControl.instance.GetHasReadDialog(MapID, 3) &&!GlobalControl.instance.isYukariLeaving, DialogId = 4 });
        // 正常 第一次
        dialogRules.Add(new DialogRule { Condition = () => !GlobalControl.instance.GetHasReadDialog(MapID, 0) && !GlobalControl.instance.isYukariLeaving, DialogId = 0 });
        // 正常 保底
        dialogRules.Add(new DialogRule { Condition = () => !GlobalControl.instance.isYukariLeaving, DialogId = 3 });

        // 结束时剧情
        dialogRules.Add(new DialogRule { Condition = () => GlobalControl.instance.GetHasReadDialog(MapID, 1) && GlobalControl.instance.isYukariLeaving, DialogId = 2 });
        dialogRules.Add(new DialogRule { Condition = () => !GlobalControl.instance.GetHasReadDialog(MapID, 1) && GlobalControl.instance.isYukariLeaving, DialogId = 1 });
    }

}
