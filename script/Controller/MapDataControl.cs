using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDataControl : MonoBehaviour
{
    // 总体地图ID，暂时用不上。红魔馆0，新手教程1那个。
    public int MapID;
    protected List<DialogBubbleRule> dialogRules = new List<DialogBubbleRule>();
    protected List<DialogBubbleRule> dialogRulesBoss = new List<DialogBubbleRule>();
    // 小地图组件代表的ID，主要对话是判断这个
    public int BigMapID;
    public string BossName;
    virtual public void InitTalkRules() { }
    virtual public void InitTalkRulesBoss() { }

    public void GetBubble(int bigmapid)
    {
        BigMapID = bigmapid;
        foreach (var rule in dialogRules)
        {
            if (rule.Condition())
            {
                rule.DialogAction();
                break;
            }
        }
    }

    // 通过bossname来确定对话，虽然string很蠢，但还是这么设计了
    public void GetBubbleByBoss(string bossname)
    {
        BossName = bossname;
        foreach (var rule in dialogRulesBoss)
        {
            if (rule.Condition())
            {
                rule.DialogAction();
                break;
            }
        }
    }

}
