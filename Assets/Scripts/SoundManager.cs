using UnityEngine;

public enum SoundType
{
    Start,
    Login,
    Typing,
    Pop,
    Crash,
    GameEnd
}


public class SoundManager : MonoBehaviour
{
    #region Singleton
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<SoundManager>();
                if (instance == null)
                {
                    var go = new GameObject(typeof(SoundManager).Name + " Auto-generated");
                    instance = go.AddComponent<SoundManager>();
                }
            }
            return instance;
        }
    }

    #endregion

    [SerializeField] private AudioSource typingSound;
    [SerializeField] private AudioSource popSound;
    [SerializeField] private AudioSource crashSound;
    [SerializeField] private AudioSource loginSound;
    [SerializeField] private AudioSource startSound;
    [SerializeField] private AudioSource gameEndSound;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        instance = this;
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
