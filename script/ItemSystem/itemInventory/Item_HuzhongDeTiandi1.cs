using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_HuzhongDeTiandi1", menuName = "Inventory/Item_HuzhongDeTiandi1")]
public class Item_HuzhongDeTiandi1 : item
{

    private string uniqueId = System.Guid.NewGuid().ToString();
    private bool isUsed;
    public override bool Use()
    {
        if (isUsed)
        {
            UIControl.instance.ShowTips("壶中的天地效果不能叠加喔", Input.mousePosition);
            return false;
        }
        BuffCoroutineControl.instance.StartCoroutine(uniqueId, RegenHealth());
        SFXManger.instance.PlaySFX(2);
        MainPlayer.instance.PlayItemAnime(this.itemImage);
        return true;
    }

    private IEnumerator RegenHealth()
    {
        isUsed = true;

        MainPlayer.instance.moveSpeed += 1;
        PlayerHealthControl.instance._PlayerDef += 1;
        PlayerHealthControl.instance._PlayerAtk += 10;
        PlayerHealthControl.instance.CriticalDamage += 0.2f;
        PlayerHealthControl.instance.CriticalRate += 0.2f;

        // 持续10秒
        int count = 0;
        while (count <= 10)
        {
            count++;
            PlayerHealthControl.instance.changeSP(1.5f);
            PlayerHealthControl.instance.changeHP(1f);
            // 等待1秒
            yield return new WaitForSeconds(1);
        }

        MainPlayer.instance.moveSpeed -= 1;
        PlayerHealthControl.instance._PlayerDef -= 1;
        PlayerHealthControl.instance._PlayerAtk -= 10;
        PlayerHealthControl.instance.CriticalDamage -= 0.2f;
        PlayerHealthControl.instance.CriticalRate -= 0.2f;

        isUsed = false;
    }

}
