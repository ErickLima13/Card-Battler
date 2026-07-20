using System;
using UnityEngine;

public static class DeckEvents 
{

    public static event Action<CardData> OnRemoveFromDeck;

    public static event Action<CardData> OnAddCardToDeck;

    public static event Action OnDeckProcessed;

    public static void RemoveFromDeck(CardData card)
    {
        OnRemoveFromDeck?.Invoke(card);
    }

    public static void AddCardToDeck(CardData card)
    {
        OnAddCardToDeck?.Invoke(card);
    }

    public static void DeckProcessed()
    {
        OnDeckProcessed?.Invoke();
    }
    
}
