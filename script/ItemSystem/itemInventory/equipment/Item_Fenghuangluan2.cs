using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Fenghuangluan2", menuName = "Equipment/Item_Fenghuangluan2")]
public class Item_Fenghuangluan2 : item
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
                PlayerHealthControl.instance._PlayerDef += 0.3f;
                BuffCoroutineControl.instance.StartCoroutine(uniqueId, RegenHealth(0.15f));
                break;
            case ItemQuality.Normal:
                PlayerHealthControl.instance._PlayerDef += 0.4f;
                BuffCoroutineControl.instance.StartCoroutine(uniqueId, RegenHealth(0.2f));
                break;
            case ItemQuality.Good:
                PlayerHealthControl.instance._PlayerDef += 0.5f;
                BuffCoroutineControl.instance.StartCoroutine(uniqueId, RegenHealth(0.23f));
                break;
            case ItemQuality.Excellent:
                PlayerHealthControl.instance._PlayerDef += 0.6f;
                BuffCoroutineControl.instance.StartCoroutine(uniqueId, RegenHealth(0.25f));
                break;
            case ItemQuality.Legendary:
                PlayerHealthControl.instance._PlayerDef += 0.7f;
                BuffCoroutineControl.instance.StartCoroutine(uniqueId, RegenHealth(0.3f));
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
                PlayerHealthControl.instance._PlayerDef -= 0.3f;
                BuffCoroutineControl.instance.StopTheCoroutine(uniqueId);
                break;
            case ItemQuality.Normal:
                PlayerHealthControl.instance._PlayerDef -= 0.4f;
                BuffCoroutineControl.instance.StopTheCoroutine(uniqueId);
                break;
            case ItemQuality.Good:
                PlayerHealthControl.instance._PlayerDef -= 0.5f;
                BuffCoroutineControl.instance.StopTheCoroutine(uniqueId);
                break;
            case ItemQuality.Excellent:
                PlayerHealthControl.instance._PlayerDef -= 0.6f;
                BuffCoroutineControl.instance.StopTheCoroutine(uniqueId);
                break;
            case ItemQuality.Legendary:
                PlayerHealthControl.instance._PlayerDef -= 0.7f;
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
                return "持有时每秒钟可以恢复0.15HP和0.15SP，并提供0.3点防御力";
            case ItemQuality.Normal:
                return "持有时每秒钟可以恢复0.2HP和0.2SP，并提供0.4点防御力";
            case ItemQuality.Good:
                return "持有时每秒钟可以恢复0.23HP和0.23SP，并提供0.5点防御力";
            case ItemQuality.Excellent:
                return "持有时每秒钟可以恢复0.25HP和0.25SP，并提点0.6防御力";
            case ItemQuality.Legendary:
                return "持有时每秒钟可以恢复0.3HP和0.3SP，并提供0.7点防御力";
            case ItemQuality.Null:
                return "持有时每秒钟可以恢复0.2HP和0.2SP，并提供0.4点防御力";
        }
        return "itemGetQualityText()报错了";
    }
}