using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_Kyuuri", menuName = "Inventory/Item_Kyuuri")]
public class Item_Kyuuri : item
{
    public override bool Use()
    {
        if (PlayerHealthControl.instance.currentSP >= PlayerHealthControl.instance.maxSP)
        {
            SFXManger.instance.PlaySFX(3);
            return false;
        }


        // 黄瓜使用时立即恢复SP的疲劳状态
        if (MainPlayer.instance.DashTried)
        {
            MainPlayer.instance.DashTried = false;
            PlayerHealthControl.instance.SPReadyTimeCounter = PlayerHealthControl.instance.SPReadyTime;
        }
        PlayerHealthControl.instance.changeSP(5f);

        SFXManger.instance.PlaySFX(2);
        MainPlayer.instance.PlayItemAnime(this.itemImage);
        return true;
    }
}
