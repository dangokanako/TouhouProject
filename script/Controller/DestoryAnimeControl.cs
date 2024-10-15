using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestoryAnimeControl : MonoBehaviour
{
    public GameObject[] destoryanime1;
    public static DestoryAnimeControl instance;
    void Awake()
    {
        instance = this;
    }


    /// <summary>
    /// 在指定位置播放动画（注意：动画时间需要在这里设置）
    /// </summary>
    /// <param name="type"></param>
    /// <param name="position"></param>
    /// <param name="rotation">新添加的参数，用于设置实例化对象的旋转</param>
    public void CreateDestoryAnime(int type, Vector2 position, Quaternion rotation = default)
    {
        if (type == 0) return;

        var gameobjecttemp = Instantiate(destoryanime1[type], position, rotation == default ? Quaternion.identity : rotation);
        gameobjecttemp.SetActive(true);

        Animator animator = gameobjecttemp.GetComponent<Animator>();
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        float clipLength = clipInfo[0].clip.length;

        StartCoroutine(DestorySeft(gameobjecttemp, clipLength));


        // switch (type)
        // {
        //     case 1:
        //         {
        //             StartCoroutine(DestorySeft(gameobjecttemp, 0.35f));
        //             break;
        //         }
        //     case 2:
        //         {
        //             StartCoroutine(DestorySeft(gameobjecttemp, 1.0f));
        //             break;
        //         }
        //     case 3:
        //         {
        //             StartCoroutine(DestorySeft(gameobjecttemp, 1.1f));
        //             break;
        //         }
        //     case 4:
        //         {
        //             StartCoroutine(DestorySeft(gameobjecttemp, 0.35f));
        //             break;
        //         }
        //     case 5:
        //         {
        //             StartCoroutine(DestorySeft(gameobjecttemp, 1.2f));
        //             break;
        //         }
        //     case 6:
        //         {
        //             StartCoroutine(DestorySeft(gameobjecttemp, 1.5f));
        //             break;
        //         }
        //     default:
        //         StartCoroutine(DestorySeft(gameobjecttemp, 2f));
        //         break;
        // }
    }

    IEnumerator DestorySeft(GameObject gb, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gb);
    }
}
