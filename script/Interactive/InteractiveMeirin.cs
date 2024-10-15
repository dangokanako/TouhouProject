using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveMeirin : InteractiveClass
{
    public InteractiveMeirin()
    {
        dialogRules.Add(new DialogRule { Condition = () => !GlobalControl.instance.GetHasReadDialog(MapID, 28) && GlobalControl.instance.teammate == 1, DialogId = 28 });
        dialogRules.Add(new DialogRule { Condition = () => GlobalControl.instance.LoopTimes == 0 && !GlobalControl.instance.GetHasReadDialog(MapID, 1), DialogId = (int)MeirinText.FirstTalk });
        dialogRules.Add(new DialogRule { Condition = () => GlobalControl.instance.LoopTimes == 0 && !GlobalControl.instance.GetHasReadDialog(MapID, 18), DialogId = 18 });
        dialogRules.Add(new DialogRule { Condition = () => GlobalControl.instance.LoopTimes == 1, DialogId = 19 });
        dialogRules.Add(new DialogRule { Condition = () => GlobalControl.instance.LoopTimes == 0, DialogId = (int)MeirinText.SecondTalk });
    }

}
