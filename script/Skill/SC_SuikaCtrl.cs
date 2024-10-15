using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

// public class SC_SuikaCtrl : SpellCardClass
// {
//     public EnemyDamagerClass damager;
//     private float throwCounter;
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
//         throwCounter -= Time.deltaTime;
//         if (throwCounter <= 0)
//         {
//             throwCounter = stats[SpellCardLevel].timeBetweenAttacks;

//             for (int i = 0; i < stats[SpellCardLevel].amout; i++)
//             {
//                 Instantiate(damager, damager.transform.position, damager.transform.rotation).gameObject.SetActive(true);
//             }
//         }
//     }
//     void SetStats()
//     {
//         damager.damageAmount = stats[SpellCardLevel].damage;
//         damager.lifeTime = stats[SpellCardLevel].duration;
//         damager.transform.localScale = Vector3.one * stats[SpellCardLevel].range;

//         throwCounter = 0;
//     }
// }
