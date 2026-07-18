using System.Collections.Generic;
using UnityEngine;

public class DeckUI : MonoBehaviour
{
    [SerializeField] private List<CardData> _tempDeck;

    [SerializeField] private Transform _cardSlot;

    [SerializeField] private GameObject _cardTabPrefab;

    private const float VERTICAL_SPACING = 0.65f;

    private void Start()
    {
        for(int i  = 0; i < _tempDeck.Count; i++)
        {
            CreateCard.CreateSetupCard(_cardTabPrefab, _cardSlot, _tempDeck[i], out GameObject newCard, out BaseCard tempCard);
            newCard.transform.localPosition = new(0, -i * VERTICAL_SPACING, 0); 
        }

    }
}
