using System.Collections;
using System.Collections.Generic;
// using UnityEditor.SceneManagement;
using UnityEngine;

public class AssetControl : MonoBehaviour
{
    public static AssetControl instance;
    public Drops_Point point;
    public Drops_Item dropsItem;
    public Drops_AddHp addHp;
    public Drops_AddSp addSp;

    // 普通P点
    public int currentPoint;
    // 超级P点
    public int currentSuperPoint;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // 增加P点和减少P带你的操作
    public void AddPoint(int pointToadd)
    {
        currentPoint += pointToadd;
        UIControl.instance.updatePoint();

        GlobalControl.instance.currentTotalPoint += pointToadd;
    }
    public void AddSuperPoint(int pointToadd)
    {
        currentSuperPoint += pointToadd;
        UIControl.instance.updateSuperPoint();
    }
    public void ReduceSuperPoint(int pointToadd)
    {
        currentSuperPoint -= pointToadd;
        UIControl.instance.updateSuperPoint();
    }

    public void ReducePoint(int _pointToreduce)
    {
        currentPoint -= _pointToreduce;
        UIControl.instance.updatePoint();
    }

    public void DropPoint(Vector3 position, int value)
    {
        if (value <= 0) return;

        for (int i = 0; i < value; i++)
        {
            Drops_Point newPoint = Instantiate(point, position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0f),
                Quaternion.identity, this.transform);
            newPoint.gameObject.SetActive(true);
        }
    }
    public void DropAddSp(Vector3 position, int value)
    {
        if (value <= 0) return;

        for (int i = 0; i < value; i++)
        {
            Drops_AddSp newAddSp = Instantiate(addSp, position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0f),
                Quaternion.identity, this.transform);
            newAddSp.gameObject.SetActive(true);
        }
    }
    public void DropAddHp(Vector3 position, int value)
    {
        if (value <= 0) return;

        for (int i = 0; i < value; i++)
        {
            Drops_AddHp newAddHp = Instantiate(addHp, position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0f),
                Quaternion.identity, this.transform);
            newAddHp.gameObject.SetActive(true);
        }
    }
    // 掉落物品
    public void DropItem(Vector3 position, item _item)
    {
        // 创建一个道具信息为_item的对象并激活
        Drops_Item newItem = Instantiate(dropsItem, position + new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f), 0f), Quaternion.identity, this.transform);


        // 如果是质量随机的物品
        if (_item.hasQuality)
        {
            // 不改变原值预制体的值
            item _itemForCopy = _item.Clone();

            _itemForCopy.Quality = Random.Range(0, 100);

            if (_itemForCopy.Quality < 25)
            {
                _itemForCopy.itemCiti = ItemQuality.Inferior;
            }
            else if (_itemForCopy.Quality < 60)
            {
                _itemForCopy.itemCiti = ItemQuality.Normal;
            }
            else if (_itemForCopy.Quality < 85)
            {
                _itemForCopy.itemCiti = ItemQuality.Good;
            }
            else if (_itemForCopy.Quality < 95)
            {
                _itemForCopy.itemCiti = ItemQuality.Excellent;
            }
            else
            {
                _itemForCopy.itemCiti = ItemQuality.Legendary;
            }

            newItem.itemText.text = ItemControl.instance.showItemPlaneName(_itemForCopy);
            newItem.itemImage.sprite = _itemForCopy.itemImage;
            newItem.itemInfo = _itemForCopy;
            newItem.gameObject.SetActive(true);

            return;
        }

        // 如果是永续符卡
        if (_item.isEquipmentSC)
        {
            // 不改变原值预制体的值
            item _itemForCopy = _item.Clone();


            _itemForCopy.isEquipmentSCLevel = 1;

            newItem.itemText.text = ItemControl.instance.showItemPlaneName(_itemForCopy);
            newItem.itemImage.sprite = _itemForCopy.itemImage;
            newItem.itemInfo = _itemForCopy;
            newItem.gameObject.SetActive(true);

            return;
        }

        newItem.itemText.color = _item.itemColor;
        newItem.itemText.text = _item.itemName;
        newItem.itemImage.sprite = _item.itemImage;
        newItem.itemInfo = _item;
        newItem.gameObject.SetActive(true);
    }

    internal void LoadState(AssetControlState assetControl)
    {
        currentPoint = assetControl.currentPoint;
        currentSuperPoint = assetControl.currentSuperPoint;
        UIControl.instance.updatePoint();
        UIControl.instance.updateSuperPoint();



        int i = 0;
        while (ItemControl.instance.MaxOfBags < assetControl.MaxOfBags)
        {
            ItemControl.instance.AddOneBag();
            i++; if (i > 10) break;
        }

        while (ItemControl.instance.MaxOfWarehouse < assetControl.MaxOfWarehouse)
        {
            ItemControl.instance.AddOneWarehouse();
            i++; if (i > 20) break;
        }


        if (ShopControl.instance == null)
        {
            UIControl.instance.OpenShopForInitialize();
        }
        StartCoroutine(WaitForCondition(assetControl));


    }
    IEnumerator WaitForCondition(AssetControlState assetControl)
    {
        int i = 0;
        while (true)
        {
            if (ShopControl.instance != null && UIControl.instance.isShopOpen() == false)
                ShopControl.instance.PatchouliShopData = assetControl.PatchouliShopData;
            i++; if (i > 50) break;
            yield return new WaitForSeconds(0.1f);
        }
    }

}
