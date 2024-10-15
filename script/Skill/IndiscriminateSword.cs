using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

// public class IndiscriminateSword : SpellCardClass
// {
//     // 属性
//     public float damage = 1f;
//     public float damageMagnification = 1f;
//     // 悬停状态
//     [SerializeField] private float ReadyTime = 1.5f;
//     [SerializeField] private float angle;

//     private Rigidbody2D rb;
//     private Collider2D co;


//     // 随机方向
//     private float randomDirectionX, randomDirectionY;

//     //测试用 目标锁定
//     void Start()
//     {
//         SetStats();

//         rb = GetComponent<Rigidbody2D>();
//         co = GetComponent<Collider2D>();

//         randomDirectionX = UnityEngine.Random.Range(-1f, 1f);
//         randomDirectionY = math.sqrt((1 - randomDirectionX * randomDirectionX));


//         if (UnityEngine.Random.Range(0f, 1f) > 0.5f)
//             randomDirectionY *= -1;

//         // 计算并设置初始角度
//         Vector3 targetDir = new Vector3(randomDirectionX + transform.position.x, randomDirectionY + transform.position.y, 0) - transform.position;
//         transform.eulerAngles = new Vector3(0, 0, Vector3.Angle(transform.up, targetDir) * (randomDirectionX > 0 ? -1 : 1));


//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if (statsUpdate == true)
//         {
//             SetStats();
//             // 检查更新，如果升级了那么更新。
//             // 更新之后重新生成。
//             // TODO 最好写到父类里
//         }
//         ReadyTime -= Time.deltaTime;
//         // 悬停状态
//         if (ReadyTime > 1)
//         {
//             //collider2D.enabled = false;
//             //transform.eulerAngles = new Vector3(0, 0, angle);

//             rb.velocity = new Vector2(randomDirectionX, randomDirectionY) * (3f - ReadyTime);
//         }
//         else if (ReadyTime > 0)
//         {
//             rb.velocity = Vector2.zero;
//             Debug.DrawLine(transform.position, PlayerHealthControl.instance.transform.position, Color.blue);

//             //transform.eulerAngles = 

//             // Vector3 enemyDir = new Vector3(randomDirectionX + transform.position.x, randomDirectionY + transform.position.y, 0) - transform.position;


//             // 角度计算这里有问题
//             //             float temp = Vector3.Angle(transform.eulerAngles, targettransform.position - transform.position);
//             //             Debug.Log("这个数字到底是多少" + temp);
//             //             transform.eulerAngles = Vector3.MoveTowards(transform.eulerAngles,
//             //             new Vector3(0, 0, temp)
//             // ,
//             //         20);
//             //collider2D.enabled = true;


//             //transform.eulerAngles = Vector3.MoveTowards(transform.eulerAngles,new Vector3(60, 60, 60);
//             // 3);


//         }
//         else if (ReadyTime > -1)
//         {
//             //直接位置=MoveTowards就可以，TODO
//             //rb.velocity = (targettransform.position - transform.position).normalized * 7;
//             // rigidbody2D.velocity = new Vector2(PlayerHealthControl.instance.transform.position.x, PlayerHealthControl.instance.transform.position.y);
//         }
//         else if (ReadyTime < -4)
//         {
//             Destroy(gameObject);
//         }
//     }


//     private void OnTriggerEnter2D(Collider2D collider)
//     {
//         if (collider.tag == "Enemy")
//         {
//             collider.GetComponent<EnemyControl>().TakeDamage(damage * damageMagnification, true, (int)DamageType.SCDamage);
//             Destroy(gameObject);
//         }
//     }

//     public void SetStats()
//     {
//         damageMagnification = stats[SpellCardLevel].damage;
//         statsUpdate = false;
//     }
// }
