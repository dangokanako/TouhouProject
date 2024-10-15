using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
//using System.Diagnostics;
using UnityEditor;
public class ItemControl : MonoBehaviour
{
    // 生成一个全局唯一的ID
    private string uniqueId = System.Guid.NewGuid().ToString();
    public static ItemControl instance;
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
    }
    // 被选中的物品栏。但是注意，从1开始。
    public int indexOfChoose;
    public int MaxOfBags;
    // 背包LIST
    public List<ItemBag> itembagList;
    // 仓库LIST
    public int MaxOfWarehouse;
    public List<ItemBag> WarehouseList;
    // 装备LIST
    public List<ItemBag> EquipmentList;

    [Header("显示道具信息面板")]
    public GameObject itemInfoPlane;
    public TMP_Text itemPlaneName;
    public TMP_Text itemPlaneInfo;
    public TMP_Text itemPlaneRemark;
    public TMP_Text itemFormulation_Text;
    public float isShowItemCount;

    // 显示配方用的新格子 废弃
    public GameObject itemFormulation;

    [Header("合成面板")]
    public GameObject combinePlane;
    public bool isCombinePlane = false;
    // 合成面板的材料输入
    public List<ItemBag> combineItemGuideList;
    // 合成面板的成品输出
    public ItemBag combineResult;
    // 垃圾桶
    public ItemBag trashCan;

    [Header("鼠标悬浮相关")]
    // 点击时的悬浮图片
    public UnityEngine.UI.Image hoverImage;
    // 悬浮在鼠标上的物品和物品数量
    public item hoverItem;
    public int hoverItemCount;
    public TMP_Text hoverItemCountText;

    // 道具列表
    public List<item> itemGuide;
    // 鼠标是否在UI上
    public bool isMouseOverUI;

    // 根据道具ID返回道具名称
    public string GetItemName(int id)
    {
        return itemGuide[id].itemName;
    }
    public void SethoverItemCountText(int num)
    {
        if (num == 1 || num == 0)
        {
            hoverItemCountText.gameObject.SetActive(false);
            hoverItemCount = num;
            return;
        }
        else
        {
            hoverItemCountText.gameObject.SetActive(true);
            hoverItemCount = num;
            hoverItemCountText.text = num.ToString();
        }
    }
    private IEnumerator AutoHealSP()
    {
        while (true)
        {
            if (GlobalControl.instance.isBattle)
            {
                if (PlayerHealthControl.instance.currentSP < 1)
                {
                    // 如果有4编号、或者28编号的物品，那么开始自动调用使用函数
                    foreach (var itembag in ItemControl.instance.itembagList)
                    {
                        if (itembag.itemInfo != null)
                        {
                            if (itembag.itemInfo.itemId == 4 || itembag.itemInfo.itemId == 28)
                            {
                                itembag.itemInfo.Use();
                                itembag.depleteItem();
                                break;
                            }
                        }
                    }
                }

            }
            // 等待0.5秒
            yield return new WaitForSeconds(0.5f);
        }
    }
    void Start()
    {
        // 引用类型的初始化
        hoverItem = itemGuide[0];

        // 合成面板初始化
        instance.combineItemGuideList[0].isActive = true;
        instance.combineItemGuideList[1].isActive = true;
        instance.combineResult.isActive = true;

        // 垃圾桶格子初始化
        instance.trashCan.isActive = true;

        // 合成配方初始化
        // 三相之力内循环
        // BUFF1 + BUFF 2 = BUFF4
        AddRecipe(new List<int> { 11, 22 }, new CraftingResult(61, 1));
        // BUFF2 + BUFF 3 = BUFF5
        AddRecipe(new List<int> { 22, 23 }, new CraftingResult(62, 1));
        // BUFF1 + BUFF 3 = BUFF6
        AddRecipe(new List<int> { 11, 23 }, new CraftingResult(63, 1));
        // BUFF4 + BUFF3 = BUFF7
        AddRecipe(new List<int> { 61, 23 }, new CraftingResult(64, 1));
        // BUFF5 + BUFF1 = BUFF7
        AddRecipe(new List<int> { 62, 11 }, new CraftingResult(64, 1));
        // BUFF6 + BUFF2 = BUFF7
        AddRecipe(new List<int> { 63, 22 }, new CraftingResult(64, 1));
        // 1 + 2 + 3 = 7
        AddRecipe(new List<int> { 11, 22, 23 }, new CraftingResult(64, 1));

        // 三相之力外循环
        AddRecipe(new List<int> { 11 }, new CraftingResult(93, 1));
        AddRecipe(new List<int> { 22 }, new CraftingResult(93, 1));
        AddRecipe(new List<int> { 23 }, new CraftingResult(93, 1));
        AddRecipe(new List<int> { 61 }, new CraftingResult(94, 1));
        AddRecipe(new List<int> { 62 }, new CraftingResult(94, 1));
        AddRecipe(new List<int> { 63 }, new CraftingResult(94, 1));
        AddRecipe(new List<int> { 64 }, new CraftingResult(95, 1));
        AddRecipe(new List<int> { 93, 93 }, new CraftingResult(94, 1));



        // 密封组
        // 空白符卡 + 羽毛 = 夜雀
        AddRecipe(new List<int> { 87, 5 }, new CraftingResult(12, 7));
        // 夜雀 + 微弱的力量
        AddRecipe(new List<int> { 12, 93 }, new CraftingResult(38, 7));

        // 空白符卡 + 冰块 = 冰符「Icicle Fall」
        AddRecipe(new List<int> { 87, 4 }, new CraftingResult(13, 7));
        // 冰符「Icicle Fall」 + 微弱的力量 = 15 雪符「Diamond Blizzard」
        AddRecipe(new List<int> { 13, 93 }, new CraftingResult(21, 7));

        // 空白符卡 + 飞刀 = 操弄玩偶
        AddRecipe(new List<int> { 87, 31 }, new CraftingResult(3, 7));
        // 操弄玩偶 + 微弱的力量 = 杀人玩偶
        AddRecipe(new List<int> { 3, 93 }, new CraftingResult(39, 7));

        // 空白符卡 + 阴阳玉=  灵符「梦想妙珠」
        AddRecipe(new List<int> { 87, 42 }, new CraftingResult(19, 7));
        // 灵符「梦想妙珠」+ 微弱的力量 = 灵符「梦想封印」
        AddRecipe(new List<int> { 19, 93 }, new CraftingResult(20, 7));

        // 空白符卡 + 微弱的力量 + 森蘑菇 = 八卦炉1
        AddRecipe(new List<int> { 87, 93, 2 }, new CraftingResult(81, 1));
        // 八卦炉1  + 普通的力量 = 八卦炉2
        AddRecipe(new List<int> { 81, 94 }, new CraftingResult(88, 1));
        // 八卦炉2  + 强大的力量 = 八卦炉3
        AddRecipe(new List<int> { 88, 95 }, new CraftingResult(89, 1));
        // 八卦炉3 + 魔理沙的信物 = 八卦炉4
        AddRecipe(new List<int> { 89, 32 }, new CraftingResult(101, 1));

        // 空白符卡 + 微弱的力量 + 驱魔针 = 驱魔针1
        AddRecipe(new List<int> { 87, 93, 90 }, new CraftingResult(79, 1));
        // 驱魔针1  + 普通的力量 = 驱魔针2
        AddRecipe(new List<int> { 79, 94 }, new CraftingResult(91, 1));
        // 驱魔针2  + 强大的力量 = 驱魔针3
        AddRecipe(new List<int> { 91, 95 }, new CraftingResult(92, 1));

        // 空白符卡 + 微弱的力量 + 阴阳玉 = 阴阳玉1
        AddRecipe(new List<int> { 87, 93, 42 }, new CraftingResult(76, 1));
        // 阴阳玉1 + 普通的力量 = 阴阳玉2
        AddRecipe(new List<int> { 76, 94 }, new CraftingResult(77, 1));
        // 阴阳玉2 + 强大的力量  = 阴阳玉3
        AddRecipe(new List<int> { 77, 95 }, new CraftingResult(78, 1));

        // 空白 + 微弱的力量 = 起点
        AddRecipe(new List<int> { 87, 93 }, new CraftingResult(6, 1));
        // 起点 + 普通的力量 =第一步
        AddRecipe(new List<int> { 6, 94 }, new CraftingResult(33, 1));
        // 第一步升级 逐梦者
        AddRecipe(new List<int> { 33, 95 }, new CraftingResult(60, 1));

        // 三月精
        // 空白 + 日光 = (仮)阳光「Sunshine Needle」
        // 空白 + 月光 = (仮)月光「Moon Stillness」
        // 空白 + 星光 = (仮)星光「Star Laser」
        AddRecipe(new List<int> { 87, 11 }, new CraftingResult(108, 1));
        AddRecipe(new List<int> { 87, 22 }, new CraftingResult(107, 1));
        AddRecipe(new List<int> { 87, 23 }, new CraftingResult(106, 1));

        // (仮)阳光「Sunshine Needle」  + 微弱的力量 = 阳光「Sunshine Needle」
        //(仮)月光「Moon Stillness」 + 微弱的力量 = 月光「Moon Stillness」 
        // (仮)星光「Star Laser」 + 微弱的力量 = 星光「Star Laser」
        AddRecipe(new List<int> { 93, 108 }, new CraftingResult(118, 1));
        AddRecipe(new List<int> { 93, 107 }, new CraftingResult(116, 1));
        AddRecipe(new List<int> { 93, 106 }, new CraftingResult(117, 1));

        // 阳光+月光+星光=  (仮)光星「Orion Belt」
        AddRecipe(new List<int> { 116, 117, 118 }, new CraftingResult(109, 1));
        // (仮)光星「Orion Belt」 + 普通的力量 = 光星「Orion Belt」
        AddRecipe(new List<int> { 109, 94 }, new CraftingResult(119, 1));

        // 核反应炉
        AddRecipe(new List<int> { 96, 96 }, new CraftingResult(112, 1));
        AddRecipe(new List<int> { 112, 112 }, new CraftingResult(113, 1));
        AddRecipe(new List<int> { 113, 113 }, new CraftingResult(114, 1));


        // 三色团子
        AddRecipe(new List<int> { 98, 99, 100 }, new CraftingResult(102, 1));

        // 冰块 +  雪符「Diamond Blizzard」 = 琪露诺的信物
        AddRecipe(new List<int> { 4 + 21 }, new CraftingResult(105, 1));


        // 蘑菇+蘑菇+森蘑菇=菌菇汤
        AddRecipe(new List<int> { 1, 1, 2 }, new CraftingResult(9, 1));

        // 面包+炒面=炒面面包
        AddRecipe(new List<int> { 10, 26 }, new CraftingResult(27, 1));



        // 锁链+拳套=美玲衣装
        AddRecipe(new List<int> { 25, 46 }, new CraftingResult(47, 1));
        // 美玲衣装升级
        AddRecipe(new List<int> { 22, 47 }, new CraftingResult(49, 1));


        // 不死鸟之羽 + 贤者之石的碎片 = 凤凰卵
        AddRecipe(new List<int> { 50, 51 }, new CraftingResult(52, 1));
        // 不死鸟之羽 + 小圆盾/铁盾 = 不死鸟之盾
        AddRecipe(new List<int> { 50, 7 }, new CraftingResult(53, 1));
        AddRecipe(new List<int> { 50, 30 }, new CraftingResult(53, 1));
        // 凤凰卵 + 不死鸟之盾 = 真 凤凰卵
        AddRecipe(new List<int> { 53, 52 }, new CraftingResult(54, 1));

        // 扫帚+团扇=风神一扇
        AddRecipe(new List<int> { 8, 55 }, new CraftingResult(56, 1));
        // 风神一扇升级
        AddRecipe(new List<int> { 56, 22 }, new CraftingResult(57, 1));

        // 小黄瓜+ 冰块 = 壶中的天地
        AddRecipe(new List<int> { 4, 28 }, new CraftingResult(58, 1));
        // 壶中的天地 + 普通的力量  = 真 壶中的天地
        AddRecipe(new List<int> { 58, 94 }, new CraftingResult(59, 8));



        // 獠牙+翅膀=吸血鬼幻想
        AddRecipe(new List<int> { 48, 72 }, new CraftingResult(74, 1));



        // 剑道类
        // 竹光 + 犬走椛之盾 + 微弱的力量 = (仮)犬走椛之剑
        AddRecipe(new List<int> { 120, 121, 93 }, new CraftingResult(123, 1));
        // (仮)犬走椛之剑 + 众星之力 = 犬走椛之剑
        AddRecipe(new List<int> { 123, 23 }, new CraftingResult(127, 1));
        // 竹光 + 137 + 微弱的力量 = (仮)天丛云
        AddRecipe(new List<int> { 120, 122, 93 }, new CraftingResult(124, 1));
        // (仮)天丛云 + 众星之力 = 天丛云
        AddRecipe(new List<int> { 124, 23 }, new CraftingResult(128, 1));
        // 竹光 + 冰块 + 微弱的力量= (仮)冰精之剑
        AddRecipe(new List<int> { 120, 4, 93 }, new CraftingResult(125, 1));
        // (仮)冰精之剑 + 众星之力 = 冰精之剑
        AddRecipe(new List<int> { 125, 23 }, new CraftingResult(126, 1));
        // 竹光 +微弱的力量 = ☆楼观剑☆ (仮) 
        AddRecipe(new List<int> { 120, 93 }, new CraftingResult(123, 1));
        // ☆楼观剑☆ (仮)  + 众星之力 = 人界剑「悟入幻想」
        AddRecipe(new List<int> { 36, 23 }, new CraftingResult(37, 1));
        // (真)人界剑「悟入幻想」
        AddRecipe(new List<int> { 37, 94 }, new CraftingResult(73, 1));

        // 测试用，开放格子
        instance.itembagList[0].isActive = true;
        instance.itembagList[1].isActive = true;
        instance.itembagList[2].isActive = true;
        // instance.itembagList[3].isActive = true;
        MaxOfBags = 3;
        WarehouseList[0].isActive = true;
        WarehouseList[1].isActive = true;
        // WarehouseList[2].isActive = true;
        MaxOfWarehouse = 2;

        // 设置初始选择
        if (instance.itembagList[0] != null)
        {
            instance.itembagList[0].isChoosed = true;
            instance.itembagList[0].isActive = true;
            indexOfChoose = 1;
        }

        // 隐藏未激活的背包格
        foreach (var bag in instance.itembagList)
        {
            if (bag.isActive == false)
                bag.gameObject.SetActive(false);
        }

        // 设置自动恢复系统
        BuffCoroutineControl.instance.StartCoroutine(uniqueId, AutoHealSP());
    }


    void Update()
    {
        // 仅在游戏未暂停时检查 为什么？
        // if (Time.timeScale != 0f)
        // {
        inputActive();
        inputUseActive();
        inputDropActive();
        showItemInfo();
        showHoverInfo();
        // }
    }
    // 增加一个背包格
    public void AddOneBag()
    {
        // 目前最大增加到8个
        if (MaxOfBags >= 8)
        {
            UnityEngine.Debug.Log("背包格子已经达到最大值");
            return;
        }

        // 写死，我一定不会改的.jpg
        if (MaxOfBags != 3)
        {
            // 将itemcontrol空间向左移动150
            RectTransform rectTransform = instance.GetComponent<RectTransform>();
            if (rectTransform == null)
            {
                Debug.LogError("RectTransform component not found on instance object");
                return;
            }
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x - 150, rectTransform.anchoredPosition.y);
        }

        // 循环找到第一个未激活的背包格子，并激活
        for (int i = 0; i < 8; i++)
        {
            if (!instance.itembagList[i].isActive)
            {
                instance.itembagList[i].isActive = true;
                instance.itembagList[i].gameObject.SetActive(true);
                MaxOfBags++;
                return;
            }
        }
    }

    // 增加一个仓库
    public void AddOneWarehouse()
    {
        // 目前最大增加到8个
        if (MaxOfWarehouse >= 5)
        {
            UnityEngine.Debug.Log("仓库格子已经达到最大值");
            return;
        }

        // 循环找到第一个未激活的背包格子，并激活
        for (int i = 0; i < 5; i++)
        {
            if (!instance.WarehouseList[i].isActive)
            {
                instance.WarehouseList[i].isActive = true;
                instance.WarehouseList[i].gameObject.SetActive(true);
                MaxOfWarehouse++;
                return;
            }
        }
    }

    public string showItemPlaneName(item _item)
    {
        if (_item.hasQuality)
        {
            // 有后缀情况
            switch (_item.itemCiti)
            {
                case ItemQuality.Null:
                    return "<color=#" + UnityEngine.ColorUtility.ToHtmlStringRGB(_item.itemColor) + ">" + _item.itemName + "</color>";
                case ItemQuality.Inferior:
                    return "<color=#544742>劣质的 </color>" + "<color=#" + UnityEngine.ColorUtility.ToHtmlStringRGB(_item.itemColor) + ">" + _item.itemName + "</color>";
                case ItemQuality.Normal:
                    return "<color=#" + UnityEngine.ColorUtility.ToHtmlStringRGB(_item.itemColor) + ">" + _item.itemName + "</color>";
                case ItemQuality.Good:
                    return "<color=#7DFA00>良好的 </color>" + "<color=#" + UnityEngine.ColorUtility.ToHtmlStringRGB(_item.itemColor) + ">" + _item.itemName + "</color>";
                case ItemQuality.Excellent:
                    return "<color=#71B9FA>优秀的 </color>" + "<color=#" + UnityEngine.ColorUtility.ToHtmlStringRGB(_item.itemColor) + ">" + _item.itemName + "</color>";
                case ItemQuality.Legendary:
                    return "<color=#FFD92D>传奇的 </color>" + "<color=#" + UnityEngine.ColorUtility.ToHtmlStringRGB(_item.itemColor) + ">" + _item.itemName + "</color>";
            }
        }
        else if (_item.isEquipmentSC)
        {
            return "<color=#" + UnityEngine.ColorUtility.ToHtmlStringRGB(_item.itemColor) + ">" + _item.itemName + " " + "Lv." + _item.isEquipmentSCLevel.ToString() + " " + "</color>";
        }
        else
        {
            // 无后缀情况
            return "<color=#" + UnityEngine.ColorUtility.ToHtmlStringRGB(_item.itemColor) + ">" + _item.itemName + "</color>";
        }
        return "showItemPlaneName报错，请检查物品颜色和品质";
    }


    string showItemPlaneInfo(item _item)
    {

        string ret = "";
        if (_item.hasQuality)
        {
            ret += _item.GetQualityText();
        }
        else if (_item.isEquipmentSC)
        {
            ret += _item.GetEquipmentSCText();
        }
        else
        {
            // 无后缀情况
            ret += _item.itemInfoText;
        }


        // 装备有限制情况
        if (_item.itemSlot == ItemSlotEnum.OnlyGreen)
        {
            ret += "\n" + "必须在<color=#15FF49>绿色</color><b>装备栏</b>才能发挥效果。";
        }
        else if (_item.itemSlot == ItemSlotEnum.OnlyRed)
        {
            ret += "\n" + "必须在<color=#FF0000>红色</color><b>装备栏</b>才能发挥效果。";
        }
        else if (_item.itemSlot == ItemSlotEnum.OnlyBlue)
        {
            ret += "\n" + "必须在<color=#0049FF>蓝色</color><b>装备栏</b>才能发挥效果。";
        }
        else if (_item.itemSlot == ItemSlotEnum.AllEquipment)
        {
            ret += "\n" + "必须在<b>装备栏</b>才能发挥效果。";
        }
        else if (_item.itemSlot == ItemSlotEnum.All)
        {
            ret += "\n" + "可以在任何栏位发挥效果。";
        }
        else if (_item.itemSlot == ItemSlotEnum.AllExceptWarehouse)
        {
            ret += "\n" + "在<b>装备栏</b>、<b>快捷栏</b>可以发挥效果。";
        }
        ret += "\n";

        ret = ret.Replace("\\n", "\n");
        return ret;

    }

    // 显示物品信息
    void showItemInfo()
    {
        // // 创建一个Stopwatch实例
        // Stopwatch stopwatch = new Stopwatch();

        // // 开始计时
        // stopwatch.Start();

        // 背包里，显示标识存在有一个为真，那么显示在那个背包上面
        var itemWithTrueIsShowItemInfo = getShowItemInfo_new();

        if (itemWithTrueIsShowItemInfo != null)
        {

            // ItemBag itemWithTrueIsShowItemInfo = getShowItemInfo();
            // 如果背包栏没有东西直接返回
            if (itemWithTrueIsShowItemInfo.itemInfo.itemId == 0)
                return;

            if (!itemInfoPlane.activeSelf)
                itemInfoPlane.SetActive(true);

            isShowItemCount += Time.unscaledDeltaTime;

            itemPlaneName.text = showItemPlaneName(itemWithTrueIsShowItemInfo.itemInfo);
            itemPlaneInfo.text = showItemPlaneInfo(itemWithTrueIsShowItemInfo.itemInfo); ;
            itemPlaneRemark.text = itemWithTrueIsShowItemInfo.itemInfo.itemRemarkText;
            itemPlaneRemark.text = itemPlaneRemark.text.Replace("\\n", "\n");
            // 显示合成配方部分
            if (itemWithTrueIsShowItemInfo.itemInfo.isMaterial && isShowItemCount > 1.0f)
            {
                itemFormulation_Text.text = GetItemFormulation(itemWithTrueIsShowItemInfo.itemInfo.itemId);
                // itemFormulation.SetActive(true);
            }
            else if (isShowItemCount > 1.0f && itemWithTrueIsShowItemInfo.BagType != BagTypeEnum.SkillPointShop)
            {
                itemFormulation_Text.text = "该物品没有相关配方";
            }
            else
            {
                itemFormulation_Text.text = "";
            }


            // 判断鼠标位置加上itemInfoPlane的显示是否超出屏幕右边，如果超出屏幕外面，那么将itemInfoPlane的位置向左移动自身宽度，向上移动自身高度。
            if (Input.mousePosition.x + itemInfoPlane.GetComponent<RectTransform>().sizeDelta.x > Screen.width)
            {
                itemInfoPlane.transform.position = new Vector3(Input.mousePosition.x - itemInfoPlane.GetComponent<RectTransform>().sizeDelta.x - 20, Input.mousePosition.y - 20, Input.mousePosition.z);
            }
            else
            {
                itemInfoPlane.transform.position = new Vector3(Input.mousePosition.x + 20, Input.mousePosition.y - 20);
            }

            // 同理，如果超出屏幕下面，那么将itemInfoPlane的位置向上移动自身高度
            if (Input.mousePosition.y - itemInfoPlane.GetComponent<RectTransform>().sizeDelta.y < 0)
            {
                itemInfoPlane.transform.position = new Vector3(itemInfoPlane.transform.position.x - 20, Input.mousePosition.y + 20 + itemInfoPlane.GetComponent<RectTransform>().sizeDelta.y, itemInfoPlane.transform.position.z);
            }

            // 根据物品名字长度，调整字体大小，窗口大小
            float totalHeight = itemPlaneName.GetPreferredValues(Screen.width / 3.5f, 1000).y + itemPlaneInfo.GetPreferredValues(Screen.width / 3.5f, 1000).y + itemPlaneRemark.GetPreferredValues(Screen.width / 3.5f, 1000).y + itemFormulation_Text.GetPreferredValues(Screen.width / 3.5f, 1000).y;

            itemInfoPlane.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 4, totalHeight);

            // // 显示合成配方部分
            // if (itemWithTrueIsShowItemInfo.itemInfo.isMaterial)
            // {
            //     ShowItemFormulation(itemWithTrueIsShowItemInfo.itemInfo.itemId);
            // }
            // else
            // {
            //     itemFormulation.SetActive(false);
            // }
        }
        else
        {
            if (itemInfoPlane.activeSelf)
            {
                itemInfoPlane.SetActive(false);
                // itemFormulation.SetActive(false);
            }
        }
        // // 停止计时
        // stopwatch.Stop();

        // // 获取经过的时间（以毫秒为单位）
        // double elapsedMilliseconds = stopwatch.Elapsed.TotalMilliseconds;

        // // 打印经过的时间
        // UnityEngine.Debug.Log("Elapsed time: " + elapsedMilliseconds + " ms");
    }


    private string GetItemFormulation(int _itemid)
    {
        string showtext = "";

        // 在recipes的中找到有_itemid的配方
        foreach (var recipe in RecipeList)
        {
            if (recipe.Key.Contains(_itemid) || recipe.Value.itemId == _itemid)
            {
                for (int i = 0; i < recipe.Key.Count; i++)
                {
                    // // 如果不是自己，并且没有发现过，那么显示为？？？，否则显示物品名字
                    // if (recipe.Key.ElementAt(i) != _itemid && !GlobalControl.instance.GetItemDiscovered(recipe.Key.ElementAt(i)))
                    // {
                    //     showtext += "？？？";
                    // }
                    // else
                    // {
                    showtext += GetItemName(recipe.Key.ElementAt(i));
                    // }
                    if (i != recipe.Key.Count - 1)
                        showtext += " + ";
                }
                showtext += " = ";
                // 如果不是自己，并且没有发现过，那么显示为？？？，否则显示物品名字
                // if (recipe.Value.itemId != _itemid && !GlobalControl.instance.GetItemDiscovered(recipe.Value.itemId))
                // {
                //     showtext += "？？？";
                // }
                // else
                // {
                showtext += GetItemName(recipe.Value.itemId);
                // }
                showtext += "\n";
            }
        }


        if (string.IsNullOrEmpty(showtext))
        {
            showtext = "该物品没有相关配方";
        }
        return showtext;
    }
    private ItemBag getShowItemInfo_new()
    {
        // 背包格子
        var itemInBag = instance.itembagList.FirstOrDefault(ItemBag => ItemBag.isShowItemInfo);
        if (itemInBag != null)
        {
            return itemInBag;
        }

        // 合成格子
        var itemInGuide = instance.combineItemGuideList.FirstOrDefault(ItemBag => ItemBag.isShowItemInfo);
        if (itemInGuide != null)
        {
            return itemInGuide;
        }

        // 合成结果格子
        if (instance.combineResult.isShowItemInfo)
        {
            return instance.combineResult;
        }

        // 仓库格子
        var itemInWarehouse = instance.WarehouseList.FirstOrDefault(ItemBag => ItemBag.isShowItemInfo);
        if (itemInWarehouse != null)
        {
            return itemInWarehouse;
        }

        // 装备格子
        var itemInEquipment = instance.EquipmentList.FirstOrDefault(ItemBag => ItemBag.isShowItemInfo);
        if (itemInEquipment != null)
        {
            return itemInEquipment;
        }

        // 垃圾桶格子
        if (instance.trashCan.isShowItemInfo)
        {
            return instance.trashCan;
        }

        // 商店格子
        if (UIControl.instance.shopPanel.activeSelf)
        {
            var itemInShop = ShopControl.instance.shopsBag.FirstOrDefault(ItemBag => ItemBag.isShowItemInfo);
            if (itemInShop != null)
            {
                return itemInShop;
            }
        }

        // 升级时的格子
        if (UIControl.instance.levelupPanel.activeSelf)
        {
            // 免费格子
            var itemInUpgrade = ExpLevelControl.instance.freeItemBagList.FirstOrDefault(ItemBag => ItemBag.isShowItemInfo);
            if (itemInUpgrade != null)
            {
                return itemInUpgrade;
            }

            // 福袋格子
            if (ExpLevelControl.instance.luckyBag.isShowItemInfo)
            {
                return ExpLevelControl.instance.luckyBag;
            }

            // 重铸格子
            if (ExpLevelControl.instance.reforgingBag.isShowItemInfo)
            {
                return ExpLevelControl.instance.reforgingBag;
            }

            // 升级格子
            if (ExpLevelControl.instance.upgradeItemBag.isShowItemInfo)
            {
                return ExpLevelControl.instance.upgradeItemBag;
            }

            // 随机格子
            var itemRandom = ExpLevelControl.instance.randomShopBag.FirstOrDefault(ItemBag => ItemBag.isShowItemInfo);
            if (itemRandom != null)
            {
                return itemRandom;
            }
        }

        // 技能点格子
        if (UIControl.instance.menuPanel_Upgrade.activeSelf)
        {
            var skillPoint = SkillPointControl.instance.SkillTree_1.FirstOrDefault(ItemBag => ItemBag.isShowItemInfo);
            if (skillPoint != null)
            {
                return skillPoint;
            }
            var skillPoint2 = SkillPointControl.instance.SkillTree_2.FirstOrDefault(ItemBag => ItemBag.isShowItemInfo);
            if (skillPoint2 != null)
            {
                return skillPoint2;
            }
            var skillPoint3 = SkillPointControl.instance.SkillTree_3.FirstOrDefault(ItemBag => ItemBag.isShowItemInfo);
            if (skillPoint3 != null)
            {
                return skillPoint3;
            }
            var skillPoint4 = SkillPointControl.instance.SkillTree_4.FirstOrDefault(ItemBag => ItemBag.isShowItemInfo);
            if (skillPoint4 != null)
            {
                return skillPoint4;
            }
        }

        return null;
    }
    // isShowItemInfoFlag和getShowItemInfo_new废弃
    bool isShowItemInfoFlag()
    {
        // 背包格子
        if (instance.itembagList.Any(ItemBag => ItemBag.isShowItemInfo))
        {
            return true;
        }
        // 合成格子
        if (instance.combineItemGuideList.Any(ItemBag => ItemBag.isShowItemInfo))
        {
            return true;
        }
        // 合成结果格子
        if (instance.combineResult.isShowItemInfo)
            return true;

        // 仓库格子
        if (instance.WarehouseList.Any(ItemBag => ItemBag.isShowItemInfo))
            return true;

        // 装备格子
        if (instance.EquipmentList.Any(ItemBag => ItemBag.isShowItemInfo))
            return true;

        // 垃圾桶格子
        if (instance.trashCan.isShowItemInfo)
            return true;


        // 商店格子
        if (UIControl.instance.shopPanel.activeSelf)
            if (ShopControl.instance.shopsBag.Any(ShopBag => ShopBag.isShowItemInfo))
                return true;



        return false;
    }
    ItemBag getShowItemInfo()
    {
        ItemBag itemWithTrueIsShowItemInfo = instance.itembagList.FirstOrDefault(itemBag => itemBag.isShowItemInfo == true);
        if (itemWithTrueIsShowItemInfo != null)
            return itemWithTrueIsShowItemInfo;

        itemWithTrueIsShowItemInfo = instance.combineItemGuideList.FirstOrDefault(itemBag => itemBag.isShowItemInfo == true);
        if (itemWithTrueIsShowItemInfo != null)
            return itemWithTrueIsShowItemInfo;


        if (UIControl.instance.menuPanel_Warehouse.activeSelf)
        {
            itemWithTrueIsShowItemInfo = instance.WarehouseList.FirstOrDefault(itemBag => itemBag.isShowItemInfo == true);
            if (itemWithTrueIsShowItemInfo != null)
                return itemWithTrueIsShowItemInfo;

            // 装备格子
            itemWithTrueIsShowItemInfo = instance.EquipmentList.FirstOrDefault(itemBag => itemBag.isShowItemInfo == true);
            if (itemWithTrueIsShowItemInfo != null)
                return itemWithTrueIsShowItemInfo;
        }

        if (UIControl.instance.shopPanel.activeSelf)
        {
            itemWithTrueIsShowItemInfo = ShopControl.instance.shopsBag.FirstOrDefault(itemBag => itemBag.isShowItemInfo == true);
            if (itemWithTrueIsShowItemInfo != null)
                return itemWithTrueIsShowItemInfo;
        }





        // 垃圾桶格子
        if (instance.trashCan.isShowItemInfo)
            return instance.trashCan;

        return instance.combineResult;
    }

    // 显示悬浮物品
    void showHoverInfo()
    {
        // 显示合成面板
        // combinePlane.SetActive(isCombinePlane);

        // 必须要有合成面板界面，才可以选中物品
        if (isCombinePlane || UIControl.instance.levelupPanel.activeSelf)
            hoverImage.gameObject.SetActive(true);
        else
        {
            hoverImage.gameObject.SetActive(false);
            return;
        }


        // 如果鼠标上没有物品，直接返回
        if (hoverItem.itemId == 0)
        {
            hoverImage.gameObject.SetActive(false);
            return;
        }



        if (combinePlane)
        {
            // 鼠标拾取的物品悬浮在鼠标上
            hoverImage.transform.position = Input.mousePosition;
        }
    }
    // 给背包增加物品
    public bool AddItemToBag(item _item)
    {
        int index = 0;
        if (IsBagEmpty(_item, ref index))
        {
            if (index >= 100)
            {
                instance.WarehouseList[index - 100].AddItem(_item);
                // 如果是被动装备，那么触发被动效果
                if (_item.isPassive && instance.WarehouseList[index - 100].isEffective(_item.itemSlot))
                    _item.Passive();
            }
            else
            {
                instance.itembagList[index].AddItem(_item);
                // 如果是被动装备，那么触发被动效果
                if (_item.isPassive && instance.itembagList[index].isEffective(_item.itemSlot))
                    _item.Passive();
            }
        }
        else
        {
            return false;
        }

        return true;
    }
    // 给背包增加多个物品
    public bool AddItemToBag(item _item, int _count)
    {
        for (int i = 0; i < _count; i++)
        {
            if (!AddItemToBag(_item))
            {
                return false;
            }
        }
        return true;
    }

    // 返回是否有空余的背包位，并返回位置索引
    public bool IsBagEmpty(item _item, ref int index)
    {
        // 遍历 WarehouseList
        for (int i = 0; i < WarehouseList.Count; i++)
        {
            // 检查每个 ItemBag 对象是否已经初始化
            if (WarehouseList[i].itemInfo == null)
            {
                Debug.Log("尝试初始化ItemBag");
                // 如果没有，那么在这里初始化它
                WarehouseList[i].itemInfo = ItemControl.instance.itemGuide[0];
                WarehouseList[i].SetNoItem();
            }
        }

        // 先检查是否可以堆叠
        if (_item.isDuidie == 1)
        {
            for (int i = 0; i < instance.itembagList.Count(ItemBag => ItemBag.isActive == true); i++)
            {
                // 检查是否是同一物品并，低于最大堆叠数量
                if (instance.itembagList[i].itemInfo.itemId == _item.itemId)
                {
                    if (instance.itembagList[i].itemCount < _item.MaxDuidie)
                    {
                        index = i;
                        return true;
                    }
                }
            }



            for (int i = 0; i < instance.WarehouseList.Count(item => item.isActive == true); i++)
            {
                // 检查是否是同一物品并，低于最大堆叠数量
                if (instance.WarehouseList[i].itemInfo.itemId == _item.itemId)
                {
                    if (instance.WarehouseList[i].itemCount < _item.MaxDuidie)
                    {
                        index = i + 100;
                        return true;
                    }
                }
            }
        }



        for (int i = 0; i < instance.itembagList.Count(ItemBag => ItemBag.isActive == true); i++)
        {
            if (instance.itembagList[i].itemInfo.itemId <= 0)
            {
                index = i;
                return true;
            }
        }

        for (int i = 0; i < instance.WarehouseList.Count(ItemBag => ItemBag.isActive == true); i++)
        {
            if (instance.WarehouseList[i].itemInfo.itemId <= 0)
            {
                index = i + 100;
                return true;
            }
        }

        index = -1;
        return false;
    }



    // 根据鼠标输入，使用道具
    void inputUseActive()
    {

        // 非战斗时不要打开。否则会导致在战斗外面也能使用道具
        if (!GlobalControl.instance.isBattle || PlayerHealthControl.instance.OnTalk)
        {
            return;
        }

        // 部分情况时禁用使用道具
        if (UIControl.instance.bigMapPanel.activeSelf || UIControl.instance.levelupPanel.activeSelf)
        {
            return;
        }

        // 检查是否点击了UI元素
        if (EventSystem.current.IsPointerOverGameObject())
        {
            isMouseOverUI = true;
            return;
        }
        else
        {
            isMouseOverUI = false;
        }

        // 键位整合
        // 没有打开面板 ｛左键使用｝
        // 打开面板｛
        // 手上没东西 → 左键无反应
        //  手上没东西 → 右键无反应
        // 手上有东西 → 右键丢弃。
        // 手上有东西 → 在UI上？ 不在 → 使用｝
        if (!isCombinePlane)
        {
            // 没打开面板，左键使用，右键无反应
            if (Input.GetMouseButtonDown((int)MouseButton.Left))
            {
                // 成功使用
                if (instance.itembagList[indexOfChoose - 1].itemInfo.Use())
                {
                    // 如果是消耗品，需要消耗
                    if (instance.itembagList[indexOfChoose - 1].itemInfo.isXiaohao)
                    {
                        instance.itembagList[indexOfChoose - 1].depleteItem();
                    }
                    return;
                }
            }

            // 某些蓄力武器
            if (instance.itembagList[indexOfChoose - 1].itemInfo.isChargeWeapon)
            {
                if (Input.GetMouseButton((int)MouseButton.Left))
                {
                    instance.itembagList[indexOfChoose - 1].itemInfo.UseChargePower();
                }
            }


        }
        else
        {
            // 打开面板时，如果手上没东西，左键无反应，右键捡起一个。
            if (hoverItem.itemId == 0)
            {
            }
            else
            {
                // 打开面板时，如果手上有东西，左键使用，右键在物品上时尝试添加。点到UI外面视为丢弃
                if (Input.GetMouseButtonDown((int)MouseButton.Left))
                {
                    if (EventSystem.current.IsPointerOverGameObject())
                    {
                        isMouseOverUI = true;
                        // 如果打开了合成面板，点击到UI界面时，不会触发使用
                        return;
                    }
                    else
                    {
                        isMouseOverUI = false;
                    }


                    // 使用鼠标上的物品
                    if (hoverItem.Use())
                    {
                        // 如果是消耗品，需要消耗
                        if (hoverItem.isXiaohao)
                        {
                            SethoverItemCountText(--hoverItemCount);
                            if (hoverItemCount == 0)
                                hoverItem = itemGuide[0];
                        }
                    }
                }

                // 某些蓄力武器
                if (hoverItem.isChargeWeapon)
                {
                    if (Input.GetMouseButton((int)MouseButton.Left))
                    {
                        hoverItem.UseChargePower();
                    }
                }

                // if (Input.GetMouseButtonDown((int)MouseButton.Right))
                // {
                //     var item = getShowItemInfo_new();
                //     if (item != null)
                //     {
                //         if (item.itemInfo.itemId == 0)
                //             return;
                //         if (item.itemInfo.isDuidie == 1)
                //         {
                //             hoverItem = item.itemInfo;
                //             hoverItemCount += 1;
                //             SethoverItemCountText(hoverItemCount);

                //             item.itemCount -= 1;
                //             if (item.itemCount == 0)
                //             {
                //                 item.itemInfo = itemGuide[0];
                //                 item.SetNoItem();
                //             }
                //         }
                //     }
                //     else
                //     {
                //         // 如果点到UI界面外面，视为丢弃道具全部（分为一个一个单独的丢弃，就像MC那样）
                //         for (int i = 0; i < hoverItemCount; i++)
                //             CreateOneDropItem(hoverItem);
                //         SethoverItemCountText(0);
                //         hoverItem = itemGuide[0];
                //     }
                // }
            }
        }
    }

    // 根据键鼠输入，切换选中背包对应格子
    void inputActive()
    {
        // 如果武器进入CD，则禁止切换背包
        if (MainPlayer.instance.WeaponCDCount > 0)
            return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (!instance.itembagList[0].isActive)
                return;

            if (!instance.itembagList[0].isChoosed)
            {
                instance.itembagList[indexOfChoose - 1].isChoosed = false;
                instance.itembagList[0].isChoosed = true;
                indexOfChoose = 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (!instance.itembagList[1].isActive)
                return;

            if (!instance.itembagList[1].isChoosed)
            {
                instance.itembagList[indexOfChoose - 1].isChoosed = false;
                instance.itembagList[1].isChoosed = true;
                indexOfChoose = 2;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (!instance.itembagList[2].isActive)
                return;

            if (!instance.itembagList[2].isChoosed)
            {
                instance.itembagList[indexOfChoose - 1].isChoosed = false;
                instance.itembagList[2].isChoosed = true;
                indexOfChoose = 3;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (!instance.itembagList[3].isActive)
                return;

            if (!instance.itembagList[3].isChoosed)
            {
                instance.itembagList[indexOfChoose - 1].isChoosed = false;
                instance.itembagList[3].isChoosed = true;
                indexOfChoose = 4;
            }
        }


        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (!instance.itembagList[4].isActive)
                return;

            if (!instance.itembagList[4].isChoosed)
            {
                instance.itembagList[indexOfChoose - 1].isChoosed = false;
                instance.itembagList[4].isChoosed = true;
                indexOfChoose = 5;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (!instance.itembagList[5].isActive)
                return;

            if (!instance.itembagList[5].isChoosed)
            {
                instance.itembagList[indexOfChoose - 1].isChoosed = false;
                instance.itembagList[5].isChoosed = true;
                indexOfChoose = 6;
            }
        }


        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            if (!instance.itembagList[6].isActive)
                return;

            if (!instance.itembagList[6].isChoosed)
            {
                instance.itembagList[indexOfChoose - 1].isChoosed = false;
                instance.itembagList[6].isChoosed = true;
                indexOfChoose = 7;
            }
        }


        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            if (!instance.itembagList[7].isActive)
                return;

            if (!instance.itembagList[7].isChoosed)
            {
                instance.itembagList[indexOfChoose - 1].isChoosed = false;
                instance.itembagList[7].isChoosed = true;
                indexOfChoose = 8;
            }
        }
        // 有个太顺滑了的问题需要解决
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && !PlayerHealthControl.instance.OnTalk)
        {
            // 滚滑轮切换背包事件，如果在最开始了，那么向左就滚到最右边去
            if (indexOfChoose == 1)
            {
                instance.itembagList[indexOfChoose - 1].isChoosed = false;
                instance.itembagList[MaxOfBags - 1].isChoosed = true;
                indexOfChoose = MaxOfBags;
            }
            else
            {
                //  其他都是向左走一格
                instance.itembagList[indexOfChoose - 1].isChoosed = false;
                instance.itembagList[indexOfChoose - 2].isChoosed = true;
                indexOfChoose = indexOfChoose - 1;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && !PlayerHealthControl.instance.OnTalk)
        {
            // 相反同理
            if (indexOfChoose == MaxOfBags)
            {
                instance.itembagList[indexOfChoose - 1].isChoosed = false;
                instance.itembagList[0].isChoosed = true;
                indexOfChoose = 1;
            }
            else
            {
                instance.itembagList[indexOfChoose - 1].isChoosed = false;
                instance.itembagList[indexOfChoose].isChoosed = true;
                indexOfChoose = indexOfChoose + 1;
            }
        }
    }

    // 根据键鼠输入，丢弃选中背包对应格子的物品一个
    void inputDropActive()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // 如果对应的背包格子里有物品
            if (instance.itembagList[indexOfChoose - 1].itemInfo.itemId <= 0)
                return;

            CreateOneDropItem(instance.itembagList[indexOfChoose - 1].itemInfo);

            // 消耗一个物品
            instance.itembagList[indexOfChoose - 1].depleteItem();
        }
    }

    void CreateOneDropItem(item dropItem)
    {
        // 之后在地面上创建一个drop_class的子类drop_item
        // 创建一个道具信息为_item的对象并激活
        // 向面朝的方向部署位置和速度
        Drops_Item newItem = Instantiate(AssetControl.instance.dropsItem, MainPlayer.instance.transform.position + new Vector3(MainPlayer.instance.facingDirection * 0.5f, 0), Quaternion.identity, this.transform);
        // 保护时间内，不会被立马再次捡起。
        newItem.ChangecheckCount(2f);

        newItem.dropFlag = true;

        newItem.itemText.color = dropItem.itemColor;
        newItem.itemText.text = dropItem.itemName;
        newItem.itemImage.sprite = dropItem.itemImage;
        newItem.itemInfo = dropItem;
        newItem.gameObject.SetActive(true);
    }

    public void ClearCombineItem()
    {
        // 如果关闭面板时，鼠标上有物品，则强制丢弃
        if (hoverItemCount != 0)
        {
            for (int i = 0; i < hoverItemCount; i++)
                CreateOneDropItem(hoverItem);
            hoverItem = itemGuide[0];
            SethoverItemCountText(0);

        }
        // 如果合成槽位上有物品，则强制丢弃
        for (int i = 0; i < instance.combineItemGuideList.Count; i++)
        {
            if (instance.combineItemGuideList[i].itemInfo.itemId != 0)
            {
                for (int j = 0; j < instance.combineItemGuideList[i].itemCount; j++)
                    CreateOneDropItem(instance.combineItemGuideList[i].itemInfo);
                instance.combineItemGuideList[i].itemInfo = itemGuide[0];
                instance.combineItemGuideList[i].SetNoItem();
            }
        }

        // 如果合成结果槽位上有物品，则直接清空
        instance.combineResult.deleteItem();

        // 关闭面板时，清空面板里的isshowinfo。否则会导致信息面板一直显示
        foreach (var itemGuide in instance.combineItemGuideList)
        {
            itemGuide.isShowItemInfo = false;
        }
        instance.combineResult.isShowItemInfo = false;
        foreach (var itemGuide in instance.WarehouseList)
        {
            itemGuide.isShowItemInfo = false;
        }
        foreach (var itemGuide in instance.EquipmentList)
        {
            itemGuide.isShowItemInfo = false;
        }
        if (SkillPointControl.instance != null)
        {
            if (SkillPointControl.instance.SkillTree_1 != null)
            {
                foreach (var itemGuide in SkillPointControl.instance.SkillTree_1)
                {
                    itemGuide.isShowItemInfo = false;
                }
            }
            if (SkillPointControl.instance.SkillTree_2 != null)
            {
                foreach (var itemGuide in SkillPointControl.instance.SkillTree_2)
                {
                    itemGuide.isShowItemInfo = false;
                }
            }
            if (SkillPointControl.instance.SkillTree_3 != null)
            {
                foreach (var itemGuide in SkillPointControl.instance.SkillTree_3)
                {
                    itemGuide.isShowItemInfo = false;
                }
            }
            if (SkillPointControl.instance.SkillTree_4 != null)
            {
                foreach (var itemGuide in SkillPointControl.instance.SkillTree_4)
                {
                    itemGuide.isShowItemInfo = false;
                }
            }
        }


        instance.trashCan.isShowItemInfo = false;
    }
    // 返回现在选中的道具栏的道具
    public item GetChooseItem()
    {
        if (instance.itembagList[indexOfChoose - 1].itemInfo)
            return instance.itembagList[indexOfChoose - 1].itemInfo;
        else
            return hoverItem;
    }

    // 合成配方系统 
    private Dictionary<HashSet<int>, CraftingResult> recipes = new Dictionary<HashSet<int>, CraftingResult>(new HashSetEqualityComparer<int>());
    // 全新配方系统
    private Dictionary<List<int>, CraftingResult> RecipeList = new Dictionary<List<int>, CraftingResult>(new ListEqualityComparer());
    public struct CraftingResult
    {
        public int itemId;
        public int quantity;
        public CraftingResult(int itemId, int quantity)
        {
            this.itemId = itemId;
            this.quantity = quantity;
        }
    }

    private void AddRecipe(List<int> inputItems, CraftingResult outputItem)
    {
        // 将输入物品组合和输出物品添加到字典中
        RecipeList.Add(inputItems, outputItem);
    }
    int HowManyTimesCreate = 999999;
    // 获取合成结果
    public void getCombine()
    {
        // 第一步，清空合成结果栏
        instance.combineResult.deleteItem();

        // 第二步，获取合成结果
        // 获取输入的物品
        List<int> inputItems = new List<int>();
        HowManyTimesCreate = 999999;
        foreach (var itemGuide in instance.combineItemGuideList)
        {
            if (itemGuide.itemInfo.itemId != 0)
            {
                inputItems.Add(itemGuide.itemInfo.itemId);
                HowManyTimesCreate = Math.Min(HowManyTimesCreate, itemGuide.itemCount);
            }
        }

        // 检查是否能够合成物品
        CraftingResult outputItem;
        if (RecipeList.TryGetValue(inputItems, out outputItem))
        {
            bool findflag = false;
            foreach (item item in itemGuide)
            {
                // 如果找到与目标ID匹配的资源
                if (item.itemId == outputItem.itemId)
                {
                    if (outputItem.quantity > 1 && item.hasQuality)
                    {
                        UnityEngine.Debug.Log("有品质的物品暂时禁止合成多个");
                        return;
                    }

                    // 如果物品不可堆叠，那么只合成一个
                    if (item.isDuidie == 0)
                        HowManyTimesCreate = 1;

                    if (!item.hasQuality)
                    {
                        // 在instance.combineResult中添加该资源
                        for (int i = 0; i < HowManyTimesCreate; i++)
                        {
                            instance.combineResult.AddItem(item, outputItem.quantity);
                        }
                    }
                    else
                    {
                        // 合成新品质物品需要创建新item
                        item _itemForCopy = item.Clone();
                        // _itemForCopy的品质值由合成材料的品质值的平均决定
                        int quality = 0; // 品质值
                        int count = 0; // 用于计算平均值的物品数量

                        if (combineItemGuideList[0].itemInfo.hasQuality)
                        {
                            quality += combineItemGuideList[0].itemInfo.Quality;
                            count++;
                        }

                        if (combineItemGuideList[1].itemInfo.hasQuality)
                        {
                            quality += combineItemGuideList[1].itemInfo.Quality;
                            count++;
                        }

                        if (count > 0)
                        {
                            quality /= count; // 计算平均值
                        }
                        else
                        {
                            quality = 50; // 如果都没有品质值，那么品质值固定为50
                        }
                        _itemForCopy.Quality = quality; // 设置新物品的品质值

                        if (_itemForCopy.Quality < 25)
                        {
                            _itemForCopy.itemCiti = ItemQuality.Inferior;
                        }
                        else if (_itemForCopy.Quality < 60)
                        {
                            _itemForCopy.itemCiti = ItemQuality.Normal;
                        }
                        else if (_itemForCopy.Quality < 85)
                        {
                            _itemForCopy.itemCiti = ItemQuality.Good;
                        }
                        else if (_itemForCopy.Quality < 95)
                        {
                            _itemForCopy.itemCiti = ItemQuality.Excellent;
                        }
                        else
                        {
                            _itemForCopy.itemCiti = ItemQuality.Legendary;
                        }
                        instance.combineResult.AddItem(_itemForCopy, 1);
                    }

                    findflag = true;
                    break;
                }
            }
            if (!findflag)
            {
                UnityEngine.Debug.Log("找不到编号为" + outputItem + "的物品");
            }
        }
    }
    // 拿取全部合成物品时，减少材料
    public void getCombineItem()
    {
        // 减少合成材料的数量
        foreach (var itemGuide in instance.combineItemGuideList)
        {
            if (itemGuide.itemInfo.itemId != 0)
            {
                itemGuide.decreaseItemWithoutPassive(HowManyTimesCreate);
            }
        }

        // 再次获取合成结果
        getCombine();

    }

    // 清空背包
    internal void ClearAllBag()
    {
        foreach (var itembag in instance.itembagList)
            itembag.deleteItemEquip();

        // 清空合成面板
        instance.combineResult.deleteItem();

        foreach (var itembag in instance.combineItemGuideList)
            itembag.deleteItem();

        // 清空垃圾桶
        instance.trashCan.deleteItem();

        // 清空仓库
        foreach (var itembag in instance.WarehouseList)
            itembag.deleteItemEquip();

        // 清空装备
        foreach (var itembag in instance.EquipmentList)
            itembag.deleteItemEquip();
    }

    // 检测背包里是否有某个ID的物品
    public bool IsHaveItem(int itemId)
    {
        foreach (var itembag in instance.itembagList)
        {
            if (itembag.itemInfo != null)
            {
                if (itembag.itemInfo.itemId == itemId)
                    return true;
            }
        }
        return false;
    }

    public ItemQuality SetItemQualityByQuality(int quality)
    {
        if (quality < 25)
        {
            return ItemQuality.Inferior;
        }
        else if (quality < 60)
        {
            return ItemQuality.Normal;
        }
        else if (quality < 85)
        {
            return ItemQuality.Good;
        }
        else if (quality < 95)
        {
            return ItemQuality.Excellent;
        }
        else
        {
            return ItemQuality.Legendary;
        }
    }
}
// HASHSET的自定义比较器
public class HashSetEqualityComparer<T> : IEqualityComparer<HashSet<T>>
{
    public bool Equals(HashSet<T> x, HashSet<T> y)
    {
        if (x == null && y == null)
            return true;
        if (x == null || y == null)
            return false;
        return x.SetEquals(y);
    }

    public int GetHashCode(HashSet<T> obj)
    {
        int hashCode = 0;
        foreach (var item in obj)
        {
            hashCode ^= item.GetHashCode();
        }
        return hashCode;
    }
}
public class ListEqualityComparer : IEqualityComparer<List<int>>
{
    public bool Equals(List<int> x, List<int> y)
    {
        // If the counts are not equal, the lists are not equal.
        if (x.Count != y.Count)
        {
            return false;
        }

        // Sort the lists.
        x.Sort();
        y.Sort();

        // Compare the elements.
        for (int i = 0; i < x.Count; i++)
        {
            if (x[i] != y[i])
            {
                return false;
            }
        }

        // If we got here, the lists are equal.
        return true;
    }

    public int GetHashCode(List<int> obj)
    {
        int hashcode = 0;
        foreach (int t in obj)
        {
            hashcode ^= t.GetHashCode();
        }
        return hashcode;
    }
}

