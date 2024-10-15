using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveSakuya : InteractiveClass
{
    public InteractiveSakuya()
    {
        dialogRules.Add(new DialogRule { Condition = () => GlobalControl.instance.LoopTimes == 0 && !GlobalControl.instance.GetHasReadDialog(MapID, 6), DialogId = (int)SakuyaText.FirstTalk });
        dialogRules.Add(new DialogRule { Condition = () => GlobalControl.instance.LoopTimes == 0 && !GlobalControl.instance.GetHasReadDialog(MapID, 8), DialogId = (int)SakuyaText.ThirdTalk });
        dialogRules.Add(new DialogRule { Condition = () => GlobalControl.instance.LoopTimes == 0, DialogId = (int)SakuyaText.SecondTalk });
        dialogRules.Add(new DialogRule { Condition = () => GlobalControl.instance.LoopTimes != 0, DialogId = 7 });
    }

}
