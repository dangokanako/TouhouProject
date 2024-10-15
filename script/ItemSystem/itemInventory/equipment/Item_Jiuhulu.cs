using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Jiuhulu", menuName = "Equipment/Item_Jiuhulu")]
public class Item_Jiuhulu : item
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
                BuffCoroutineControl.instance.StartCoroutine(uniqueId, RegenHealth(0.5f, 0.2f));
                break;
            case ItemQuality.Normal:
                BuffCoroutineControl.instance.StartCoroutine(uniqueId, RegenHealth(0.55f, 0.17f));
                break;
            case ItemQuality.Good:
                BuffCoroutineControl.instance.StartCoroutine(uniqueId, RegenHealth(0.6f, 0.15f));
                break;
            case ItemQuality.Excellent:
                BuffCoroutineControl.instance.StartCoroutine(uniqueId, RegenHealth(0.65f, 0.13f));
                break;
            case ItemQuality.Legendary:
                BuffCoroutineControl.instance.StartCoroutine(uniqueId, RegenHealth(0.7f, 0.1f));
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

    private IEnumerator RegenHealth(float regenPerSecond, float ref2)
    {
        while (true)
        {
            PlayerHealthControl.instance.changeSP(regenPerSecond);
            PlayerHealthControl.instance.changeHP(-ref2);
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
                return "持有时每秒钟可以恢复0.5SP，但会减少0.2HP";
            case ItemQuality.Normal:
                return "持有时每秒钟可以恢复0.55SP，但会减少0.17HP";
            case ItemQuality.Good:
                return "持有时每秒钟可以恢复0.6SP，但会减少0.15HP";
            case ItemQuality.Excellent:
                return "持有时每秒钟可以恢复0.65SP，但会减少0.13HP";
            case ItemQuality.Legendary:
                return "持有时每秒钟可以恢复0.7SP，但会减少0.1HP";
            case ItemQuality.Null:
                return "持有时每秒钟可以恢复0.65SP，但会减少0.35HP";

        }
        return "itemGetQualityText()报错了";
    }
}