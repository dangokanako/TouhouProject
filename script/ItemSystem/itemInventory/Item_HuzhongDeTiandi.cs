using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_HuzhongDeTiandi", menuName = "Inventory/Item_HuzhongDeTiandi")]
public class Item_HuzhongDeTiandi : item
{
    private string uniqueId = System.Guid.NewGuid().ToString();

    public override bool Use()
    {
        BuffCoroutineControl.instance.StartCoroutine(uniqueId, RegenHealth(1.5f));
        SFXManger.instance.PlaySFX(2);
        MainPlayer.instance.PlayItemAnime(this.itemImage);
        return true;
    }

    private IEnumerator RegenHealth(float regenPerSecond)
    {
        // 持续10秒
        int count = 0;
        while (count <= 10)
        {
            count++;
            PlayerHealthControl.instance.changeSP(regenPerSecond);
            // 等待1秒
            yield return new WaitForSeconds(1);
        }
    }

}
