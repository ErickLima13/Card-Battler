using System;

public static class HealthEvents
{
    public static event Action<bool> OnApplyPoison;

    public static void ApplyPoison(bool value)
    {
        OnApplyPoison?.Invoke(value);
    }
}
