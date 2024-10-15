using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 废弃
// public class SpellCardClass : MonoBehaviour
// {
//     public string SCName;
//     // 技能等级
//     public int SpellCardLevel;
//     public List<SpellCardStats> stats;

//     // 显示图标
//     public Sprite icon;
//     //[HideInInspector]
//     // 检查更新，如果升级了那么更新。
//     // 更新之后重新生成。
//     public bool statsUpdate;

//     public void SpellCardLevelUp()
//     {
//         if (SpellCardLevel < stats.Count - 1)
//         {
//             SpellCardLevel++;
//             statsUpdate = true;

//             if (SpellCardLevel >= stats.Count - 1)
//             {
//                 MainPlayer.instance.fullyUpgradeSC.Add(this);
//                 MainPlayer.instance.assignedSC.Remove(this);
//             }
//         }
//     }
// }

// // 用于记录符卡的每级不同属性，攻击啊，频率啊，之类的
// [System.Serializable]
// public class SpellCardStats
// {
//     // 伤害
//     public float damage;

//     // 速度  大小 攻击间隔 几个（指创建几个攻击对象） 持续时间
//     public float speed, range, timeBetweenAttacks, amout, duration;

//     // 升级描述
//     public string upgradeDescripton;
// }
