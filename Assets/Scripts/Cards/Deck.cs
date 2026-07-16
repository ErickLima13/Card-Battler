using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    private const float _verticalSpace = -0.1f;

    [SerializeField] private List<CardData> _drawPile = new();

    [SerializeField] private GameObject _cardBack;

    [SerializeField] private DiscardPile _discardPile;

    private void Start()
    {
        Shuffle();
        DrawVisuals();
    }

    public CardData DrawCard()
    {
        if (_drawPile.Count > 0)
        {
            int topIndex = _drawPile.Count - 1;
            CardData data = _drawPile[topIndex];
            _drawPile.RemoveAt(topIndex);
            DrawVisuals();
            return data;
        }

        return null;
    }

    private void DrawVisuals()
    {
        foreach (Transform discardedCard in transform)
        {
            Destroy(discardedCard.gameObject);
        }

        for (int i = 0; i < _drawPile.Count; i++)
        {
            GameObject newCardBack = Instantiate(_cardBack, transform);
            newCardBack.GetComponent<SpriteRenderer>().sortingOrder = i;
            newCardBack.transform.localPosition = new(0, i * _verticalSpace, 0);
        }
    }

    private void Shuffle()
    {
        for (int i = 0; i < _drawPile.Count; i++)
        {
            CardData card = _drawPile[i];
            int randomIndex = Random.Range(i, _drawPile.Count);
            _drawPile[i] = _drawPile[randomIndex];
            _drawPile[randomIndex] = card;
        }
    }

    public void ReshufleFromDiscardPile()
    {
        _discardPile.MoveCardsToDeck(_drawPile);
        Shuffle();
        DrawVisuals();
    }

    private void OnMouseDown()
    {
        if (_drawPile.Count <= 0)
        {
            print("no cards left in deck");
            return;
        }

        if (TurnSystem.Instance.HasReimainingAction() || TurnSystem.Instance.CanDrawCard())
        {
            PlayerEvents.DrawCardRequest();
        }
    }

}
