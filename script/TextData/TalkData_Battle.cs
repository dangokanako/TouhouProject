using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public class TalkData_Battle : TalkData
{
    public static TalkData_Battle instance;
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
                            "reimu_1",
                            "yukari_0",
                            "博丽灵梦",
                            "八云紫",
                            CharecterColor.Reimu,
                            CharecterColor.Yukari,
                            (int)ShowName.Right,
                            "『哟，灵梦，你来了呀。』 ",
                            CharecterColor.Yukari
                        ),
                new talkPiece((int) ShowName.Left, "『紫，你在这装什么傻。要不是你在我面前开了个间隙，我怎么会来你家？』"),
                new talkPiece("reimu_1","yukari_10",(int) ShowName.Right, "『哈哈~』"),
                new talkPiece("reimu_1","yukari_0",(int) ShowName.Right, "『难得来一趟，不坐一坐吗？』"),
                new talkPiece("reimu_3","yukari_0",(int) ShowName.Left, "『不了，我还忙着去解决异变呢。不是所有人都跟你一样无所事事游手好闲的。』"),
                new talkPiece("reimu_3","yukari_5",(int) ShowName.Right, "『讨厌啦，怎么跟长辈说话的呢，现在年轻人真是的。』"),
                new talkPiece("reimu_1","yukari_5",(int) ShowName.Left, "『…………帮你去永远亭抓点药？』"),
                new talkPiece("reimu_1","yukari_0",(int) ShowName.Right, "『还是说点正事吧。』"),
                new talkPiece("reimu_1","yukari_0",(int) ShowName.Left, "『好，请直接告诉我异变的幕后黑手。』"),
                new talkPiece("reimu_1","yukari_0",(int) ShowName.Right, "『那种事情要自己去找才有意思啦。』"),
                new talkPiece("reimu_1","yukari_0",(int) ShowName.Right, "『自己亲手揪出犯人不是很有意思吗？』"),
                new talkPiece("reimu_3","yukari_0",(int) ShowName.Left, "『红魔馆家的摸鱼门卫都比你有用。』"),
                new talkPiece("reimu_3","yukari_0",(int) ShowName.Left, "『说到底关于异变你有什么头绪吗，不要再说谜语了。』"),
                new talkPiece("reimu_3","yukari_6",(int) ShowName.Right, "『……反正按照这个步调去调查吧，没事出门走走，不是挺好的吗？』"),
                new talkPiece("reimu_3","yukari_6",(int) ShowName.Left, "『我就知道来你这是白费口舌。』"),
                new talkPiece("reimu_3","yukari_0",(int) ShowName.Right, "『好啦，给你一点小礼物啦，从三个里选一个吧。』"),
                new talkPiece("reimu_3","yukari_0",(int) ShowName.Left, "『为什么只能选一个？』"),
                new talkPiece("reimu_3","yukari_0",(int) ShowName.Right, "『这可是外面世界最流行最时髦的选择方式喔。此外卖一点小福袋，可以开出一点小礼物。』"),
                new talkPiece("reimu_3","yukari_0",(int) ShowName.Left, "『怎么还福袋要收费？你还会缺钱？随便变点不就行了吗？』"),
                new talkPiece("reimu_3","yukari_6",(int) ShowName.Right, "『直接印钱会导致经济学上的严重问题喔，钱流通起来才对幻想乡是好事。』"),
                new talkPiece("reimu_1","yukari_6",(int) ShowName.Left, "『那个什么经济学能解决我家神社的财政问题吗？』"),
                new talkPiece("reimu_1","yukari_0",(int) ShowName.Right, "『我想博丽神社应该是更加深层次的问题。』",OpenYukariPlane),
            },
            // 1
            new talkPiece[]{
                new talkPiece(
                            "reimu_0",
                            "yukari_0",
                            "博丽灵梦",
                            "八云紫",
                            CharecterColor.Reimu,
                            CharecterColor.Yukari,
                            (int)ShowName.Left,
                            "『哦对了，那个，出口的门怎么走？』 ",
                            CharecterColor.Reimu
                        ),
                new talkPiece((int) ShowName.Right, "『你看我需要门吗？点下面那个按钮就可以啦。』"),
                new talkPiece((int) ShowName.Right, "『记得常来喔。』"),
                new talkPiece("reimu_1","yukari_10",(int) ShowName.Left, "『好，有空会来关爱空巢老人的。』"),
            },
            // 2
            new talkPiece[]{
                new talkPiece(
                            "reimu_0",
                            "yukari_0",
                            "博丽灵梦",
                            "八云紫",
                            CharecterColor.Reimu,
                            CharecterColor.Yukari,
                            (int)ShowName.Right,
                            "『记得常来喔。』 ",
                            CharecterColor.Reimu
                        ),
                new talkPiece("reimu_1","yukari_10",(int) ShowName.Left, "『好，有空会来关爱空巢老人的。』"),
            },
            // 3
            new talkPiece[]{
                new talkPiece(
                            "reimu_1",
                            "yukari_0",
                            "博丽灵梦",
                            "八云紫",
                            CharecterColor.Reimu,
                            CharecterColor.Yukari,
                            (int)ShowName.Left,
                            "『把东西交出来。』 ",
                            CharecterColor.Reimu
                        ),
                new talkPiece((int) ShowName.Right, "『哎呀，现在年轻人性情真急躁啊~』",OpenYukariPlane),
            },
            // 4
            new talkPiece[]{
                new talkPiece(
                            "reimu_1",
                            "yukari_0",
                            "博丽灵梦",
                            "八云紫",
                            CharecterColor.Reimu,
                            CharecterColor.Yukari,
                            (int)ShowName.Left,
                            "『吶，紫啊。』 ",
                            CharecterColor.Reimu
                        ),
                new talkPiece((int) ShowName.Right, "『怎么？』"),
                new talkPiece("reimu_1","yukari_0",(int) ShowName.Left, "『你不睡觉吗？印象里你每天都在睡觉。』"),
                new talkPiece("reimu_1","yukari_6",(int) ShowName.Right, "『为了你特地醒来的喔？』"),
                new talkPiece("reimu_3","yukari_6",(int) ShowName.Left, "『当我没问。』",OpenYukariPlane),
            },
            // 5
            new talkPiece[]{
                new talkPiece(
                            "reimu_0",
                            "yukari_0",
                            "博丽灵梦",
                            "八云紫",
                            CharecterColor.Reimu,
                            CharecterColor.Yukari,
                            (int)ShowName.Right,
                            "『吶，灵梦，你昨天晚上做的什么梦呢？』",
                            CharecterColor.Reimu
                        ),
                new talkPiece("reimu_0","yukari_0",(int) ShowName.Left, "『怎么突然对我嘘寒问暖的，好可怕，该不会有什么阴谋吧。』"),
                new talkPiece("reimu_1","yukari_6",(int) ShowName.Right, "『没有啦，就是随便问问，很好奇人类最近在做什么样的梦呢。』"),
                new talkPiece("reimu_0","yukari_0",(int) ShowName.Left, "『昨天的梦嘛……很普通的变成百万富翁的梦吧。』"),
                new talkPiece("reimu_1","yukari_6",(int) ShowName.Right, "『呵呵……』"),
                new talkPiece("reimu_3","yukari_0",(int) ShowName.Left, "『魂淡，笑什么，偶尔有点梦想怎么了。』"),
                new talkPiece("reimu_3","yukari_6",(int) ShowName.Right, "『没什么，只是很有灵梦风格的梦。』"),
                new talkPiece("reimu_3","yukari_0",(int) ShowName.Left, "『那你一年到头都在睡觉，都梦到什么东西了啊？』"),
                new talkPiece("reimu_3","yukari_0",(int) ShowName.Right, "『保~密~』"),
                new talkPiece("reimu_3","yukari_0",(int) ShowName.Left, "『啊，狡猾。』",OpenYukariPlane),
            },
            // 6
            new talkPiece[]{
                new talkPiece(
                            "reimu_0",
                            "nitori_0",
                            "博丽灵梦",
                            "河城荷取",
                            CharecterColor.Reimu,
                            CharecterColor.Nitori,
                            (int)ShowName.Left,
                            "『嗯？荷取？你怎么在这里？』",
                            CharecterColor.Reimu
                        ),
                new talkPiece("reimu_0","nitori_0",(int) ShowName.Right, "『早啊灵梦。听说你又要去解决异变了？』"),
                new talkPiece("reimu_0","nitori_0",(int) ShowName.Left, "『倒是有这么回事……你是怎么知道的？』"),
                new talkPiece("reimu_0","nitori_0",(int) ShowName.Right, "『山里的天狗那群家伙的消息一直很灵通的嘛。』"),
                new talkPiece("reimu_3","nitori_0",(int) ShowName.Left, "『感觉被天狗看着一举一动，总觉得有点不爽呢。』"),
                new talkPiece("reimu_3","nitori_0",(int) ShowName.Right, "『总之就是这样了，所以我来帮你升级改造一下装备。』"),
                new talkPiece("reimu_0","nitori_0",(int) ShowName.Left, "『免费的？』"),
                new talkPiece("reimu_0","nitori_1",(int) ShowName.Right, "『怎么可能嘛。哈哈。』"),
                new talkPiece("reimu_3","nitori_0",(int) ShowName.Left, "『你这个守财奴和我撞人设了，请换一个人设。』"),
                new talkPiece("reimu_3","nitori_6",(int) ShowName.Right, "『虽然都是守财奴，但我可没有你那么穷，都快家徒四壁嘞。』"),
                new talkPiece("reimu_10","nitori_6",(int) ShowName.Left, "『你这家伙！把血条给我亮出来！』"),
                new talkPiece("reimu_10","nitori_3",(int) ShowName.Right, "『哎呀呀，别冲动，别冲动！来看下商品吧，给你打个折喔（实际没有）。』"),
                new talkPiece("reimu_3","nitori_3",(int) ShowName.Left, "『（怀疑的目光）』",OpenNitoriPlane),
            },
            // 7
            new talkPiece[]{
                new talkPiece(
                            "reimu_1",
                            "nitori_0",
                            "博丽灵梦",
                            "河城荷取",
                            CharecterColor.Reimu,
                            CharecterColor.Nitori,
                            (int)ShowName.Left,
                            "『怎么感觉价格怪怪的……』",
                            CharecterColor.Reimu
                        ),
                new talkPiece("reimu_1","nitori_0",(int) ShowName.Right, "『嘛，价格的决定因素啊，也没有什么对灵梦保密的必要，就告诉你吧』"),
                new talkPiece("reimu_1","nitori_0",(int) ShowName.Right, "『装备的品质，角色的等级，重铸的次数，以及随机浮动。大概就是这些决定的了。』"),
                new talkPiece("reimu_1","nitori_0",(int) ShowName.Left, "『还是好贵啊……』"),
                new talkPiece("reimu_1","nitori_0",(int) ShowName.Right, "『那就以后加入商店打折的道具吧？』"),
                new talkPiece("reimu_4","nitori_3",(int) ShowName.Left, "『不，我想加入打折商店的道具。』"),
                new talkPiece("reimu_4","nitori_3",(int) ShowName.Right, "『喂！』"),
            },
            // 8
            new talkPiece[]{
                new talkPiece(
                            "reimu_0",
                            "nitori_0",
                            "博丽灵梦",
                            "河城荷取",
                            CharecterColor.Reimu,
                            CharecterColor.Nitori,
                            (int)ShowName.Right,
                            "『欢迎光临~』",
                            CharecterColor.Nitori
                        ),
                new talkPiece("reimu_1","nitori_0",(int) ShowName.Left, "『唔……』",OpenNitoriPlane),
            },
            // 9
            new talkPiece[]{
                new talkPiece(
                            "reimu_0",
                            "crino_13",
                            "博丽灵梦",
                            "琪露诺",
                            CharecterColor.Reimu,
                            CharecterColor.Crino,
                            (int)ShowName.Right,
                            "『喂！那边的巫女！在我家大闹什么呢！』",
                            CharecterColor.Crino
                        ),
                new talkPiece("reimu_0","crino_13",(int) ShowName.Left, "『啊，是蓝色的笨蛋。那应该是走对路了？』"),
                new talkPiece("reimu_0","crino_1",(int) ShowName.Right, "『？』"),
                new talkPiece("reimu_4","crino_1",(int) ShowName.Left, "『毕竟各种同人游戏里，第一个打琪露诺是惯例了呢。』"),
                new talkPiece("reimu_4","crino_1",(int) ShowName.Right, "『？？？什么意思』"),
                new talkPiece("reimu_4","crino_1",(int) ShowName.Left, "『以你的智商我很难跟你解释。』"),
                new talkPiece("reimu_4","crino_13",(int) ShowName.Right, "『虽然不明白，但是很不爽，那就开打吧！』"),
                new talkPiece("reimu_4","crino_13",(int) ShowName.Left, "『正合我意！』",AfterBattleTalk_Crino),
            },
            // 10
            new talkPiece[]{
                new talkPiece(
                            "reimu_0",
                            "marisa_0",
                            "博丽灵梦",
                            "雾雨魔理沙",
                            CharecterColor.Reimu,
                            CharecterColor.Marisa,
                            (int)ShowName.Left,
                            "『这不是魔理沙吗，还真是巧呢。』",
                            CharecterColor.Reimu
                        ),
                new talkPiece("reimu_0","marisa_3",(int) ShowName.Right, "『你这家伙，来到雾雨魔法店还想见到谁不成？』"),
                new talkPiece("reimu_0","marisa_3",(int) ShowName.Left, "『黑幕？』"),
                new talkPiece("reimu_0","marisa_6",(int) ShowName.Right, "『黑幕不会那么轻易找到的。你解决异变的时候，有哪一次出门就撞上黑幕过？』"),
                new talkPiece("reimu_1","marisa_0",(int) ShowName.Left, "『这倒也是呢……所以，能不能让一下，我还要解决异变』"),
                new talkPiece("reimu_1","marisa_6",(int) ShowName.Right, "『异变不会那么轻易解决的。你解决异变的时候，有哪一次让别人让开就会让开的？』"),
                new talkPiece("reimu_3","marisa_6",(int) ShowName.Left, "『啊，你这家伙，不要碍事啊！』",AfterBattleTalk_Marisa),
            },
            // 11
            new talkPiece[]{
                new talkPiece(
                            "reimu_3",
                            "marisa_6",
                            "博丽灵梦",
                            "雾雨魔理沙",
                            CharecterColor.Reimu,
                            CharecterColor.Marisa,
                            (int)ShowName.Left,
                            "『能不能让我过去。』",
                            CharecterColor.Reimu
                        ),
                new talkPiece("reimu_3","marisa_6",(int) ShowName.Right, "『不行，我可不能让连我都打不过的人去挑战黑幕。』"),
                new talkPiece("reimu_3","marisa_10",(int) ShowName.Right, "『吶，刚才那句台词是不是很酷！我好早之前就喜欢那句台词了！』"),
                new talkPiece("reimu_3","marisa_10",(int) ShowName.Left, "『谁管你……』",AfterBattleTalk_Marisa)
            },
            // 12
            new talkPiece[]{
                new talkPiece(
                            "reimu_13",
                            "crino_12",
                            "博丽灵梦",
                            "琪露诺",
                            CharecterColor.Reimu,
                            CharecterColor.Crino,
                            (int)ShowName.Right,
                            "『巫女又来了！刚才还没有长教训吗？』",
                            CharecterColor.Crino
                        ),
                new talkPiece("reimu_13","crino_12",(int) ShowName.Left, "『刚才稍微放了点水，想看看你的实力而已，别得意』"),
                new talkPiece("reimu_3","crino_12",(int) ShowName.Right, "『是吗，那我也要拿出真本领了！』"),
                new talkPiece("reimu_3","crino_12",(int) ShowName.Left, "『你有什么真本领吗……看我把你抓来做刨冰』",AfterBattleTalk_Crino),
            },
                        // 13
            new talkPiece[]{
                new talkPiece(
                            "reimu_3",
                            "crino_1",
                            "博丽灵梦",
                            "琪露诺",
                            CharecterColor.Reimu,
                            CharecterColor.Crino,
                            (int)ShowName.Left,
                            "『喂，都已经是手下败将了，为什么还在这里挡路』",
                            CharecterColor.Reimu
                        ),
                new talkPiece("reimu_3","crino_13",(int) ShowName.Right, "『你这家伙，这里是我家！当然会遇到我好吧！』"),
                new talkPiece("reimu_1","crino_13",(int) ShowName.Left, "『好像也有道理，那就顺手退治一下吧』"),
                new talkPiece("reimu_1","crino_13",(int) ShowName.Left, "『喂~！！！』",AfterBattleTalk_Crino),
            },
            // 14
            new talkPiece[]{
                new talkPiece(
                            "reimu_3",
                            "marisa_6",
                            "博丽灵梦",
                            "雾雨魔理沙",
                            CharecterColor.Reimu,
                            CharecterColor.Marisa,
                            (int)ShowName.Left,
                            "『为什么你还在这里，我之前不是已经打败过你了吗？』",
                            CharecterColor.Reimu
                        ),
                new talkPiece("reimu_3","marisa_10",(int) ShowName.Right, "『东方本篇游戏里，重开的话也要所有人重新打一遍的对吧？』"),
                new talkPiece("reimu_3","marisa_10",(int) ShowName.Left, "『不要拿那个举例子……根本不是同一种游戏好吧……』",AfterBattleTalk_Marisa),
            },
            // 15
            new talkPiece[]{
                new talkPiece(
                            "reimu_3",
                            "marisa_1",
                            "博丽灵梦",
                            "雾雨魔理沙",
                            CharecterColor.Reimu,
                            CharecterColor.Marisa,
                            (int)ShowName.Left,
                            "『要不你还是让我打一顿吧』",
                            CharecterColor.Reimu
                        ),
                new talkPiece("reimu_3","marisa_8",(int) ShowName.Right, "『为嘛欸！』"),
                new talkPiece("reimu_4","marisa_8",(int) ShowName.Left, "『少一个BOSS掉落，后面会打的很辛苦喔』"),
                new talkPiece("reimu_4","marisa_8",(int) ShowName.Right, "『那是你自己的问题！』",SkipBattle),

            },
            //16 
                        new talkPiece[]{
                new talkPiece(
                            "marisa_10",
                            "crino_4",
                            "雾雨魔理沙",
                            "琪露诺",
                            CharecterColor.Marisa,
                            CharecterColor.Crino,
                            (int)ShowName.Left,
                            "『来来来把值钱的东西都交出来！』",
                            CharecterColor.Marisa
                        ),
                new talkPiece("marisa_10","crino_4",(int) ShowName.Right, "『你们主角组欺负我一个妖精好意思吗？！』"),
                new talkPiece("marisa_10","crino_4",(int) ShowName.Left, "『咱可不管那些~！』"),
                new talkPiece(
                            "reimu_4",
                            "crino_4",
                            "博丽灵梦",
                            "琪露诺",
                            CharecterColor.Reimu,
                            CharecterColor.Crino,
                            (int)ShowName.Left,
                            "『搞不清楚谁是反派了……』",
                            CharecterColor.Reimu
                        ),
                new talkPiece(
                            "marisa_10",
                            "crino_4",
                            "雾雨魔理沙",
                            "琪露诺",
                            CharecterColor.Marisa,
                            CharecterColor.Crino,
                            (int)ShowName.Left,
                            "『~』",
                            CharecterColor.Marisa,AfterBattleTalk_Crino
                        ),
            },
        };
    }
    public void OpenYukariPlane()
    {
        GlobalControl.instance.enterYukariRoom += 1;
        ExpLevelControl.instance.SetYukariPlane();
    }

    public void OpenNitoriPlane()
    {
        GlobalControl.instance.enterNitoriRoom += 1;
        ExpLevelControl.instance.SetNitoriPlane();
    }

    // 请务必在战斗后的委托里执行该函数。用途是提示enemycreator该刷新怪了
    public void AfterBattleTalk_Crino()
    {
        GlobalControl.instance.isCloseCreateEnemy = false;
        SFXManger.instance.playBGM(8);
    }
    public void AfterBattleTalk_Marisa()
    {
        GlobalControl.instance.isCloseCreateEnemy = false;
        SFXManger.instance.playBGM(7);
    }
    // 跳过战斗并且不刷新怪
    public void SkipBattle()
    {
        GlobalControl.instance.isCloseCreateEnemy = false;
        EnemyCreator.instance.SkipWaveWithOutEnemy();
    }

}