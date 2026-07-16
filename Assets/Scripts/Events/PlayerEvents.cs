using System;
using UnityEngine;

public static class PlayerEvents 
{
    public static event Action<CardData> OnCardPlayed;

    public static event Action<int> OnPlayerHit;

    public static event Action OnPlayerDeath;

    public static event Action OnDrawCardRequested;

    public static event Action OnReshufleResquested;

    public static event Action OnPlayerHealed;

    public static void CardPlayerd(CardData cardData)
    {
        OnCardPlayed?.Invoke(cardData);
    }

    public static void PlayerHit(int amount)
    {
        OnPlayerHit?.Invoke(amount);
    }

    public static void PlayerDeath()
    {
        OnPlayerDeath?.Invoke();
    }

    public static void DrawCardRequest()
    {
        OnDrawCardRequested?.Invoke();
    }

    public static void PlayerHealed()
    {
        OnPlayerHealed?.Invoke();
    }

    public static void ReshufleResquested()
    {
        OnReshufleResquested?.Invoke();
    }
}
