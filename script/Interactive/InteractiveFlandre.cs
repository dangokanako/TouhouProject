using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveFlandre : InteractiveClass
{

    public InteractiveFlandre()
    {
        dialogRules.Add(new DialogRule { Condition = () => !GlobalControl.instance.GetHasReadDialog(MapID, 30) && GlobalControl.instance.teammate == 1, DialogId = 30 });
        dialogRules.Add(new DialogRule { Condition = () => GlobalControl.instance.LoopTimes == 0 && !GlobalControl.instance.GetHasReadDialog(MapID, 10), DialogId = (int)FlandreText.FirstTalk });
        dialogRules.Add(new DialogRule { Condition = () => GlobalControl.instance.LoopTimes == 0, DialogId = (int)FlandreText.SecondTalk });
        dialogRules.Add(new DialogRule { Condition = () => GlobalControl.instance.LoopTimes != 0, DialogId = 16 });
    }

}
