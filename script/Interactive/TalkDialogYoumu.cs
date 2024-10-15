using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkDialogYoumu : MonoBehaviour
{
    // 战斗对话组件

    private static List<DialogRule> dialogRules = new List<DialogRule>
    {
        // // 魔理沙在队伍时
        // new DialogRule { Condition = () => GlobalControl.instance.teammate == 1, DialogId = 15 },

        // 击败后的多次
        new DialogRule { Condition = () => GlobalControl.instance.isGetYoumu == true&& GlobalControl.instance.teammate == 1  , DialogId = 18 },
        new DialogRule { Condition = () => GlobalControl.instance.isGetYoumu == true && GlobalControl.instance.teammate == 0 , DialogId = 17 },


        // 没有击败时的多次     魔理沙在
        new DialogRule { Condition = () => GlobalControl.instance.isGetYoumu == false && (GlobalControl.instance.GetHasReadDialog(3, 13)||GlobalControl.instance.GetHasReadDialog(3, 14)) && GlobalControl.instance.teammate == 1    , DialogId = 16 },      


        // 没有击败时的多次     魔理沙不在
        new DialogRule { Condition = () => GlobalControl.instance.isGetYoumu == false && (GlobalControl.instance.GetHasReadDialog(3, 13)||GlobalControl.instance.GetHasReadDialog(3, 14)) && GlobalControl.instance.teammate == 0   , DialogId = 15 },      

        // 第一次 魔理沙在
        new DialogRule { Condition = () => GlobalControl.instance.isGetYoumu == false && !(GlobalControl.instance.GetHasReadDialog(3, 13) && GlobalControl.instance.GetHasReadDialog(3, 14))  && GlobalControl.instance.teammate == 1, DialogId = 14 },

        // 第一次 并且魔理沙不在
        new DialogRule { Condition = () => GlobalControl.instance.isGetYoumu == false && !(GlobalControl.instance.GetHasReadDialog(3, 13) && GlobalControl.instance.GetHasReadDialog(3, 14)) && GlobalControl.instance.teammate == 0, DialogId = 13 }
    };

    // 战斗对话组件
    // 妖梦 MAPID = 3
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
    public static int GetText(int id)
    {
        TalkControl.instance.ShowText(id);
        // 阅读之后增加阅读次数
        GlobalControl.instance.AddHasReadDialog(3, id);
        return -1;
    }
}
