using System.Collections.Generic;
using UnityEngine;

public class CardCollection : MonoBehaviour
{
    [SerializeField] private List<CardData> _availableCards;

    [SerializeField] private Transform[] _cardSlots;

    [SerializeField] private GameObject _cardPrefab;

    private void Start()
    {
        for(int i = 0; i < _availableCards.Count; i++)
        {
            AddCardToCollection(i);
        }
    }

    private void AddCardToCollection(int cardIndex)
    {
        //GameObject card = Instantiate(_cardPrefab, _cardSlots[cardIndex].position,Quaternion.identity);
        //Card cardComponent = card.GetComponent<Card>();
        //cardComponent.LoadCardData(_availableCards[cardIndex]);

        CreateCard.CreateSetupCard(_cardPrefab, _cardSlots[cardIndex].transform, _availableCards[cardIndex], out GameObject card, out Card cardComponent);

    }
}
