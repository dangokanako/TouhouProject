using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal_SDM_Public : TriggerClass
{
    public float PortalX;
    public float PortalY;
    public bool isDesotryAfterActive;
    override public void Active()
    {

        FadeInOut.instance.GoToSceneFake(
() =>
                {
                    PlayerHealthControl.instance.transform.position = new Vector3(PortalX, PortalY, 0);
                }

, 0.3f);

        if (isDesotryAfterActive)
        {
            Destroy(gameObject);
        }

    }
}
