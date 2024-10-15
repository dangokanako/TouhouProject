using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public class TalkData_Battle_baiyulou : TalkData
{
    public static TalkData_Battle_baiyulou instance;
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
                new talkPiece("reimu_3","yukari_0",(int) ShowName.Right, "『这可是时下最流行最时髦的选择方式喔。此外卖一点小福袋，可以开出一点小礼物。』"),
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
                            "redi_0",
                            "博丽灵梦",
                            "蕾蒂",
                            CharecterColor.Reimu,
                            CharecterColor.Crino,
                            (int)ShowName.Left,
                            "『白玉楼上虽然蛮凉快的，但还没有冷到冰雪妖怪出来的地步吧？』",
                            CharecterColor.Crino
                        ),
                new talkPiece("reimu_0","redi_0",(int) ShowName.Left, "『明明也不是冬天，为什么蕾蒂已经出来了？』"),
                new talkPiece("reimu_0","redi_0",(int) ShowName.Right, "『只是很高兴而已喔。』"),
                new talkPiece("reimu_1","redi_0",(int) ShowName.Left, "『那只能委屈一下你，让你不高兴一下吧。』"),
                new talkPiece("reimu_1","redi_0",(int) ShowName.Right, "『哎呀~现在的人类好过分啊~』",AfterBattleTalk_Crino),
            },
            // 10
            new talkPiece[]{
                new talkPiece(
                            "reimu_0",
                            "redi_0",
                            "博丽灵梦",
                            "蕾蒂",
                            CharecterColor.Reimu,
                            CharecterColor.Crino,
                            (int)ShowName.Left,
                            "『白玉楼上虽然蛮凉快的，但还没有冷到冰雪妖怪出来的地步吧？』",
                            CharecterColor.Crino
                        ),
                new talkPiece("reimu_0","redi_0",(int) ShowName.Left, "『明明也不是冬天，为什么蕾蒂已经出来了？』"),
                new talkPiece("reimu_0","redi_0",(int) ShowName.Right, "『只是很高兴而已喔。』"),
                new talkPiece("reimu_1","redi_0",(int) ShowName.Left, "『那只能委屈一下你，让你不高兴一下吧。』"),
                new talkPiece(
                            "marisa_10",
                            "redi_0",
                            "雾雨魔理沙",
                            "蕾蒂",
                            CharecterColor.Marisa,
                            CharecterColor.Crino,
                            (int)ShowName.Left,
                            "『话说完了吗，可以开打了吗』",
                            CharecterColor.Crino
                        ),
                new talkPiece("reimu_1","redi_0",(int) ShowName.Right, "『哎呀~现在的人类好过分啊~』",AfterBattleTalk_Crino),
            },
            // 11
            new talkPiece[]{
                new talkPiece(
                            "reimu_3",
                            "redi_0",
                            "博丽灵梦",
                            "蕾蒂",
                            CharecterColor.Reimu,
                            CharecterColor.Crino,
                            (int)ShowName.Right,
                            "『今天似乎不适合巫女出行呢~』",
                            CharecterColor.Crino
                        ),
                new talkPiece("reimu_3","redi_0",(int) ShowName.Left, "『刚才稍微放了点水而已。设定怎么看我都是无敌的好吧。』"),
                new talkPiece(
                            "marisa_1",
                            "redi_0",
                            "雾雨魔理沙",
                            "蕾蒂",
                            CharecterColor.Marisa,
                            CharecterColor.Crino,
                            (int)ShowName.Left,
                            "『……灵梦你好像也没有那么强吧？』",
                            CharecterColor.Crino
                        ),
                    new talkPiece(
                            "reimu_3",
                            "redi_0",
                            "博丽灵梦",
                            "蕾蒂",
                            CharecterColor.Reimu,
                            CharecterColor.Crino,
                            (int)ShowName.Right,
                            "『魔理沙你闭嘴啦』",
                            CharecterColor.Crino,AfterBattleTalk_Crino
                        ),
            },
            // 12
            new talkPiece[]{
                new talkPiece(
                            "reimu_3",
                            "redi_0",
                            "博丽灵梦",
                            "蕾蒂",
                            CharecterColor.Reimu,
                            CharecterColor.Crino,
                            (int)ShowName.Left,
                            "『没到冬天的话就老老实实回去睡觉！』",
                            CharecterColor.Crino,AfterBattleTalk_Crino
                        ),
            },
            // 13
            new talkPiece[]{
                new talkPiece(
                            "reimu_1",
                            "youmu_7",
                            "博丽灵梦",
                            "魂魄妖梦",
                            CharecterColor.Reimu,
                            CharecterColor.Youmu,
                            (int)ShowName.Right,
                            "『擅闯冥界，来者何人？』",
                            CharecterColor.Youmu
                        ),
                new talkPiece(
                            "reimu_1",
                            "youmu_7",
                            "博丽灵梦",
                            "魂魄妖梦",
                            CharecterColor.Reimu,
                            CharecterColor.Youmu,
                            (int)ShowName.Left,
                            "『睁开眼睛看看嘛，是我啦是我啦，博丽灵梦』",
                            CharecterColor.Reimu
                        ),
                new talkPiece("reimu_1","youmu_0",(int) ShowName.Right, "『原来是灵梦啊，好久不见，最近又有什么事情吗？』"),
                new talkPiece("reimu_1","youmu_0",(int) ShowName.Left, "『就是那个啊，异变啊，你懂的，异变的事情。』"),
                new talkPiece("reimu_1","youmu_0",(int) ShowName.Left, "『有没有什么头绪？』"),
                new talkPiece("reimu_1","youmu_7",(int) ShowName.Right, "『唔，没有呢。』"),
                new talkPiece("reimu_1","youmu_5",(int) ShowName.Left, "『那就让我进去见见幽幽子』"),
                new talkPiece("reimu_1","youmu_5",(int) ShowName.Right, "『啊…不…那个，规矩上人类不能进入冥界……』"),
                new talkPiece("reimu_4","youmu_5",(int) ShowName.Left, "『啊~真是的，冥界的结界早就被捅穿了，不是吗？』"),
                new talkPiece("reimu_4","youmu_5",(int) ShowName.Right, "『而且，那个幽幽子大人正在进食，最好不要打扰……』"),
                new talkPiece("reimu_4","youmu_5",(int) ShowName.Left, "『幽幽子那个进食量，我想什么时候都在进食吧？』"),
                new talkPiece("reimu_4","youmu_5",(int) ShowName.Right, "『啊…那个，擅自放人类进去，我会挨骂的。』"),
                new talkPiece("reimu_6","youmu_5",(int) ShowName.Left, "『啊啊，我明白了。我把你打倒就可以了吧？』"),
                new talkPiece("reimu_6","youmu_0",(int) ShowName.Right, "『可是，灵梦小姐，我可是很强的喔？』"),
                new talkPiece("reimu_0","youmu_0",(int) ShowName.Left, "『我连魔理沙都打败了的喔？』"),
                new talkPiece("reimu_0","youmu_0",(int) ShowName.Right, "『那个只是因为魔理沙今天放了很多水吧？她连一张符卡都没有用。』"),
                new talkPiece("reimu_12","youmu_0",(int) ShowName.Left, "『虽然确实是那样……』"),
                new talkPiece("reimu_13","youmu_0",(int) ShowName.Left, "『来吧，放马过来吧！』"),
                new talkPiece("reimu_13","youmu_0",(int) ShowName.Right, "『妖怪所锻造的这把楼观剑——』"),
                new talkPiece("reimu_13","youmu_0",(int) ShowName.Right, "『无法斩断的东西，几乎不存在！』", AfterBattleTalk_Marisa),
            },
            // 14
            new talkPiece[]{
                new talkPiece(
                            "reimu_1",
                            "youmu_7",
                            "博丽灵梦",
                            "魂魄妖梦",
                            CharecterColor.Reimu,
                            CharecterColor.Youmu,
                            (int)ShowName.Right,
                            "『擅闯冥界，来者何人？』",
                            CharecterColor.Youmu
                        ),
                new talkPiece(
                            "reimu_1",
                            "youmu_7",
                            "博丽灵梦",
                            "魂魄妖梦",
                            CharecterColor.Reimu,
                            CharecterColor.Youmu,
                            (int)ShowName.Left,
                            "『睁开眼睛看看嘛，是我啦是我啦，灵梦和魔理沙』",
                            CharecterColor.Reimu
                        ),
                new talkPiece("reimu_1","youmu_0",(int) ShowName.Right, "『原来是主角组啊，好久不见，最近又有什么事情吗？』"),
                new talkPiece("reimu_1","youmu_0",(int) ShowName.Left, "『就是那个啊，异变啊，你懂的，异变的事情。』"),
                new talkPiece("reimu_1","youmu_0",(int) ShowName.Left, "『有没有什么头绪？』"),
                new talkPiece("reimu_1","youmu_7",(int) ShowName.Right, "『唔，没有呢。』"),
                new talkPiece("reimu_1","youmu_5",(int) ShowName.Left, "『那就让我们进去见见幽幽子』"),
                new talkPiece("reimu_1","youmu_5",(int) ShowName.Right, "『啊…不…那个，规矩上人类不能进入冥界……』"),
                new talkPiece("reimu_4","youmu_5",(int) ShowName.Left, "『啊~真是的，冥界的结界早就被捅穿了，不是吗？』"),
                new talkPiece("reimu_4","youmu_5",(int) ShowName.Right, "『而且，那个幽幽子大人正在进食，最好不要打扰……』"),
                new talkPiece("reimu_4","youmu_5",(int) ShowName.Left, "『幽幽子那个进食量，我想什么时候都在进食吧？』"),
                new talkPiece("reimu_4","youmu_5",(int) ShowName.Right, "『啊…而且，擅自放人类进去，我会挨骂的。』"),
                new talkPiece("reimu_6","youmu_5",(int) ShowName.Left, "『啊啊，我明白了。我把你打倒就可以了吧？』"),
                new talkPiece("reimu_6","youmu_0",(int) ShowName.Right, "『不过，灵梦小姐，我可是很强的喔？』"),
                new talkPiece("reimu_0","youmu_0",(int) ShowName.Left, "『我连魔理沙都打败了的喔？』"),
                new talkPiece("reimu_0","youmu_0",(int) ShowName.Right, "『那个只是因为魔理沙今天放了很多水吧？她连一张符卡都没有用。』"),
                new talkPiece(
                            "marisa_0",
                            "youmu_0",
                            "雾雨魔理沙",
                            "魂魄妖梦",
                            CharecterColor.Marisa,
                            CharecterColor.Youmu,
                            (int)ShowName.Left,
                            "『（吹口哨~♪）』",
                            CharecterColor.Marisa
                        ),
                                        new talkPiece(
                            "reimu_13",
                            "youmu_0",
                            "博丽灵梦",
                            "魂魄妖梦",
                            CharecterColor.Reimu,
                            CharecterColor.Youmu,
                            (int)ShowName.Left,
                            "『魔理沙！喂！』",
                            CharecterColor.Reimu
                        ),
                new talkPiece("reimu_12","youmu_0",(int) ShowName.Left, "『虽然确实是那样……』"),
                new talkPiece("reimu_13","youmu_0",(int) ShowName.Left, "『来吧，放马过来吧！』"),
                new talkPiece("reimu_13","youmu_0",(int) ShowName.Right, "『妖怪所锻造的这把楼观剑——』"),
                new talkPiece("reimu_13","youmu_0",(int) ShowName.Right, "『无法斩断的东西，几乎不存在！』"),
                                new talkPiece(
                            "marisa_0",
                            "youmu_0",
                            "雾雨魔理沙",
                            "魂魄妖梦",
                            CharecterColor.Marisa,
                            CharecterColor.Youmu,
                            (int)ShowName.Left,
                            "『所以说还是有斩不断的东西啦？』",
                            CharecterColor.Marisa
                        ),
                                        new talkPiece(
                            "reimu_12",
                            "youmu_0",
                            "博丽灵梦",
                            "魂魄妖梦",
                            CharecterColor.Reimu,
                            CharecterColor.Youmu,
                            (int)ShowName.Left,
                            "『魔理沙！太破坏气氛了啦。』",
                            CharecterColor.Reimu,AfterBattleTalk_Marisa
                        ),
            },
            // 15
            new talkPiece[]{
                new talkPiece(
                            "reimu_0",
                            "youmu_0",
                            "博丽灵梦",
                            "魂魄妖梦",
                            CharecterColor.Reimu,
                            CharecterColor.Youmu,
                            (int)ShowName.Right,
                            "『又来了呢』",
                            CharecterColor.Youmu
                        ),
                new talkPiece("reimu_0","youmu_0",(int) ShowName.Left, "『反正冥界每天进进出出那么多人……哦不，幽灵……』"),
                new talkPiece("reimu_0","youmu_7",(int) ShowName.Right, "『不行。』"),
                new talkPiece("reimu_4","youmu_0",(int) ShowName.Left, "『那还是只能诉诸武力了吗』",AfterBattleTalk_Marisa),
            },
                        // 16
            new talkPiece[]{
                new talkPiece(
                            "reimu_0",
                            "youmu_0",
                            "博丽灵梦",
                            "魂魄妖梦",
                            CharecterColor.Reimu,
                            CharecterColor.Youmu,
                            (int)ShowName.Right,
                            "『又来了呢』",
                            CharecterColor.Youmu
                        ),
                new talkPiece("reimu_0","youmu_0",(int) ShowName.Left, "『反正冥界每天进进出出那么多人……哦不，幽灵……』"),
                new talkPiece("reimu_0","youmu_7",(int) ShowName.Right, "『不行。』"),
                new talkPiece("reimu_4","youmu_0",(int) ShowName.Left, "『那还是只能诉诸武力了吗』"),
                                                new talkPiece(
                            "marisa_0",
                            "youmu_0",
                            "雾雨魔理沙",
                            "魂魄妖梦",
                            CharecterColor.Marisa,
                            CharecterColor.Youmu,
                            (int)ShowName.Left,
                            "『小心喔，被那两把剑砍到可是要青一块紫一块的』",
                            CharecterColor.Marisa
                        ),
                        new talkPiece(
                            "reimu_12",
                            "youmu_0",
                            "博丽灵梦",
                            "魂魄妖梦",
                            CharecterColor.Reimu,
                            CharecterColor.Youmu,
                            (int)ShowName.Left,
                            "『被剑砍到怎么会青一块紫一块的喂！』",
                            CharecterColor.Reimu,AfterBattleTalk_Marisa
                        ),
            },
                                    // 17
            new talkPiece[]{
                new talkPiece(
                            "reimu_0",
                            "youmu_0",
                            "博丽灵梦",
                            "魂魄妖梦",
                            CharecterColor.Reimu,
                            CharecterColor.Youmu,
                            (int)ShowName.Right,
                            "『来吧，再好好切磋一下吧！』",
                            CharecterColor.Youmu
                        ),
                new talkPiece("reimu_12","youmu_0",(int) ShowName.Left, "『没有新的掉落的话，请容我拒绝……』",AfterBattleTalk_Marisa),
            },
            // 18
            new talkPiece[]{
                new talkPiece(
                            "reimu_0",
                            "youmu_0",
                            "博丽灵梦",
                            "魂魄妖梦",
                            CharecterColor.Reimu,
                            CharecterColor.Youmu,
                            (int)ShowName.Right,
                            "『来吧，再好好切磋一下吧！』",
                            CharecterColor.Youmu
                        ),
                new talkPiece("reimu_12","youmu_0",(int) ShowName.Left, "『没有新的掉落的话，请容我拒绝……』"),
                                                                new talkPiece(
                            "marisa_0",
                            "youmu_0",
                            "雾雨魔理沙",
                            "魂魄妖梦",
                            CharecterColor.Marisa,
                            CharecterColor.Youmu,
                            (int)ShowName.Left,
                            "『妖梦已经冲过来了喔？』",
                            CharecterColor.Marisa
                        ),
                                        new talkPiece(
                            "reimu_12",
                            "youmu_0",
                            "博丽灵梦",
                            "魂魄妖梦",
                            CharecterColor.Reimu,
                            CharecterColor.Youmu,
                            (int)ShowName.Right,
                            "『欸？！』",
                            CharecterColor.Youmu,AfterBattleTalk_Marisa
                        ),
            },
            // 19
            new talkPiece[]{
                                new talkPiece(
                            "reimu_10",
                            "youmu_p_2",
                            "博丽灵梦",
                            "魂魄妖梦",
                            CharecterColor.Reimu,
                            CharecterColor.Youmu,
                            (int)ShowName.Right,
                            "『啊……疼疼疼』",
                            CharecterColor.Youmu
                        ),
                new talkPiece(
                            "reimu_10",
                            "youmu_p_2",
                            "博丽灵梦",
                            "魂魄妖梦",
                            CharecterColor.Reimu,
                            CharecterColor.Youmu,
                            (int)ShowName.Left,
                            "『堂堂大胜利！轻松轻松』",
                            CharecterColor.Reimu
                        ),
                                                                new talkPiece(
                            "marisa_1",
                            "youmu_p_2",
                            "雾雨魔理沙",
                            "魂魄妖梦",
                            CharecterColor.Marisa,
                            CharecterColor.Youmu,
                            (int)ShowName.Left,
                            "『刚才打的好像也没那么轻松吧？』",
                            CharecterColor.Marisa
                        ),
                                        new talkPiece(
                            "reimu_6",
                            "youmu_p_2",
                            "博丽灵梦",
                            "魂魄妖梦",
                            CharecterColor.Reimu,
                            CharecterColor.Youmu,
                            (int)ShowName.Left,
                            "『咳。』",
                            CharecterColor.Reimu
                        ),
                new talkPiece("reimu_0","youmu_p_2",(int) ShowName.Left, "『那我们现在总可以进去了吧。』"),
                new talkPiece("reimu_4","youmu_p_2",(int) ShowName.Right, "『你们要被大小姐吃掉我可不管喔。』"),
                new talkPiece("reimu_4","youmu_p_2",(int) ShowName.Left, "『幽幽子是什么吞食天地的妖怪吗……』"),
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