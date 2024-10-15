using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TA_Tutorial_1 : TriggerClass
{


    override public void Active()
    {
        TalkControl.instance.ShowText(14);
    }
}
