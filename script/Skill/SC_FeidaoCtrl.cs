using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.Mathematics;
using UnityEngine;

// public class SC_FeidaoCtrl : SpellCardClass
// {
//     public SC_FeidaoDamage damager;
//     public float shotCounter;
//     // 探测范围
//     public float SCRange_Base = 5f;
//     public float SCRange;
//     public LayerMask whatIsEnemyLayer;
//     // Start is called before the first frame update
//     void Start()
//     {
//         SetStats();
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if (statsUpdate)
//         {
//             statsUpdate = false;
//             SetStats();
//         }



//         shotCounter -= Time.deltaTime;
//         if (shotCounter <= 0)
//         {
//             shotCounter = stats[SpellCardLevel].timeBetweenAttacks;
//             // 如果范围里有敌人才发射飞刀
//             Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, SCRange,
//             whatIsEnemyLayer);
//             if (enemies.Length > 0)
//             {
//                 for (int i = 0; i < stats[SpellCardLevel].amout; i++)
//                 {
//                     Vector3 targetPosition = enemies[UnityEngine.Random.Range(0, enemies.Length)].transform.position;
//                     Vector3 direction = targetPosition - transform.position;
//                     float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
//                     angle -= 90;
//                     // angle加-15到+15的随机范围
//                     angle += UnityEngine.Random.Range(-15, 15);
//                     damager.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
//                     Instantiate(damager, transform.position, damager.transform.rotation).gameObject.SetActive(true);
//                 }
//             }
//         }
//     }

//     void SetStats()
//     {
//         damager.damageAmount = stats[SpellCardLevel].damage;
//         damager.lifeTime = stats[SpellCardLevel].duration;
//         // 大小
//         damager.transform.localScale = Vector3.one * stats[SpellCardLevel].range;
//         // 服了，两个range 
//         SCRange = SCRange_Base * stats[SpellCardLevel].range;
//         damager.moveSpeed = stats[SpellCardLevel].speed;
//         shotCounter = 0f;
//     }
// }
