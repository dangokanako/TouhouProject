using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class InteractiveRemilia : InteractiveClass
{
    public InteractiveRemilia()
    {
        dialogRules.Add(new DialogRule { Condition = () => GlobalControl.instance.teammate == 1 && !GlobalControl.instance.GetHasReadDialog(MapID, 31), DialogId = 31 });
        dialogRules.Add(new DialogRule { Condition = () => GlobalControl.instance.LoopTimes == 0 && !GlobalControl.instance.GetHasReadDialog(MapID, 4), DialogId = (int)ReimiliaText.FirstTalk });
        dialogRules.Add(new DialogRule { Condition = () => GlobalControl.instance.LoopTimes == 0 && GlobalControl.instance.GetHasReadDialog(MapID, 4), DialogId = (int)ReimiliaText.SecondTalk });
        dialogRules.Add(new DialogRule { Condition = () => GlobalControl.instance.LoopTimes != 0 && !GlobalControl.instance.GetHasReadDialog(MapID, 14), DialogId = 14 });
        dialogRules.Add(new DialogRule { Condition = () => GlobalControl.instance.LoopTimes != 0, DialogId = 15 });
    }


}
