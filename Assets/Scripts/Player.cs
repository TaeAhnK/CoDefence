using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int HackPercent { get; private set; } = 0;
    public int PlayerLevel { get; private set; } = 1;
    public int PlayerExp { get; private set; } = 0;
    
    public TMP_InputField inputField;

    private void OnEnable()
    {
        GameEvent.OnWordDestroyed += AddExp;
        GameEvent.OnWordAttack += OnWordAttack;
    }

    private void OnDisable()
    {
        GameEvent.OnWordDestroyed -= AddExp;
        GameEvent.OnWordAttack -= OnWordAttack;
    }
    
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

    public void OnWordAttack(Word word)
    {
        Damage(word.damage);
    }

    private void Damage(int damage)
    {
        HackPercent += damage;
        SoundEvent.PlaySound(SoundType.Crash);
        
        UIEvent.RaiseOnGameStatUIUpdate();

        if (IsHacked())
        {
            GameEvent.RaiseOnHacked();
        }
    }

    public void OnWordEnter()
    {
        if (inputField.text == "quit")
        {
            Damage(100);
        }
        else
        {
            GameEvent.RaiseOnWordInput(inputField.text);
        }
        
        UIEvent.RaiseOnGameStatUIUpdate();
        inputField.text = "";
        inputField.ActivateInputField();
    }

    public void OnTyping()
    {
        SoundEvent.PlaySound(SoundType.Typing);
    }

    private void AddExp(int exp)
    {
        PlayerExp += exp;
        if (PlayerExp >= 50)
        {
            PlayerExp = 0;
            PlayerLevel++;
            GameEvent.RaiseOnLevelChanged(PlayerLevel);
        }
        UIEvent.RaiseOnGameStatUIUpdate();
    }
}
