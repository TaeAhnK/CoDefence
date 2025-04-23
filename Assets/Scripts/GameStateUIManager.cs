using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStateUIManager : Singleton<GameStateUIManager>
{
    public TextMeshProUGUI level;
    public TextMeshProUGUI progress;
    public Slider slider;
    [field: SerializeField] private Player player;

    private int cachedLevel = -1;
    private int cachedHackPercent = -1;
    private StringBuilder progressSB = new StringBuilder();
    
    private void OnEnable()
    {
        UIEvent.OnGameStatUIUpdate += UpdateUI;
    }

    private void OnDisable()
    {
        UIEvent.OnGameStatUIUpdate -= UpdateUI;
    }

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        UpdateLevel();
        UpdateProgress();
        UpdateSlider();
    }

    private void UpdateLevel()
    {
        int currentLevel = player.PlayerLevel;
        if (currentLevel == cachedLevel) return;
        else
        {
            level.text = currentLevel.ToString();
            cachedLevel = currentLevel;
        }
    }

    private void UpdateProgress()
    {
        int currentPercent = Mathf.Clamp(player.HackPercent, 0, 100);
        if  (currentPercent == cachedHackPercent) return;
        
        progressSB.Clear();
        progressSB.Append("Attacking... ");
        progressSB.Append(currentPercent);
        progressSB.Append("% done");

        progress.text = progressSB.ToString();
        
        cachedHackPercent = currentPercent;
    }

    private void UpdateSlider()
    {
        slider.value = player.HackPercent;
    }
}
