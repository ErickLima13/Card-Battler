using System;
using UnityEditor;

public static class TurnEvents
{
    public static event Action OnPlayerTurnStart;
    public static event Action OnPlayerTurnEnd;
    public static event Action OnBossTurnStart;

    public static void PlayerTurnStart()
    {
        OnPlayerTurnStart?.Invoke();
    }

    public static void PlayerTurnEnd()
    {
        OnPlayerTurnEnd?.Invoke();
    }

    public static void BossTurnStart()
    {
        OnBossTurnStart?.Invoke();
    }
}
