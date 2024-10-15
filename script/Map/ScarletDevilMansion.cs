using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarletDevilMansion : MapDataControl
{
    public GameObject marisaObject;

    // Start is called before the first frame update
    void Start()
    {
        PlayerHealthControl.instance.OnPeace = true;

        // 播放BGM
        SFXManger.instance.playBGM(0);

        // 移动玩家位置
        MainPlayer.instance.transform.position = new Vector3(-1.022f, 0.37f, 0);

        // 读取文本
        // TalkControl.instance.talkdata = TalkData_SDM.instance;

        // 给一个起点
        AssetControl.instance.DropItem(PlayerHealthControl.instance.transform.position, ItemControl.instance.itemGuide[87]);



        // 开场白说点什么吧
        // TODO 需要重构
        if (GlobalControl.instance.LoopTimes == 0)
        {
            if (!GlobalControl.instance.GetHasReadDialog(0, 9))
            {
                TalkControl.instance.ShowText((int)OtherText.EnterText);
                GlobalControl.instance.AddHasReadDialog(0, 9);
            }
        }
        else if (GlobalControl.instance.LoopTimes == 1 && GlobalControl.instance.isGetMarisa != true)
        {
            TalkControl.instance.ShowText(13);
        }
        else if (GlobalControl.instance.teammate == 1)
        {
            if (!GlobalControl.instance.GetHasReadDialog(0, 29))
                TalkControl.instance.ShowText(29);
        }




        // 根据全局变量设置像素小人
        if (GlobalControl.instance.isGetMarisa && GlobalControl.instance.teammate != 1)
        {
            marisaObject.SetActive(true);
        }
        else
        {
            marisaObject.SetActive(false);
        }
        // 开放新路径
        if (GlobalControl.instance.isGetMarisa)
        {
            BigMapControl.instance.SetMarisaRoute();
        }

    }

}
