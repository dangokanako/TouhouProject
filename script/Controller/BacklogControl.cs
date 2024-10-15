using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BacklogControl : MonoBehaviour
{
    // 回放数据
    private Queue<GameObject> backlogDataQueue = new Queue<GameObject>();

    Dictionary<string, string> spritePaths = new Dictionary<string, string>
{
    { "博丽灵梦", "smallHead/reimu_small_0" },
    { "雾雨魔理沙", "smallHead/marisa_small_0" },
    { "琪露诺", "smallHead/crino_small_0" },
    { "上白澤慧音", "smallHead/keine_small_0" },
    { "八云紫", "smallHead/yukari_small_0" },
    { "魂魄妖梦", "smallHead/youmu_small_1_0" }
};


    public void AddBacklogData(BacklogData data)
    {
        // 创建新的GameObject
        GameObject newBacklog = Instantiate(backlogPrefab);
        newBacklog.SetActive(true);
        var smallhead = newBacklog.GetComponentInChildren<Image>();


        if (spritePaths.TryGetValue(data.name, out string path))
        {
            smallhead.sprite = Resources.Load<Sprite>(path);
        }
        else
        {
            smallhead.sprite = null;
        }



        var tMP_Texts = newBacklog.GetComponentsInChildren<TMP_Text>();
        if (string.IsNullOrEmpty(data.name))
            tMP_Texts[0].text = "";
        else
            tMP_Texts[0].text = "【" + data.name + "】";
        tMP_Texts[1].text = data.content;

        // 设置父对象（挂载在CONTENT下）
        newBacklog.transform.SetParent(ContentFatherObject.transform);

        backlogDataQueue.Enqueue(newBacklog);

        // 将新的GameObject设置为ContentFatherObject的第一个子对象
        newBacklog.transform.SetAsFirstSibling();

        // 如果队列的大小超过了限制，删除最早添加的GameObject
        if (backlogDataQueue.Count > 30)
        {
            GameObject toRemove = backlogDataQueue.Dequeue();
            Destroy(toRemove);
        }
    }
    // 回放的游戏对象
    public GameObject backlogPrefab;
    // 插入内容时的父对象
    public GameObject ContentFatherObject;

    //单例模式
    public static BacklogControl instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    // 回放数据


    // 回放UI
    public GameObject BacklogUI;
    // 是否打开回放
    public bool isOpenBacklog;
    // 竖滚动条
    public Scrollbar scrollbar;
    void FixedUpdate()
    {
        // 只在对话时打开 
        // 如果向上滚滑轮
        if (!isOpenBacklog && PlayerHealthControl.instance.OnTalk && Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            //打开回放
            isOpenBacklog = true;
            BacklogUI.SetActive(true);
            scrollbar.value = 0;
        }
        // 在最下面滚下滑轮或者右键
        if (isOpenBacklog && ((Input.GetAxis("Mouse ScrollWheel") < 0 && scrollbar.value < -0.3) || (Input.GetMouseButtonDown(1))))
        {
            //关闭回放
            isOpenBacklog = false;
            BacklogUI.SetActive(false);
        }
    }
}


public struct BacklogData
{
    public string name;
    public string content;

    public BacklogData(string name, string content)
    {
        this.name = name;
        this.content = content;
    }
}

public class LimitedQueue<T> : Queue<T>
{
    private int _limit = 30;
    public int Limit
    {
        get { return _limit; }
        set
        {
            _limit = value;
            while (Count > _limit)
            {
                Dequeue();
            }
        }
    }

    public LimitedQueue(int limit)
    {
        _limit = limit;
    }

    public new void Enqueue(T item)
    {
        base.Enqueue(item);
        while (Count > _limit)
        {
            Dequeue();
        }
    }
}