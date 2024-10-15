using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_HeTongDeFuDai", menuName = "Inventory/Item_HeTongDeFuDai")]
public class Item_HeTongDeFuDai : item
{
    public override bool Use()
    {
        if (!ItemControl.instance.AddItemToBag(ItemControl.instance.itemGuide[EnemyDropTable.GetGroupItem((int)DropTableGroupEnum.PermanentAddition)]))
            AssetControl.instance.DropItem(PlayerHealthControl.instance.transform.position, ItemControl.instance.itemGuide[EnemyDropTable.GetGroupItem((int)DropTableGroupEnum.PermanentAddition)]);
        SFXManger.instance.PlaySFX(2);

        return true;
    }
}
