using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Drops_Star : DropsClass
{
    override protected void Start()
    {
        base.Start();
        expValue = 1;
    }

    override protected void Update()
    {
        base.Update();
    }

    override protected void PlaySE()
    {
        SFXManger.instance.PlaySFX(1);
    }

    override protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ExpLevelControl.instance.GetExp(expValue);
            PlaySE();
            Invoke("DestorySelf", 0.1f);

            // 统计数据
            GlobalControl.instance.currentTotalExp += expValue;
        }
    }
}
