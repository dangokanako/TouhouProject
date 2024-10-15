using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public enum DamageType
{
    bluntDamage = 1,
    sharpDamage,
    SCDamage,
    collisionDamage,
}

public class DamageNumberControl : MonoBehaviour
{
    public static DamageNumberControl instance;
    public DamageNumber numberToCreat;
    public Transform numberCanvas;

    private List<DamageNumber> numberPool = new List<DamageNumber>();
    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// 用伤害数字组件显示文本
    /// </summary>
    /// <param name="text"></param>
    /// <param name="location"></param>
    /// <param name="color"></param>
    /// <param name="lifetime">参考：标准伤害时间为0.5f</param>
    public void ShowText(string _text, Vector3 location, Color color, float _lifetime)
    {
        DamageNumber newDamage = GetFromPool();
        var text = newDamage.GetComponent<TMP_Text>();

        newDamage.lifetime = _lifetime;

        text.color = color;

        newDamage.gameObject.SetActive(true);
        newDamage.transform.position = location;
        newDamage.SetText(_text);
    }

    public void ShowDamage(float damage, Vector3 location, int type, int fontsize = default)
    {
        ShowDamage(damage, location, false, type, fontsize);
    }

    public void ShowDamage(float damage, Vector3 location, bool isPlayer, int type, int fontsize = default)
    {
        ShowDamage(damage, location, isPlayer, false, type, fontsize);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="location"></param>
    /// <param name="isPlayer">是否玩家</param>
    /// <param name="isCritical">是否暴击</param>
    /// <param name="type">伤害类型</param>
    public void ShowDamage(float damage, Vector3 location, bool isPlayer, bool isCritical, int type, int fontsize = default)
    {


        int rounded = Mathf.RoundToInt(damage);

        DamageNumber newDamage = GetFromPool();
        var text = newDamage.GetComponent<TMP_Text>();

        if (fontsize != default)
        {
            text.fontSize = fontsize;
        }

        if (isCritical)
        {
            text.fontSize *= 1.4f;

            CameraControl.instance.DoShake(0.1f, 0.1f);
        }

        // 显示时间0.5f
        newDamage.lifetime = 0.5f;

        if (isPlayer)
            text.color = Color.red;
        else
        {
            switch (type)
            {
                case (int)DamageType.bluntDamage:
                    text.color = Color.grey;
                    break;
                case (int)DamageType.sharpDamage:
                    text.color = Color.yellow;
                    //text.color = new Color(0.607843f, 0.054902f, 0.141176f, 1f);
                    break;
                case (int)DamageType.SCDamage:
                    text.color = new Color(0.514019f, 0.839216f, 0.4f, 1f);
                    break;
                case 4:
                    text.color = Color.grey;
                    break;
                //  // 特殊的，使用99表示治愈。
                case 99:
                    text.color = new Color(0.2f, 0.73f, 0.69f, 1f);
                    break;
                default:
                    text.color = Color.white; break;

            }
        }

        newDamage.gameObject.SetActive(true);
        newDamage.transform.position = location;

        if (isCritical)
            newDamage.SetupCritical(rounded);
        else
            newDamage.Setup(rounded);


    }


    /// <summary>
    /// 从对象池中取出对象，减少占用
    /// </summary>
    /// <returns></returns>
    public DamageNumber GetFromPool()
    {
        DamageNumber numberToOutPut = null;
        if (numberPool.Count == 0)
        {
            numberToOutPut = Instantiate(numberToCreat, numberCanvas);
        }
        else
        {
            numberToOutPut = numberPool[0];
            numberPool.RemoveAt(0);
        }
        return numberToOutPut;
    }

    public void PlaceInPool(DamageNumber numberToPlace)
    {
        numberToPlace.gameObject.SetActive(false);
        numberPool.Add(numberToPlace);
    }
}
