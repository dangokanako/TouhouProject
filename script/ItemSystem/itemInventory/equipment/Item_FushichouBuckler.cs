using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_FushichouBuckler", menuName = "Equipment/Item_FushichouBuckler")]
public class Item_FushichouBuckler : item
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
                PlayerHealthControl.instance._PlayerDef += 0.5f;
                BuffCoroutineControl.instance.StartCoroutine(uniqueId, RegenHealth(0.1f));
                break;
            case ItemQuality.Normal:
                PlayerHealthControl.instance._PlayerDef += 0.6f;
                BuffCoroutineControl.instance.StartCoroutine(uniqueId, RegenHealth(0.13f));
                break;
            case ItemQuality.Good:
                PlayerHealthControl.instance._PlayerDef += 0.7f;
                BuffCoroutineControl.instance.StartCoroutine(uniqueId, RegenHealth(0.16f));
                break;
            case ItemQuality.Excellent:
                PlayerHealthControl.instance._PlayerDef += 0.8f;
                BuffCoroutineControl.instance.StartCoroutine(uniqueId, RegenHealth(0.2f));
                break;
            case ItemQuality.Legendary:
                PlayerHealthControl.instance._PlayerDef += 1f;
                BuffCoroutineControl.instance.StartCoroutine(uniqueId, RegenHealth(0.25f));
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
                PlayerHealthControl.instance._PlayerDef -= 0.5f;
                BuffCoroutineControl.instance.StopCoroutine(uniqueId);
                break;
            case ItemQuality.Normal:
                PlayerHealthControl.instance._PlayerDef -= 0.6f;
                BuffCoroutineControl.instance.StopCoroutine(uniqueId);
                break;
            case ItemQuality.Good:
                PlayerHealthControl.instance._PlayerDef -= 0.7f;
                BuffCoroutineControl.instance.StopCoroutine(uniqueId);
                break;
            case ItemQuality.Excellent:
                PlayerHealthControl.instance._PlayerDef -= 0.8f;
                BuffCoroutineControl.instance.StopCoroutine(uniqueId);
                break;
            case ItemQuality.Legendary:
                PlayerHealthControl.instance._PlayerDef -= 1f;
                BuffCoroutineControl.instance.StopCoroutine(uniqueId);
                break;
        }
        return true;
    }

    private IEnumerator RegenHealth(float regenPerSecond)
    {
        while (true)
        {
            PlayerHealthControl.instance.changeHP(regenPerSecond);
            // 等待1秒
            yield return new WaitForSeconds(1f);
        }
    }
    public override string GetQualityText()
    {
        switch (this.itemCiti)
        {
            case ItemQuality.Inferior:
                return "持有时可以提供0.5点防御力，并每秒钟恢复0.1HP";
            case ItemQuality.Normal:
                return "持有时可以提供0.6点防御力，并每秒钟恢复0.13HP";
            case ItemQuality.Good:
                return "持有时可以提供0.7点防御力，并每秒钟恢复0.16HP";
            case ItemQuality.Excellent:
                return "持有时可以提供0.8点防御力，并每秒钟恢复0.2HP";
            case ItemQuality.Legendary:
                return "持有时可以提供1.0点防御力，并每秒钟恢复0.25HP";
            case ItemQuality.Null:
                return "持有时可以提供0.6点防御力，并每秒钟恢复0.13HP";
        }
        return "itemGetQualityText()报错了";
    }
}