using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TA_SDM_1 : TriggerClass
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    override public void Active()
    {
        if (GlobalControl.instance.LoopTimes == 0)
        {
            if (GlobalControl.instance.isTutorial)
                TalkControl.instance.ShowText(24);
            else
                TalkControl.instance.ShowText((int)OtherText.ChufaText);

            Destroy(gameObject);
        }
    }
}
