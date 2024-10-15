using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_CallNextWave", menuName = "Inventory/Item_CallNextWave")]
public class Item_CallNextWave : item
{
    public override bool Use()
    {
        if (EnemyCreator.instance.currentWave == EnemyCreator.instance.waves.Count - 1)
        {
            return false;
        }
        SFXManger.instance.PlaySFX(2);
        MainPlayer.instance.PlayItemAnime(this.itemImage);
        EnemyCreator.instance.waves[EnemyCreator.instance.currentWave].immediatelyFlag = true;
        return true;

    }
}
