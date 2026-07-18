using System.Collections.Generic;
using UnityEngine;

public class DeckUI : MonoBehaviour
{
    [SerializeField] private Transform _cardSlot;

    [SerializeField] private GameObject _cardTabPrefab;

    private List<GameObject> _cardTabGameObjects = new();

    private const float VERTICAL_SPACING = 0.65f;

    private void BuildUI()
    {
        foreach(GameObject tab in _cardTabGameObjects)
        {
            Destroy(tab);
        }

        _cardTabGameObjects.Clear();

        List<CardData> deck = DeckManager.Instance.CurrentDeck;

        for (int i = 0; i < deck.Count; i++)
        {
            CreateCard.CreateSetupCard(_cardTabPrefab, _cardSlot, deck[i], out GameObject newCard, out BaseCard tempCard);
            newCard.transform.localPosition = new(0, -i * VERTICAL_SPACING, 0);
            _cardTabGameObjects.Add(newCard);
        }
    }

    private void OnEnable()
    {
        DeckEvents.OnDeckProcessed += BuildUI;
    }

    private void OnDisable()
    {
        DeckEvents.OnDeckProcessed -= BuildUI;
    }

}
