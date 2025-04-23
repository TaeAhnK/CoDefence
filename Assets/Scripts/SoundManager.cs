using UnityEngine;
using System;

public enum SoundType
{
    Start,
    Login,
    Typing,
    Pop,
    Crash,
    GameEnd
}

public static class SoundEvent
{
    public static event Action<SoundType> OnSoundPlayEvent;
    public static event Action<SoundType> OnSoundStopEvent;
    
    public static void PlaySound(SoundType soundType)
    {
        OnSoundPlayEvent?.Invoke(soundType);
    }

    public static void StopSound(SoundType soundType)
    {
        OnSoundStopEvent?.Invoke(soundType);
    }
}

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource typingSound;
    [SerializeField] private AudioSource popSound;
    [SerializeField] private AudioSource crashSound;
    [SerializeField] private AudioSource loginSound;
    [SerializeField] private AudioSource startSound;
    [SerializeField] private AudioSource gameEndSound;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SoundEvent.OnSoundPlayEvent += PlaySound;
        SoundEvent.OnSoundStopEvent += StopSound;
    }

    private void OnDisable()
    {
        SoundEvent.OnSoundPlayEvent -= PlaySound;
        SoundEvent.OnSoundStopEvent -= StopSound;
    }
    
    public void PlaySound(SoundType sound)
    {
        switch (sound)
        {
            case SoundType.Typing:
                typingSound.Play();
                break;
            case SoundType.Pop:
                popSound.Play();
                break;
            case SoundType.Crash:
                crashSound.Play();
                break;
            case SoundType.Start:
                startSound.Play();
                break;
            case SoundType.Login:
                loginSound.Play();
                break;
            case SoundType.GameEnd:
                gameEndSound.Play();
                break;
            default:
                break;
        }
    }

    public void StopSound(SoundType sound)
    {
        switch (sound)
        {
            case SoundType.Start:
                startSound.Stop();
                break;
            case SoundType.Login:
                loginSound.Stop();
                break;
            case SoundType.Typing:
                typingSound.Stop();
                break;
            case SoundType.Pop:
                popSound.Stop();
                break;
            case SoundType.Crash:
                crashSound.Stop();
                break;
            case SoundType.GameEnd:
                gameEndSound.Stop();
                break;
            default:
                break;
        }
    }
}
