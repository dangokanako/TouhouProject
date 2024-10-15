using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Fenghuangluan", menuName = "Equipment/Item_Fenghuangluan")]
public class Item_Fenghuangluan : item
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
                BuffCoroutineControl.instance.StartCoroutine(uniqueId, RegenHealth(0.1f));
                break;
            case ItemQuality.Normal:
                BuffCoroutineControl.instance.StartCoroutine(uniqueId, RegenHealth(0.12f));
                break;
            case ItemQuality.Good:
                BuffCoroutineControl.instance.StartCoroutine(uniqueId, RegenHealth(0.14f));
                break;
            case ItemQuality.Excellent:
                BuffCoroutineControl.instance.StartCoroutine(uniqueId, RegenHealth(0.16f));
                break;
            case ItemQuality.Legendary:
                BuffCoroutineControl.instance.StartCoroutine(uniqueId, RegenHealth(0.2f));
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
            PlayerHealthControl.instance.changeHP(regenPerSecond);
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
                return "持有时每秒钟可以恢复0.1HP和0.1SP";
            case ItemQuality.Normal:
                return "持有时每秒钟可以恢复0.12HP和0.12SP";
            case ItemQuality.Good:
                return "持有时每秒钟可以恢复0.14HP和0.14SP";
            case ItemQuality.Excellent:
                return "持有时每秒钟可以恢复0.16HP和0.16SP";
            case ItemQuality.Legendary:
                return "持有时每秒钟可以恢复0.2HP和0.2SP";
            case ItemQuality.Null:
                return "持有时每秒钟可以恢复0.12HP和0.12SP";
        }
        return "itemGetQualityText()报错了";
    }
}