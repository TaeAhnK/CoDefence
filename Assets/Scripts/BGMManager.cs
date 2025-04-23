using UnityEngine;
using System;

public enum BGMList
{
    MainTheme
}

public static class BGMEvent
{
    public static event Action<BGMList> OnBGMPlayEvent;
    public static event Action<BGMList> OnBGMStopEvent;
    public static void PlayBGM(BGMList bgm)
    {
        OnBGMPlayEvent?.Invoke(bgm);
    }

    public static void StopBGM(BGMList bgm)
    {
        OnBGMStopEvent?.Invoke(bgm);
    }
}

public class BGMManager : Singleton<BGMManager>
{
    [SerializeField] private AudioSource mainTheme;

    private void OnEnable()
    {
        BGMEvent.OnBGMPlayEvent += PlayBGM;
        BGMEvent.OnBGMStopEvent += StopBGM;
    }

    private void OnDisable()
    {
        BGMEvent.OnBGMPlayEvent -= PlayBGM;
        BGMEvent.OnBGMStopEvent -= StopBGM;
    }

    private void Start()
    {
        if (!mainTheme.isPlaying)
        {
            PlayBGM(BGMList.MainTheme);
        }
    }

    private void PlayBGM(BGMList bgm)
    {
        switch (bgm)
        {
            case BGMList.MainTheme:
                mainTheme.Play();
                break;
            default:
                break;
        }
    }

    private void StopBGM(BGMList bgm)
    {
        switch (bgm)
        {
            case BGMList.MainTheme:
                mainTheme.Stop();
                break;
            default:
                break;
        }
    }
}