using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<GameManager>();
                if (instance == null)
                {
                    var go = new GameObject(typeof(GameManager).Name + " Auto-generated");
                    instance = go.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    #endregion

    public Player player;
    public WordSpawner spawner;
    public bool IsGameOver = false;

    [field: SerializeField] private GameObject GameOverUI;

    private void Awake()
    {
        // Singleton
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        instance = this;
    }

    void Start()
    {
        GameOverUI.SetActive(false);
        IsGameOver = false;
        BGMManager.Instance.PlayBGM(BGMList.MainTheme);
    }

    void Update()
    {
        if (player.IsHacked())
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        IsGameOver = true;
        GameOverUI.SetActive(true);
        return;
    }
}
