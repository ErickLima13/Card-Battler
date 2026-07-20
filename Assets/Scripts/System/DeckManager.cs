using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DeckManager : Singleton<DeckManager>
{
    [SerializeField] private List<CardData> _currentDeck = new();
 
    [SerializeField] private int _maxDeckSize = 9;

    [SerializeField] private DefaultDeck _defaultDeck;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        _currentDeck = _defaultDeck.cards;
    }

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
        if(_currentDeck.Count >= _maxDeckSize)
        {
            return;
        }    

        _currentDeck.Add(card);
        DeckEvents.DeckProcessed();
    }

    private void RemoveCard(CardData card)
    {
        _currentDeck.Remove(card);
        DeckEvents.DeckProcessed();
    }

    public List<CardData> GetDeck()
    {
        return new List<CardData>(_currentDeck);
    }

}
