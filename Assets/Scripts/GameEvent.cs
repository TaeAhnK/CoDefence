using System;

public static class GameEvent
{
    public static event Action<String> OnWordInput;
    public static void RaiseOnWordInput(String input)
    {
        OnWordInput?.Invoke(input);
    }

    public static event Action<int> OnWordDestroyed;
    public static void RaiseOnWordDestroyed(int score)
    {
        OnWordDestroyed?.Invoke(score);
    }

    public static event Action<Word> OnWordAttack;
    public static void RaiseOnWordAttack(Word word)
    {
        OnWordAttack?.Invoke(word);
    }
    
    public static event Action OnHacked;
    public static void RaiseOnHacked()
    {
        OnHacked?.Invoke();
    }
    
    public static event Action OnGameOver;
    public static void RaiseOnGameOver()
    {
        OnGameOver?.Invoke();
    }

    public static event Action<int> OnLevelChanged;
    public static void RaiseOnLevelChanged(int level)
    {
        OnLevelChanged?.Invoke(level);
    }

}
