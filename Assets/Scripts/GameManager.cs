using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Player player;
    public WordSpawner spawner;
    public bool IsGameOver = false;

    [field: SerializeField] private GameObject GameOverUI;

    private void OnEnable()
    {
        GameEvent.OnHacked += GameOver;
    }

    private void OnDisable()
    {
        GameEvent.OnHacked -= GameOver;
    }

    void Start()
    {
        GameOverUI.SetActive(false);
        IsGameOver = false;
        BGMEvent.PlayBGM(BGMList.MainTheme);
    }

    private void GameOver()
    {
        IsGameOver = true;
        GameOverUI.SetActive(true);
        GameEvent.RaiseOnGameOver();
    }
}
