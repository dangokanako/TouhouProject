using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Drops_Item : DropsClass
{
    // 道具文本显示
    public TMP_Text itemText;
    // 道具图像
    public SpriteRenderer itemImage;
    // 道具信息
    public item itemInfo;
    // 道具数量
    public int itemCount;

    override protected void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            // 检查背包是否可以添加
            int index = 0;
            if (ItemControl.instance.IsBagEmpty(itemInfo, ref index))
            {
                ItemControl.instance.AddItemToBag(itemInfo);
                PlaySE();
                Destroy(gameObject);

                // Invoke("DestorySelf", 0.1f);
            }
            else
            {
                // 如果物品正在向玩家移动，并且背包没有空位的话。立即关闭速度，并且轻轻落下。
                if (moveingToPlayer)
                {
                    moveingToPlayer = false;
                    rb.velocity = Vector2.zero;
                    rb.velocity = Vector2.up + new Vector2(Random.Range(-2f, 2f), 0);
                    rb.gravityScale = GraSet;
                    dropAnim = 2f;
                }
                else
                {
                    rb.velocity = Vector2.zero;
                }
            }
        }
    }

    override protected void PlaySE()
    {
        SFXManger.instance.PlaySFX(0);
    }

    // 物品掉落需要检测背包是否可以放下。如果可以，才吸收
    override protected void CheckMove()
    {
        // 一段时间检测一次，降低消耗
        checkCount -= Time.deltaTime;
        if (checkCount <= 0)
        {
            checkCount = timeBetweenCheck;

            // 如果距离计算合适，就移动
            if (Vector3.Distance(transform.position, PlayerHealthControl.instance.transform.position) < PlayerHealthControl.instance.pickupRange)
            {
                // 检查背包是否可以添加
                int index = 0;
                // Debug.Log("判断背包是否可以吸收物品：" + ItemControl.instance.IsBagEmpty(itemInfo, ref index));
                if (ItemControl.instance.IsBagEmpty(itemInfo, ref index))
                {
                    moveingToPlayer = true;
                }

            }

            // 存在一個BUG。如果背包满了，玩家去和物品重叠。之后再清理背包，并不会触发吸收。
            if (Vector3.Distance(transform.position, PlayerHealthControl.instance.transform.position) == 0)
            {
                // 检查背包是否可以添加
                int index = 0;
                if (ItemControl.instance.IsBagEmpty(itemInfo, ref index))
                {
                    ItemControl.instance.AddItemToBag(itemInfo);
                    PlaySE();
                    Destroy(gameObject);
                    //Invoke("DestorySelf", 0.1f);
                }
                else
                {
                    // 如果物品正在向玩家移动，并且背包没有空位的话。立即关闭速度，并且轻轻落下。
                    if (moveingToPlayer)
                    {
                        moveingToPlayer = false;
                        rb.velocity = Vector2.zero;
                        rb.velocity = Vector2.up + new Vector2(Random.Range(-2f, 2f), 0);
                        rb.gravityScale = GraSet;
                        dropAnim = 2f;
                    }
                    else
                    {
                        rb.velocity = Vector2.zero;
                    }
                }
            }
        }
    }
}
