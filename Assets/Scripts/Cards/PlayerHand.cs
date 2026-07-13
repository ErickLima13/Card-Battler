using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [SerializeField] private Deck _deck;

    [SerializeField] private Transform[] _cardsSlots;

    [SerializeField] private GameObject _cardPrefab;

    [SerializeField] private int _startingHandSize = 2;

    [SerializeField] private List<Card> _cardsInHand = new();

    [SerializeField] private DiscardPile _discardPile;

    private void Start()
    {
        for (int i = 0; i < _startingHandSize; i++)
        {
            DrawNextCard();
        }
    }

    public void DrawNextCard()
    {
        if (_cardsSlots == null || _cardsInHand.Count >= _cardsSlots.Length)
        {
            print("hands is full or slots are null");
            return;
        }

        CardData cardData = _deck.DrawCard();

        if (cardData == null)
        {
            print("no cards left in deck");
            return;
        }

        int slotIndex = _cardsInHand.Count;
        GameObject newCard = Instantiate(_cardPrefab, _cardsSlots[slotIndex].position, Quaternion.identity);
        Card cardComponent = newCard.GetComponent<Card>();
        cardComponent.LoadCardData(cardData);
        _cardsInHand.Add(cardComponent);
        _cardsInHand[slotIndex].transform.SetParent(_cardsSlots[slotIndex]);
    }

    public void PlayCard(Card card)
    {
        _cardsInHand.Remove(card);
        _discardPile.DiscardCard(card.CardData);
        Destroy(card.gameObject);
        RepositionCard();
        PlayerEvents.CardPlayerd(card.CardData);
    }

    private void RepositionCard()
    {
        for(int i = 0; i < _cardsInHand.Count; i++)
        {
            _cardsInHand[i].transform.SetParent(null);
        }

        for (int i = 0; i < _cardsInHand.Count; i++)
        {
            _cardsInHand[i].transform.SetParent(_cardsSlots[i]);
            _cardsInHand[i].transform.position = _cardsSlots[i].transform.position;
        }
    }
}
