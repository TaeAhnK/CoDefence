using System;

public static class UIEvent
{
    public static Action OnGameStatUIUpdate;
    public static void RaiseOnGameStatUIUpdate()
    {
        OnGameStatUIUpdate?.Invoke();
    }
}