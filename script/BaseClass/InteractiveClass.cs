using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractiveClass : MonoBehaviour
{
    [Header("设置已读文本")]
    public int hasReadIndex;
    // 备用
    public int hasReadIndex2;
    // 备用
    public bool condition;

    [Header("转身组件")]
    public Transform filePic;
    public bool isFlip;

    [Header("在玩家范围内，显示按键提示")]
    public bool isShowTips;
    public GameObject PressSpaceShow;
    public GameObject PressSpaceShowNew;

    // 对话规则控制系统
    protected List<DialogRule> dialogRules = new List<DialogRule>();

    [Header("像素画位移控制")]
    private int timecount = 0;
    public GameObject pixelMove;

    [Header("用于检测上一句对话")]
    public int LastTalk = -1;

    // 红魔馆地图ID：0（即默认），新手教学地图ID为1
    [Header("地图ID")]
    public int MapID;
    [Header("是否是单次互动模式")]
    public bool isOnce = false;

    virtual public void Start()
    {
        if (!isOnce)
        {
            // 先获取子类的spriterenderer类型的gameobject
            if (filePic != null)
            {
                // timecount随机加0-7 
                timecount += UnityEngine.Random.Range(0, 8);
                // filePic = this.gameObject.transform.Find("filePic").gameObject.transform;
                pixelMove = filePic.gameObject;
                StartCoroutine(MovePixel());
            }
        }
    }
    virtual public void Update()
    {
        if (isOnce && isShowTips)
        {
            PressSpaceShow.SetActive(true);
            return;
        }
        else if (isOnce && !isShowTips)
        {
            PressSpaceShow.SetActive(false);
            return;
        }

        if (isShowTips)
        {

            // X向右为正。所以我比玩家小的话，那么玩家在右边，应向右。
            // 但是图像要固定0为向右。
            if (transform.position.x < MainPlayer.instance.transform.position.x)
            {
                filePic.transform.eulerAngles = new Vector3(0, 0);
            }
            else if (transform.position.x > MainPlayer.instance.transform.position.x)
            {

                filePic.transform.eulerAngles = new Vector3(0, 180);
            }


            // 根据上一次对话ID和本次对话ID判断显示哪一个
            if (GetNextTalk() != LastTalk)
            {
                PressSpaceShow.SetActive(false);
                PressSpaceShowNew.SetActive(true);
            }
            else
            {
                PressSpaceShowNew.SetActive(false);
                PressSpaceShow.SetActive(true);
            }

        }
        else
        {
            if (PressSpaceShow.activeSelf || PressSpaceShowNew.activeSelf)
            {
                PressSpaceShow.SetActive(false);
                PressSpaceShowNew.SetActive(false);
            }
        }
    }

    // 互动
    virtual public void Interactive()
    {
        foreach (var rule in dialogRules)
        {
            if (rule.Condition())
            {
                TalkControl.instance.ShowText(rule.DialogId);
                // 阅读之后增加阅读次数
                GlobalControl.instance.AddHasReadDialog(MapID, rule.DialogId);

                // 设置上一次对话ID
                this.LastTalk = rule.DialogId;
                break;
            }
        }
    }

    // 获取下一次对话的ID
    private int GetNextTalk()
    {
        foreach (var rule in dialogRules)
        {
            if (rule.Condition())
            {
                return rule.DialogId;
            }
        }
        return -1;
    }

    // 像素画位移控制
    private IEnumerator MovePixel()
    {
        // 每0.5秒按照以下顺序循环移动
        // 图像左移2像素
        // 图像左移1像素，上移1像素
        // 图像上移2像素
        // 图像右移1像素，上移1像素
        // 图像右移2像素
        // 图像右移1像素，上移1像素
        // 图像上移2像素
        // 图像左移1像素，上移1像素
        while (true)
        {
            timecount++;
            switch (timecount % 8)
            {
                case 0:
                    pixelMove.transform.position = new Vector3(this.transform.position.x - 0.02f, this.transform.position.y, 0);
                    break;
                case 1:
                    pixelMove.transform.position = new Vector3(this.transform.position.x - 0.01f, this.transform.position.y + 0.01f, 0);
                    break;
                case 2:
                    pixelMove.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.02f, 0);
                    break;
                case 3:
                    pixelMove.transform.position = new Vector3(this.transform.position.x + 0.01f, this.transform.position.y + 0.01f, 0);
                    break;
                case 4:
                    pixelMove.transform.position = new Vector3(this.transform.position.x + 0.02f, this.transform.position.y, 0);
                    break;
                case 5:
                    pixelMove.transform.position = new Vector3(this.transform.position.x + 0.01f, this.transform.position.y + 0.01f, 0);
                    break;
                case 6:
                    pixelMove.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.02f, 0);
                    break;
                case 7:
                    pixelMove.transform.position = new Vector3(this.transform.position.x - 0.01f, this.transform.position.y + 0.01f, 0);
                    break;
            }
            //等待0.5秒
            yield return new WaitForSeconds(0.25f);
        }

    }
}

public class DialogRule
{
    public System.Func<bool> Condition { get; set; }
    public int DialogId { get; set; }
}
public class DialogBubbleRule
{
    public System.Func<bool> Condition { get; set; }
    public System.Action DialogAction { get; set; }
}