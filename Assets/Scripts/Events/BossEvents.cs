using System;
using UnityEngine;

public static class BossEvents 
{
    public static event Action<CardData> OnBossHit;

    public static event Action OnBossDeath;

    public static event Action<int> OnApplyPoison;

    public static void ApplyPoison(int value)
    {
        OnApplyPoison?.Invoke(value);
    }

    public static void BossHit(CardData cardData)
    {
        OnBossHit?.Invoke(cardData);
    }

    public static void BossDeath()
    {
        OnBossDeath?.Invoke();
    }
}
