using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using System.Linq;
// using Microsoft.Unity.VisualStudio.Editor;

public class ItemBag : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDropHandler
{

    public int AutoHealReady;

    // 是否启用
    public bool isActive;
    // 背包格子的位置
    // public int Postion;
    // 背包格子的物品
    public item itemInfo;
    // 选中的背包格子，如果被选中那么为true
    public bool isChoosed;
    // 物品数量
    public int itemCount;
    // 显示物品信息的标识
    public bool isShowItemInfo;
    [Header("背包类型")]
    // 背包类型
    public BagTypeEnum BagType;
    [Header("技能点专用，其他背包请留空")]
    // 技能点专用，是否已经激活
    public SkillTypeEnum skillType;
    // 技能点专用，指向可以激活的目标
    public List<ItemBag> SkillPoint_Active_ItemBag;

    [Header("物品显示组件")]
    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private TMP_Text itemNumText;
    [SerializeField] private UnityEngine.UI.Image itemImage;
    [Header("是否改变过大小")]
    public bool isChangeSize;
    void Awake()
    {
        // 注意，背包里的这里要预设好
        if (itemNameText == null)
        {
            var texts = this.GetComponentsInChildren<TMP_Text>();
            if (texts.Length == 1)
            {
                itemNumText = texts[0];
            }
            else
            {
                itemNameText = texts[0]; // 获取第一个TMP_Text组件
                itemNumText = texts[1]; // 获取第二个TMP_Text组件
            }
        }
        if (itemImage == null)
        {
            var images = this.GetComponentsInChildren<UnityEngine.UI.Image>();
            itemImage = images[1];

        }

    }

    void Start()
    {
        if (itemInfo == null)
        {
            itemInfo = ItemControl.instance.itemGuide[0];
            SetNoItem();
        }
        // 在没有打开背包的时候，仓库没有初始化，此时遍历到仓库的时候会报错。

    }


    void Update()
    {

        if (isChoosed != isChangeSize) // 检查 isChoosed 的状态是否发生了改变
        {
            if (isChoosed)
            {
                this.transform.localScale = new Vector2(1.2f, 1.2f);
                // 向上移动8.3个像素
                this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 8.3f);
            }
            else
            {
                this.transform.localScale = new Vector2(1f, 1f);
                this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - 8.3f);
            }
            isChangeSize = isChoosed; // 更新 previousIsChoosed 的值
        }
    }


    public void AddItem(item _item)
    {
        if (itemInfo == null)
        {
            Debug.Log("22");
        }
        // 堆叠操作
        if (itemInfo.itemId == _item.itemId)
        {
            SetItemImage(++itemCount);
        }
        else if (itemInfo.itemId == 0)
        {
            itemInfo = _item;
            itemCount = 1;
            SetItemImage(itemCount);
        }
        else
        {
            Debug.LogError("致命错误，添加物品时，背包已有一个其他物品");
        }

        // 图鉴增加
        GlobalControl.instance.AddItemDiscovered(_item.itemId);
    }
    public void AddItem(item _item, int num)
    {

        // 堆叠操作
        if (itemInfo.itemId == _item.itemId)
        {
            itemCount += num;
            SetItemImage(itemCount);
        }
        else if (itemInfo.itemId == 0)
        {
            itemInfo = _item;
            itemCount = num;
            SetItemImage(itemCount);
        }
        else
        {
            Debug.LogError("致命错误，添加物品时，背包已有一个其他物品");
        }

        GlobalControl.instance.AddItemDiscovered(_item.itemId);
    }
    // 删除物品，直接清零
    public void deleteItem()
    {
        itemInfo = ItemControl.instance.itemGuide[0];
        itemCount = 0;
        SetNoItem();
    }
    // 删除在有效装备栏的物品，直接清零
    public void deleteItemEquip()
    {
        if (itemInfo == null) return;
        if (itemInfo.itemId == ItemControl.instance.itemGuide[0].itemId)
            return;

        // 如果是被动装备，那么丢弃被动效果
        if (itemInfo.isPassive)
            itemInfo.discardPassive();

        itemInfo = ItemControl.instance.itemGuide[0];
        itemCount = 0;
        SetNoItem();
    }
    // 减少物品数量
    public void decreaseItem(int num)
    {
        itemCount -= num;
        if (itemCount <= 0)
        {
            // 如果是被动装备，那么丢弃被动效果
            if (itemInfo.isPassive)
                itemInfo.discardPassive();

            itemInfo = ItemControl.instance.itemGuide[0];
            SetNoItem();
        }
        else
        {
            SetItemImage(itemCount);
        }
    }
    // 减少物品数量，不执行被动效果
    public void decreaseItemWithoutPassive(int num)
    {
        itemCount -= num;
        if (itemCount <= 0)
        {
            itemInfo = ItemControl.instance.itemGuide[0];
            SetNoItem();
        }
        else
        {
            SetItemImage(itemCount);
        }
    }

    // 设置物品显示的文字和信息（需要传入数量）
    public void SetItemImage(int num)
    {

        if (num == 0) num = 1;

        if (itemNameText != null)
            itemNameText.text = itemInfo.itemName;
        itemNumText.text = "" + num;

        // 如果是不可堆叠的物品，则不显示数字
        if (itemInfo.isDuidie == 0 && num == 1)
        {
            itemNumText.text = "";
        }
        // 图像设置
        if (!itemImage.IsActive())
        {
            itemImage.gameObject.SetActive(true);
        }
        itemImage.sprite = itemInfo.itemImage;
        if (BagType == BagTypeEnum.SkillPointShop)
        {
            // 设置图片控件的大小为新图片的原始大小
            itemImage.rectTransform.sizeDelta = new Vector2(itemInfo.itemImage.rect.width, itemInfo.itemImage.rect.height);
        }

        // itemNumText.color = Color.black;
        if (itemNameText != null)
            itemNameText.color = itemInfo.itemColor;
    }
    // 无物品显示状态
    public void SetNoItem()
    {
        // 8.31新加
        itemInfo = ItemControl.instance.itemGuide[0];

        if (itemNameText != null)
            itemNameText.text = "";
        itemNumText.text = "";
        itemImage.gameObject.SetActive(false);

        // 如果是垃圾桶，显示垃圾桶的图片
        if (BagType == BagTypeEnum.Trash)
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = ItemControl.instance.itemGuide[44].itemImage;
        }
    }

    // 物品消耗
    public void depleteItem()
    {
        // 开始消耗
        itemCount--;
        if (itemCount <= 0)
        {
            // 如果是被动装备，那么丢弃被动效果
            if (itemInfo.isPassive)
                itemInfo.discardPassive();

            itemInfo = ItemControl.instance.itemGuide[0];
            SetNoItem();
        }
        else
        {
            SetItemImage(itemCount);
        }
    }

    // 鼠标进入时显示信息框
    public void OnPointerEnter()
    {
        isShowItemInfo = true;
    }

    public void OnPointerExit()
    {
        isShowItemInfo = false;
        ItemControl.instance.isShowItemCount = 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (BagType == BagTypeEnum.NitoriShop)
            return;

        if (eventData.button == PointerEventData.InputButton.Left)
        {



            // 处理左键点击事件
            if (BagType == BagTypeEnum.CombineResult)
            {
                OnClickBag_Combine();
            }
            else if (BagType == BagTypeEnum.CombineMaterial)
            {
                OnClickBag_Combine_progress();
            }
            else if (BagType == BagTypeEnum.SkillPointShop)
            {
                OnClickSkillPoint();
            }
            else
                OnClickBag();
        }
        else
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            // 处理右键点击事件
            // 如果是合成界面
            if (ItemControl.instance.isCombinePlane)
            {
                // 如果手上没有东西
                if (ItemControl.instance.hoverItem.itemId == 0)
                {
                    // 如果可以堆叠
                    if (itemInfo.isDuidie == 1)
                    {
                        // 那么拿取一个物品
                        ItemControl.instance.hoverItem = itemInfo;
                        ItemControl.instance.hoverImage.sprite = itemInfo.itemImage;
                        ItemControl.instance.hoverItemCount += 1;
                        ItemControl.instance.SethoverItemCountText(ItemControl.instance.hoverItemCount);

                        // 减少一个物品
                        itemCount -= 1;
                        SetItemImage(itemCount);
                        if (itemCount <= 0)
                        {
                            this.SetNoItem();
                        }

                    }
                }
                else
                {

                }
            }
        }
    }

    // 整理背包功能
    // 打开合成界面时。鼠标点击时，背包物品移动到鼠标上。
    public void OnClickBag()
    {
        if (BagType == BagTypeEnum.CombineResult || BagType == BagTypeEnum.SkillPointShop)
            return;

        if (BagType == BagTypeEnum.CombineMaterial)
        {
            OnClickBag_Combine_progress();
            return;
        }


        if (ItemControl.instance.isCombinePlane || UIControl.instance.levelupPanel.activeSelf)
        {
            // 如果鼠标上和背包都有物品了，那么执行替换或者堆叠操作
            if (ItemControl.instance.hoverItem.itemId != 0 && this.itemInfo.itemId != 0)
            {

                // 如果本背包是垃圾桶，那么直接覆盖原物品
                if (BagType == BagTypeEnum.Trash)
                {
                    // 设置背包上的物品
                    this.itemInfo = ItemControl.instance.hoverItem;
                    this.itemCount = ItemControl.instance.hoverItemCount;
                    SetItemImage(this.itemCount);

                    // 移除鼠标上的物品
                    ItemControl.instance.hoverItem = ItemControl.instance.itemGuide[0];
                    ItemControl.instance.SethoverItemCountText(0);

                    return;
                }

                if (ItemControl.instance.hoverItem.itemId != this.itemInfo.itemId)
                {

                    // 获取鼠标上的物品
                    item tempItem = ItemControl.instance.hoverItem;
                    int tempItemCount = ItemControl.instance.hoverItemCount;

                    // 设置鼠标上的物品
                    ItemControl.instance.hoverItem = this.itemInfo;
                    ItemControl.instance.SethoverItemCountText(this.itemCount);

                    // 图片切换到背包上的物品
                    ItemControl.instance.hoverImage.sprite = this.itemInfo.itemImage;

                    // 移除背包上物品时，取消被动效果
                    if (itemInfo.isPassive && isEffective(this.itemInfo.itemSlot))
                        itemInfo.discardPassive();


                    // 设置背包上的物品
                    this.itemInfo = tempItem;
                    this.itemCount = tempItemCount;
                    SetItemImage(this.itemCount);
                    // 设置背包上物品时，增加被动效果
                    if (itemInfo.isPassive && isEffective(this.itemInfo.itemSlot))
                        itemInfo.Passive();
                }
                else
                {

                    if (this.itemInfo.isDuidie == 1)
                    {
                        // 如果鼠标上的物品等于背包上的物品，那么执行堆叠操作。我相信自己暂时不会写可堆叠的被动物品。
                        // 堆叠操作
                        if (itemCount + ItemControl.instance.hoverItemCount <= itemInfo.MaxDuidie)
                        {
                            itemCount += ItemControl.instance.hoverItemCount;
                            SetItemImage(itemCount);
                            // 移除鼠标上的物品
                            ItemControl.instance.hoverItem = ItemControl.instance.itemGuide[0];
                            ItemControl.instance.SethoverItemCountText(0);

                        }
                        else
                        {
                            itemCount = itemInfo.MaxDuidie;
                            SetItemImage(itemCount);
                            ItemControl.instance.SethoverItemCountText(ItemControl.instance.hoverItemCount - itemInfo.MaxDuidie + itemCount);
                        }
                    }
                }

            }

            // 如果鼠标上有物品，背包上没有物品，那么执行放置操作
            else if (ItemControl.instance.hoverItem.itemId != 0 && this.itemInfo.itemId == 0 && isVaildPutIn(ItemControl.instance.hoverItem.itemSlot))
            {
                // 设置背包上的物品
                this.itemInfo = ItemControl.instance.hoverItem;
                this.itemCount = ItemControl.instance.hoverItemCount;
                SetItemImage(this.itemCount);
                // 设置背包上物品时，增加被动效果。垃圾桶在这个判定内
                if (itemInfo.isPassive && isEffective(this.itemInfo.itemSlot))
                    itemInfo.Passive();

                // 移除鼠标上的物品
                ItemControl.instance.hoverItem = ItemControl.instance.itemGuide[0];
                ItemControl.instance.SethoverItemCountText(0);
            }

            // 如果鼠标上没有物品，背包上有物品，那么执行拿取操作
            else if (ItemControl.instance.hoverItem.itemId == 0 && this.itemInfo.itemId != 0)
            {
                // 设置鼠标上的物品
                ItemControl.instance.hoverItem = this.itemInfo;
                ItemControl.instance.SethoverItemCountText(this.itemCount);

                // 图片切换
                ItemControl.instance.hoverImage.sprite = this.itemInfo.itemImage;



                // 移除物品
                // 如果能发动效果，那么丢弃被动效果
                if (this.itemInfo.isPassive && isEffective(this.itemInfo.itemSlot))
                {
                    this.itemInfo.discardPassive();
                }

                itemInfo = ItemControl.instance.itemGuide[0];
                this.SetNoItem();
            }
        }
    }

    // 合成结果点击事件，因为逻辑略有差别，所以单独维护。
    // 只许拿取，不许放入。
    public void OnClickBag_Combine()
    {
        // 如果鼠标上没有物品，背包上有物品，那么执行拿取操作
        if (ItemControl.instance.hoverItem.itemId == 0 && this.itemInfo.itemId != 0)
        {
            // 设置鼠标上的物品
            ItemControl.instance.hoverItem = this.itemInfo;
            ItemControl.instance.SethoverItemCountText(this.itemCount);

            // 图片切换
            ItemControl.instance.hoverImage.sprite = this.itemInfo.itemImage;

            deleteItem();

            // 播放音效
            SFXManger.instance.PlaySFX(2);

            // 事件
            ItemControl.instance.getCombineItem();
        }
    }

    // 合成材料的点击事件，因为还要考虑到合成结果逻辑，所以单独维护。
    public void OnClickBag_Combine_progress()
    {
        if (ItemControl.instance.isCombinePlane)
        {
            // 非材料不参与合成
            if (!ItemControl.instance.hoverItem.isMaterial && ItemControl.instance.hoverItem.itemId != 0)
            {
                return;
            }

            // 如果鼠标上和背包都有物品了，那么执行替换或者堆叠操作
            if (ItemControl.instance.hoverItem.itemId != 0 && this.itemInfo.itemId != 0)
            {
                if (ItemControl.instance.hoverItem.itemId != this.itemInfo.itemId)
                {

                    // 获取鼠标上的物品
                    item tempItem = ItemControl.instance.hoverItem;
                    int tempItemCount = ItemControl.instance.hoverItemCount;

                    // 设置鼠标上的物品
                    ItemControl.instance.hoverItem = this.itemInfo;
                    ItemControl.instance.SethoverItemCountText(this.itemCount);

                    // 图片切换到背包上的物品
                    ItemControl.instance.hoverImage.sprite = this.itemInfo.itemImage;
                    // 移除背包上物品时，取消被动效果  //20240721 手上的东西也没有被动的，所以替换之后不触发这里
                    // if (itemInfo.isPassive)
                    //     itemInfo.discardPassive();


                    // 设置背包上的物品
                    this.itemInfo = tempItem;
                    this.itemCount = tempItemCount;
                    SetItemImage(this.itemCount);
                }
                else
                {
                    if (this.itemInfo.isDuidie == 1)
                    {
                        // 如果鼠标上的物品等于背包上的物品，那么执行堆叠操作
                        // 堆叠操作
                        if (itemCount + ItemControl.instance.hoverItemCount <= itemInfo.MaxDuidie)
                        {
                            itemCount += ItemControl.instance.hoverItemCount;
                            SetItemImage(itemCount);
                            // 移除鼠标上的物品
                            ItemControl.instance.hoverItem = ItemControl.instance.itemGuide[0];
                            ItemControl.instance.SethoverItemCountText(0);

                        }
                        else
                        {
                            itemCount = itemInfo.MaxDuidie;
                            SetItemImage(itemCount);
                            ItemControl.instance.SethoverItemCountText(ItemControl.instance.hoverItemCount - itemInfo.MaxDuidie + itemCount);
                        }
                    }
                }
            }

            // 如果鼠标上有物品，背包上没有物品，那么执行放置操作
            else if (ItemControl.instance.hoverItem.itemId != 0 && this.itemInfo.itemId == 0)
            {
                // 设置背包上的物品
                this.itemInfo = ItemControl.instance.hoverItem;
                this.itemCount = ItemControl.instance.hoverItemCount;
                SetItemImage(this.itemCount);

                // 移除鼠标上的物品
                ItemControl.instance.hoverItem = ItemControl.instance.itemGuide[0];
                ItemControl.instance.SethoverItemCountText(0);

            }

            // 如果鼠标上没有物品，背包上有物品，那么执行拿取操作
            else if (ItemControl.instance.hoverItem.itemId == 0 && this.itemInfo.itemId != 0)
            {
                // 设置鼠标上的物品
                ItemControl.instance.hoverItem = this.itemInfo;
                ItemControl.instance.SethoverItemCountText(this.itemCount);

                // 图片切换
                ItemControl.instance.hoverImage.sprite = this.itemInfo.itemImage;

                // 移除物品
                itemInfo = ItemControl.instance.itemGuide[0];
                this.SetNoItem();
            }

            // 点完之后开始判断是否存在合成配方，并显示合成结果物品
            ItemControl.instance.getCombine();
        }
    }


    // 点击技能点页面
    public void OnClickSkillPoint()
    {
        Debug.Log("点击技能点页面");
        // 如果鼠标上有物品，那么直接返回
        if (ItemControl.instance.hoverItem.itemId != 0)
            return;


        if (ExpLevelControl.instance.CurrentSkillPoint < 1)
        {
            SFXManger.instance.PlaySFX(3);
            return;
        }

        // 如果鼠标上没有物品，目标上有物品，那么执行购买操作
        if (ItemControl.instance.hoverItem.itemId == 0 && this.itemInfo.itemId != 0)
        {
            if (this.skillType == SkillTypeEnum.Activated)
            {
                UIControl.instance.ShowTips("该技能已获得", Input.mousePosition);
                SFXManger.instance.PlaySFX(3);
                return;
            }

            if (this.skillType == SkillTypeEnum.Unactived)
            {
                UIControl.instance.ShowTips("该技能尚未解锁", Input.mousePosition);
                SFXManger.instance.PlaySFX(3);
                return;
            }
            // // 计算价格是否能购买
            // int price = int.Parse(shopPriceText.text);
            // // 货币类型 普通P点情况
            // // 获取priceImage路径
            // if (currencytype == 0)
            // {
            //     if (price > AssetControl.instance.currentPoint)
            //     {
            //         SFXManger.instance.PlaySFX(3);
            //         return;
            //     }
            // }
            // else if (currencytype == 1)
            // {
            //     if (price > AssetControl.instance.currentSuperPoint)
            //     {
            //         SFXManger.instance.PlaySFX(3);
            //         return;
            //     }
            // }
            // 播放音效
            SFXManger.instance.PlaySFX(7);
            ExpLevelControl.instance.CurrentSkillPoint -= 1;

            // // 扣除P点
            // if (currencytype == 0)
            //     AssetControl.instance.ReducePoint(price);
            // else if (currencytype == 1)
            //     AssetControl.instance.ReduceSuperPoint(price);


            // 如果是角色升级，那么直接执行物品使用操作，并返回。
            if (this.itemInfo.isUseImmediately)
            {
                this.itemInfo.Use();

                this.isShowItemInfo = false;
                SkillPointControl.SetToActivated(this);

                foreach (var itembag in SkillPoint_Active_ItemBag)
                {
                    SkillPointControl.SetToActive(itembag);
                }

                // this.SetNoItem();
                return;
            }
            else
            {
                Debug.LogError("技能点商店物品未设置为立即使用");
            }

        }
    }
    /// <summary>
    /// 是否能放入
    /// </summary>
    /// <param name="itemSlot">传入的道具槽位编号</param>
    /// <returns>是否发挥效果另写判断</returns>
    public bool isVaildPutIn(ItemSlotEnum itemSlot)
    {
        // 反写
        // 譬如，红色装备栏只允许OnlyRed，AllEquipment，AllExceptWarehouse，All
        if (this.BagType == BagTypeEnum.RedEquip)
        {
            if (itemSlot != ItemSlotEnum.OnlyRed && itemSlot != ItemSlotEnum.AllEquipment && itemSlot != ItemSlotEnum.AllExceptWarehouse && itemSlot != ItemSlotEnum.All)
            {
                UIControl.instance.ShowTips("槽位不符", Input.mousePosition);
                return false;
            }
        }

        if (this.BagType == BagTypeEnum.BlueEquip)
        {
            if (itemSlot != ItemSlotEnum.OnlyBlue && itemSlot != ItemSlotEnum.AllEquipment && itemSlot != ItemSlotEnum.AllExceptWarehouse && itemSlot != ItemSlotEnum.All)
            {
                UIControl.instance.ShowTips("槽位不符", Input.mousePosition);
                return false;
            }
        }

        if (this.BagType == BagTypeEnum.GreenEquip)
        {
            if (itemSlot != ItemSlotEnum.OnlyGreen && itemSlot != ItemSlotEnum.AllEquipment && itemSlot != ItemSlotEnum.AllExceptWarehouse && itemSlot != ItemSlotEnum.All)
            {
                UIControl.instance.ShowTips("槽位不符", Input.mousePosition);
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// 是否能发挥效果
    /// </summary>
    /// <param name="itemSlot"></param>
    /// <returns></returns>
    public bool isEffective(ItemSlotEnum itemSlot)
    {

        if (this.BagType == BagTypeEnum.Trash)
            return false;

        if (itemSlot == 0)
            return true;

        if (itemSlot == ItemSlotEnum.All)
        {
            SFXManger.instance.PlaySFX(23);
            return true;
        }

        if (itemSlot == ItemSlotEnum.AllExceptWarehouse)
        {
            if (this.BagType == BagTypeEnum.Warehouse)
            {
                return false;
            }
        }

        if (itemSlot == ItemSlotEnum.AllEquipment)
        {
            if (this.BagType == BagTypeEnum.HandBag || this.BagType == BagTypeEnum.Warehouse)
            {
                return false;
            }
        }

        if (itemSlot == ItemSlotEnum.OnlyRed)
        {
            if (this.BagType != BagTypeEnum.RedEquip)
            {
                return false;
            }
        }

        if (itemSlot == ItemSlotEnum.OnlyBlue)
        {
            if (this.BagType != BagTypeEnum.BlueEquip)
            {
                return false;
            }
        }

        if (itemSlot == ItemSlotEnum.OnlyGreen)
        {
            if (this.BagType != BagTypeEnum.GreenEquip)
            {
                return false;
            }
        }


        SFXManger.instance.PlaySFX(23);
        return true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnClickBag();
    }
    public void OnDrop(PointerEventData eventData)
    {
        OnClickBag();
    }
}


// 背包类型枚举
public enum BagTypeEnum
{
    // 铁匠铺那边的特殊值
    NitoriShop = -4,
    // 合成结果
    CombineResult = -3,
    // 合成材料
    CombineMaterial = -2,
    // 垃圾桶
    Trash = -1,
    // 字面意思，右上角的背包位，可以直接使用，也可以发挥效果
    HandBag = 0,
    // 仓库位，只能存放物品，不能发挥被动效果。
    Warehouse = 1,
    // 红装备槽，只能存放红色装备。
    RedEquip = 2,
    // 蓝装备槽，只能存放蓝色装备。
    BlueEquip = 3,
    // 绿装备槽，只能存放绿色装备。
    GreenEquip = 4,
    // 金装备槽，可以存放全部装备。
    GoldEquip = 5,
    // 技能购买槽
    SkillPointShop = 10,
}

public enum SkillTypeEnum
{
    // 不能激活
    Unactived = 0,
    // 可以激活
    Active = 1,
    // 已经激活
    Activated = 2,
}