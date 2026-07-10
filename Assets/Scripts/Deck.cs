using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    private const float _verticalSpace = -0.1f;

    [SerializeField] private List<CardData> _drawPile = new();

    [SerializeField] private GameObject _cardBack;

    private void Start()
    {
        DrawVisuals();
    }

    public CardData DrawCard()
    {
        if (_drawPile.Count > 0)
        {
            int topIndex = _drawPile.Count - 1;
            CardData data = _drawPile[topIndex];
            _drawPile.RemoveAt(topIndex);
            return data;
        }

        return null;
    }

    private void DrawVisuals()
    {
        for (int i = 0; i < _drawPile.Count; i++)
        {
            GameObject newCardBack = Instantiate(_cardBack, transform);
            newCardBack.transform.localPosition = new(0, i * _verticalSpace, 0);
        }
    }

}
