using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;



public class FadeInOut : MonoBehaviour
{

    public static FadeInOut instance; // 单例实例
    public Image backImage;

    void Awake()
    {
        // 确保只有一个FadeInOut实例
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 使实例在加载新场景时不被自动销毁
        }
        else if (instance != this)
        {
            Destroy(gameObject); // 销毁新的实例，保持单例
        }


        RectTransform rt = backImage.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0, 0);
        rt.anchorMax = new Vector2(1, 1);
        rt.offsetMin = rt.offsetMax = new Vector2(0, 0);
    }

    public void GoToScene(string sceneName)
    {
        // 先将面板颜色渐变到黑色
        backImage.DOColor(Color.black, 1f).OnComplete(() =>
        {
            // 在这里加载你的新场景
            SceneManager.LoadScene(sceneName);

            // 然后将面板颜色渐变回透明
            backImage.DOColor(Color.clear, 3f);
        });
    }

    // // 假切换场景
    // public void GoToSceneFake()
    // {
    //     // 先将面板颜色渐变到黑色
    //     backImage.DOColor(Color.black, 1f).OnComplete(() =>
    //     {
    //         // 然后将面板颜色渐变回透明
    //         backImage.DOColor(Color.clear, 3f);
    //     });
    // }

    // 假切换场景 带委托
    public void GoToSceneFake(Action onCompleteAction)
    {
        CameraFather.instance.FadeStart();
        // 先将面板颜色渐变到黑色
        backImage.DOColor(Color.black, 1f).OnComplete(() =>
        {
            // 执行函数
            onCompleteAction?.Invoke();

            backImage.DOColor(Color.black, 0.5f).OnComplete(() =>
            {
                // 然后将面板颜色渐变回透明
                backImage.DOColor(Color.clear, 3f);
                CameraFather.instance.FadeEnd();
            });

        });
    }

    // 假切换场景 带委托 带时间 场景内小传送用
    public void GoToSceneFake(Action onCompleteAction, float time)
    {
        CameraFather.instance.FadeStart();
        // 先将面板颜色渐变到黑色
        backImage.DOColor(Color.black, time / 2).OnComplete(() =>
        {
            // 执行函数
            onCompleteAction?.Invoke();

            backImage.DOColor(Color.black, time / 2).OnComplete(() =>
            {
                // 然后将面板颜色渐变回透明
                backImage.DOColor(Color.clear, time / 2);
                CameraFather.instance.FadeEnd();
            });

        });
    }
}