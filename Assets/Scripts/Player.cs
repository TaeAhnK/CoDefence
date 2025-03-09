using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int HackPercent = 0;
    public int PlayerLevel = 1;
    public int PlayerExp = 0;

    public TMP_InputField inputField;
    public Slider hackBar;

    private void Start()
    {
        HackPercent = 0;
        inputField.ActivateInputField();
        GameStateUIManager.Instance.UpdateUI();
    }
    
    public bool IsHacked()
    {
        return HackPercent >= 100;
    }

    public void Damage(int damage)
    {
        HackPercent += damage;
        SoundManager.Instance.PlaySound(SoundType.Crash);
        GameStateUIManager.Instance.UpdateUI();
    }

    public void OnWordEnter()
    {
        if (GameManager.Instance.spawner.spawned.ContainsKey(inputField.text))
        {
            AddExp(10);
            GameManager.Instance.spawner.DestroyWord(inputField.text);
            SoundManager.Instance.PlaySound(SoundType.Pop);
            GameStateUIManager.Instance.UpdateUI();
        }
        else if (inputField.text == "quit")
        {
            Damage(100);
            GameStateUIManager.Instance.UpdateUI();
        }
        inputField.text = "";
        inputField.ActivateInputField();
    }

    public void OnTyping()
    {
        SoundManager.Instance.PlaySound(SoundType.Typing);
    }

    private void AddExp(int exp)
    {
        PlayerExp += exp;
        if (PlayerExp >= 50)
        {
            PlayerExp = 0;
            PlayerLevel++;
        }
        GameStateUIManager.Instance.UpdateUI();
    }
}
