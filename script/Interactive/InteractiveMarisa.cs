using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveMarisa : InteractiveClass
{
    // 所有构造函数建议写入start()
    public InteractiveMarisa()
    {
        dialogRules.Add(new DialogRule { Condition = () => !GlobalControl.instance.GetHasReadDialog(MapID, 25), DialogId = 25 });
        dialogRules.Add(new DialogRule { Condition = () => !GlobalControl.instance.GetHasReadDialog(MapID, 26), DialogId = 26 });
    }

}
