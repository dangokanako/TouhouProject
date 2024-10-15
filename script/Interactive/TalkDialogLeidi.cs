using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkDialogLeidi
{
    // 战斗对话组件
    private static List<DialogRule> dialogRules = new List<DialogRule>
    {
        // // 魔理沙在队伍时
        // new DialogRule { Condition = () => GlobalControl.instance.teammate == 1, DialogId = 15 },

        // 击败后的多次
        new DialogRule { Condition = () => GlobalControl.instance.isGetRedi == true , DialogId = 12 },


        // 没有击败时的多次     魔理沙在(暂时没写)
        new DialogRule { Condition = () => GlobalControl.instance.isGetRedi == false &&( GlobalControl.instance.GetHasReadDialog(3, 9)||GlobalControl.instance.GetHasReadDialog(3, 10))    , DialogId = 11 },      


        // 第一次 魔理沙在
        new DialogRule { Condition = () => GlobalControl.instance.isGetRedi == false && !GlobalControl.instance.GetHasReadDialog(3, 9) && GlobalControl.instance.teammate == 1, DialogId = 10 },
        // 第一次 并且魔理沙不在
        new DialogRule { Condition = () => GlobalControl.instance.isGetRedi == false && !GlobalControl.instance.GetHasReadDialog(3, 9) && GlobalControl.instance.teammate == 0, DialogId = 9 }
    };

    // 战斗对话组件
    // 蕾蒂 MAPID = 3
    public static int GetText()
    {
        foreach (var rule in dialogRules)
        {
            if (rule.Condition())
            {
                TalkControl.instance.ShowText(rule.DialogId);
                // 阅读之后增加阅读次数
                GlobalControl.instance.AddHasReadDialog(3, rule.DialogId);

                GlobalControl.instance.isCloseCreateEnemy = true;
                return rule.DialogId;
            }
        }


        Debug.LogError("没有找到对话");
        return -1;
    }

}
