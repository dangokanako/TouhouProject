using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Battle_Baiyulou_1 : MapDataControl
{
    public static Map_Battle_Baiyulou_1 instance;
    public List<Collider2D> collider2DList = new List<Collider2D>();
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

    }


    void Start()
    {
        MapID = 3;

        // 加载碰撞
        EnemyCreator.instance.Colliders = new List<Collider2D>();
        foreach (var item in collider2DList)
        {
            EnemyCreator.instance.Colliders.Add(item);
        }

        // 移动玩家位置
        MainPlayer.instance.transform.position = new Vector3(2f, 7.9f, 0);

        // 播放BGM
        SFXManger.instance.PlayBgmBattleGroup();

        EnemyCreator.instance.LoadEnemyCretor("BaiyuLou_1");


        // 初始化对话规则
        instance.InitTalkRules();
        instance.InitTalkRulesBoss();
        instance.GetBubble(2);
        Debug.Log("Map_Battle_Wuzhihu_1 Start End");
    }

    override public void InitTalkRules()
    {
        // 魔理沙队友对话
        dialogRules.Add(new DialogBubbleRule
        {
            Condition = () => GlobalControl.instance.teammate == 1,
            DialogAction = () =>
            {
                int Random = UnityEngine.Random.Range(0, 8);
                switch (Random)
                {
                    case 0:
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『灵梦你要吃糖嘛』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『不要』"));
                        break;
                    case 1:
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『你能不能别放水了，你打我的时候可没这么弱』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『嘛~』"));
                        break;
                    case 2:
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『今天炸点什么呢？』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『红魔馆。』"));
                        break;
                    case 3:
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『今天早晨刚做的，趁热喝喔』"));
                        AssetControl.instance.DropItem(PlayerHealthControl.instance.transform.position, ItemControl.instance.itemGuide[9]);
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『……谢谢』"));
                        break;
                    case 4:
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『给你把扫帚玩玩』"));
                        AssetControl.instance.DropItem(PlayerHealthControl.instance.transform.position, ItemControl.instance.itemGuide[8]);
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『这个很好用喔，再来点』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『没有了』"));
                        break;
                    case 5:
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『早晨刚做的经验~收好~』"));
                        ExpLevelControl.instance.CreateDrop(PlayerHealthControl.instance.transform.position, 10);
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『为什么经验也能赠予啊？！』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『经验都能掉在地上，就别管那些了~』"));
                        break;
                    case 6:
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『早晨在红魔馆小卖部买的面包』"));
                        AssetControl.instance.DropItem(PlayerHealthControl.instance.transform.position, ItemControl.instance.itemGuide[27]);
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『唔……谢谢……』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『（笑）』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『干嘛啦。』"));
                        break;
                    case 7:
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『之前从天子那借来的桃子』"));
                        AssetControl.instance.DropItem(PlayerHealthControl.instance.transform.position, ItemControl.instance.itemGuide[17]);
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『赃物？』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『啊哈哈~』"));
                        break;
                    default:
                        break;
                }
            }
        });

        // 魔法之森无队友保底对话
        dialogRules.Add(new DialogBubbleRule
        {
            Condition = () => GlobalControl.instance.teammate == 0 && BigMapComponentControl.MoFaSenLin.Contains(BigMapID),
            DialogAction = () =>
            {
                // 创建一个包含三条语句的列表
                List<Action> actions = new List<Action>
                {
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『魔法之森其实湿度很高，其实住起来很不舒服呢』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『不知道魔理沙是怎么在这个鬼地方住下去的』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『爱丽丝好像住在附近……算了，不管她』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『好多虫子，好烦』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『最好不要随地采蘑菇吃』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『啊，三月精在附近，好麻烦』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『桑尼米尔克的能力会折射光线，所以难以发现』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『斯塔萨菲雅的能力能躲避攻击，所以难以击中』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『露娜切露德的能力……没什么用，使劲打吧』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『夜雀能够遮挡视线，很麻烦，尽快解决掉』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『发射的弹幕可以和敌人的弹幕根据伤害抵消喔』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『如果两个质量速度很大的物体碰撞，可以造成可观的动能伤害喔』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『BOSS进入增强模式的话，会获得相当高的伤害抗性』")),
                };

                // 随机选择一条语句来执行
                int index = UnityEngine.Random.Range(0, actions.Count);
                actions[index]();
            }
        });

        // 雾之湖无队友保底对话
        dialogRules.Add(new DialogBubbleRule
        {
            Condition = () => GlobalControl.instance.teammate == 0 && BigMapComponentControl.WuZhiHu.Contains(BigMapID),
            DialogAction = () =>
            {
                // 创建一个包含三条语句的列表
                List<Action> actions = new List<Action>
                {
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『据说雾之湖里可以钓到大鱼呢』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『等我解决完这场异变，我就回老家和暖炉结婚』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『今天的雾之湖也是祥和的一天啊』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『蓝色笨蛋你在吗，我来退治你咯』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『雾之湖的雾一直是个谜呢』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『红魔馆就坐落于雾之湖旁边』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『好多妖精啊，统统处理掉吧』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『发射的弹幕可以和敌人的弹幕根据伤害抵消喔』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『如果两个质量速度很大的物体碰撞，可以造成可观的动能伤害喔』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『BOSS进入增强模式的话，会获得相当高的伤害抗性』")),

                };

                // 随机选择一条语句来执行
                int index = UnityEngine.Random.Range(0, actions.Count);
                actions[index]();
            }
        });
    }

}
