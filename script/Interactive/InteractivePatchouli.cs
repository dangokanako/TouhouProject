using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractivePatchouli : InteractiveClass
{
    public InteractivePatchouli()
    {
        dialogRules.Add(new DialogRule { Condition = () => !GlobalControl.instance.GetHasReadDialog(MapID, 27) && GlobalControl.instance.teammate == 1, DialogId = 27 });
        dialogRules.Add(new DialogRule { Condition = () => GlobalControl.instance.LoopTimes != 0 && !GlobalControl.instance.GetHasReadDialog(MapID, 21), DialogId = 21 });
        dialogRules.Add(new DialogRule { Condition = () => GlobalControl.instance.LoopTimes == 0 && !GlobalControl.instance.GetHasReadDialog(MapID, 0) && !GlobalControl.instance.GetHasReadDialog(MapID, 4), DialogId = (int)PatchouliText.FirstTalk });
        dialogRules.Add(new DialogRule { Condition = () => GlobalControl.instance.LoopTimes == 0 && !GlobalControl.instance.GetHasReadDialog(MapID, 3) && !GlobalControl.instance.GetHasReadDialog(MapID, 4), DialogId = (int)PatchouliText.SecondTalk });
        dialogRules.Add(new DialogRule { Condition = () => !GlobalControl.instance.GetHasReadDialog(MapID, 4) && GlobalControl.instance.LoopTimes == 0, DialogId = 20 });
        dialogRules.Add(new DialogRule { Condition = () => GlobalControl.instance.GetHasReadDialog(MapID, 4) && !GlobalControl.instance.GetHasReadDialog(MapID, 22), DialogId = 22 });
        dialogRules.Add(new DialogRule { Condition = () => GlobalControl.instance.GetHasReadDialog(MapID, 22), DialogId = 23 });
        dialogRules.Add(new DialogRule { Condition = () => 1 == 1, DialogId = 23 });
    }


}
