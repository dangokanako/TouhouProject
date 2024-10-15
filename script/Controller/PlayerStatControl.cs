using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 废弃
// public class PlayerStatControl : MonoBehaviour
// {
//     public static PlayerStatControl instance;
//     void Awake()
//     {
//         instance = this;
//     }

//     public List<PlayerStatValue> moveSpeed, health, pickupRange, maxWeapon;
//     public int moveSpeedLevelCount, healthLevelCount, pickupRangeLevelCount;
//     public int moveSpeedLevel, healthLevel, pickupRangeLevel, maxWeaponLevel;

//     void Start()
//     {
//         // 懒得在编辑器里写一堆，于是自动生成
//         for (int i = moveSpeed.Count - 1; i < moveSpeedLevelCount; i++)
//         {
//             moveSpeed.Add(new PlayerStatValue(moveSpeed[i].cost + moveSpeed[1].cost,
//             moveSpeed[i].value + (moveSpeed[1].value - moveSpeed[0].value)
//             ));
//         }
//         for (int i = health.Count - 1; i < healthLevelCount; i++)
//         {
//             health.Add(new PlayerStatValue(health[i].cost + health[1].cost,
//             health[i].value + (health[1].value - health[0].value)
//             ));
//         }

//         for (int i = pickupRange.Count - 1; i < pickupRangeLevelCount; i++)
//         {
//             pickupRange.Add(new PlayerStatValue(pickupRange[i].cost + pickupRange[1].cost,
//             pickupRange[i].value + (pickupRange[1].value - pickupRange[0].value)
//             ));
//         }
//     }

//     public void UpdateUpgradeButtonDisplay()
//     {



//         //     if (moveSpeedLevel < moveSpeed.Count - 1)
//         //     {
//         //         UIControl.instance.moveSpeedUpgaredeDisplay.UpgradeDisplay(moveSpeed[moveSpeedLevel + 1].cost, moveSpeed[moveSpeedLevel].value, moveSpeed[moveSpeedLevel + 1].value);
//         //     }
//         //     else
//         //     {
//         //         UIControl.instance.moveSpeedUpgaredeDisplay.ShowMaxLevel();
//         //     }


//         //     if (healthLevel < health.Count - 1)
//         //     {
//         //         UIControl.instance.healthUpgaredeDisplay.UpgradeDisplay(health[healthLevel + 1].cost, health[healthLevel].value, health[healthLevel + 1].value);
//         //     }
//         //     else
//         //     {
//         //         UIControl.instance.healthUpgaredeDisplay.ShowMaxLevel();
//         //     }

//         //     if (pickupRangeLevel < pickupRange.Count - 1)
//         //     {
//         //         UIControl.instance.pickupUpgaredeDisplay.UpgradeDisplay(pickupRange[pickupRangeLevel + 1].cost,
//         // pickupRange[pickupRangeLevel].value, pickupRange[pickupRangeLevel + 1].value);
//         //     }
//         //     else
//         //     {
//         //         UIControl.instance.pickupUpgaredeDisplay.ShowMaxLevel();
//         //     }

//         //     if (maxWeaponLevel < maxWeapon.Count - 1)
//         //     {
//         //         UIControl.instance.maxweaponUpgaredeDisplay.UpgradeDisplay(maxWeapon[maxWeaponLevel + 1].cost,
//         //         maxWeapon[maxWeaponLevel].value, maxWeapon[maxWeaponLevel + 1].value);
//         //     }
//         //     else
//         //     {
//         //         UIControl.instance.maxweaponUpgaredeDisplay.ShowMaxLevel();
//         //     }
//     }

//     // 四个购买按钮
//     public void Purcharge1()
//     {
//         moveSpeedLevel++;
//         AssetControl.instance.ReducePoint(moveSpeed[moveSpeedLevel].cost);
//         UpdateUpgradeButtonDisplay();
//         MainPlayer.instance.moveSpeed = moveSpeed[moveSpeedLevel].value;
//     }

//     public void Purcharge2()
//     {
//         healthLevel++;
//         AssetControl.instance.ReducePoint(health[healthLevel].cost);
//         UpdateUpgradeButtonDisplay();
//         PlayerHealthControl.instance.maxHealth = health[healthLevel].value;
//         // TODO
//         PlayerHealthControl.instance.currentHealth += PlayerHealthControl.instance.maxHealth * 0.2f;
//     }

//     public void Purcharge3()
//     {
//         pickupRangeLevel++;
//         AssetControl.instance.ReducePoint(pickupRange[pickupRangeLevel].cost);
//         UpdateUpgradeButtonDisplay();
//         PlayerHealthControl.instance.pickupRange = pickupRange[pickupRangeLevel].value;
//     }

//     public void Purcharge4()
//     {
//         maxWeaponLevel++;
//         AssetControl.instance.ReducePoint(maxWeapon[maxWeaponLevel].cost);
//         UpdateUpgradeButtonDisplay();
//         MainPlayer.instance.MaxSC = Mathf.RoundToInt(maxWeapon[maxWeaponLevel].value);
//     }


//     void Update()
//     {
//         // 这个写法没问题吗
//         if (UIControl.instance.levelupPanel.activeSelf == true)
//         {
//             UpdateUpgradeButtonDisplay();
//         }
//     }
// }

// [System.Serializable]
// public class PlayerStatValue
// {

//     public int cost;
//     public float value;
//     public PlayerStatValue(int _newCost, float _newamount)
//     {
//         cost = _newCost;
//         value = _newamount;
//     }
// }