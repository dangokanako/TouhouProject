using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal_SDM_Battlefiled : TriggerClass
{
    public bool isBattle;
    public string SceneName;

    override public void Active()
    {

        if (SceneName == "ScarletDevilMansion")
        {
            FadeInOut.instance.GoToScene(SceneName);

            GlobalControl.instance.isBattle = isBattle;
            if (GlobalControl.instance.isBattle)
            {
                PlayerHealthControl.instance.OnPeace = false;
            }
            FadeInOut.instance.GoToScene(SceneName);
        }
        else
        {
            UIControl.instance.OpenBigMap();
        }





    }
}
