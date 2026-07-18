using System;
using UnityEngine;

public static class DeckEvents 
{

    public static event Action<CardData> OnRemoveFromDeck;

    public static void RemoveFromDeck(CardData card)
    {
        OnRemoveFromDeck?.Invoke(card);
    }
    
}
