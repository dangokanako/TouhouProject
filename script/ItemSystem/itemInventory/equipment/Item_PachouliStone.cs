using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_PachouliStone", menuName = "Equipment/Item_PachouliStone")]
public class Item_PachouliStone : item
{
    // 生成一个全局唯一的ID
    private string uniqueId = System.Guid.NewGuid().ToString();

    public override bool Use()
    {
        SFXManger.instance.PlaySFX(3);
        return false;
    }

    public override bool Passive()
    {
        // 根据品质调整数值
        switch (this.itemCiti)
        {
            case ItemQuality.Inferior:
                BuffCoroutineControl.instance.StartCoroutine(uniqueId, RegenHealth(0.2f));
                break;
            case ItemQuality.Normal:
                BuffCoroutineControl.instance.StartCoroutine(uniqueId, RegenHealth(0.25f));
                break;
            case ItemQuality.Good:
                BuffCoroutineControl.instance.StartCoroutine(uniqueId, RegenHealth(0.3f));
                break;
            case ItemQuality.Excellent:
                BuffCoroutineControl.instance.StartCoroutine(uniqueId, RegenHealth(0.35f));
                break;
            case ItemQuality.Legendary:
                BuffCoroutineControl.instance.StartCoroutine(uniqueId, RegenHealth(0.4f));
                break;
        }
        return true;
    }

    public override bool discardPassive()
    {
        // 根据品质调整数值
        switch (this.itemCiti)
        {
            case ItemQuality.Inferior:
                BuffCoroutineControl.instance.StopTheCoroutine(uniqueId);
                break;
            case ItemQuality.Normal:
                BuffCoroutineControl.instance.StopTheCoroutine(uniqueId);
                break;
            case ItemQuality.Good:
                BuffCoroutineControl.instance.StopTheCoroutine(uniqueId);
                break;
            case ItemQuality.Excellent:
                BuffCoroutineControl.instance.StopTheCoroutine(uniqueId);
                break;
            case ItemQuality.Legendary:
                BuffCoroutineControl.instance.StopTheCoroutine(uniqueId);
                break;
        }
        return true;
    }

    private IEnumerator RegenHealth(float regenPerSecond)
    {
        while (true)
        {
            PlayerHealthControl.instance.changeSP(regenPerSecond);
            // 等待1秒
            yield return new WaitForSeconds(1);
        }
    }

    // 获取品质文本
    public override string GetQualityText()
    {
        switch (this.itemCiti)
        {
            case ItemQuality.Inferior:
                return "持有时每秒钟可以恢复0.2SP";
            case ItemQuality.Normal:
                return "持有时每秒钟可以恢复0.25SP";
            case ItemQuality.Good:
                return "持有时每秒钟可以恢复0.3SP";
            case ItemQuality.Excellent:
                return "持有时每秒钟可以恢复0.35SP";
            case ItemQuality.Legendary:
                return "持有时每秒钟可以恢复0.4SP";
            case ItemQuality.Null:
                return "持有时每秒钟可以恢复0.25SP";

        }
        return "itemGetQualityText()报错了";
    }
}