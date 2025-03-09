using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStateUIManager : MonoBehaviour
{
    #region Singleton
    private static GameStateUIManager instance;
    public static GameStateUIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<GameStateUIManager>();
                if (instance == null)
                {
                    var go = new GameObject(typeof(GameStateUIManager).Name + " Auto-generated");
                    instance = go.AddComponent<GameStateUIManager>();
                }   
            }
            return instance;
        }
    }
    #endregion
    public TextMeshProUGUI level;
    public TextMeshProUGUI progress;
    public Slider slider;
    private Player player;

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

        player = GameManager.Instance.player;
    }

    public void UpdateUI()
    {
        level.text = player.PlayerLevel.ToString();
        progress.text = "Attacking... " + Mathf.Clamp(player.HackPercent, 0, 100).ToString() + "% done";
        slider.value = player.HackPercent;
    }

}
