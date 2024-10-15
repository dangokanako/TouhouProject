using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDropTable
{
    // 单例模式
    private static EnemyDropTable instance;
    public static EnemyDropTable Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EnemyDropTable();
            }
            return instance;
        }
    }


    public List<DropTable> dropTables = new List<DropTable>();
    public static readonly List<int> shouldnotFreshItem = new List<int> { 0, 44, 32, 24, 40, 41, 43, 34, 35, 110, 111, 115 };

    public static int GetGroupItem(int groupID)
    {
        // 从物品组中获取物品
        // 初级治疗药组
        if (groupID == (int)DropTableGroupEnum.BaseHeal)
        {
            // 从1，4
            int[] itemIDs = { 1, 4 };
            return itemIDs[Random.Range(0, itemIDs.Length)];
        }

        // 初级装备组
        if (groupID == (int)DropTableGroupEnum.BaseEquip)
        {
            int[] itemIDs = { 30, 48, 55, 51, 8, 25, 50, 7, 46, 70, 71, 72, 75, 76, 81, 82, 83, 84, 85, 86, 32, 93, 96, 105, 106, 107, 121, 123, 125 };
            return itemIDs[Random.Range(0, itemIDs.Length)];
        }
        // 装备升级组
        if (groupID == (int)DropTableGroupEnum.BaseEquipUpgrade)
        {
            int[] itemIDs = { 11, 22, 23 };
            return itemIDs[Random.Range(0, itemIDs.Length)];
        }
        // 一级符卡
        if (groupID == (int)DropTableGroupEnum.BaseSpellCard)
        {
            int[] itemIDs = { 3, 12, 13, 19, 122 };
            return itemIDs[Random.Range(0, itemIDs.Length)];
        }
        // 二级符卡
        if (groupID == (int)DropTableGroupEnum.NormalSpellCard)
        {
            int[] itemIDs = { 20, 21, 38, 39 };
            return itemIDs[Random.Range(0, itemIDs.Length)];
        }
        // 起手装备组
        if (groupID == (int)DropTableGroupEnum.BaseEquipStart)
        {
            int[] itemIDs = { 87, 120, 87 };
            return itemIDs[Random.Range(0, itemIDs.Length)];
        }
        // 二级治疗药
        if (groupID == (int)DropTableGroupEnum.NormalHeal)
        {
            // 从1，2，4，28
            int[] itemIDs = { 1, 2, 4, 28 };
            return itemIDs[Random.Range(0, itemIDs.Length)];
        }
        // 合成材料系（阴阳玉 飞刀 小小的黑暗 驱魔针）
        if (groupID == (int)DropTableGroupEnum.Material)
        {
            int[] itemIDs = { 31, 42, 5, 90 };
            return itemIDs[Random.Range(0, itemIDs.Length)];
        }
        // 三级治疗药
        if (groupID == (int)DropTableGroupEnum.HighHeal)
        {
            int[] itemIDs = { 26, 28, 10 };
            return itemIDs[Random.Range(0, itemIDs.Length)];
        }
        // 永久加成系
        if (groupID == (int)DropTableGroupEnum.PermanentAddition)
        {
            int[] itemIDs = { 14, 15, 16, 17, 18, 29, 97, 98, 99, 100, 103, 104 };
            return itemIDs[Random.Range(0, itemIDs.Length)];
        }
        // 魔理沙的信物专用
        if (groupID == (int)DropTableGroupEnum.MarisaToken)
        {
            if (GlobalControl.instance.isGetMarisa == false)
                return 32;
            else
                return -1;
        }
        // 永续符卡系列
        if (groupID == (int)DropTableGroupEnum.PassiveSpellCard)
        {
            int[] itemIDs = { 65, 66, 67, 68, 69 };
            return itemIDs[Random.Range(0, itemIDs.Length)];
        }
        if (groupID == (int)DropTableGroupEnum.FreeItem)
        {
            //50%概率出现一级装备
            //20%概率出现一级符卡
            //10%概率出现二级符卡
            //10%概率出现起手装备组
            //10%概率出现装备升级组
            int random = Random.Range(0, 100);
            if (random < 50)
            {
                return GetGroupItem((int)DropTableGroupEnum.BaseEquip);
            }
            else if (random < 70)
            {
                return GetGroupItem((int)DropTableGroupEnum.BaseSpellCard);
            }
            else if (random < 80)
            {
                return GetGroupItem((int)DropTableGroupEnum.NormalSpellCard);
            }
            else if (random < 90)
            {
                return GetGroupItem((int)DropTableGroupEnum.BaseEquipStart);
            }
            else
            {
                return GetGroupItem((int)DropTableGroupEnum.BaseEquipUpgrade);
            }
        }



        return 1;
    }

    public EnemyDropTable()
    {
        dropTables = new List<DropTable>
        {
            // 0 编号，备用
            new DropTable
            {
                expToGiveTimes = 1,
                pointToGiveTimes = 1,
                DropItemTables = new List<DropItem>
                {
                        new DropItem { ItemID = 1, DropRate = 30 },
                        new DropItem { ItemID = 2, DropRate = 30 },
                }
            },
            // 1 新手敌人_1 类型 大妖精 琪露诺 
            new DropTable
            {
                expToGiveTimes = 1,
                pointToGiveTimes = 1,
                DropItemTables = new List<DropItem>
                {
                    // 初级治疗药组
                    new DropItem { ItemGroupId = (int)DropTableGroupEnum.BaseHeal, DropRate = 250 },
                    // 装备起点系
                    new DropItem { ItemGroupId = (int)DropTableGroupEnum.BaseEquipStart, DropRate = 50 },
                    // 一级符卡系
                    new DropItem { ItemGroupId = (int)DropTableGroupEnum.BaseSpellCard, DropRate = 50 },
                    // 稀有
                    new DropItem { ItemGroupId = (int)DropTableGroupEnum.BaseEquipUpgrade, DropRate = 30 },
                    // 合成材料系
                    new DropItem { ItemGroupId = (int)DropTableGroupEnum.Material, DropRate = 30 },
                }
            },
            // 2 普通敌人
            new DropTable
            {
                expToGiveTimes = 2,
                pointToGiveTimes = 2,
                DropItemTables = new List<DropItem>
                {
                    // 二级治疗药
                    new DropItem { ItemGroupId = (int)DropTableGroupEnum.NormalHeal, DropRate = 200 },
                    // 装备起点系
                    new DropItem { ItemGroupId = (int)DropTableGroupEnum.BaseEquipStart, DropRate = 50 },
                    // 一级装备系
                    new DropItem { ItemGroupId = (int)DropTableGroupEnum.BaseEquip, DropRate = 50 },
                    // 符卡系
                    new DropItem { ItemGroupId = (int)DropTableGroupEnum.BaseSpellCard, DropRate = 50 },
                    // 材料系
                    new DropItem { ItemGroupId = (int)DropTableGroupEnum.Material, DropRate = 30 },
                    // 稀有
                    new DropItem { ItemGroupId = (int)DropTableGroupEnum.BaseEquipUpgrade, DropRate = 30 },
                }
            },
            // 3 BOSS 
            new DropTable
            {
                expToGiveTimes = 8,
                pointToGiveTimes = 8,
                DropItemTables = new List<DropItem>
                {
                    // 固定掉一个永久加成
                    new DropItem { ItemGroupId = (int)DropTableGroupEnum.PermanentAddition, DropRate = 7500 },
                    // 固定要一个一级装备
                    new DropItem { ItemGroupId = (int)DropTableGroupEnum.BaseEquip, DropRate = 7500 },
                    // 稀有
                    new DropItem { ItemGroupId = (int)DropTableGroupEnum.BaseEquipUpgrade, DropRate = 7500 },
                }
            },
            // 4 BOSS - 仮
            new DropTable
            {
                expToGiveTimes = 4,
                pointToGiveTimes = 4,
                DropItemTables = new List<DropItem>
                {
                    new DropItem { ItemGroupId = (int)DropTableGroupEnum.NormalHeal, DropRate = 1000 },
                    new DropItem { ItemGroupId = (int)DropTableGroupEnum.PermanentAddition, DropRate = 1000 },
                    new DropItem { ItemGroupId = (int)DropTableGroupEnum.BaseEquip, DropRate = 1000 },
                    new DropItem { ItemGroupId = (int)DropTableGroupEnum.BaseEquipUpgrade, DropRate = 1000 },
                }
            },
            // 5 letty  lili
            new DropTable
            {
                expToGiveTimes = 10,
                pointToGiveTimes = 10,
                DropItemTables = new List<DropItem>
                {
                    // 固定掉一个永久加成
                    new DropItem { ItemGroupId = (int)DropTableGroupEnum.PermanentAddition, DropRate = 7500 },
                    // 固定要一个一级装备
                    new DropItem { ItemGroupId = (int)DropTableGroupEnum.BaseEquip, DropRate = 7500 },
                    // 稀有
                    new DropItem { ItemGroupId = (int)DropTableGroupEnum.BaseEquipUpgrade, DropRate = 7500 },
                    // 固定一个二级符卡
                    new DropItem { ItemGroupId = (int)DropTableGroupEnum.NormalSpellCard, DropRate = 7500 },
                }
            },
            // 6 仮
            new DropTable
            {
                expToGiveTimes = 5,
                pointToGiveTimes = 5,
                DropItemTables = new List<DropItem>
                {
                    // 固定掉一个永久加成
                    new DropItem { ItemGroupId = (int)DropTableGroupEnum.PermanentAddition, DropRate = 1000 },
                    // 固定要一个一级装备
                    new DropItem { ItemGroupId = (int)DropTableGroupEnum.BaseEquip, DropRate = 1000 },
                    // 稀有
                    new DropItem { ItemGroupId = (int)DropTableGroupEnum.BaseEquipUpgrade, DropRate = 1000 },
                    // 固定一个二级符卡
                    new DropItem { ItemGroupId = (int)DropTableGroupEnum.NormalSpellCard, DropRate = 1000 },
                }
            },
            // 7 3级小怪
            new DropTable
            {
                expToGiveTimes = 4,
                pointToGiveTimes = 4,
                DropItemTables = new List<DropItem>
                {
                    // 三级治疗药
                    new DropItem { ItemGroupId = (int)DropTableGroupEnum.HighHeal, DropRate = 250 },
                    // 装备起点系
                    new DropItem { ItemGroupId = (int)DropTableGroupEnum.BaseEquipStart, DropRate = 40 },
                    // 一级装备系
                    new DropItem { ItemGroupId = (int)DropTableGroupEnum.BaseEquip, DropRate = 80 },
                    // 符卡系
                    new DropItem { ItemGroupId = (int)DropTableGroupEnum.BaseSpellCard, DropRate = 50 },
                    // 材料系
                    new DropItem { ItemGroupId = (int)DropTableGroupEnum.Material, DropRate = 50 },
                    // 稀有
                    new DropItem { ItemGroupId = (int)DropTableGroupEnum.BaseEquipUpgrade, DropRate = 30 },
                }
            },
            // 8 魔理沙
            new DropTable
            {
                expToGiveTimes = 20,
                pointToGiveTimes = 20,
                DropItemTables = new List<DropItem>
                {
                    // 固定掉一个永久加成
                    new DropItem { ItemGroupId = (int)DropTableGroupEnum.PermanentAddition, DropRate = 7500 },
                    // 固定要一个一级装备
                    new DropItem { ItemGroupId = (int)DropTableGroupEnum.BaseEquip, DropRate = 7500 },
                    // 稀有
                    new DropItem { ItemGroupId = (int)DropTableGroupEnum.BaseEquipUpgrade, DropRate = 7500 },
                    // 固定一个二级符卡
                    new DropItem { ItemGroupId = (int)DropTableGroupEnum.NormalSpellCard, DropRate = 7500 },
                    // 魔理沙的信物 固定掉落
                    new DropItem { ItemGroupId = (int)DropTableGroupEnum.MarisaToken, DropRate = 10000 },
                }
            },
            // 9 麻薯
            new DropTable
            {
                expToGiveTimes = 1,
                pointToGiveTimes = 1,
                DropItemTables = new List<DropItem>
                {
                    // 初级治疗药组
                    new DropItem { ItemGroupId = (int)DropTableGroupEnum.BaseHeal, DropRate = 28 },
                    // 装备起点系
                    // new DropItem { ItemGroupId = (int)DropTableGroupEnum.BaseEquipStart, DropRate = 10 },
                    // 一级符卡系
                    new DropItem { ItemGroupId = (int)DropTableGroupEnum.BaseSpellCard, DropRate = 5 },
                    // 稀有
                    new DropItem { ItemGroupId = (int)DropTableGroupEnum.BaseEquipUpgrade, DropRate = 3 },
                    // 合成材料系
                    new DropItem { ItemGroupId = (int)DropTableGroupEnum.Material, DropRate = 3 },
                }
            },
        };
    }

}




public class DropTable
{
    /// <summary>
    /// 掉落概率采用一个莫名其妙的方法。expToGiveTimes为随机次数。每次30%概率掉落一个。期望0.3。
    /// </summary>
    // 掉落的次数概率
    public int expToGiveTimes;

    /// <summary>
    /// 掉落点数
    /// </summary>
    public int pointToGiveTimes;

    public List<DropItem> DropItemTables; // 掉落表数组
}
public struct DropItem
{
    public int ItemGroupId; // 物品组ID
    public int ItemID; // 物品名称
    public int DropRate; // 掉落概率
}

public enum DropTableEnum
{
    Normal_1 = 1,
}


public enum DropTableGroupEnum
{
    // 初级治疗药组
    BaseHeal = 1,
    // 初级装备组
    BaseEquip = 2,
    // 装备升级组
    BaseEquipUpgrade = 3,
    // 一级符卡
    BaseSpellCard = 4,
    // 二级符卡
    NormalSpellCard = 5,
    // 起手装备组
    BaseEquipStart = 6,
    // 二级治疗药
    NormalHeal = 7,
    // 合成材料系（阴阳玉 飞刀 小小的黑暗 ）
    Material = 8,
    // 三级治疗药
    HighHeal = 9,
    // 永久加成系
    PermanentAddition = 10,
    // 魔理沙的信物专用
    MarisaToken = 11,
    // 永续符卡系列
    PassiveSpellCard = 12,
    // 免费道具专用刷新    
    FreeItem = 13,

}


