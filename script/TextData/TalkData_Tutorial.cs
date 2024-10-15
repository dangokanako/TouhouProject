using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TalkData_Tutorial : TalkData
{
    public static TalkData_Tutorial instance;
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
        instance = this;
    }



    public GameObject crino;
    public GameObject portal;

    void Start()
    {
        TalkControl.instance.talkdata = this;

        _talkPiece = new talkPiece[][]{
            // 0
            new talkPiece[]{
                new talkPiece(
                            "reimu_4",
                            "keine_3",
                            "博丽灵梦",
                            "上白澤慧音",
                            CharecterColor.Reimu,
                            CharecterColor.Keine,
                            (int)ShowName.Left,
                            "『哎呀呀~幻想乡的学校还真是与时俱进了呢~』 ",
                            CharecterColor.Reimu
                        ),
                new talkPiece((int) ShowName.Right, "『那边的巫女！少说闲话！开始学习基础操作了！』"),
                new talkPiece((int) ShowName.Left, "『你这个实力只有三面的家伙说什么呢……』"),
                new talkPiece(
                            "reimu_4",
                            "keine_4",
                            "博丽灵梦",
                            "上白澤慧音",
                            CharecterColor.Reimu,
                            CharecterColor.Keine,
                            (int)ShowName.Right,
                            "『满月的时候可有EX道中的实力！』 ",
                            CharecterColor.Keine
                        ),
                new talkPiece(
                            "reimu_3",
                            "keine_4",
                            "博丽灵梦",
                            "上白澤慧音",
                            CharecterColor.Reimu,
                            CharecterColor.Keine,
                            (int)ShowName.Left,
                            "『EX道中好像谁都可以当的吧……』 ",
                            CharecterColor.Reimu
                        ),
                new talkPiece(
                            "reimu_3",
                            "keine_10",
                            (int)ShowName.Right,
                            "『…………总之呢这里是新游戏！接下来是新手教学』 "
                        ),
                new talkPiece((int) ShowName.Left, "『好好……』"),
                new talkPiece(
                            "reimu_0",
                            "keine_0",
                            (int)ShowName.Right,
                            "『首先是WASD键控制移动，按住 *shift键* 可以跑动。』 "
                ),
                new talkPiece((int) ShowName.Right, "『移动时按 *空格键* 可以突进。』"),
                new talkPiece((int) ShowName.Right, "『跑动和突进会消耗体力，而体力槽消耗殆尽时，会进入一小段疲劳状态的时间，此时无法再进行消耗体力的操作。』"),
                new talkPiece((int) ShowName.Right, "『体力会缓缓恢复，如果站在原地不动，体力恢复速度会更快一点。』"),
                new talkPiece((int) ShowName.Right, "『请走到我面前按 *空格键* 互动，进行下一阶段教程。』"),
                new talkPiece((int) ShowName.Right, "『*鼠标滑轮上滑* 为对话回放，此时右键为关闭对话回放。』"),
                new talkPiece((int) ShowName.Left, "『嘛，很简单的操作嘛。』"),
            },
            // 1
            new talkPiece[]{
                new talkPiece(
                            "reimu_0",
                            "keine_0",
                            "博丽灵梦",
                            "上白澤慧音",
                            CharecterColor.Reimu,
                            CharecterColor.Keine,
                            (int)ShowName.Right,
                            "『好，接下来我们进行第二课，道具的使用』 ",
                            CharecterColor.Keine
                        ),
                new talkPiece("reimu_12","keine_0",(int)ShowName.Left,"『嘛。』 "),
                new talkPiece((int) ShowName.Right, "『在战斗中我们会获得非常多的道具，比如恢复品、升级品、符卡、武器、装备等等……』"),
                new talkPiece((int) ShowName.Right, "『看到右上角的 *快捷栏* 了吗？』"),
                new talkPiece((int) ShowName.Right, "『*选中的快捷栏* 的边框会稍大一些，此时点击鼠标左键就可以使用选中的物品』"),
                new talkPiece((int) ShowName.Right, "『使用 *鼠标滑轮* 或者 *键盘上方的数字键* 可以切换选中的背包栏』"),
                new talkPiece((int) ShowName.Right, "『接下来尝试使用蘑菇这种恢复品来恢复HP吧』",AddItem1),
                new talkPiece("reimu_10","keine_0",(int) ShowName.Left, "『嗯，使用物品恢复一下HP吧（记仇中）』"),
            },
            // 2
            new talkPiece[]{
                new talkPiece(
                            "reimu_0",
                            "keine_0",
                            "博丽灵梦",
                            "上白澤慧音",
                            CharecterColor.Reimu,
                            CharecterColor.Keine,
                            (int)ShowName.Right,
                            "『你的HP还没有恢复满，需要我再给你几个蘑菇吗』 ",
                            CharecterColor.Keine
                        ),
                new talkPiece("reimu_10","keine_0",(int) ShowName.Left, "『好好，多多益善~』"),
                new talkPiece("reimu_10","keine_3",(int) ShowName.Right, "『蘑菇将会在训练结束后统一回收，用这种方式刷蘑菇是没有用的。』",AddItem2),
                new talkPiece("reimu_3","keine_3",(int) ShowName.Left, "『切~小气~』"),
            },
            // 3
            new talkPiece[]{
                new talkPiece(
                            "reimu_3",
                            "keine_0",
                            "博丽灵梦",
                            "上白澤慧音",
                            CharecterColor.Reimu,
                            CharecterColor.Keine,
                            (int)ShowName.Right,
                            "『很好，接下来我们第四课，物品整理和合成教学。』 ",
                            CharecterColor.Keine
                        ),
                new talkPiece((int) ShowName.Left, "『你这么热情搞得我很尴尬。』"),
                new talkPiece("reimu_0","keine_0",(int) ShowName.Right, "『在战斗中我们会捡到很多的道具，但我们希望它们在合适的位置』"),
                new talkPiece((int) ShowName.Right, "『按 *TAB键* 或 *ESC键* 打开背包面板，此时在背包上点击 *鼠标左键* 可以拿起物品到鼠标上』"),
                new talkPiece((int) ShowName.Right, "『之后在其他格子按下 *左键* ，就可以放置或者交换物品』"),
                new talkPiece((int) ShowName.Right, "『顺便一提，*拖曳物品* 也是可以的。』"),
                new talkPiece((int) ShowName.Right, "『此外，物品拿在鼠标上时，在屏幕上其他位置按左键，可以直接使用。右键可以丢弃。』"),
                new talkPiece((int) ShowName.Right, "『接下来请你把任意蘑菇 *挪动到三号快捷栏位* 。』",AddItem2),
                new talkPiece((int) ShowName.Left, "『如果有玩过《泰拉○亚》的话会上手比较容易喔』"),
                new talkPiece("reimu_0","keine_3",(int) ShowName.Right, "『？』"),
            },

            // 4
            new talkPiece[]{
                new talkPiece(
                            "reimu_0",
                            "keine_0",
                            "博丽灵梦",
                            "上白澤慧音",
                            CharecterColor.Reimu,
                            CharecterColor.Keine,
                            (int)ShowName.Right,
                            "『你右上角的第三个快捷栏还没有东西，要按 *TAB键* 打开面板才能移动物品，需要我再给你几个蘑菇吗』 ",
                            CharecterColor.Keine
                        ),
                new talkPiece("reimu_10","keine_0",(int) ShowName.Left, "『好好，多多益善~』"),
                new talkPiece("reimu_10","keine_3",(int) ShowName.Right, "『都说了蘑菇将会在训练结束后统一回收，请不要用这种方式刷蘑菇。』",AddItem2),
                new talkPiece("reimu_3","keine_3",(int) ShowName.Left, "『切~小气~』"),
            },
            // 5
            new talkPiece[]{
                new talkPiece(
                            "reimu_0",
                            "keine_0",
                            "博丽灵梦",
                            "上白澤慧音",
                            CharecterColor.Reimu,
                            CharecterColor.Keine,
                            (int)ShowName.Right,
                            "『接下来是第三课，物品合成！这是本游戏的核心机制！』 ",
                            CharecterColor.Keine
                        ),
                new talkPiece((int) ShowName.Left, "『是吗，我希望这游戏先把地图好好做做呢。』"),
                new talkPiece((int) ShowName.Right, "『妖精们会有概率掉落各种各样的东西，我们在战斗时要好好的利用这些物品。』"),
                new talkPiece((int) ShowName.Right, "『一个合成配方最多可以由5种材料组成，顺序随意。』"),
                new talkPiece((int) ShowName.Right, "『根据合成配方，将物品放入合成材料栏，如果配方正确，就会出现合成结果』"),
                new talkPiece("reimu_1","keine_0",(int) ShowName.Left, "『那么，合成配方是什么呢？』"),
                new talkPiece((int) ShowName.Right, "『好问题。』"),
                new talkPiece((int) ShowName.Right, "『把鼠标放在物品上停留两秒钟，会在下方提示合成配方。』"),
                new talkPiece((int) ShowName.Right, "『最基础的就是 *蘑菇+蘑菇+森之蘑菇* 的配方，接下来实际自己操作试一试吧。』",AddItem2),
                new talkPiece((int) ShowName.Right, "『注意一下，由于需要两个蘑菇，需要 *鼠标右键* 拆分一下物品。』"),
                new talkPiece("reimu_3","keine_0",(int) ShowName.Left, "『怎么又是这两个破蘑菇……』"),
            },
            // 6
            new talkPiece[]{
                new talkPiece(
                            "reimu_0",
                            "keine_0",
                            "博丽灵梦",
                            "上白澤慧音",
                            CharecterColor.Reimu,
                            CharecterColor.Keine,
                            (int)ShowName.Right,
                            "『你的快捷栏里还没有合成结果，要按 *TAB键* 打开背包面板才能合成物品。合成结果要放到快捷栏我才能看得见。』 ",
                            CharecterColor.Keine
                        ),
                new talkPiece("reimu_0","keine_0",(int) ShowName.Right, "『合成配方是 “蘑菇”+“蘑菇”+“森之蘑菇”。按 *右键* 可以拆分已堆叠的物品。需要我再给你几个蘑菇吗』"),
                new talkPiece("reimu_10","keine_0",(int) ShowName.Left, "『好好，多多益善~』"),
                new talkPiece("reimu_10","keine_3",(int) ShowName.Right, "『我再说一遍，蘑菇将会在训练结束后统一回收，请不要用这种方式刷蘑菇。』",AddItem2),
                new talkPiece("reimu_3","keine_3",(int) ShowName.Left, "『切~小气~』"),
                new talkPiece("reimu_3","keine_3",(int) ShowName.Right, "『……』"),

                },
            // 7
            new talkPiece[]{
                new talkPiece(
                            "reimu_0",
                            "keine_0",
                            "博丽灵梦",
                            "上白澤慧音",
                            CharecterColor.Reimu,
                            CharecterColor.Keine,
                            (int)ShowName.Right,
                            "『很好，我们可以看到HP+1的道具和HP+3的道具合成出了HP+12的道具。』 ",
                            CharecterColor.Keine
                        ),
                new talkPiece("reimu_3","keine_0",(int) ShowName.Left, "『但是这个菌菇汤作为成品，居然不能堆叠呢……是什么鸡肋设定？』"),
                new talkPiece((int) ShowName.Right, "『接下来是道具丢弃流程。』"),
                new talkPiece((int) ShowName.Right, "『众所周知，灵梦是开了自动拾取的。丢在地面上的东西会自动再次捡起来。』"),
                new talkPiece("reimu_3","keine_0",(int) ShowName.Left, "『……』"),
                new talkPiece((int) ShowName.Right, "『为了解决这个问题，在合成界面的 *左下角* 设置了一个异次元垃圾桶』"),
                new talkPiece((int) ShowName.Right, "『垃圾桶的物品可以无限堆入物品，并且覆盖掉原先的物品。』"),
                new talkPiece("reimu_4","keine_0",(int) ShowName.Left, "『其实还是《泰拉○亚》那个机制喔』"),
                new talkPiece((int) ShowName.Right, "『接下来把背包里的物品都丢到垃圾桶里吧』"),
                new talkPiece("reimu_3","keine_0",(int) ShowName.Left, "『不会很浪费吗……』"),
                new talkPiece((int) ShowName.Right, "『不用担心浪费问题，幻想乡会有专门的工作人员对垃圾桶的资源进行再回收利用』",AddItem2),
                new talkPiece((int) ShowName.Left, "『真的？』"),
            },
            // 8
            new talkPiece[]{
                new talkPiece(
                            "reimu_0",
                            "keine_0",
                            "博丽灵梦",
                            "上白澤慧音",
                            CharecterColor.Reimu,
                            CharecterColor.Keine,
                            (int)ShowName.Right,
                            "『你的背包里还有物品没有丢弃完，应该不需要我给你蘑菇吧。』 ",
                            CharecterColor.Keine
                        ),
                new talkPiece("reimu_10","keine_0",(int) ShowName.Left, "『好好，多多益善~』"),
                new talkPiece("reimu_9","keine_2_10",(int) ShowName.Right, "『你脑子是有洞吗！（怒）』",TakeDamage_4),
            },
            // 9
            new talkPiece[]{
                new talkPiece(
                            "reimu_0",
                            "keine_0",
                            "博丽灵梦",
                            "上白澤慧音",
                            CharecterColor.Reimu,
                            CharecterColor.Keine,
                            (int)ShowName.Left,
                            "『接下来又是什么新东西？』 ",
                            CharecterColor.Reimu
                        ),
                new talkPiece((int) ShowName.Right, "『接下来学习战斗道具和符卡的使用。』"),
                new talkPiece((int) ShowName.Right, "『请用那边的笨蛋妖精作为靶子，每种武器请都体验一下。』"),
                new talkPiece((int) ShowName.Right, "『*鼠标拾取道具* 之后试一下吧。』"),
                new talkPiece("reimu_10","keine_0",(int) ShowName.Left, "『耶，是我最喜欢的工作』",AddItem3),
            },
            // 10
            new talkPiece[]{
                new talkPiece(
                            "reimu_0",
                            "keine_0",
                            "博丽灵梦",
                            "上白澤慧音",
                            CharecterColor.Reimu,
                            CharecterColor.Keine,
                            (int)ShowName.Right,
                            "『请至少把符卡都用一遍吧？记得用鼠标拾取符卡。』 ",
                            CharecterColor.Keine
                        ),
                new talkPiece((int) ShowName.Left, "『我的其他符卡呢？』"),
                new talkPiece((int) ShowName.Right, "『会有的。』"),
                new talkPiece("reimu_10","keine_0",(int) ShowName.Left, "『耶』"),
            },
            // 11
            new talkPiece[]{
                new talkPiece(
                            "reimu_0",
                            "keine_0",
                            "博丽灵梦",
                            "上白澤慧音",
                            CharecterColor.Reimu,
                            CharecterColor.Keine,
                            (int)ShowName.Right,
                            "『最后我们学习合成规则。』 ",
                            CharecterColor.Keine
                        ),
                new talkPiece((int) ShowName.Right, "『可以合成的配方会显示在物品下方，所以应该不会有什么疑问了。』"),
                new talkPiece((int) ShowName.Right, "『非常建议战斗前购买一本“『』”，或者☆楼观剑☆ (仮)这种无限次使用的武器』"),
                new talkPiece("reimu_3","keine_0",(int) ShowName.Left, "『手感很差喔，能改一下吗。』"),
                new talkPiece("reimu_3","keine_3",(int) ShowName.Right, "『不行。』"),
                new talkPiece("reimu_0","keine_0",(int) ShowName.Right, "『接下来请尝试用道具合成出来中级符卡并且试一下吧』",AddItem4),
                new talkPiece((int) ShowName.Right, "『要用「曜日之力」单个道具合成出“微弱的自然之力”，之后再和符卡参与合成。』"),
                new talkPiece("reimu_3","keine_0",(int) ShowName.Left, "『多这一层转换有什么意义吗？』"),
                new talkPiece("reimu_3","keine_3",(int) ShowName.Right, "『有的。』"),
            },
            // 12
            new talkPiece[]{
                new talkPiece(
                            "reimu_0",
                            "keine_0",
                            "博丽灵梦",
                            "上白澤慧音",
                            CharecterColor.Reimu,
                            CharecterColor.Keine,
                            (int)ShowName.Right,
                            "『最后，来聊一点其他的TIPS。』 ",
                            CharecterColor.Keine
                        ),
                new talkPiece((int) ShowName.Right, "『这些本来打算写在载入时的黑屏界面里的，但游戏体量太小根本不用载入。』"),
                new talkPiece((int) ShowName.Right, "『有些装备有限制在某些特定的位置才能发挥作用』"),
                new talkPiece((int) ShowName.Right, "『一些被动装备会存在词条，会影响装备的属性。』"),
                new talkPiece((int) ShowName.Right, "『碰撞也可以造成伤害。但同时也会对自己产生一些伤害。』"),
                new talkPiece((int) ShowName.Right, "『可以通过按钮立即刷新当前波次所有敌人，并且获得P点奖励。』"),
                new talkPiece((int) ShowName.Right, "『部分弹幕和敌人弹幕之间是可以互相抵消的。算法也很简单，会抵消掉攻击力的部分。』"),
                new talkPiece("reimu_3","keine_0",(int) ShowName.Left, "『啊，真好玩呢。（棒读）』"),
                new talkPiece((int) ShowName.Right, "『因为本游戏最开始的计划是仿roguelike制作的。』"),
                new talkPiece((int) ShowName.Left, "『是吗？现在看起来反而成累赘了呢。』"),
                new talkPiece("reimu_0","keine_0",(int) ShowName.Right, "『更多道具，诸如装备、消耗品，也可以到游戏中体验。』"),
                new talkPiece((int) ShowName.Right, "『那么本次教程就到这里了。祝你好运，红白的巫女。』",SetPortal),
            },
            // 13
            new talkPiece[]{
                new talkPiece(
                            "reimu_0",
                            "keine_3",
                            "博丽灵梦",
                            "上白澤慧音",
                            CharecterColor.Reimu,
                            CharecterColor.Keine,
                            (int)ShowName.Left,
                            "『慧音老师没说话，只做了一个手势：“放学了，你们走吧”。』 ",
                            CharecterColor.Reimu
                        ),
                new talkPiece((int) ShowName.Right, "『请不要给我配莫名其妙的台词。』"),
                new talkPiece((int) ShowName.Right, "『传送点在上面的光圈。』"),
            },
            // 14
            new talkPiece[]{
                new talkPiece(
                            "reimu_0",
                            "keine_3",
                            "博丽灵梦",
                            "上白澤慧音",
                            CharecterColor.Reimu,
                            CharecterColor.Keine,
                            (int)ShowName.Right,
                            "『请不要乱跑。』 ",
                            CharecterColor.Keine
                        ),
                new talkPiece("reimu_10","keine_3",(int) ShowName.Left, "『啊哈哈~人家只想逛逛地图看看有没有什么隐藏宝箱……』"),
                new talkPiece("reimu_10","keine_3",(int) ShowName.Right, "『没有。』"),
            },
        };
    }

    public void AddItem1()
    {
        PlayerHealthControl.instance.TakeDamage(4);
        AssetControl.instance.DropItem(PlayerHealthControl.instance.transform.position, ItemControl.instance.itemGuide[1]);
        AssetControl.instance.DropItem(PlayerHealthControl.instance.transform.position, ItemControl.instance.itemGuide[2]);
    }
    public void AddItem2()
    {
        AssetControl.instance.DropItem(PlayerHealthControl.instance.transform.position, ItemControl.instance.itemGuide[1]);
        AssetControl.instance.DropItem(PlayerHealthControl.instance.transform.position, ItemControl.instance.itemGuide[1]);
        AssetControl.instance.DropItem(PlayerHealthControl.instance.transform.position, ItemControl.instance.itemGuide[2]);
    }

    public void AddItem3()
    {
        AssetControl.instance.DropItem(new Vector3(-3.65f, 0f, 0f), ItemControl.instance.itemGuide[87]);
        //AssetControl.instance.DropItem(new Vector3(-3.65f, 0f, 0f), ItemControl.instance.itemGuide[20]);
        AssetControl.instance.DropItem(new Vector3(-3.65f, 0f, 0f), ItemControl.instance.itemGuide[39]);
        AssetControl.instance.DropItem(new Vector3(-3.65f, 0f, 0f), ItemControl.instance.itemGuide[36]);
        AssetControl.instance.DropItem(new Vector3(-3.65f, 0f, 0f), ItemControl.instance.itemGuide[81]);
        AssetControl.instance.DropItem(new Vector3(-3.65f, 0f, 0f), ItemControl.instance.itemGuide[106]);
        this.crino.SetActive(true);
    }

    public void AddItem4()
    {
        AssetControl.instance.DropItem(PlayerHealthControl.instance.transform.position, ItemControl.instance.itemGuide[4]);
        AssetControl.instance.DropItem(PlayerHealthControl.instance.transform.position, ItemControl.instance.itemGuide[11]);
    }
    public void TakeDamage_4()
    {
        if (PlayerHealthControl.instance.currentHealth > 5)
            PlayerHealthControl.instance.TakeDamage(4);
        else
        {
            BubbleTalkControl.instance.ShowBubbleTalkButWaitingFormouse(new BubbleTalkData("reimu_small_0", "『笨蛋吗……』"));
        }
    }
    public void SetPortal()
    {
        // 删除地面上的DropsClass类型的物品
        DropsClass[] drops = FindObjectsOfType<DropsClass>();
        foreach (DropsClass drop in drops)
        {
            Destroy(drop.gameObject);
        }
        ItemControl.instance.ClearAllBag();

        GlobalControl.instance.isTutorial = true;
        GlobalControl.instance.SetStatusToOriginal();

        portal.SetActive(true);
    }
}