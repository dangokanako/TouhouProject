using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManger : MonoBehaviour
{
    public static SFXManger instance;
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

    public AudioSource[] soundEffects;
    public AudioSource[] bgm;



    public void PlaySFX(int sfxToPlay, float time = default, bool stopLastSFX = true)
    {
        if (!stopLastSFX)
        {
            if (soundEffects[sfxToPlay].isPlaying)
                return;
        }

        switch (sfxToPlay)
        {
            default:
                soundEffects[sfxToPlay].Stop();
                break;
        }



        if (time != default)
            // 设置为循环播放
            soundEffects[sfxToPlay].loop = true;
        else
            soundEffects[sfxToPlay].loop = false;


        soundEffects[sfxToPlay].pitch = Random.Range(0.8f, 1.2f);
        soundEffects[sfxToPlay].Play();

        if (time != default)
        {
            StartCoroutine(StopSFX(sfxToPlay, time));
        }
    }

    IEnumerator StopSFX(int sfxToStop, float time)
    {
        yield return new WaitForSeconds(time);
        soundEffects[sfxToStop].Stop();
    }

    public void PlaySFXPitched(int _sfxToPlay)
    {
        soundEffects[_sfxToPlay].pitch = Random.Range(0.8f, 1.2f);
        PlaySFX(_sfxToPlay);
    }

    // 播放BGM
    public void playBGM(int bgmToPlay)
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }
        StopBgmBattleGroup();
        bgm[bgmToPlay].Play();
    }

    // 停止所有音效
    public void stopAllSFX()
    {
        for (int i = 0; i < soundEffects.Length; i++)
        {
            soundEffects[i].Stop();
        }
    }


    private List<int> BattleBgmIndex = new List<int> { 1, 2, 4, 5, 6 };
    private bool isPlayingBgm = false;
    private int currentBgm = -1; // 新增变量来跟踪当前正在播放的 BGM
    private Coroutine bgmCoroutine = null; // 新增变量来跟踪当前的协程
    public void PlayBgmBattleGroup()
    {
        if (!isPlayingBgm)
        {
            bgmCoroutine = StartCoroutine(PlayBgmGroupCoroutine());
        }
    }
    private IEnumerator PlayBgmGroupCoroutine()
    {
        isPlayingBgm = true;

        while (true)
        {
            // 停止所有 BGM
            for (int i = 0; i < bgm.Length; i++)
            {
                bgm[i].Stop();
            }


            currentBgm = BattleBgmIndex[Random.Range(0, BattleBgmIndex.Count)];
            // 播放当前 BGM
            bgm[currentBgm].Play();

            // 等待当前 BGM 结束
            while (bgm[currentBgm].isPlaying)
            {
                yield return null;
            }
        }
    }
    public void StopBgmBattleGroup()
    {
        if (isPlayingBgm)
        {
            if (currentBgm != -1)
            {
                bgm[currentBgm].Stop();
            }

            if (bgmCoroutine != null)
            {
                StopCoroutine(bgmCoroutine);
                bgmCoroutine = null;
            }

            isPlayingBgm = false;
        }
    }
}
