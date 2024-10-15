using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Map_Battle_Wuzhihu_1 : MapDataControl
{
    public static Map_Battle_Wuzhihu_1 instance;
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
        MapID = 2;
        // 加载碰撞
        EnemyCreator.instance.Colliders = new List<Collider2D>();
        foreach (var item in collider2DList)
        {
            EnemyCreator.instance.Colliders.Add(item);
        }


        // 播放BGM
        SFXManger.instance.PlayBgmBattleGroup();

        EnemyCreator.instance.LoadEnemyCretor("WuZhiHu_1");

        // 进入战斗场景时，随机给一个被动符卡
        // 从65到69随机一个
        AssetControl.instance.DropItem(PlayerHealthControl.instance.transform.position, ItemControl.instance.itemGuide[UnityEngine.Random.Range(65, 70)]);

        // 初始化对话规则
        instance.InitTalkRules();
        instance.InitTalkRulesBoss();
        Debug.Log("Map_Battle_Wuzhihu_1 Start End");
        instance.GetBubble(3);
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

    override public void InitTalkRulesBoss()
    {
        dialogRules.Add(new DialogBubbleRule
        {
            Condition = () => GlobalControl.instance.teammate == 1 && BossName == "露米娅",
            DialogAction = () =>
            {
                int Random = UnityEngine.Random.Range(0, 5);
                switch (Random)
                {
                    case 0:
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『不太熟，是路过的杂鱼吗？』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『应该是』"));
                        break;
                    case 1:
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『明明设定很少呢』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『是啊，真是令人羡慕的人气』"));
                        break;
                    case 2:
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『住在雾之湖吗？』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『附近的小妖精很多，应该是住在附近』"));
                        break;
                    case 3:
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『真羡慕妖怪啊，被打碎之后也能复原』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『嘛……』"));
                        break;
                    case 4:
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『醒醒，别偷懒了，头目战了』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『是是~』"));
                        break;
                    default:
                        break;
                }
            }
        });
        dialogRulesBoss.Add(new DialogBubbleRule
        {
            Condition = () => GlobalControl.instance.teammate == 0 && BossName == "大妖精",
            DialogAction = () =>
            {
                List<Action> actions = new List<Action>
                {
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『大妖精都打不过的话，我不如去学辉夜当家里蹲算了』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『据说大妖精只是路过的较强的杂鱼』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『明明是没名字的杂鱼，却稍有人气』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『掉落点好东西吧』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『退治开始』")),
                };

                // 随机选择一条语句来执行
                int index = UnityEngine.Random.Range(0, actions.Count);
                actions[index]();
            }
        });

        dialogRules.Add(new DialogBubbleRule
        {
            Condition = () => GlobalControl.instance.teammate == 1 && BossName == "露米娅",
            DialogAction = () =>
            {
                int Random = UnityEngine.Random.Range(0, 5);
                switch (Random)
                {
                    case 0:
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『不太熟，路过的笨蛋？』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『应该是』"));
                        break;
                    case 1:
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『和琪露诺谁厉害一些呢？』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『琪露诺吧，琪露诺是二面』"));
                        break;
                    case 2:
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『“操纵黑暗！”，能力虽然很帅气……』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『但有点笨笨的』"));
                        break;
                    case 3:
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『真羡慕妖怪啊，被打碎之后也能复原』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『嘛……』"));
                        break;
                    case 4:
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『醒醒，别偷懒了，头目战了』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『是是~』"));
                        break;
                    default:
                        break;
                }
            }
        });
        dialogRulesBoss.Add(new DialogBubbleRule
        {
            Condition = () => GlobalControl.instance.teammate == 0 && BossName == "露米娅",
            DialogAction = () =>
            {
                List<Action> actions = new List<Action>
                {
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『是~这~样~吗？（模仿）』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『红魔乡一面，很有纪念意义呢』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『据说也是笨蛋』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『明明很弱，但挺有人气，干掉吧』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『退治开始』")),
                };

                // 随机选择一条语句来执行
                int index = UnityEngine.Random.Range(0, actions.Count);
                actions[index]();
            }
        });


        dialogRules.Add(new DialogBubbleRule
        {
            Condition = () => GlobalControl.instance.teammate == 1 && BossName == "琪露诺",
            DialogAction = () =>
            {
                int Random = UnityEngine.Random.Range(0, 5);
                switch (Random)
                {
                    case 0:
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『笨蛋』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『笨蛋』"));
                        break;
                    case 1:
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『是笨蛋啊』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『是笨蛋呢』"));
                        break;
                    case 2:
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『夏天的时候可以跟在琪露诺后面乘凉』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『还真是奇妙的用途』"));
                        break;
                    case 3:
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『真羡慕妖怪啊，被打碎之后也能复原』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『嘛……但是是笨蛋』"));
                        break;
                    case 4:
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『醒醒，别偷懒了，头目战了』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『是是~』"));
                        break;
                    default:
                        break;
                }
            }
        });
        dialogRulesBoss.Add(new DialogBubbleRule
        {
            Condition = () => GlobalControl.instance.teammate == 0 && BossName == "琪露诺",
            DialogAction = () =>
            {
                List<Action> actions = new List<Action>
                {
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『笨蛋』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『人气相当高，痛快地干掉吧』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『会在雾之湖冻青蛙玩』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『夏天的时候会化掉吧』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『在妖精里算是比较强的』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『退治开始』")),
                };

                // 随机选择一条语句来执行
                int index = UnityEngine.Random.Range(0, actions.Count);
                actions[index]();
            }
        });

        dialogRules.Add(new DialogBubbleRule
        {
            Condition = () => GlobalControl.instance.teammate == 1 && BossName == "小恶魔",
            DialogAction = () =>
            {
                int Random = UnityEngine.Random.Range(0, 5);
                switch (Random)
                {
                    case 0:
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『去红魔馆借书的时候经常会遇到这家伙』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『喔……』"));
                        break;
                    case 1:
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『没什么存在感的家伙呢』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『是啊』"));
                        break;
                    case 2:
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『是不是只有她是恶魔种族啊？』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『别问我，我不知道啦』"));
                        break;
                    case 3:
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『真羡慕妖怪啊，被打碎之后也能复原』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『嘛……不过她的种族是恶魔』"));
                        break;
                    case 4:
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『醒醒，别偷懒了，头目战了』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『是是~』"));
                        break;
                    default:
                        break;
                }
            }
        });
        dialogRulesBoss.Add(new DialogBubbleRule
        {
            Condition = () => GlobalControl.instance.teammate == 0 && BossName == "小恶魔",
            DialogAction = () =>
            {
                List<Action> actions = new List<Action>
                {
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『从红魔馆跑出来的吗』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『人气一般，随便地干掉吧』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『其实小恶魔没什么设定呢』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『和大妖精一样，是路过的较强的杂鱼』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『种族是恶魔……当然的吧！（自我吐槽）』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『退治开始』")),
                };

                // 随机选择一条语句来执行
                int index = UnityEngine.Random.Range(0, actions.Count);
                actions[index]();
            }
        });


        dialogRules.Add(new DialogBubbleRule
        {
            Condition = () => GlobalControl.instance.teammate == 1 && BossName == "莉格露",
            DialogAction = () =>
            {
                int Random = UnityEngine.Random.Range(0, 6);
                switch (Random)
                {
                    case 0:
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『好讨厌的妖怪种类！』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『难得和魔理沙同感』"));
                        break;
                    case 1:
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『我家里经常有小虫子呢』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『不……那个和莉格露没关系吧……』"));
                        break;
                    case 2:
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『住在森林里不嫌吵吗』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『还好啦』"));
                        break;
                    case 3:
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『真羡慕妖怪啊，被打碎之后也能复原』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『嘛……』"));
                        break;
                    case 4:
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『其实莉格露有夜光的喔』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『不想知道，赶紧退治吧』"));
                        break;
                    case 5:
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『醒醒，别偷懒了，头目战了』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『是是~』"));
                        break;
                    default:
                        break;
                }
            }
        });
        dialogRulesBoss.Add(new DialogBubbleRule
        {
            Condition = () => GlobalControl.instance.teammate == 0 && BossName == "莉格露",
            DialogAction = () =>
            {
                List<Action> actions = new List<Action>
                {
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『杀虫剂……杀虫剂……（摸索）』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『没什么存在感』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『虫子，讨厌。可能是因为这个人气更低了』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『还好发射的是弹幕而不是虫子』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『这种妖怪为了大家的精神健康还是尽快退治吧』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『说起来很像男孩子呢……』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『奈特巴格(ナイトバグ)其实就是Nightbug(笑)』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『虫子不要过来啊！』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『笨蛋（应该是）』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『退治开始』")),
                };

                // 随机选择一条语句来执行
                int index = UnityEngine.Random.Range(0, actions.Count);
                actions[index]();
            }
        });


        dialogRules.Add(new DialogBubbleRule
        {
            Condition = () => GlobalControl.instance.teammate == 1 && BossName == "小夜雀",
            DialogAction = () =>
            {
                int Random = UnityEngine.Random.Range(0, 6);
                switch (Random)
                {
                    case 0:
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『那个烤八目鳗很好吃喔』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『是吗，下次一起去吃吧』"));
                        break;
                    case 1:
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『小夜雀有时候在森林里举行演唱会』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『喔……』"));
                        break;
                    case 2:
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『住在森林里不嫌吵吗』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『还好啦』"));
                        break;
                    case 3:
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『真羡慕妖怪啊，被打碎之后也能复原』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『嘛……』"));
                        break;
                    case 4:
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『森林里经常会遇到小夜雀呢』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『有好好退治吗』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『不扰民就无所谓了』"));
                        break;
                    case 5:
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『醒醒，别偷懒了，头目战了』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『是是~』"));
                        break;
                    default:
                        break;
                }
            }
        });
        dialogRulesBoss.Add(new DialogBubbleRule
        {
            Condition = () => GlobalControl.instance.teammate == 0 && BossName == "小夜雀",
            DialogAction = () =>
            {
                List<Action> actions = new List<Action>
                {
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『据说在森林里开了一家摊铺，在卖什么呢』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『夜盲？还好现在是白天』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『能用歌声迷惑人的能力，塞壬吗（自我吐槽）』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『夜雀，喜欢吵闹的地方』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『好像是在烤八目鳗』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『笨蛋（应该是）』")),
                () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『退治开始』")),
                };

                // 随机选择一条语句来执行
                int index = UnityEngine.Random.Range(0, actions.Count);
                actions[index]();
            }
        });



        dialogRules.Add(new DialogBubbleRule
        {
            Condition = () => GlobalControl.instance.teammate == 1 && BossName == "魔理沙",
            DialogAction = () =>
            {
                int Random = UnityEngine.Random.Range(0, 5);
                switch (Random)
                {
                    case 0:
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『……』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『看我干嘛，我也觉得挺奇怪的』"));
                        break;
                    case 1:
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『此事必有蹊跷……』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『真的？』"));
                        break;
                    case 2:
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『为什么那边那个魔理沙就能放那么多弹幕？』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『不知道。（移开目光）』"));
                        break;
                    case 3:
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『揍她揍她』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『这可是你说的』"));
                        break;
                    case 4:
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『据说看到和自己一模一样的人的话……』"));
                        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "『不要再说啦！』"));
                        break;
                    default:
                        break;
                }
            }
        });
        // dialogRulesBoss.Add(new DialogBubbleRule
        // {
        //     Condition = () => GlobalControl.instance.teammate == 0 && BossName == "魔理沙",
        //     DialogAction = () =>
        //     {
        //         List<Action> actions = new List<Action>
        //         {
        //         () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『喂？魔理沙？你混在一群笨蛋里干什么呢？』")),
        //         () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『喂？魔理沙？你吃什么奇怪的蘑菇了吗？』")),
        //         () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『喂？魔理沙？没睡醒吗？』")),
        //         () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『早啊魔理沙，什么？为什么出现血条了』")),
        //         () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『喂，我要趁机报复了喔』")),
        //         () => BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "『喂？魔理沙？不说话装高手是吧？』")),
        //         };

        //         // 随机选择一条语句来执行
        //         int index = UnityEngine.Random.Range(0, actions.Count);
        //         actions[index]();
        //     }
        // });
    }
}
