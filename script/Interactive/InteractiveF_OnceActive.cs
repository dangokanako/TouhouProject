using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveF_OnceActive : InteractiveClass
{
    public float PortalX;
    public float PortalY;
    public bool isDesotryAfterActive;
    public bool isBattle;
    override public void Interactive()
    {
        FadeInOut.instance.GoToSceneFake(() =>
                {
                    PlayerHealthControl.instance.transform.position = new Vector3(PortalX, PortalY, 0);
                    GlobalControl.instance.isBattle = isBattle;
                }, 0.3f);

        if (isDesotryAfterActive)
        {
            Destroy(gameObject);
        }

    }
}
