using UnityEngine;

public enum BGMList
{
    MainTheme
}

public class BGMManager : MonoBehaviour
{
    private static BGMManager instance;
    public static BGMManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<BGMManager>();
                if (instance == null)
                {
                    var go = new GameObject(typeof(BGMManager).Name + " Auto-generated");
                    instance = go.AddComponent<BGMManager>();
                }
            }
            return instance;
        }
    }

    [SerializeField] private AudioSource mainTheme;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        if (!mainTheme.isPlaying)
        {
            PlayBGM(BGMList.MainTheme);
        }
    }

    public void PlayBGM(BGMList bgm)
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

    public void StopBGM(BGMList bgm)
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