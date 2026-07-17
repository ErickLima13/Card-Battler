using System.Collections.Generic;
using UnityEngine;

public class DiscardPile : MonoBehaviour
{
    private const float _verticalSpace = .25f;

    [SerializeField] private GameObject _cardPrefab;

    [SerializeField] private List<CardData> _discardPile = new();

    [SerializeField] private Deck _deck;

    public void DiscardCard(CardData cardData)
    {
        _discardPile.Add(cardData);
        DrawDiscard(cardData);
    }

    private void DrawDiscard(CardData cardData)
    {
        CreateCard.CreateSetupCard(_cardPrefab,transform, cardData, out GameObject newCard, out Card tempCard);

        int indexCard = _discardPile.Count - 1;
        tempCard.CardInDiscardZone(indexCard);
        newCard.transform.localPosition = new(0, (indexCard) * -_verticalSpace, 0);
    }

    public void MoveCardsToDeck(List<CardData> drawPile)
    {
        if(drawPile == null || _discardPile.Count == 0) return;

        drawPile.AddRange(_discardPile);
        ClearPile();
    }

    private void ClearPile()
    {
        _discardPile.Clear();

        foreach(Transform discardedCard in transform)
        {
            Destroy(discardedCard.gameObject);
        }
    }

    private void OnMouseDown()
    {
        if (_discardPile.Count == 0) return;

        if (TurnSystem.Instance.CanReshufleDiscard())
        {
            PlayerEvents.ReshufleResquested();
            _deck.ReshufleFromDiscardPile();
        }
        else
        {
            GameManager.Instance.SetMessageGame("Insuficient actions point");
        }

    }
}
