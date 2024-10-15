using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkDialogCrino
{
    private static List<DialogRule> dialogRules = new List<DialogRule>
    {
        // 魔理沙在队伍时
        new DialogRule { Condition = () => GlobalControl.instance.teammate == 1, DialogId = 16 },

        // 击败后的多次
        new DialogRule { Condition = () => GlobalControl.instance.isGetCrino == true && GlobalControl.instance.GetHasReadDialog(2, 9), DialogId = 13 },


        // 没有击败时的多次
        new DialogRule { Condition = () => GlobalControl.instance.isGetCrino == false && GlobalControl.instance.GetHasReadDialog(2, 9), DialogId = 12 },

        // 第一次
        new DialogRule { Condition = () => GlobalControl.instance.isGetCrino == false && !GlobalControl.instance.GetHasReadDialog(2, 9), DialogId = 9 }
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
                Debug.Log("Crino对话" + rule.DialogId);
                GlobalControl.instance.isCloseCreateEnemy = true;
                return rule.DialogId;
            }
        }


        Debug.LogError("没有找到对话");
        return -1;
    }

}
