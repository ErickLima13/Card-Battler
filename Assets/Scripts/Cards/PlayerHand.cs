using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [SerializeField] private Deck _deck;

    [SerializeField] private Transform[] _cardsSlots;

    [SerializeField] private GameObject _cardPrefab;

    [SerializeField] private int _startingHandSize = 2;

    [SerializeField] private List<BaseCard> _cardsInHand = new();

    [SerializeField] private DiscardPile _discardPile;

    public bool CanConsumeAction() => _cardsSlots == null || _cardsInHand.Count >= _cardsSlots.Length;

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
            return;
        }

        CardData cardData = _deck.DrawCard();

        if (cardData == null)
        {
            return;
        }

        int slotIndex = _cardsInHand.Count;

        CreateCard.CreateSetupCard(_cardPrefab, _cardsSlots[slotIndex].transform, cardData, out GameObject newCard, out BaseCard cardComponent);

        _cardsInHand.Add(cardComponent);

        if (!TurnSystem.Instance.HasReimainingAction())
        {
            cardComponent.ActiveCard(false);
        }
    }

    public void PlayCard(Card card)
    {
        if (TurnSystem.Instance.CanPlayCard(card.CardData.actionCost))
        {
            GameManager.Instance.SetMessageGame("Insuficient actions point");
            return;
        }

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

    private void DisableHand()
    {
        for (int i = 0; i < _cardsInHand.Count; i++)
        {
            _cardsInHand[i].ActiveCard(false);
        }
    }

    private void EnableHand()
    {
        for (int i = 0; i < _cardsInHand.Count; i++)
        {
            _cardsInHand[i].ActiveCard(true);
        }
    }

    private void OnEnable()
    {
        TurnEvents.OnPlayerTurnEnd += DisableHand;
        TurnEvents.OnPlayerTurnStart += EnableHand;

        PlayerEvents.OnDrawCardRequested += DrawNextCard;
    }

    private void OnDisable()
    {
        TurnEvents.OnPlayerTurnEnd -= DisableHand;
        TurnEvents.OnPlayerTurnStart -= EnableHand;

        PlayerEvents.OnDrawCardRequested -= DrawNextCard;
    }
}
