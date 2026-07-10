using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [SerializeField] private Deck _deck;

    [SerializeField] private Transform[] _cardsSlots;

    [SerializeField] private GameObject _cardPrefab;

    [SerializeField] private int _startingHandSize = 2;

    [SerializeField] private List<Card> _cardsInHand = new();



    private void Start()
    {
        for (int i = 0; i < _startingHandSize; i++)
        {
            DrawNextCard();
        }
    }

    private void DrawNextCard()
    {
        if (_cardsSlots == null || _cardsInHand.Count >= _cardsSlots.Length)
        {
            print("hans is full or slots are null");
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
}
