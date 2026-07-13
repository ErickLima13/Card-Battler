using System;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{
    public static event Action<CardData> OnCardPlayed;

    public static void CardPlayerd(CardData cardData)
    {
        OnCardPlayed?.Invoke(cardData);
    }
}
