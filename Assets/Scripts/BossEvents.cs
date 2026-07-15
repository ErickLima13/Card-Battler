using System;
using UnityEngine;

public static class BossEvents 
{
    public static event Action<CardData> OnBossHit;

    public static void BossHit(CardData cardData)
    {
        OnBossHit?.Invoke(cardData);
    }
}
