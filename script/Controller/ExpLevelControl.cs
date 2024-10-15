using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using DG.Tweening;
using Unity.VisualScripting;
using Sequence = DG.Tweening.Sequence;
using System.Runtime.CompilerServices;


/// <summary>
/// 注：本来计划改造成升级后商店，于是在ExpLevelControl本类里写了。但后来分离出去了，代码还是放在这里了。
/// </summary>
public class ExpLevelControl : MonoBehaviour
{
    public static ExpLevelControl instance;
    public int currentExp;
    public DropsClass drops;
    // 升级所需经验
    public List<int> expLevelNeed = new List<int>();
    public int currentLevel = 1;
    public int MaxLevel = 100;

    [Header("免费物品")]
    public GameObject freeItemBagGameObject;
    // 免费物品
    public List<ItemBag> freeItemBagList = new List<ItemBag>();
    // 是否已经领取过免费物品
    public bool isGetFreeItem = false;
    public int GetFreeTime;
    [Header("福袋")]
    public GameObject luckyBagGameObject;
    // 福袋物品
    public ItemBag luckyBag;
    // 福袋价格
    public TMP_Text luckyBagPriceText;
    public int luckyBagPrice;
    [Header("重铸物品")]
    public GameObject reforgingBagGameObject;
    public TMP_Text reforgingPriceText;
    public int reforgingPrice;
    public ItemBag reforgingBag;
    public Button reforgingButton;
    public int reforgingTimes;
    [Header("升级物品")]
    public GameObject upgradeItemBagGameObject;
    public ItemBag upgradeItemBag;

    public TMP_Text upgradePriceText;
    public int upgradePrice;
    public Button upgradeButton;
    public int upgradeTimes;
    [Header("随机商店")]
    public GameObject randomShopBagGameObject;
    public List<ItemBag> randomShopBag = new List<ItemBag>();
    public List<TMP_Text> randomShopPriceText = new List<TMP_Text>();
    public List<int> randomShopPrice;


    [Header("商店的立绘和文本")]
    public TMP_Text PlaneNameText;
    public TMP_Text PlaneText;
    public UnityEngine.UI.Image PlaneImage;
    // 文本动画系统
    private Tween textTween;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        int initexp = 30;
        while (expLevelNeed.Count < MaxLevel)
        {
            expLevelNeed.Add(initexp);
            initexp = Mathf.RoundToInt((initexp + 3
            ) * 1.035f);
        }
    }
    // 技能点
    private int currentSkillPoint;

    public int CurrentSkillPoint
    {
        get
        {
            return currentSkillPoint;
        }
        set
        {
            currentSkillPoint = value;
            if (SkillPointControl.instance != null)
                SkillPointControl.instance.UpdateSkillPointText();
        }
    }
    public void GetExp(int getexp)
    {
        currentExp += getexp;

        if (currentExp >= expLevelNeed[currentLevel])
        {
            currentExp -= expLevelNeed[currentLevel];
            this.CurrentSkillPoint++;
            currentLevel++;
            Vector3 worldPosition = MainPlayer.instance.transform.position;
            Vector2 screenPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, worldPosition);
            UIControl.instance.ShowTips("升级了！", screenPosition);

            DestoryAnimeControl.instance.CreateDestoryAnime(9, MainPlayer.instance.transform.position);
            if (!GlobalControl.instance.isHasShowLevelUpTips)
            {
                GlobalControl.instance.isHasShowLevelUpTips = true;
                StartCoroutine(ShowLevelUpTips());
            }
            // currentExp = expLevelNeed[currentLevel];
        }
        UIControl.instance.UpdateExp(currentExp, expLevelNeed[currentLevel], currentLevel);
    }
    IEnumerator ShowLevelUpTips()
    {
        yield return new WaitForSeconds(1.6f);
        UIControl.instance.ShowBigTips("BigTips1");
    }

    public void CreateDrop(Vector3 position, int expToGive)
    {
        if (expToGive <= 0) return;
        for (int i = 0; i < expToGive; i++)
            CreateDrop(position);

    }

    public void CreateDrop(Vector3 position)
    {
        Instantiate(drops, position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0f), quaternion.identity, AssetControl.instance.transform);
    }

    public void SetYukariPlane()
    {
        // // 如果当前经验值小于50%，那么返回
        // if (currentExp < expLevelNeed[currentLevel] * expNeedRate_Yukari * 0.01f)
        // {
        //     return;
        // }
        // else
        // {
        //     // 如果当前经验值大于50%，那么扣除50%的经验值
        //     currentExp -= Mathf.RoundToInt(expLevelNeed[currentLevel] * expNeedRate_Yukari * 0.01f);
        //     currentLevelfloat += expNeedRate_Yukari * 0.01f;
        //     currentLevel = (int)currentLevelfloat;
        //     GetExp(0);
        // }

        if (UIControl.instance.levelupPanel.activeSelf)
        {
            return;
        }

        // 关闭BOSS血条
        UIControl.instance.BossHealth.SetActive(false);

        Time.timeScale = 0f;

        if (currentLevel >= expLevelNeed.Count)
        {
            currentLevel = expLevelNeed.Count - 1;
        }

        UIControl.instance.levelupPanel.SetActive(true);

        // 界面布局开始
        this.freeItemBagGameObject.SetActive(true);
        this.luckyBagGameObject.SetActive(true);
        this.reforgingBagGameObject.SetActive(false);
        this.upgradeItemBagGameObject.SetActive(false);
        this.randomShopBagGameObject.SetActive(false);
        // 设置紫的对话
        SetYukariPlaneText();
        // 设置免费物品
        SetFreeItem();
        // 设置福袋物品
        SetLuckyBag();

        GlobalControl.instance.isYukariLeaving = true;

    }
    public void SetNitoriPlane()
    {
        if (UIControl.instance.levelupPanel.activeSelf)
        {
            return;
        }

        // // 关闭BOSS血条
        // UIControl.instance.BossHealth.SetActive(false);

        // Time.timeScale = 0f;

        // currentExp -= expLevelNeed[currentLevel];
        // currentLevel++;

        // if (currentLevel >= expLevelNeed.Count)
        // {
        //     currentLevel = expLevelNeed.Count - 1;
        // }

        UIControl.instance.levelupPanel.SetActive(true);

        // 界面布局开始
        this.freeItemBagGameObject.SetActive(false);
        this.luckyBagGameObject.SetActive(false);
        this.reforgingBagGameObject.SetActive(true);
        this.upgradeItemBagGameObject.SetActive(true);
        this.randomShopBagGameObject.SetActive(true);
        // 设置河城荷取对话
        SetNitoriPlaneText();
        // 设置重铸物品
        SetReforgingItem();
        // 设置升级物品
        SetUpgradeItem();
        // 设置随机商店
        SetRandomShop();

        GlobalControl.instance.isNitoriLeaving = true;

    }
    // 设置免费物品
    public void SetFreeItem()
    {
        GetFreeTime = 0;
        isGetFreeItem = false;
        for (int i = 0; i < 3; i++)
        {
            freeItemBagList[i].gameObject.SetActive(true);
            freeItemBagList[i].itemInfo = ItemControl.instance.itemGuide[0];
            freeItemBagList[i].SetNoItem();

            int randomitem = EnemyDropTable.GetGroupItem((int)DropTableGroupEnum.FreeItem);

            //判断，如果是一级符卡给7个，如果是二级符卡给5个。其余都是1个
            if (randomitem == 3 || randomitem == 12 || randomitem == 13 || randomitem == 19)
            {
                freeItemBagList[i].AddItem(ItemControl.instance.itemGuide[randomitem], 7);
            }
            else if (randomitem == 20 || randomitem == 21 || randomitem == 38 || randomitem == 39)
            {
                freeItemBagList[i].AddItem(ItemControl.instance.itemGuide[randomitem], 5);
            }
            else
            {
                freeItemBagList[i].AddItem(ItemControl.instance.itemGuide[randomitem]);
            }
        }
    }
    // 免费物品获取，因为逻辑略有差别，所以单独维护。
    // 只许拿取，不许放入。
    public void OnClickBag_FreeItem_1()
    {
        if (!ExpLevelControl.instance.isGetFreeItem)
        {
            // 添加到背包
            if (this.AddItemToBag_Check(freeItemBagList[0].itemInfo, freeItemBagList[0].itemCount))
            {
                // 播放音效
                SFXManger.instance.PlaySFX(2);
                // 删除物品
                freeItemBagList[0].SetNoItem();
                isGetFreeItem = true;

                string[] texts = new string[]
{
                            "『喜欢就拿走吧。反正是路边捡的（小声）』",
                            "『不要问我使用方法啊，我不知道啊。物品说明上面不是有写吗？』",
                            "『嘛，灵梦，好好加油吧』",
                            "『不要让我失望喔』",
                            "『高级道具需要自己合成喔』",
};
                SetPlaneTextByDOTWEEN(texts[Random.Range(0, texts.Length)], "yukari_q_0");
            }
            else
            {
                SetPlaneTextByDOTWEEN("『灵梦，你的背包满了，东西放不进去了。打开背包整理一下啊。』", "yukari_q_0");
                return;
            }
        }
        else
        {
            GetFreeTime++;
            if (GetFreeTime < 4)
            {
                string[] texts = new string[]
                {
                                "『说好了只能拿一个吧，你这家伙，太贪心了吧』",
                                "『灵梦？干啥呢』",
                                "『别点了，再点也不会给你的』",
                };
                SetPlaneTextByDOTWEEN(texts[Random.Range(0, texts.Length)], "yukari_q_0");
            }
            else
            {
                string[] texts = new string[]
                {
                                "『让我清静一会儿吧』",
                                "『……』",
                };
                SetPlaneTextByDOTWEEN(texts[Random.Range(0, texts.Length)], "yukari_q_0");

                foreach (var bag in freeItemBagList)
                {
                    bag.isShowItemInfo = false;
                }


                freeItemBagList[0].gameObject.SetActive(false);
                freeItemBagList[1].gameObject.SetActive(false);
                freeItemBagList[2].gameObject.SetActive(false);

            }
        }
    }
    public void OnClickBag_FreeItem_2()
    {
        if (!ExpLevelControl.instance.isGetFreeItem)
        {
            // 添加到背包
            if (this.AddItemToBag_Check(freeItemBagList[1].itemInfo, freeItemBagList[1].itemCount))
            {
                // 播放音效
                SFXManger.instance.PlaySFX(2);
                // 删除物品
                freeItemBagList[1].SetNoItem();
                isGetFreeItem = true;

                string[] texts = new string[]
{
                            "『喜欢就拿走吧。反正是路边捡的（小声）』",
                            "『不要问我使用方法啊，我不知道啊。物品说明上面不是有写吗？』",
                            "『嘛，好好加油吧』"
};
                SetPlaneTextByDOTWEEN(texts[Random.Range(0, texts.Length)], "yukari_q_0");
            }
            else
            {
                SetPlaneTextByDOTWEEN("『灵梦，你的背包满了，东西放不进去了。打开背包整理一下啊。』", "yukari_q_0");
                return;
            }
        }
        else
        {
            GetFreeTime++;
            if (GetFreeTime < 4)
            {
                string[] texts = new string[]
                {
                                "『说好了只能拿一个吧，你这家伙，太贪心了吧』",
                                "『灵梦？干啥呢』",
                                "『别点了，再点也不会给你的』",
                };
                SetPlaneTextByDOTWEEN(texts[Random.Range(0, texts.Length)], "yukari_q_0");
            }
            else
            {
                string[] texts = new string[]
                {
                                "『让我清静会儿吧』",
                                "『……』",
                };
                SetPlaneTextByDOTWEEN(texts[Random.Range(0, texts.Length)], "yukari_q_0");
                freeItemBagList[0].gameObject.SetActive(false);
                freeItemBagList[1].gameObject.SetActive(false);
                freeItemBagList[2].gameObject.SetActive(false);
            }
        }
    }
    public void OnClickBag_FreeItem_3()
    {
        if (!ExpLevelControl.instance.isGetFreeItem)
        {
            // 添加到背包
            if (this.AddItemToBag_Check(freeItemBagList[2].itemInfo, freeItemBagList[2].itemCount))
            {
                // 播放音效
                SFXManger.instance.PlaySFX(2);
                // 删除物品
                freeItemBagList[2].SetNoItem();
                isGetFreeItem = true;

                string[] texts = new string[]
{
                            "『喜欢就拿走吧。反正是路边捡的（小声）』",
                            "『不要问我使用方法啊，我不知道啊。物品说明上面不是有写吗？』",
                            "『嘛，好好加油吧』",
                            "『异变？不要问我，自己解决去吧』",
};
                SetPlaneTextByDOTWEEN(texts[Random.Range(0, texts.Length)], "yukari_q_0");
            }
            else
            {
                SetPlaneTextByDOTWEEN("『灵梦，你的背包满了，东西放不进去了。打开背包整理一下啊。』", "yukari_q_0");
                return;
            }
        }
        else
        {
            GetFreeTime++;
            if (GetFreeTime < 4)
            {
                string[] texts = new string[]
                {
                                "『说好了只能拿一个吧，你这家伙，太贪心了吧』",
                                "『灵梦？干啥呢』",
                                "『别点了，再点也不会给你的』",
                };
                SetPlaneTextByDOTWEEN(texts[Random.Range(0, texts.Length)], "yukari_q_0");
            }
            else
            {
                string[] texts = new string[]
                {
                                "『让我清静会儿吧』",
                                "『……』",
                };
                SetPlaneTextByDOTWEEN(texts[Random.Range(0, texts.Length)], "yukari_q_0");
                freeItemBagList[0].gameObject.SetActive(false);
                freeItemBagList[1].gameObject.SetActive(false);
                freeItemBagList[2].gameObject.SetActive(false);
            }
        }
    }
    private void SetNitoriPlaneText()
    {
        PlaneNameText.text = "河城荷取";
        string[] texts = new string[]
        {
        "『欢迎光临~！』",
        "『恭喜恭喜~』",
        "『辛苦了~』"
        };
        SetPlaneTextByDOTWEEN(texts[Random.Range(0, texts.Length)], "nitori_q_0");
    }
    private void SetYukariPlaneText()
    {
        PlaneNameText.text = "八云紫";
        string[] texts = new string[]
 {
        "『怎么？灵梦你又需要帮助了吗？』",
        "『啊，又是灵梦啊，你随便看看吧』",
        "『好困啊啊啊ZZZzzzzz』",
        "『随便找了点东西，看看喜欢哪个就挑一个拿走吧』",
        "『ZZZZzzzz（打盹中）』",
 };
        SetPlaneTextByDOTWEEN(texts[Random.Range(0, texts.Length)], "yukari_q_0");

    }
    private void SetPlaneTextByDOTWEEN(string str, string imagestr)
    {

        if (textTween != null)
            textTween.Kill();
        textTween = PlaneText.DOText(str, str.Length * 0.03f).SetUpdate(true);

        PlaneImage.sprite = Resources.Load<Sprite>("lihui/" + imagestr);
    }

    // 设置福袋物品
    private void SetLuckyBag()
    {
        luckyBag.SetNoItem();

        luckyBag.AddItem(ItemControl.instance.itemGuide[80]);

        luckyBagPrice = 50 + currentLevel * 5;

        luckyBagPriceText.text = "福袋的价格为：" + luckyBagPrice.ToString();
    }
    // 购买福袋
    public void OnClickBag_BuyLuckBag()
    {
        if (AssetControl.instance.currentPoint >= luckyBagPrice)
        {
            if (!this.AddItemToBag_Check(ItemControl.instance.itemGuide[80]))
            {
                SetPlaneTextByDOTWEEN("『灵梦，你的背包满了，东西放不进去了。打开背包整理一下啊。』", "yukari_q_0");
                return;
            }

            AssetControl.instance.ReducePoint(luckyBagPrice);
            SFXManger.instance.PlaySFX(7);

            luckyBag.SetNoItem();


            string[] texts = new string[]
     {
        "『开不出想要的不要来找我喔。』",
        "『本商品的全部收益将用于给式神购买油炸豆腐。』",
        "『蓝那个家伙吃的也太多了。』",
     };
            SetPlaneTextByDOTWEEN(texts[Random.Range(0, texts.Length)], "yukari_q_9");
        }
        else
        {
            SFXManger.instance.PlaySFX(3);
            SetPlaneTextByDOTWEEN("『灵梦，你的钱不够，别想从我这里套白食吃。』", "yukari_q_0");
            return;
        }


    }
    // 重铸物品
    private void SetReforgingItem()
    {
        reforgingBag.SetNoItem();
        reforgingPriceText.text = "请将需要重铸的物品放入重铸槽";
        reforgingButton.interactable = false;
        if (!GlobalControl.instance.isNitoriLeaving)
            reforgingTimes = 0;
    }
    // 重铸物品点击
    public void OnClickBag_Reforging()
    {
        if (reforgingBag.itemInfo.itemId == 0)
        {

            if (ItemControl.instance.hoverItem.itemId == 0)
            {
                // UIControl.instance.ShowTips("需要放入物品才能重铸", Input.mousePosition);
                reforgingPriceText.text = "请将需要重铸的物品放入重铸槽";
                SetPlaneTextByDOTWEEN("『重铸啦，简单来讲就是洗刷词条。如果有遇到过那种装备的话一下子就能理解了』", "nitori_q_0");
                return;
            }

            if (!ItemControl.instance.hoverItem.hasQuality)
            {
                reforgingPriceText.text = "请将需要重铸的物品放入重铸槽";
                UIControl.instance.ShowTips("该物品类型不能重铸", Input.mousePosition);

                SetPlaneTextByDOTWEEN("『这件装备没有词条属性，不能重铸喔？只有部分被动装备才可以重铸喔』", "nitori_q_0");
                return;
            }

            reforgingBag.AddItem(ItemControl.instance.hoverItem, ItemControl.instance.hoverItemCount);

            ItemControl.instance.hoverItem = ItemControl.instance.itemGuide[0];
            ItemControl.instance.SethoverItemCountText(0);

            // 品质越好越便宜
            reforgingPrice = CalReforgingPrice(reforgingBag);

            reforgingPriceText.text = "重置价格为：" + reforgingPrice.ToString();
            reforgingButton.interactable = true;

            SetPlaneTextByDOTWEEN("『嗯嗯……这件装备的话，这个价格……怎么样？』", "nitori_q_0");
        }
        else
        {
            // 设置鼠标上的物品
            ItemControl.instance.hoverItem = reforgingBag.itemInfo;
            ItemControl.instance.SethoverItemCountText(reforgingBag.itemCount);

            // 图片切换
            ItemControl.instance.hoverImage.sprite = reforgingBag.itemInfo.itemImage;

            // 移除物品
            reforgingBag.SetNoItem();

            reforgingPriceText.text = "请将需要重铸的物品放入重铸槽";
            reforgingButton.interactable = false;
            if (reforgingTimes > 0)
                SetPlaneTextByDOTWEEN("『多谢惠顾~』", "nitori_q_9");
            else
            {
                SetPlaneTextByDOTWEEN("『很贵吗？没有吧，这么多年一直是这个价格，有没有认真退治捡P点？』", "nitori_q_9");

            }
        }
    }
    public void OnClickReforging()
    {
        if (reforgingBag.itemInfo.itemId == 0)
        {
            UIControl.instance.ShowTips("请将需要重铸的物品放入重铸槽", Input.mousePosition);
            return;
        }
        else
        {
            if (!reforgingBag.itemInfo.hasQuality)
            {
                PlaneText.text = "『?』";
                UIControl.instance.ShowTips("绕过BUG请注意", Input.mousePosition);
            }


            if (AssetControl.instance.currentPoint >= reforgingPrice)
            {
                AssetControl.instance.ReducePoint(reforgingPrice);
                SFXManger.instance.PlaySFX(25);

                // 重铸
                int random = Random.Range(0, 100);
                reforgingBag.itemInfo.Quality = random;
                reforgingBag.itemInfo.itemCiti = ItemControl.instance.SetItemQualityByQuality(reforgingBag.itemInfo.Quality);
                if (random >= 95)
                {
                    string[] texts = new string[]
                    {
                            "『完美品质！』",
                            "『哇哦！这个品质，可真是不得了！』",
                            "『今天状态绝赞！』"
                    };
                    SetPlaneTextByDOTWEEN(texts[Random.Range(0, texts.Length)], "nitori_q_9");
                }
                else if (random >= 85)
                {
                    string[] texts = new string[]
                    {
                            "『超强喔！』",
                            "『已经很好啦，还要再试一试完美品质吗』",
                            "『今天状态不错！』"
                    };
                    SetPlaneTextByDOTWEEN(texts[Random.Range(0, texts.Length)], "nitori_q_9");
                }
                else if (random >= 60)
                {
                    string[] texts = new string[]
                    {
                            "『不错吧？多夸赞夸赞我喔~』",
                            "『这个品质不错啦，还要试一试吗』",
                            "『今天状态还好』"
                    };
                    SetPlaneTextByDOTWEEN(texts[Random.Range(0, texts.Length)], "nitori_q_9");
                }
                else if (random >= 25)
                {
                    string[] texts = new string[]
                    {
                            "『嘛，马马虎虎……』",
                            "『正常品质啦，别对我手艺有那么多抱怨啦』",
                            "『今天状态一般般~』"
                    };
                    SetPlaneTextByDOTWEEN(texts[Random.Range(0, texts.Length)], "nitori_q_9");
                }
                else
                {
                    string[] texts = new string[]
                    {
                            "『今天状态不太好呢……』",
                            "『……人生不如意十有八九，别介意别介意~』",
                            "『哎呀呀，这可不能怪我（心虚）』",
                            "『……今天妖怪之山很晴朗呢』"
                    };
                    SetPlaneTextByDOTWEEN(texts[Random.Range(0, texts.Length)], "nitori_q_4");
                }

                // 品质越好越便宜
                reforgingPrice = CalReforgingPrice(reforgingBag);

                reforgingPriceText.text = "重置价格为：" + reforgingPrice.ToString();
                if (reforgingPrice > AssetControl.instance.currentPoint)
                    reforgingButton.interactable = false;
                else
                    reforgingButton.interactable = true;

                reforgingTimes++;
            }
            else
            {
                SFXManger.instance.PlaySFX(3);
                SetPlaneTextByDOTWEEN("『重铸稍微收点手续费，不介意吧？（暗示钱不够）』", "nitori_q_9");
                return;
            }
        }
    }

    public void SetUpgradeItem()
    {
        upgradeItemBag.SetNoItem();
        upgradePriceText.text = "请将需要升级的物品放入升级槽";
        upgradeButton.interactable = false;

        if (!GlobalControl.instance.isNitoriLeaving)
            upgradeTimes = 0;
    }
    // 升级物品
    public void OnClickBag_Upgrade()
    {
        {
            if (upgradeItemBag.itemInfo.itemId == 0)
            {

                if (ItemControl.instance.hoverItem.itemId == 0)
                {
                    UIControl.instance.ShowTips("需要放入物品才能升级", Input.mousePosition);
                    upgradePriceText.text = "请将需要升级的物品放入升级槽";
                    SetPlaneTextByDOTWEEN("『升级啦。只有名字里有等级的符卡才能升级喔。』", "nitori_q_0");
                    return;
                }
                if (!ItemControl.instance.hoverItem.isEquipmentSC)
                {
                    upgradePriceText.text = "请将需要升级的物品放入升级槽";
                    UIControl.instance.ShowTips("该物品类型不能升级", Input.mousePosition);

                    SetPlaneTextByDOTWEEN("『这件道具不能升级喔？只有名字里有等级的符卡才能升级喔』", "nitori_q_0");
                    return;
                }
                if (ItemControl.instance.hoverItem.isEquipmentSCLevel >= ItemControl.instance.hoverItem.maxEquipmentSCLevel)
                {
                    upgradePriceText.text = "请将需要升级的物品放入升级槽";
                    UIControl.instance.ShowTips("该物品已经满级", Input.mousePosition);

                    SetPlaneTextByDOTWEEN("『这件装备已经满级了喔？目前大概15级就是满级了。』", "nitori_q_0");
                    return;
                }

                // 设置升级物品
                upgradeItemBag.AddItem(ItemControl.instance.hoverItem, ItemControl.instance.hoverItemCount);

                ItemControl.instance.hoverItem = ItemControl.instance.itemGuide[0];
                ItemControl.instance.SethoverItemCountText(0);

                // 计算价格  (8 + 个人等级 * 2) *  (物品等级*15%  + 1) * (1 + 次数*15%)
                upgradePrice = CalUpdatePrice(upgradeItemBag);

                upgradePriceText.text = "升级价格为：" + upgradePrice.ToString();
                if (upgradePrice > AssetControl.instance.currentPoint)
                    upgradeButton.interactable = false;
                else
                    upgradeButton.interactable = true;

                SetPlaneTextByDOTWEEN("『嗯嗯……这张符卡的话，这个价格……怎么样？至少目前升级是100%成功的』", "nitori_q_0");

            }
            else
            {
                // 设置鼠标上的物品
                ItemControl.instance.hoverItem = upgradeItemBag.itemInfo;
                ItemControl.instance.SethoverItemCountText(upgradeItemBag.itemCount);

                // 图片切换
                ItemControl.instance.hoverImage.sprite = upgradeItemBag.itemInfo.itemImage;

                // 移除物品
                upgradeItemBag.SetNoItem();

                upgradePriceText.text = "请将需要重铸的物品放入重铸槽";
                upgradeButton.interactable = false;
                if (upgradeTimes > 0)
                    SetPlaneTextByDOTWEEN("『巫女你好啊，我是来自妖怪之山的荷取~（装模作样）』", "nitori_q_9");
                else
                {
                    SetPlaneTextByDOTWEEN("『很贵吗？没有吧，这么多年一直是这个价格，有没有认真退治捡P点？』", "nitori_q_9");

                }
            }
        }
    }

    public void OnClickUpgrade()
    {
        if (upgradeItemBag.itemInfo.itemId == 0)
        {
            UIControl.instance.ShowTips("请将需要升级的物品放入升级槽", Input.mousePosition);
            return;
        }
        else
        {
            if (AssetControl.instance.currentPoint >= upgradePrice)
            {
                AssetControl.instance.ReducePoint(upgradePrice);
                SFXManger.instance.PlaySFX(25);

                upgradeTimes++;

                // 升级
                upgradeItemBag.itemInfo.UpgradeEquipmentSC();

                // 重新计算价格  (基础50 + 个人等级 * 10) *  (物品等级*20%  + 1) * (1 + 次数*20%)
                upgradePrice = CalUpdatePrice(upgradeItemBag);

                upgradePriceText.text = "升级价格为：" + upgradePrice.ToString();


                if (upgradeItemBag.itemInfo.isEquipmentSCLevel >= upgradeItemBag.itemInfo.maxEquipmentSCLevel)
                {
                    SetPlaneTextByDOTWEEN("『满级咯！』", "nitori_q_9");
                    upgradeButton.interactable = false;
                }
                else
                {
                    SetPlaneTextByDOTWEEN("『嗯，升级成功了，看一下新属性吧』", "nitori_q_9");
                }

            }
            else
            {
                SFXManger.instance.PlaySFX(3);
                SetPlaneTextByDOTWEEN("『钱，不够喔』", "nitori_q_4");
                return;
            }
        }
    }
    private int CalUpdatePrice(ItemBag itembag)
    {
        return (int)((12 + currentLevel * 3) * (upgradeItemBag.itemInfo.isEquipmentSCLevel * 0.17f + 1) * (1 + upgradeTimes * 0.17f));
    }
    private int CalReforgingPrice(ItemBag itemBag)
    {
        if (reforgingBag.itemInfo.price > 0)
            return (int)((60 + currentLevel * 5) * ((50 + reforgingBag.itemInfo.Quality) * 0.01f) * Mathf.Sqrt(reforgingBag.itemInfo.price / 100) * (1 + reforgingTimes * 0.1f));
        else
            return (int)((60 + currentLevel * 10) * ((50 + reforgingBag.itemInfo.Quality) * 0.01f) * (1 + reforgingTimes * 0.1f));
    }
    // 随机商店
    private void SetRandomShop()
    {
        if (!GlobalControl.instance.isNitoriLeaving)
        {
            for (int i = 0; i < randomShopBag.Count; i++)
            {

                randomShopBag[i].SetNoItem();

                randomShopBag[i].AddItem(UIControl.instance.GetRandomItem());
                //价格= 基础价格 * (1 + 当前等级*4%) * (0.9~1.1)
                randomShopPrice[i] = (int)(randomShopBag[i].itemInfo.price * Random.Range(0.9f, 1.1f) * (1 + currentLevel * 0.04f));
                randomShopPriceText[i].text = randomShopPrice[i].ToString() + "P点";
            }
        }
    }
    // EventTrigger 组件本身不支持传递参数，所以写了三个
    public void OnClickRandomShop_1()
    {
        if (AssetControl.instance.currentPoint >= randomShopPrice[0])
        {

            if (!this.AddItemToBag_Check(randomShopBag[0].itemInfo))
            {
                SetPlaneTextByDOTWEEN("『你的背包满了喔？打开背包整理一下吧，把不需要的东西丢掉吧』", "nitori_q_0");
                return;
            }

            AssetControl.instance.ReducePoint(randomShopPrice[0]);
            SFXManger.instance.PlaySFX(7);

            randomShopBag[0].SetNoItem();

            SetPlaneTextByDOTWEEN("『多谢惠顾~产品如有不满意概不退货~』", "nitori_q_0");
        }
        else
        {
            SFXManger.instance.PlaySFX(3);
            string[] texts = new string[]
            {
                            "『钱，不够啊』",
                            "『钱，不够哦』",
                            "『没钱就别乱点喂』"
            };
            SetPlaneTextByDOTWEEN(texts[Random.Range(0, texts.Length)], "nitori_q_4");
            return;
        }
    }
    public void OnClickRandomShop_2()
    {
        if (AssetControl.instance.currentPoint >= randomShopPrice[1])
        {

            if (!this.AddItemToBag_Check(randomShopBag[1].itemInfo))
            {
                SetPlaneTextByDOTWEEN("『你的背包满了喔？打开背包整理一下吧，把不需要的东西丢掉吧』", "nitori_q_0");
                return;
            }

            AssetControl.instance.ReducePoint(randomShopPrice[1]);
            SFXManger.instance.PlaySFX(7);

            randomShopBag[1].SetNoItem();

            SetPlaneTextByDOTWEEN("『多谢惠顾~产品如有不满意概不退货~』", "nitori_q_0");
        }
        else
        {
            SFXManger.instance.PlaySFX(3);
            string[] texts = new string[]
            {
                            "『钱，不够啊』",
                            "『钱，不够哦』",
                            "『没钱就别乱点喂』"
            };
            SetPlaneTextByDOTWEEN(texts[Random.Range(0, texts.Length)], "nitori_q_4");
            return;
        }
    }
    public void OnClickRandomShop_3()
    {
        if (AssetControl.instance.currentPoint >= randomShopPrice[2])
        {

            if (!this.AddItemToBag_Check(randomShopBag[2].itemInfo))
            {
                SetPlaneTextByDOTWEEN("『你的背包满了喔？打开背包整理一下吧，把不需要的东西丢掉吧』", "nitori_q_0");
                return;
            }

            AssetControl.instance.ReducePoint(randomShopPrice[2]);
            SFXManger.instance.PlaySFX(7);

            randomShopBag[2].SetNoItem();

            SetPlaneTextByDOTWEEN("『多谢惠顾~产品如有不满意概不退货~』", "nitori_q_0");
        }
        else
        {
            SFXManger.instance.PlaySFX(3);
            string[] texts = new string[]
            {
                            "『钱，不够啊』",
                            "『钱，不够哦』",
                            "『没钱就别乱点喂』"
            };
            SetPlaneTextByDOTWEEN(texts[Random.Range(0, texts.Length)], "nitori_q_4");
            return;
        }
    }

    private bool AddItemToBag_Check(item itemInfo, int itemCount = 1)
    {
        if (itemInfo.hasQuality)
        {
            // 不改变原值预制体的值
            item _itemForCopy = itemInfo.Clone();

            _itemForCopy.Quality = Random.Range(0, 100);
            if (_itemForCopy.Quality < 25)
                _itemForCopy.itemCiti = ItemQuality.Inferior;
            else if (_itemForCopy.Quality < 60)
                _itemForCopy.itemCiti = ItemQuality.Normal;
            else if (_itemForCopy.Quality < 85)
                _itemForCopy.itemCiti = ItemQuality.Good;
            else if (_itemForCopy.Quality < 95)
                _itemForCopy.itemCiti = ItemQuality.Excellent;
            else
                _itemForCopy.itemCiti = ItemQuality.Legendary;

            return ItemControl.instance.AddItemToBag(_itemForCopy, itemCount);

        }
        else
        {
            return ItemControl.instance.AddItemToBag(itemInfo, itemCount);
        }

    }

}