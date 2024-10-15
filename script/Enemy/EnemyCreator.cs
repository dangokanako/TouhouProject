using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using LitJson;
using Unity.VisualScripting;

public class EnemyCreator : MonoBehaviour
{
    public static EnemyCreator instance;
    [SerializeField] private GameObject enemyToCreat;
    [Header("生成参数")]
    [SerializeField] private float timeToCreat;
    [SerializeField] private float creatCounter;
    // Start is called before the first frame update
    [Header("生成坐标")]
    [SerializeField] public Transform playerPosition;
    [SerializeField] public Transform minCreat, maxCreat;
    [Header("清除过远敌人")]
    [SerializeField] private float disappearDistance;
    [SerializeField] private int chechPerFrame;
    [SerializeField] private int enemyToCheck;
    [SerializeField] public List<GameObject> creatorEnemy = new List<GameObject>();

    // 生成波次信息
    [SerializeField] public List<WaveInfo> waves;
    public int currentWave;
    // 波次时间计数
    public float waveCounter;
    // 用于检测碰撞区域
    public List<Collider2D> Colliders;

    // 生成的传送门
    public GameObject portal_1;
    public GameObject portal_2;
    public List<GameObject> portal_List = new List<GameObject>();
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void LoadEnemyCretor(string path)
    {
        Debug.Log("读取敌人生成列表：" + path);
        TextAsset jsonFile = Resources.Load<TextAsset>("other/enemyToCreate/" + path);
        waves = JsonMapper.ToObject<List<WaveInfo>>(jsonFile.text);

        timeToCreat = creatCounter;
        playerPosition = PlayerHealthControl.instance.transform;


        currentWave = -1;
        GoToNextWave();
    }


    public List<GameObject> getAllEnemy()
    {
        return creatorEnemy;
    }

    public delegate void AddEnemyCallback(GameObject gb);
    public event AddEnemyCallback addEnemyCallback_event;
    public void Start()
    {
        StartCoroutine(CheckEnemy());
    }
    /// <summary>
    /// 协程，每10秒检测一次creatorEnemy是否有NULL值，有的话就删掉
    /// </summary>
    /// <returns></returns>
    IEnumerator CheckEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            for (int i = 0; i < creatorEnemy.Count; i++)
            {
                if (creatorEnemy[i] == null)
                {
                    creatorEnemy.RemoveAt(i);
                }
            }
        }
    }

    void Update()
    {

        if (waves == null || waves.Count == 0)
            return;

        if (GlobalControl.instance.isBattle == false)
            return;

        if (GlobalControl.instance.isCloseCreateEnemy)
            return;

        // 刷新固定在玩家周围
        transform.position = PlayerHealthControl.instance.transform.position;

        if (!PlayerHealthControl.instance.isDead)
        {
            if (currentWave < waves.Count)
            {
                // 如果有立即flag，那么立即生成这一波次的所有敌人
                if (waves[currentWave].immediatelyFlag)
                {
                    if (waves[currentWave].restFlag)
                    {
                        GoToNextWave();
                        return;
                    }

                    // 计算总数，之后直接循环生成
                    for (float j = waves[currentWave].waveLength - waveCounter; j < waves[currentWave].waveLength; j += waves[currentWave].timeBetweenCreate)
                    {
                        CreateEnemy();
                    }
                    GoToNextWave();
                    return;
                }



                // 如果有休息flag，那么只减时间不生成怪
                if (!waves[currentWave].restFlag)
                {
                    timeToCreat -= Time.deltaTime;
                    if (timeToCreat <= 0)
                    {
                        timeToCreat = waves[currentWave].timeBetweenCreate;
                        // 创建队列里的敌人
                        CreateEnemy();

                    }
                }

                waveCounter -= Time.deltaTime;
                if (waveCounter <= 0)
                {
                    GoToNextWave();
                }

                SetPortal = false;
            }
            else
            {
                // 如果没有波次了，并且没有敌人，那么仅尝试生成一次
                if (creatorEnemy.Count == 0)
                {
                    if (!SetPortal)
                        CreatePortal();
                }
            }
        }
    }
    private bool SetPortal;

    public void test()
    {
        CreatePortal();
    }
    private void CreatePortal()
    {
        SetPortal = true;
        // 创建生成点 
        List<Vector2> creatPoint = new List<Vector2>{
            new Vector2(PlayerHealthControl.instance.transform.position.x - 0.8f, PlayerHealthControl.instance.transform.position.y - 0.8f),
            new Vector2(PlayerHealthControl.instance.transform.position.x - 0.8f, PlayerHealthControl.instance.transform.position.y + 0.8f),
            new Vector2(PlayerHealthControl.instance.transform.position.x + 0.8f, PlayerHealthControl.instance.transform.position.y - 0.8f),
            new Vector2(PlayerHealthControl.instance.transform.position.x + 0.8f, PlayerHealthControl.instance.transform.position.y + 0.8f),
            };

        bool hasCreated_1 = false;
        foreach (var point in creatPoint)
        {
            if (CheckInCollider(point))
            {
                continue;
            }

            if (!hasCreated_1)
            {
                DestoryAnimeControl.instance.CreateDestoryAnime(2, point);

                GlobalControl.instance.isYukariLeaving = false;
                GameObject newPortal = Instantiate(portal_1, point, Quaternion.identity);
                newPortal.SetActive(true);
                portal_List.Add(newPortal);

                hasCreated_1 = true;
                continue;
            }

            GlobalControl.instance.isNitoriLeaving = false;
            DestoryAnimeControl.instance.CreateDestoryAnime(2, point);
            GameObject newPortal_2 = Instantiate(portal_2, point, Quaternion.identity);
            newPortal_2.SetActive(true);
            portal_List.Add(newPortal_2);

            return;
        }


        Debug.Log("传送门生成失败，我的天啊，四个点都在阻挡，你在什么位置啊？");
    }

    public void CloseAllProtal()
    {
        foreach (var portal in portal_List)
        {
            if (portal != null)
                Destroy(portal.gameObject);
        }
    }
    private void CreateEnemy()
    {
        for (int i = 0; i < waves[currentWave].enemyToCreateName.Count; i++)
        {
            if (waves[currentWave].enemyToCreateName[i] == null)
                continue;

            try
            {
                // 如果在collider里面，重新生成一次，还不行的话就算了
                Vector2 creatPoint = SelectCreatorPoint();
                if (CheckInCollider(creatPoint))
                {
                    creatPoint = SelectCreatorPoint();
                    if (CheckInCollider(creatPoint))
                    {
                        return;
                    }
                }

                GameObject enemyPrefab = Resources.Load<GameObject>("enemy/" + waves[currentWave].enemyToCreateName[i]);

                GameObject newEnemy = Instantiate(enemyPrefab, creatPoint, Quaternion.identity);
                // addEnemyCallback_event?.Invoke(newEnemy);
                creatorEnemy.Add(newEnemy);
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
                Debug.Log("敌人生成失败，检查enemyToCreate敌人参数");
            }
        }
    }
    private Vector2 SelectCreatorPoint()
    {
        Vector2 ret = Vector2.zero;
        bool creatorEdge = UnityEngine.Random.Range(0f, 1f) > 0.5f;
        if (creatorEdge)
        {
            ret.y = UnityEngine.Random.Range(minCreat.position.y, maxCreat.position.y);
            if (UnityEngine.Random.Range(0f, 1f) > 0.5f)
            {
                ret.x = maxCreat.position.x;
            }
            else
            {
                ret.x = minCreat.position.x;
            }
        }
        else
        {
            ret.x = UnityEngine.Random.Range(minCreat.position.x, maxCreat.position.x);
            if (UnityEngine.Random.Range(0f, 1f) > 0.5f)
            {
                ret.y = maxCreat.position.y;
            }
            else
            {
                ret.y = minCreat.position.y;
            }
        }
        return ret;
    }

    // 检测生成点是不是在collider里面
    private bool CheckInCollider(Vector2 point)
    {
        for (int i = 0; i < Colliders.Count; i++)
            if (Colliders[i].OverlapPoint(point))
            {
                return true;
            }
        return false;
    }

    public void GoToNextWave()
    {
        currentWave++;
        if (currentWave >= waves.Count)
        {
            return;
            // currentWave = waves.Count - 1;
        }

        waveCounter = waves[currentWave].waveLength;
        timeToCreat = waves[currentWave].timeBetweenCreate;
    }

    public void SkipWaveWithOutEnemy()
    {
        waveCounter = -1;
    }
}
[SerializeField]
public class WaveInfo
{
    // public List<GameObject> enemyToCreate;
    public List<string> enemyToCreateName;
    public float waveLength;
    public float timeBetweenCreate;

    // 立即刷新FLAG
    public bool immediatelyFlag;
    // 休息FLAG
    public bool restFlag;
    // 序号 目前仅作注释使用
    public string indexStr;
}