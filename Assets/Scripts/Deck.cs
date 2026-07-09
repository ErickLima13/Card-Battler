using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] private List<CardData> _drawPile = new();


    private void Start()
    {
       print( DrawCard());
    }

    private CardData DrawCard()
    {
        if(_drawPile.Count > 0)
        {
            int topIndex = _drawPile.Count - 1;
            CardData data = _drawPile[topIndex];
            _drawPile.RemoveAt(topIndex);
            return data;
        }

        return null;
    }



}
