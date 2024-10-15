using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TalkData_SDM : TalkData
{
    public static TalkData_SDM instance;
    [Header("文本调试")]

    private talkPiece[][] _talkPiece;
    public override talkPiece[][] TalkPiece
    {
        get
        {
            return _talkPiece;
        }
        set
        {
            _talkPiece = value;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        TalkControl.instance.talkdata = this;

        _talkPiece = new talkPiece[][]{
            // 0
            new talkPiece[]{
                new talkPiece(
                            "reimu_10",
                            "patchouli_1",
                            "博丽灵梦",
                            "帕秋莉·诺雷姬",
                            CharecterColor.Reimu,
                            CharecterColor.Patchouli,
                            (int)ShowName.Left,
                            "『帕琪，这场异变到底怎么回事呀？』 ",
                            CharecterColor.Reimu
                        ),
                new talkPiece(
                    "reimu_0",
                    "patchouli_1",
                    "",
                    "",
                    CharecterColor.Reimu,
                    CharecterColor.Patchouli,
                    (int)ShowName.Right,
                    "『……目前没什么头绪呢……』",
                    CharecterColor.Patchouli
                ),
                new talkPiece((int) ShowName.Right, "『目前可以知道的只是妖怪们在源源不断地涌出……』"),
                new talkPiece("reimu_4","patchouli_1",(int) ShowName.Left, "『嘛，虽然听上去好像在幻想乡是家常便饭的事情了……』"),
            },
            // 1
            new talkPiece[]{
                new talkPiece(
                                "reimu_0",
                                "meilin_0",
                                "博丽灵梦",
                                "红美铃",
                            CharecterColor.Reimu,
                            CharecterColor.Meirin,
                            (int)ShowName.Right,
                                "『大小姐在楼上等你喔。』",
                            CharecterColor.Meirin),
                new talkPiece("reimu_4","meilin_0",(int) ShowName.Left, "『这个传送有什么意义吗……』"),
                new talkPiece("reimu_4","meilin_0",(int) ShowName.Right, "『不要问我，我只是门卫。』"),
            },
            // 2
            new talkPiece[]{
                new talkPiece(
                            "reimu_0",
                            "meilin_0",
                            "博丽灵梦",
                            "红美铃",
                CharecterColor.Reimu,
                CharecterColor.Meirin,
                (int)ShowName.Left,
                            "『你个门卫至少去守一下大门，帮我减少一下打怪压力呗……』",
                CharecterColor.Reimu),
                new talkPiece(
                                "reimu_0",
                                "meilin_3",
                                "博丽灵梦",
                                "红美铃",
                    CharecterColor.Reimu,
                    CharecterColor.Meirin,
                    (int)ShowName.Right,
                                "『不要。（斩钉截铁）』",
                    CharecterColor.Meirin
                )
            },
            // 3
            new talkPiece[]{
                new talkPiece(
                            "reimu_0",
                            "patchouli_1",
                            "博丽灵梦",
                            "帕秋莉·诺雷姬",
                            CharecterColor.Reimu,
                            CharecterColor.Patchouli,
                            (int)ShowName.Left,
                            "『作为红魔馆的智力担当，对这次异变的处理有没有什么建议？』",
                            CharecterColor.Reimu
                        ),
                new talkPiece((int) ShowName.Right, "『……试试像以前那样，把所有挡在前面的生物都打倒』"),
                new talkPiece((int) ShowName.Right, "『一直打到黑幕出现为止……』"),
                new talkPiece((int) ShowName.Right, "『虽然有点简单粗暴了，不过我看灵梦你也不像和平主义者呢……』"),
                new talkPiece("reimu_4","patchouli_1",(int) ShowName.Left, "『啊哈哈……』"),
                new talkPiece("reimu_4","patchouli_0",(int) ShowName.Right, "『对了，说起来蕾咪在找你呢，去拜访一下她吧』"),
                new talkPiece("reimu_0","patchouli_0",(int) ShowName.Left, "『好喔』",Talk_3)
            },
            // 4
            new talkPiece[]{
                new talkPiece(
                            "reimu_0",
                            "remilia_0",
                            "博丽灵梦",
                            "蕾米莉亚·斯卡雷特",
                            CharecterColor.Reimu,
                            CharecterColor.Reimilia,
                            (int)ShowName.Right,
                            "『灵梦，快去把外面的那群喧闹的笨蛋都干掉』",
                            CharecterColor.Reimilia
                        ),
                new talkPiece("reimu_3","remilia_0",(int) ShowName.Left, "『你有空在这里喝茶的话，也出去活动一下筋骨吧』"),
                new talkPiece("reimu_3","remilia_7",(int) ShowName.Right, "『之前我也出去调查了一下，这场异变很有趣呢，笨蛋们一直源源不断地出现』"),
                new talkPiece("reimu_3","remilia_8",(int) ShowName.Left, "『那种事情只要看一眼就能明白了吧』"),
                new talkPiece("reimu_3","remilia_7",(int) ShowName.Right, "『果然异变这种事情还是交给专业的灵梦处理吧，这才是知人善用』"),
                new talkPiece("reimu_3","remilia_8",(int) ShowName.Left, "『请回想起你当自机时的尊严。』"),
                new talkPiece((int) ShowName.Right, "『如果调查有什么进展、获得什么主线道具的话，可以拿给我分析一下』"),
                new talkPiece((int) ShowName.Right, "『此外我已经吩咐过咲夜了，可以给你一些有用物资』"),
                new talkPiece((int) ShowName.Right, "『帕琪那边则在做一些升级研究，如果缺乏战斗力的话，你可以找她聊一聊』"),
                new talkPiece((int) ShowName.Right, "『我们红魔馆会尽力给你一些帮助的』"),
                new talkPiece("reimu_1","remilia_8",(int) ShowName.Left, "『你这人还怪好的嘞』"),
                new talkPiece("reimu_1","remilia_7",(int) ShowName.Right, "『嘛，难得出现这么有意思的情况呢』"),
            },
                        // 5
            new talkPiece[]{
                new talkPiece(
                            "reimu_0",
                            "remilia_8",
                            "博丽灵梦",
                            "蕾米莉亚·斯卡雷特",
                            CharecterColor.Reimu,
                            CharecterColor.Reimilia,
                            (int)ShowName.Right,
                            "『至少出击一次再回来报告吧?』",
                            CharecterColor.Reimilia
                        ),
                new talkPiece("reimu_3","remilia_8",(int) ShowName.Left, "『（恼）』"),
            },
            // 6
            new talkPiece[]{
                new talkPiece(
                            "reimu_0",
                            "sakuya_1",
                            "博丽灵梦",
                            "十六夜咲夜",
                            CharecterColor.Reimu,
                            CharecterColor.Sakuya,
                            (int)ShowName.Right,
                            "『是灵梦啊，大小姐已经吩咐过我了，红魔馆武具库的部分道具会向您开放』",
                            CharecterColor.Sakuya
                        ),
                new talkPiece("reimu_10","sakuya_1",(int) ShowName.Left, "『真的嘛！（金灿灿眼）』"),
                new talkPiece("reimu_3","sakuya_1",(int) ShowName.Right, "『…………（咳）当然，为了弥补一下馆内的财政支出平衡，还是要收取一定费用的』"),
                new talkPiece("reimu_3","sakuya_1",(int) ShowName.Right, "『作为初次访问的礼物，这是我的符卡，虽然有次数限制，但您可以拿去使用』",AddItem_Sakuya),
                new talkPiece((int) ShowName.Left, "『为什么我能使用你的符卡……』"),
                new talkPiece((int) ShowName.Right, "『毕竟幻想乡嘛，有什么事情发生都不为奇』"),
                new talkPiece((int) ShowName.Left, "『是吗，感觉稍微有点牵强呢……』"),
                new talkPiece((int) ShowName.Right, "『进入战斗之后，在右上快捷栏选中符卡之后，按鼠标左键使用就可以了』"),
                new talkPiece((int) ShowName.Right, "『此外符卡使用时会消耗一定的SP，也就是绿条，请注意』"),
                new talkPiece((int) ShowName.Right, "『符卡可以在我这里购买，也可以在战斗地图中合成，具体看您的需求和喜好』"),
                new talkPiece("reimu_0","sakuya_1",(int) ShowName.Left, "『嗯~都有什么符卡出售呢~』"),
                new talkPiece((int) ShowName.Right, "『请过目』",OpenShopSakuya),
                new talkPiece("reimu_3","sakuya_1",(int) ShowName.Left, "『虽然东西很多呢，但我的背包格子好少啊……』"),
                new talkPiece((int) ShowName.Right, "『那就要靠灵梦小姐您自己深思熟虑的规划了』"),
                new talkPiece((int) ShowName.Left, "『好吧~』"),
            },
            // 7 
            new talkPiece[]{
                new talkPiece(
                            "reimu_0",
                            "sakuya_1",
                            "博丽灵梦",
                            "十六夜咲夜",
                            CharecterColor.Reimu,
                            CharecterColor.Sakuya,
                            (int)ShowName.Left,
                            "『咲夜，我又来啦~看看有什么好东西』",
                            CharecterColor.Reimu
                        ),
                new talkPiece((int) ShowName.Right, "『请过目』",OpenShopSakuya),
            },
            // 8
            new talkPiece[]{
                new talkPiece(
                            "reimu_0",
                            "sakuya_1",
                            "博丽灵梦",
                            "十六夜咲夜",
                            CharecterColor.Reimu,
                            CharecterColor.Sakuya,
                            (int)ShowName.Left,
                            "『咲夜，我又来啦~看看有什么好东西』",
                            CharecterColor.Reimu
                        ),
                new talkPiece((int) ShowName.Right, "『请过目』",OpenShopSakuya),
                new talkPiece("reimu_3","sakuya_1",(int) ShowName.Left, "『说起来为什么我的符卡会在红魔馆有售？』"),
                new talkPiece("reimu_3","sakuya_5",(int) ShowName.Right, "『…………………………』"),
                new talkPiece("reimu_3","sakuya_5",(int) ShowName.Right, "『这是商业机密』"),
                new talkPiece("reimu_3","sakuya_5",(int) ShowName.Left, "『请至少不要一脸自豪！』"),
            },
            // 9
            new talkPiece[]{
                new talkPiece(
                            "不显示",
                            "不显示",
                            " ",
                            " ",
                            CharecterColor.Reimu,
                            CharecterColor.Sakuya,
                            (int)ShowName.Left,
                            "某日——幻想乡——",
                            CharecterColor.Active
                        ),
                        new talkPiece(
                            "不显示",
                            "不显示",
                            " ",
                            " ",
                            CharecterColor.Reimu,
                            CharecterColor.Sakuya,
                            (int)ShowName.Left,
                            "红魔馆内",
                            CharecterColor.Active
                        ),
                        new talkPiece(
                            "reimu_0",
                            "不显示",
                            "博丽灵梦",
                            "",
                            CharecterColor.Reimu,
                            CharecterColor.Sakuya,
                            (int)ShowName.Left,
                            "『嗯…又是新的异变呢…』",
                            CharecterColor.Reimu
                        ),
                        new talkPiece(
                            "reimu_0",
                            "不显示",
                            "博丽灵梦",
                            "",
                            CharecterColor.Reimu,
                            CharecterColor.Sakuya,
                            (int)ShowName.Left,
                            "『不过这次异变真是了不得呢…』",
                            CharecterColor.Reimu
                        ),
                        new talkPiece(
                            "reimu_0",
                            "不显示",
                            "博丽灵梦",
                            "",
                            CharecterColor.Reimu,
                            CharecterColor.Sakuya,
                            (int)ShowName.Left,
                            "『总之先问一下红魔馆的大家，了解一下情况吧』",
                            CharecterColor.Reimu
                        ),
            },
            // 10
            new talkPiece[]{
                new talkPiece(
                            "reimu_0",
                            "flandre_12",
                            "博丽灵梦",
                            "芙兰朵露·斯卡雷特",
                            CharecterColor.Reimu,
                            CharecterColor.Flandre,
                            (int)ShowName.Right,
                            "『是神社的巫女！一起来玩吧！』",
                            CharecterColor.Flandre
                        ),
                new talkPiece("reimu_10","flandre_12",(int) ShowName.Left, "『呃……你看今天外面阳光那么好~我们一起出去玩弹幕游戏吧』"),
                new talkPiece((int) ShowName.Right, "『好啊好啊！芙兰也想出去玩』"),
                new talkPiece((int) ShowName.Left, "『（耶！骗到新的战斗力！）』"),
                new talkPiece((int) ShowName.Right, "『不过姐姐不让芙兰出门呢…』"),
                new talkPiece("reimu_3","flandre_1",(int) ShowName.Left, "『（那个贵族大小姐在搞什么名堂……）』"),
                new talkPiece((int) ShowName.Right, "『很遗憾喔…』"),
            },
            // 11
            new talkPiece[]{
                new talkPiece(
                            "reimu_0",
                            "flandre_12",
                            "博丽灵梦",
                            "芙兰朵露·斯卡雷特",
                            CharecterColor.Reimu,
                            CharecterColor.Flandre,
                            (int)ShowName.Left,
                            "『芙兰在这里做什么呢』",
                            CharecterColor.Reimu
                        ),
                new talkPiece((int) ShowName.Right, "『芙兰在玩“捏碎方便面里的面饼”的游戏哦』"),
                new talkPiece("reimu_3","flandre_12",(int) ShowName.Left, "『……（还是不要管她了吧）』",Talk_1),
            },
            // 12
            new talkPiece[]{
                new talkPiece(
                            "reimu_0",
                            "patchouli_1",
                            "博丽灵梦",
                            "帕秋莉·诺雷姬",
                            CharecterColor.Reimu,
                            CharecterColor.Patchouli,
                            (int)ShowName.Right,
                            "『……要出发了吗』",
                            CharecterColor.Patchouli
                        ),
                new talkPiece("reimu_0","patchouli_0",(int) ShowName.Left, "『嗯』"),
                new talkPiece("reimu_0","patchouli_0",(int) ShowName.Right, "『……那么……武运昌盛』"),
                new talkPiece("reimu_12","patchouli_0",(int) ShowName.Left, "『不要给我立FLAG，好恐怖的』"),
                // new talkPiece((int) ShowName.Left, "『不不~妖怪退治也不是那么危险的工作啦~』"),
                // new talkPiece((int) ShowName.Right, "『对了，等一下……』"),
                // new talkPiece((int) ShowName.Left, "『什么事？』"),
                // new talkPiece((int) ShowName.Right, "『鼠标滑轮和键盘上面的数字键可以切换背包栏，如果有玩过《泰拉○亚》的话应该很容易上手……』"),
                // new talkPiece((int) ShowName.Left, "『直接说出来别的游戏的名字没有问题吗……』"),
                // new talkPiece((int) ShowName.Right, "『之后TAB键是打开合成面板，之后鼠标左键右键鼠标点击物品都可以自己试一下……』"),
                // new talkPiece((int) ShowName.Right, "『Q键是丢弃物品』"),
                // new talkPiece((int) ShowName.Right, "『此外鼠标也可以控制子弹或者道具信息之类的东西……都是很常规的按键……』"),
                // new talkPiece((int) ShowName.Left, "『合成物品是有什么合成配方吗？』"),
                // new talkPiece((int) ShowName.Right, "『目前配方主要是符卡类，像是“起点”和一些材料都可以合成符卡，之后和「信仰心增加祈愿之仪」合成就可以升级符卡。』"),
                // new talkPiece((int) ShowName.Right, "『消耗品的配方则是“蘑菇”和“森之蘑菇”，“面包”和“炒面”。两种蘑菇可以在咲夜那里购买，可以尝试一下。』"),
                // new talkPiece((int) ShowName.Left, "『谢谢你啦，帕琪』"),
            },
            // 13
            new talkPiece[]{
                new talkPiece(
                                "reimu_3",
                                "meilin_0",
                                "博丽灵梦",
                                "红美铃",
                            CharecterColor.Reimu,
                            CharecterColor.Meirin,
                            (int)ShowName.Right,
                                "『怎么一脸刚宿醉过的样子呢』",
                            CharecterColor.Meirin),
                new talkPiece((int)ShowName.Left, "『唔~~~，可恶哇~~###』"),
                new talkPiece("reimu_7","meilin_0",(int)ShowName.Left, "『兜里的P点也减少了！』"),
                new talkPiece((int)ShowName.Right, "『哦，那个啊，被击坠之后会损失很多的P点喔，要当心呢』"),
                new talkPiece("reimu_3","meilin_0",(int)ShowName.Left, "『这个没有办法注意吧……』",Talk_4),
            },
            // 14
            new talkPiece[]{
                new talkPiece(
                            "reimu_8",
                            "remilia_0",
                            "博丽灵梦",
                            "蕾米莉亚·斯卡雷特",
                            CharecterColor.Reimu,
                            CharecterColor.Reimilia,
                            (int)ShowName.Right,
                            "『哟，灵梦，你回来了啊』",
                            CharecterColor.Reimilia
                        ),
                new talkPiece((int) ShowName.Right, "『外面情况怎么样』"),
                new talkPiece((int) ShowName.Left, "『外面人也太多了吧！！！！！』"),
                                new talkPiece(
                            "reimu_3",
                            "remilia_7",
                            "博丽灵梦",
                            "蕾米莉亚·斯卡雷特",
                            CharecterColor.Reimu,
                            CharecterColor.Reimilia,
                            (int)ShowName.Right,
                            "『是吧是吧~』",
                            CharecterColor.Reimilia
                        ),
                new talkPiece("reimu_1","remilia_0",(int) ShowName.Left, "『哎，言归正传。那，你有什么解决方案吗』"),
                new talkPiece((int) ShowName.Right, "『去白玉楼看一下吧。这么多打不完的笨蛋们蜂拥而至，冥界那边肯定是出了什么变故』"),
                new talkPiece((int) ShowName.Left, "『说得也有道理……只怕这路上人是不少』"),
                new talkPiece((int) ShowName.Right, "『那就要你自己想办法咯，还记得白玉楼的路怎么走吧。』"),
                new talkPiece("reimu_3","remilia_0",(int) ShowName.Left, "『幻想乡的路我可比你这个足不出户的大小姐熟悉得多了——』"),
                new talkPiece((int) ShowName.Right, "『记得替我向幽幽子问个好』"),
                new talkPiece("reimu_3","remilia_0",(int) ShowName.Left, "『自己去。』"),

            },
            // 15
            new talkPiece[]{
                new talkPiece(
                            "reimu_1",
                            "remilia_7",
                            "博丽灵梦",
                            "蕾米莉亚·斯卡雷特",
                            CharecterColor.Reimu,
                            CharecterColor.Reimilia,
                            (int)ShowName.Right,
                            "『还有什么事吗，没事就不要打扰我喝茶了（闭眼）』",
                            CharecterColor.Reimilia
                        ),
                new talkPiece("reimu_3","remilia_0",(int) ShowName.Left, "『你也真是悠哉……』"),
            },
            // 16
            new talkPiece[]{
                new talkPiece(
                            "reimu_0",
                            "flandre_12",
                            "博丽灵梦",
                            "芙兰朵露·斯卡雷特",
                            CharecterColor.Reimu,
                            CharecterColor.Flandre,
                            (int)ShowName.Left,
                            "『芙兰在这里做什么呢』",
                            CharecterColor.Reimu
                        ),
                new talkPiece((int) ShowName.Right, "『芙兰在玩“用铅笔卷磁带”的游戏哦』"),
                new talkPiece("reimu_3","flandre_12",(int) ShowName.Left, "『……（还是不要管她了吧）』",Talk_1),
            },
            //17 已废弃
            new talkPiece[]{
                new talkPiece(
                            "reimu_1",
                            "sakuya_1",
                            "博丽灵梦",
                            "十六夜咲夜",
                            CharecterColor.Reimu,
                            CharecterColor.Sakuya,
                            (int)ShowName.Right,
                            "『灵梦小姐，红魔馆的部分武器库已经整理好了，请过目』",
                            CharecterColor.Sakuya
                        ),
                new talkPiece((int) ShowName.Left, "『终于好了吗！（两眼发光），快让我看看』"),
                                                new talkPiece(
                            "不显示",
                            "不显示",
                            " ",
                            " ",
                            CharecterColor.Reimu,
                            CharecterColor.Sakuya,
                            (int)ShowName.Left,
                            "道具店系统（假的，还没做）",
                            CharecterColor.Active

                        ),
                new talkPiece(
                            "reimu_1",
                            "sakuya_1",
                            "博丽灵梦",
                            "十六夜咲夜",
                            CharecterColor.Reimu,
                            CharecterColor.Sakuya,
                            (int)ShowName.Left,
                            "『怎么感觉东西有点少……（嫌弃）』",
                            CharecterColor.Reimu
                        ),
                new talkPiece((int) ShowName.Right, "『大小姐吩咐过了，有些道具威力过于强大，还是要等您的实力进一步提升之后，才可以开放出售……』"),
                new talkPiece((int) ShowName.Left, "『可恶！那个混……』"),
                new talkPiece((int) ShowName.Right, "『（瞪）嗯？混什么』"),
                new talkPiece((int) ShowName.Left, "『混……浑身上下充满威严气息的蕾咪（微笑）』"),
                new talkPiece((int) ShowName.Right, "『嗯嗯（点头）』"),

            },
            // 18
            new talkPiece[]{
                new talkPiece(
                            "reimu_0",
                            "meilin_0",
                            "博丽灵梦",
                            "红美铃",
                CharecterColor.Reimu,
                CharecterColor.Meirin,
                (int)ShowName.Left,
                            "『你个门卫至少去守一下大门，帮我减少一下打怪压力呗……』",
                CharecterColor.Reimu),
                new talkPiece(
                                "reimu_0",
                                "meilin_3",
                                "博丽灵梦",
                                "红美铃",
                    CharecterColor.Reimu,
                    CharecterColor.Meirin,
                    (int)ShowName.Right,
                                "『不要。（斩钉截铁）』",
                    CharecterColor.Meirin
                ),
                new talkPiece(
                                "reimu_0",
                                "meilin_0",
                                "博丽灵梦",
                                "红美铃",
                    CharecterColor.Reimu,
                    CharecterColor.Meirin,
                    (int)ShowName.Right,
                                "『对了，灵梦小姐，你吃面包吗，分你一个。』",
                    CharecterColor.Meirin
                ),
                new talkPiece("reimu_10","meilin_0",(int) ShowName.Left, "『好~谢谢~(开心)』",AddItem_Loaf)
            },
            // 19 
            new talkPiece[]{
                new talkPiece(
                            "reimu_0",
                            "meilin_0",
                            "博丽灵梦",
                            "红美铃",
                CharecterColor.Reimu,
                CharecterColor.Meirin,
                (int)ShowName.Left,
                            "『作为红魔馆的看门担当，这场异变你有什么头绪吗？』",
                CharecterColor.Reimu),
                new talkPiece(
                                "reimu_0",
                                "meilin_3",
                                "博丽灵梦",
                                "红美铃",
                    CharecterColor.Reimu,
                    CharecterColor.Meirin,
                    (int)ShowName.Right,
                                "『你来问我是不是有点问道于盲了……』",
                    CharecterColor.Meirin
                ),
            },
            // 20
            new talkPiece[]{
                new talkPiece(
                            "reimu_0",
                            "patchouli_0",
                            "博丽灵梦",
                            "帕秋莉·诺雷姬",
                            CharecterColor.Reimu,
                            CharecterColor.Patchouli,
                            (int)ShowName.Right,
                            "『蕾咪在找你喔』",
                            CharecterColor.Patchouli
                        ),
                new talkPiece((int) ShowName.Right, "『需要我帮你在这个小屏幕里指路吗，就在美铃旁边的传送点。』"),
                new talkPiece("reimu_3","patchouli_0",(int) ShowName.Left, "『好啦好啦我知道啦』",Talk_3),
            },
            // 21
            new talkPiece[]{
                new talkPiece(
                            "reimu_3",
                            "patchouli_0",
                            "博丽灵梦",
                            "帕秋莉·诺雷姬",
                            CharecterColor.Reimu,
                            CharecterColor.Patchouli,
                            (int)ShowName.Left,
                            "『说起来除了那两个符卡还算有点创意，剩下的宝珠简直就像劣质抽卡手游徒增无意义数值时随手想的名字呢』",
                            CharecterColor.Reimu
                        ),
                new talkPiece((int) ShowName.Right, "『我也这么觉得呢。』",OpenShopPatchouli),
            },
            // 22
            new talkPiece[]{
                new talkPiece(
                            "reimu_0",
                            "patchouli_0",
                            "博丽灵梦",
                            "帕秋莉·诺雷姬",
                            CharecterColor.Reimu,
                            CharecterColor.Patchouli,
                            (int)ShowName.Left,
                            "『帕琪，听说你这边在研究什么吗？』 ",
                            CharecterColor.Reimu
                        ),
                new talkPiece(
                    "reimu_0",
                    "patchouli_1",
                    "",
                    "",
                    CharecterColor.Reimu,
                    CharecterColor.Patchouli,
                    (int)ShowName.Right,
                    "『角色升级，是永久的喔……』",
                    CharecterColor.Patchouli
                ),
                new talkPiece((int) ShowName.Right, "『不过升级材料都很珍贵呢，我希望收取超级P点作为报酬呢~』"),
                new talkPiece((int) ShowName.Left, "『那又是什么』"),
                new talkPiece((int) ShowName.Right, "『看到屏幕左上角的P点了吗，红色的是普通P点，紫色的是超级P点』"),
                new talkPiece((int) ShowName.Right, "『超级P点只有在每一次探索结束之后，才能获得』"),
                new talkPiece((int) ShowName.Right, "『击败的敌人越多，获得的经验P点越多，坚持的时间越长，那么结算获得的超级P点就会越多』"),
                new talkPiece("reimu_3","patchouli_0",(int) ShowName.Left, "『也就是说是肉鸽游戏里的局外成长吧。』"),
                new talkPiece((int) ShowName.Left, "『正好有20超级P点，看一下买什么吧』"),
                new talkPiece((int) ShowName.Right, "『推荐买背包扩充喔，背包超级紧缺的』",OpenShopPatchouli),
            },
            // 23
            new talkPiece[]{
                new talkPiece(
                            "reimu_0",
                            "patchouli_0",
                            "博丽灵梦",
                            "帕秋莉·诺雷姬",
                            CharecterColor.Reimu,
                            CharecterColor.Patchouli,
                            (int)ShowName.Left,
                            "『帕琪，我来了，看一下升级』 ",
                            CharecterColor.Reimu
                        ),
                new talkPiece((int) ShowName.Right, "『好喔』",OpenShopPatchouli),
            },
            // 24
            new talkPiece[]{
                new talkPiece(
                            "reimu_0",
                            "patchouli_1",
                            "博丽灵梦",
                            "帕秋莉·诺雷姬",
                            CharecterColor.Reimu,
                            CharecterColor.Patchouli,
                            (int)ShowName.Right,
                            "『……要出发了吗』",
                            CharecterColor.Patchouli
                        ),
                new talkPiece((int) ShowName.Left, "『嗯』"),
                new talkPiece((int) ShowName.Right, "『似乎已经在慧音老师那里接受过训练了……那么……祝你武运昌盛』"),
                new talkPiece((int) ShowName.Left, "『谢谢你啦，帕琪』"),
            },
            // 25
            new talkPiece[]{
                new talkPiece(
                            "reimu_1",
                            "marisa_10",
                            "博丽灵梦",
                            "雾雨魔理沙",
                            CharecterColor.Reimu,
                            CharecterColor.Marisa,
                            (int)ShowName.Right,
                            "『哎呀~~~！好久不见！这不是灵梦吗！』",
                            CharecterColor.Marisa
                        ),
                new talkPiece("reimu_3","marisa_10",(int) ShowName.Left, "『没记错的话，你刚才还对着我疯狂丢星星弹幕来着』"),
                new talkPiece("reimu_3","marisa_10",(int) ShowName.Right, "『是吗？那还真是奇妙呢~』"),
                new talkPiece("reimu_10","marisa_10",(int) ShowName.Left, "『是阿尔兹海默症的症状呢，需要我去永远亭帮你挂个号吗？还是说现在就帮你物理治疗一下？』"),
                new talkPiece((int) ShowName.Right, "『哎呀你这人真是的，哈哈哈哈哈~』"),
                new talkPiece("reimu_3","marisa_1",(int) ShowName.Left, "『关于黑幕有没有什么头绪，快说。』"),
                new talkPiece((int) ShowName.Right, "『咱也不太清楚呢其实』"),
                new talkPiece((int) ShowName.Left, "『嗯，嫌疑人没有任何理由，对路过解决异变的巫女大打出手。』"),
                new talkPiece("reimu_3","marisa_10",(int) ShowName.Right, "『哎呀~好像是这样的呢DA☆ZE。』"),
                new talkPiece((int) ShowName.Left, "『以上就是犯人的全部供述了，将于择日执行死刑。』"),
            },
            // 26
            new talkPiece[]{
                new talkPiece(
                            "reimu_0",
                            "marisa_0",
                            "博丽灵梦",
                            "雾雨魔理沙",
                            CharecterColor.Reimu,
                            CharecterColor.Marisa,
                            (int)ShowName.Left,
                            "『对了，魔理沙，一起出去调查异变吧。』",
                            CharecterColor.Reimu
                        ),
                new talkPiece("reimu_3","marisa_10",(int) ShowName.Right, "『想和我成为那种关系的意思吗？当然可以咯！』"),
                new talkPiece("reimu_3","marisa_10",(int) ShowName.Left, "『明明只是调查异变的组合关系。不要用那种误会的说法。』"),
                new talkPiece("reimu_3","marisa_0",(int) ShowName.Right, "『那个组合不是要人类和妖怪才能组队的吗？』"),
                new talkPiece("reimu_3","marisa_10",(int) ShowName.Left, "『无所谓了，快来搭把手。』"),
                new talkPiece(
                            "不显示",
                            "不显示",
                            "系统",
                            " ",
                            CharecterColor.Reimu,
                            CharecterColor.Sakuya,
                            (int)ShowName.Left,
                            "『魔理沙加入了队伍。』",
                            CharecterColor.Active
                        ),
                new talkPiece(
                            "reimu_0",
                            "marisa_10",
                            "博丽灵梦",
                            "雾雨魔理沙",
                            CharecterColor.Reimu,
                            CharecterColor.Marisa,
                            (int)ShowName.Right,
                            "『我会在你身后慢悠悠地丢星星弹幕的。』",
                            CharecterColor.Marisa
                        ),
                new talkPiece("reimu_3","marisa_10",(int) ShowName.Left, "『如果可以的话我希望你能放几个魔炮出来』"),
                new talkPiece((int) ShowName.Right, "『很累的，不要。』"),
                new talkPiece("reimu_3","marisa_22",(int) ShowName.Right, "『以后好感度高了说不定会解锁喔~』"),
                new talkPiece((int) ShowName.Left, "『那你还是丢星星弹幕吧。』",AddMarisa),
            },
            // 27
            new talkPiece[]{
                new talkPiece(
                            "reimu_0",
                            "patchouli_1",
                            "博丽灵梦",
                            "帕秋莉·诺雷姬",
                            CharecterColor.Reimu,
                            CharecterColor.Patchouli,
                            (int)ShowName.Right,
                            "『……红魔馆又进老鼠了呢，得尽快联系咲夜呢』",
                            CharecterColor.Patchouli
                        ),
                new talkPiece((int) ShowName.Left, "『是呢。』"),
                new talkPiece(
                        "reimu_1",
                        "marisa_8",
                        "博丽灵梦",
                        "雾雨魔理沙",
                        CharecterColor.Reimu,
                        CharecterColor.Marisa,
                        (int)ShowName.Right,
                        "『不是！为什么灵梦你也在帮腔！你到底是哪边的？』",
                        CharecterColor.Marisa
                    ),
                new talkPiece((int) ShowName.Left, "『永远在你的对立边。』"),
                new talkPiece(
                            "patchouli_0",
                            "marisa_8",
                            "帕秋莉·诺雷姬",
                            "雾雨魔理沙",
                            CharecterColor.Patchouli,
                            CharecterColor.Marisa,
                            (int)ShowName.Left,
                            "『……因为是事实呢』",
                            CharecterColor.Patchouli
                ),
                new talkPiece(
                            "reimu_1",
                            "marisa_8",
                            "博丽灵梦",
                            "雾雨魔理沙",
                            CharecterColor.Reimu,
                            CharecterColor.Marisa,
                            (int)ShowName.Left,
                            "『是事实呢』",
                            CharecterColor.Reimu
                ),
            },
            // 28
            new talkPiece[]{
                new talkPiece(
                            "marisa_0",
                            "meilin_0",
                            "雾雨魔理沙",
                            "红美铃",
                CharecterColor.Marisa,
                CharecterColor.Meirin,
                (int)ShowName.Right,
                            "『看到了从正门进来的魔理沙，看来今天的运势可真不得了呢……』",
                CharecterColor.Meirin),
                new talkPiece("marisa_10","meilin_0",(int) ShowName.Left, "『占卜书里说看到“从正门进来的魔理沙”要赶紧睡觉，不然会有坏事发生喔』"),
                new talkPiece((int) ShowName.Right, "『你是凶兆的黑猫吗？』"),
            },
            // 29
            new talkPiece[]{
                new talkPiece(
                            "reimu_1",
                            "marisa_1",
                            "博丽灵梦",
                            "雾雨魔理沙",
                            CharecterColor.Reimu,
                            CharecterColor.Marisa,
                            (int)ShowName.Left,
                            "『魔理沙啊……』",
                            CharecterColor.Reimu
                        ),
                new talkPiece("reimu_1","marisa_8",(int) ShowName.Right, "『怎么…………哎哟！掐我脸干什么！』"),
                new talkPiece((int) ShowName.Left, "『因为我看你根本没血条。只是确认一下你是不是已经死掉了。』"),
                new talkPiece((int) ShowName.Left, "『经常有那种好友的亡灵缠在身上的鬼故事呢。』"),
                new talkPiece((int) ShowName.Right, "『（怒）判定点不是在你身上吗！我怎么会有血条嘛！』"),
                new talkPiece((int) ShowName.Right, "『没有玩过永夜抄吗？！』"),
                new talkPiece((int) ShowName.Left, "『说的也是啊，那这事儿就算了吧。』"),
                new talkPiece((int) ShowName.Right, "『你这红白！刚才掐我的账怎么算！』"),
                new talkPiece("reimu_4","marisa_8",(int) ShowName.Left, "『那你也掐回来就算摆平了？』"),
                new talkPiece("reimu_4","marisa_15",(int) ShowName.Right, "『……』"),
                new talkPiece((int) ShowName.Left, "『……』"),
                new talkPiece(
                            "reimu_1",
                            "meilin_3",
                            "博丽灵梦",
                            "红美铃",
                            CharecterColor.Reimu,
                            CharecterColor.Meirin,
                            (int)ShowName.Right,
                            "『……』",
                            CharecterColor.Meirin
                ),
                new talkPiece(
                            "reimu_1",
                            "meilin_3",
                            "博丽灵梦",
                            "红美铃",
                            CharecterColor.Reimu,
                            CharecterColor.Meirin,
                            (int)ShowName.Right,
                            "『呃……我只是在这里守门而已，请不要在意我。』",
                            CharecterColor.Meirin
                ),
                new talkPiece(
                            "reimu_6",
                            "marisa_15",
                            "博丽灵梦",
                            "雾雨魔理沙",
                            CharecterColor.Reimu,
                            CharecterColor.Marisa,
                            (int)ShowName.Right,
                            "『还是算了吧……』",
                            CharecterColor.Marisa
                        ),
            },
            // 30 
            new talkPiece[]{
                new talkPiece(
                            "marisa_1",
                            "flandre_12",
                            "雾雨魔理沙",
                            "芙兰朵露·斯卡雷特",
                            CharecterColor.Marisa,
                            CharecterColor.Flandre,
                            (int)ShowName.Right,
                            "『是森林的小偷！一起来玩吧！』",
                            CharecterColor.Flandre
                        ),
                new talkPiece("marisa_8","flandre_12",(int) ShowName.Left, "『才不是咧！！』"),
                new talkPiece(
                            "marisa_8",
                            "reimu_4",
                            "雾雨魔理沙",
                            "博丽灵梦",
                            CharecterColor.Marisa,
                            CharecterColor.Reimu,
                            (int)ShowName.Right,
                            "『抱歉问一下，红魔馆所有人对你的好感度都是负数吗』",
                            CharecterColor.Reimu
                        ),
            },
            // 31
            new talkPiece[]{
                new talkPiece(
                            "remilia_0",
                            "marisa_0",
                            "蕾米莉亚·斯卡雷特",
                            "雾雨魔理沙",
                CharecterColor.Reimilia,
                CharecterColor.Marisa,
                (int)ShowName.Left,
                            "『哟，这不是红魔馆的偷书大盗吗？帕琪经常来抱怨你的事，怎么，今天过来是想来打一架吗？』",
                CharecterColor.Reimilia),
                new talkPiece("remilia_0","marisa_10",(int) ShowName.Right, "『哎呀，帕秋莉那家伙真会添油加醋，哪有那么严重。』"),
                new talkPiece("remilia_0","marisa_10",(int) ShowName.Right, "『咱只不过是借书而已啦，借书！』"),
                new talkPiece("remilia_0","marisa_10",(int) ShowName.Right, "『魔法使的事儿，窃书不算偷！』"),
                new talkPiece("remilia_8","marisa_0",(int) ShowName.Left, "『哦？那你倒是说说，你从我家书库借的书什么时候还？』"),
                new talkPiece("remilia_8","marisa_0",(int) ShowName.Right, "『嗯……』"),
                new talkPiece("reimu_12","marisa_0","博丽灵梦","雾雨魔理沙",
                CharecterColor.Reimu,
                CharecterColor.Marisa,
                (int)ShowName.Left,
                            "『（小声）喂，魔理沙，我可不想在这种时候进入头目战，谨言慎行啊』",
                CharecterColor.Reimu),
                new talkPiece("reimu_3","marisa_8",(int) ShowName.Right, "『（小声）咱的性格你还不了解吗，你说什么都不会管用的★ZE』"),
                new talkPiece( "remilia_0","marisa_0","蕾米莉亚·斯卡雷特","雾雨魔理沙",
                CharecterColor.Reimilia,
                CharecterColor.Marisa,
                (int)ShowName.Left,
                            "『所以？什么时候还书？』",
                CharecterColor.Reimilia),
                new talkPiece("remilia_0","marisa_6",(int) ShowName.Right, "『反正咱人类的寿命那么短，等咱死了你们再把东西收回去就好了★ZE』"),
                new talkPiece("remilia_8","marisa_6",(int) ShowName.Left, "『……』"),
                new talkPiece("remilia_7","marisa_6",(int) ShowName.Left, "『哈哈！有意思！也好！』"),
                new talkPiece("remilia_7","marisa_6",(int) ShowName.Left, "『那就让我满足你这一介人类的任性吧！』"),
                new talkPiece("remilia_7","marisa_6",(int) ShowName.Left, "『偷书的事情我就既往不咎了！』"),
                new talkPiece("remilia_7", "patchouli_4", "蕾米莉亚·斯卡雷特", "帕秋莉·诺雷姬",
                CharecterColor.Reimilia,
                CharecterColor.Patchouli,
                (int)ShowName.Right,
                            "『这……蕾米莉亚大人……』",
                CharecterColor.Patchouli),
                new talkPiece("remilia_7","patchouli_4",(int) ShowName.Left, "『帕琪，反倒是你要多注意点喔，不要太钟情了。』"),
                new talkPiece("remilia_7","patchouli_4",(int) ShowName.Left, "『到时候去回收书的时候可不要哭鼻子喔？』"),
                new talkPiece("remilia_7","patchouli_15",(int) ShowName.Right, "『蕾、蕾米，你在说些什么啊？！』"),
                new talkPiece("remilia_7","不显示","旁白","",
                CharecterColor.Reimilia,
                CharecterColor.Patchouli,
                (int)ShowName.Right,
                            "帕秋莉气鼓鼓地离开了。",
                CharecterColor.Active ),
                new talkPiece("remilia_0","marisa_0","蕾米莉亚·斯卡雷特","雾雨魔理沙",
                CharecterColor.Reimilia,
                CharecterColor.Marisa,
                (int)ShowName.Left,
                            "『那孩子相当中意你的。记得有空常来玩喔。』",
                CharecterColor.Reimilia),
                new talkPiece("remilia_0","marisa_1",(int) ShowName.Right, "『欸？什么意思？』"),
                new talkPiece("reimu_3","marisa_0","博丽灵梦","雾雨魔理沙",
                CharecterColor.Reimu,
                CharecterColor.Marisa,
                (int)ShowName.Left,
                            "『别装傻！』",
                CharecterColor.Reimu),
                new talkPiece("reimu_3","marisa_0",(int) ShowName.Left, "（给了魔理沙一拳）",PlaySFX_REIMI1),

            },

        };
    }
    public void OpenShopSakuya()
    {
        UIControl.instance.OpenShop();
        ShopControl.instance.LoadShop(ShopControl.instance.SakuyaShop);
    }

    public void OpenShopPatchouli()
    {
        UIControl.instance.OpenShop();
        ShopControl.instance.LoadShop(ShopControl.instance.PatchouliShop);
    }

    public void AddItem_Loaf()
    {
        AssetControl.instance.DropItem(PlayerHealthControl.instance.transform.position, ItemControl.instance.itemGuide[10]);
    }

    public void AddItem_Sakuya()
    {
        for (int i = 0; i < 6; i++)
            AssetControl.instance.DropItem(PlayerHealthControl.instance.transform.position, ItemControl.instance.itemGuide[3]);

    }
    public void PlaySFX_REIMI1()
    {
        SFXManger.instance.PlaySFX(12);
    }
    // 魔理沙入队事件
    public void AddMarisa()
    {
        GlobalControl.instance.teammate = 1;
        // 寻找存在InteractiveMarisa的脚本的对象，并设置消失
        InteractiveMarisa marisa = FindObjectOfType<InteractiveMarisa>();
        if (marisa != null)
        {
            // 设置游戏对象的活动状态为false
            marisa.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("No game object with InteractiveMarisa script found");
        }

        TeamMateControl.instance.ActiveTeamMate(0);
    }
    // 额外对话
    public void Talk_1()
    {
        BubbleTalkControl.instance.ShowBubbleTalkButWaitingFormouse(new BubbleTalkData("reimu_small_0", "『什么时候把这熊孩子请回地下室……』"));
    }
    // public void Talk_2()
    // {
    //     BubbleTalkControl.instance.ShowBubbleTalkButWaitingFormouse(new BubbleTalkData("reimu_small_0", "『等我解决完这场异变，我就回老家和暖炉结婚』"));
    // }
    public void Talk_3()
    {
        BubbleTalkControl.instance.ShowBubbleTalkButWaitingFormouse(new BubbleTalkData("reimu_small_0", "『拜访一下蕾咪吧。』"));
        // GlobalControl.instance.AddHasReadDialog(0, 4);

    }
    public void Talk_4()
    {
        BubbleTalkControl.instance.ShowBubbleTalkButWaitingFormouse(new BubbleTalkData("reimu_small_0", "『希望下次不要从这个水池子里复活爬出来……』"));
    }
    public void Talk_5()
    {
        BubbleTalkControl.instance.ShowBubbleTalkButWaitingFormouse(new BubbleTalkData("reimu_small_0", "『前途多灾多难啊……』"));
    }
}