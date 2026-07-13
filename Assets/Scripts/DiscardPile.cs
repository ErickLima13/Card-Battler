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

        int indexCard = _discardPile.Count - 1;
        tempCard.CardInDiscardZone(indexCard);
        newCard.transform.localPosition = new(0, (indexCard) * -_verticalSpace, 0);
    }
}
