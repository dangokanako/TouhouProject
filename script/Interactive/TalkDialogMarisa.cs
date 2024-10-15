using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkDialogMarisa
{
    // 战斗对话组件
    // 魔理沙 MAPID = 2
    // 魔理沙击败标志 
    // public static int GetTextNum()
    // {

    //     GlobalControl.instance.isCloseCreateEnemy = true;


    //     if (GlobalControl.instance.GetHasReadDialog(2, 10) && GlobalControl.instance.isGetMarisa == false)
    //     {
    //         return 11;
    //     }


    //     GlobalControl.instance.AddHasReadDialog(2, 10);
    //     return 10;
    // }

    private static List<DialogRule> dialogRules = new List<DialogRule>
    {
        // 魔理沙在队伍时
        new DialogRule { Condition = () => GlobalControl.instance.teammate == 1, DialogId = 15 },

        // 击败后的多次
        new DialogRule { Condition = () => GlobalControl.instance.isGetMarisa == false && GlobalControl.instance.GetHasReadDialog(2, 10), DialogId = 14 },


        // 没有击败时的多次
        new DialogRule { Condition = () => GlobalControl.instance.isGetMarisa == false && GlobalControl.instance.GetHasReadDialog(2, 10), DialogId = 11 },

        // 第一次
        new DialogRule { Condition = () => GlobalControl.instance.isGetMarisa == false && !GlobalControl.instance.GetHasReadDialog(2, 10), DialogId = 10 }
    };

    // 战斗对话组件
    // 琪露诺 MAPID = 2
    public static int GetText()
    {
        foreach (var rule in dialogRules)
        {
            if (rule.Condition())
            {
                TalkControl.instance.ShowText(rule.DialogId);
                // 阅读之后增加阅读次数
                GlobalControl.instance.AddHasReadDialog(2, rule.DialogId);

                GlobalControl.instance.isCloseCreateEnemy = true;
                return rule.DialogId;
            }
        }


        Debug.LogError("没有找到对话");
        return -1;
    }

}
