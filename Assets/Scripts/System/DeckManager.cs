using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DeckManager : Singleton<DeckManager>
{
    public List<CardData> CurrentDeck { get; private set; } = new();

    [SerializeField] private int _maxDeckSize = 9;


    private void OnEnable()
    {
        DeckEvents.OnAddCardToDeck += AddCard;
        DeckEvents.OnRemoveFromDeck += RemoveCard;
    }

    private void OnDisable()
    {
        DeckEvents.OnAddCardToDeck -= AddCard;
        DeckEvents.OnRemoveFromDeck -= RemoveCard;
    }

    private void AddCard(CardData card)
    {
        if(CurrentDeck.Count >= _maxDeckSize)
        {
            return;
        }    

        CurrentDeck.Add(card);
        DeckEvents.DeckProcessed();
    }

    private void RemoveCard(CardData card)
    {
        CurrentDeck.Remove(card);
        DeckEvents.DeckProcessed();
    }

}
