using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveKeine : InteractiveClass
{

    public void Awake()
    {
        MapID = 1;
    }

    public InteractiveKeine()
    {
        dialogRules.Add(new DialogRule
        {
            Condition = () => GlobalControl.instance.GetHasReadDialog(MapID, 12),
            DialogId = 13
        });

        dialogRules.Add(new DialogRule
        {
            Condition = () => GlobalControl.instance.GetHasReadDialog(MapID, 11),
            DialogId = 12
        });

        dialogRules.Add(new DialogRule
        {
            Condition = () => GlobalControl.instance.GetHasReadDialog(MapID, 9) && (ItemControl.instance.itembagList[0].itemInfo.itemId == 39 || ItemControl.instance.itembagList[1].itemInfo.itemId == 39 || ItemControl.instance.itembagList[2].itemInfo.itemId == 39),
            DialogId = 10
        });
        dialogRules.Add(new DialogRule
        {
            Condition = () => GlobalControl.instance.GetHasReadDialog(MapID, 9),
            DialogId = 11
        });

        dialogRules.Add(new DialogRule
        {
            Condition = () => GlobalControl.instance.GetHasReadDialog(MapID, 7) && (ItemControl.instance.itembagList[0].itemInfo.itemId != 0 || ItemControl.instance.itembagList[1].itemInfo.itemId != 0 || ItemControl.instance.itembagList[2].itemInfo.itemId != 0),
            DialogId = 8
        });
        dialogRules.Add(new DialogRule
        {
            Condition = () => GlobalControl.instance.GetHasReadDialog(MapID, 7),
            DialogId = 9
        });


        dialogRules.Add(new DialogRule
        {
            Condition = () => GlobalControl.instance.GetHasReadDialog(MapID, 5) && !ItemControl.instance.IsHaveItem(9),
            DialogId = 6
        });
        dialogRules.Add(new DialogRule
        {
            Condition = () => GlobalControl.instance.GetHasReadDialog(MapID, 5) && ItemControl.instance.IsHaveItem(9),
            DialogId = 7
        });


        dialogRules.Add(new DialogRule
        {
            Condition = () => GlobalControl.instance.GetHasReadDialog(MapID, 3) && ItemControl.instance.itembagList[2].itemInfo.itemId != 0,
            DialogId = 5
        });
        dialogRules.Add(new DialogRule
        {
            Condition = () => GlobalControl.instance.GetHasReadDialog(MapID, 3) && ItemControl.instance.itembagList[2].itemInfo.itemId == 0,
            DialogId = 4
        });


        dialogRules.Add(new DialogRule
        {
            Condition = () => GlobalControl.instance.GetHasReadDialog(MapID, 1) && PlayerHealthControl.instance.maxHealth > PlayerHealthControl.instance.currentHealth,
            DialogId = 2
        });
        dialogRules.Add(new DialogRule
        {
            Condition = () => GlobalControl.instance.GetHasReadDialog(MapID, 1) && PlayerHealthControl.instance.maxHealth <= PlayerHealthControl.instance.currentHealth,
            DialogId = 3
        });
        dialogRules.Add(new DialogRule { Condition = () => !GlobalControl.instance.GetHasReadDialog(MapID, 1), DialogId = 1 });
    }


}
