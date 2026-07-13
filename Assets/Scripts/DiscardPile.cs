using System.Collections.Generic;
using UnityEngine;

public class DiscardPile : MonoBehaviour
{
    private const float _verticalSpace = .1f;

    [SerializeField] private GameObject _cardPrefab;

    [SerializeField] private List<CardData> _discardPile = new();


    public void DiscardCard(CardData cardData)
    {
        print("discard card + " + cardData);
        _discardPile.Add(cardData);
        DrawDiscard(cardData);
    }

    private void DrawDiscard(CardData cardData)
    {
        GameObject newCard = Instantiate(_cardPrefab, transform);
        Card tempCard = newCard.GetComponent<Card>();
        tempCard.LoadCardData(cardData);
        tempCard.CardInDiscard(_discardPile.Count - 1);
        newCard.transform.localPosition = new(0, (_discardPile.Count-1) * -_verticalSpace, 0);
    }
}
