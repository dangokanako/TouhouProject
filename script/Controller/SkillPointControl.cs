using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillPointControl : MonoBehaviour
{
    // 单例模式
    public static SkillPointControl instance;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void Start()
    {
        Initialize();
        UpdateSkillPointText();
    }
    public void ResetSkillPoint()
    {
        ExpLevelControl.instance.CurrentSkillPoint = 0;
        Initialize();
    }

    // 当前技能点
    public TMP_Text skillPointText;
    public void UpdateSkillPointText()
    {
        skillPointText.text = "可分配技能点：" + ExpLevelControl.instance.CurrentSkillPoint.ToString();
    }

    // 技能树
    public List<ItemBag> SkillTree_1 = new List<ItemBag>();
    public List<ItemBag> SkillTree_2 = new List<ItemBag>();
    public List<ItemBag> SkillTree_3 = new List<ItemBag>();
    public List<ItemBag> SkillTree_4 = new List<ItemBag>();

    public void Initialize()
    {
        // Debug.Log("初始化就绪");
        foreach (ItemBag item in SkillTree_1)
        {
            item.SetItemImage(1);
            SetToUnactived(item);
        }
        foreach (ItemBag item in SkillTree_2)
        {
            item.SetItemImage(1);
            SetToUnactived(item);
        }
        foreach (ItemBag item in SkillTree_3)
        {
            item.SetItemImage(1);
            SetToUnactived(item);
        }
        foreach (ItemBag item in SkillTree_4)
        {
            item.SetItemImage(1);
            SetToUnactived(item);
        }

        SetToActive(SkillTree_1[0]);
        SetToActive(SkillTree_2[0]);
        SetToActive(SkillTree_3[0]);
        SetToActive(SkillTree_4[0]);
    }

    public static void SetToUnactived(ItemBag itemBag)
    {
        itemBag.skillType = SkillTypeEnum.Unactived;
        UnityEngine.UI.Image[] images = itemBag.gameObject.GetComponentsInChildren<UnityEngine.UI.Image>();
        if (images.Length > 0)
        {
            images[0].color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        }
        if (images.Length > 1)
        {
            images[1].color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        }
    }

    public static void SetToActivated(ItemBag itemBag)
    {
        itemBag.skillType = SkillTypeEnum.Activated;
        UnityEngine.UI.Image[] images = itemBag.gameObject.GetComponentsInChildren<UnityEngine.UI.Image>();
        if (images.Length > 0)
        {
            images[0].color = new Color(1, 1, 1, 1);
        }
        if (images.Length > 1)
        {
            images[1].color = new Color(1, 1, 1, 1);
        }
    }

    public static void SetToActive(ItemBag itemBag)
    {
        if (itemBag.skillType == SkillTypeEnum.Unactived)
            itemBag.skillType = SkillTypeEnum.Active;
        else
            return;
        UnityEngine.UI.Image[] images = itemBag.gameObject.GetComponentsInChildren<UnityEngine.UI.Image>();
        if (images.Length > 0)
        {
            images[0].color = new Color(1, 1, 1, 1);
        }
        if (images.Length > 1)
        {
            images[1].color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        }
    }
}
