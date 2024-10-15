using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BigMapControl : MonoBehaviour
{
    public static BigMapControl instance;
    public int BigMapLoctionID;
    // 地图路径点
    public Dictionary<int, List<int>> explorationMap = new Dictionary<int, List<int>>();
    // 已走过的路径
    public List<int> explored = new List<int>();
    public Dictionary<int, BigMapComponentControl> mapControlDict = new Dictionary<int, BigMapComponentControl>();

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

    void Start()
    {
        InitMapRoute();
    }
    bool hasInitMap = false;

    // 我更喜欢 OnEnable() 
    void Update()
    {
        if (!hasInitMap && mapControlDict.Count > 0)
        {
            InitMap();
            hasInitMap = true;
        }
    }

    public void InitMap()
    {
        Debug.Log("初始化大地图");

        // 将mapControlDict里的bigMapComponentControls全部设置为false.
        foreach (var control in instance.mapControlDict.Values)
        {
            control.gameObject.SetActive(false);
        }

        // 根据已经探索的路径，设置bigMapComponentControls为true
        foreach (var location in explored)
        {
            if (instance.mapControlDict.ContainsKey(location))
            {
                instance.mapControlDict[location].gameObject.SetActive(true);
                instance.mapControlDict[location].button.interactable = false;

            }
        }

        // 根据当前位置，设置可探索的路径为true
        if (instance.explorationMap.ContainsKey(instance.BigMapLoctionID))
        {
            foreach (var location in instance.explorationMap[instance.BigMapLoctionID])
            {
                if (instance.mapControlDict.ContainsKey(location))
                {
                    instance.mapControlDict[location].gameObject.SetActive(true);
                    instance.mapControlDict[location].button.interactable = true;
                }
            }
        }
    }

    public void InitMap_Test()
    {
        Debug.Log("测试初始化大地图");
        foreach (var control in instance.mapControlDict)
        {
            control.Value.gameObject.SetActive(true);
            control.Value.button.interactable = true;
        }
    }


    public void InitMapRoute()
    {
        instance.explorationMap = new Dictionary<int, List<int>>
        {
            // 1是红魔馆,2雾之湖西,3雾之湖,4雾之湖东
            {1, new List<int>{2}},
            {2, new List<int>{3}},
            {3, new List<int>{22}},
            {22, new List<int>{4}},
            // 5魔法之森、白玉楼方向 6人间之里、博丽神社方向 7妖怪之山、守矢神社方向
            {4, new List<int>{5,6,7}},
            // 8魔法之森入口
            {5, new List<int>{8}},
            {8, new List<int>{9}},
            {9, new List<int>{1,23}},
            // 9 雾雨魔法店 
            {23, new List<int>{10}},
            // 白玉楼线
            // {10, new List<int>{11}},
            {10, new List<int>{11}},
            {11, new List<int>{12}},
            {12, new List<int>{13}},
            {13, new List<int>{14}},
            {14, new List<int>{15}},
            {15, new List<int>{16}},
            {16, new List<int>{17}},
            {17, new List<int>{18}},
            {18, new List<int>{19}},
            {19, new List<int>{20}},
            {20, new List<int>{21}},
            {21, new List<int>{1}},
        };
    }

    void OnEnable()
    {
        hasInitMap = false;
    }

    public void Reset()
    {
        BigMapLoctionID = 1;
        explored = new List<int>
        {
            1
        };
    }

    public void SetMarisaRoute()
    {
        // 检查字典中是否存在键为10的项
        if (explorationMap.ContainsKey(9))
        {
            // 获取键为10的值
            List<int> value = explorationMap[9];

            // 检查值是否为 {1}
            if (value.SequenceEqual(new List<int> { 1 }))
            {
                // 如果值为 {1}，则替换为 {11}
                explorationMap[9] = new List<int> { 23 };
            }
        }
    }
}
