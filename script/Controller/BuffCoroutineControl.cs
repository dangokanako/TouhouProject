using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffCoroutineControl : MonoBehaviour
{

    public static BuffCoroutineControl instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject); return;
        }
        else
        {
            instance = this;
        }
    }

    private Dictionary<string, Coroutine> coroutines = new Dictionary<string, Coroutine>();

    public void StartCoroutine(string name, IEnumerator routine)
    {
        // 如果已经有一个同名的协程在运行，那么先停止它
        if (coroutines.ContainsKey(name))
        {
            StopCoroutine(coroutines[name]);
        }

        // 开始新的协程，并把它添加到字典中
        coroutines[name] = StartCoroutine(routine);
    }
    // 注意有一个同名的系统StopCoroutine参数也一样千万不要搞错！！！
    public void StopTheCoroutine(string name)
    {
        // 如果有一个同名的协程在运行，那么停止它，并把它从字典中移除
        if (coroutines.ContainsKey(name))
        {
            StopCoroutine(coroutines[name]);
            coroutines.Remove(name);
        }
    }

    /// 停止所有协程
    public void StopAll()
    {
        foreach (var coroutine in coroutines)
        {
            StopCoroutine(coroutine.Value);
        }
        coroutines.Clear();
    }
}
