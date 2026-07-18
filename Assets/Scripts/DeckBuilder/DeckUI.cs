using System.Collections.Generic;
using UnityEngine;

public class DeckUI : MonoBehaviour
{
    [SerializeField] private List<CardData> _tempDeck;

    [SerializeField] private Transform _cardSlot;

    [SerializeField] private GameObject _cardTabPrefab;

    private List<GameObject> _cardTabGameObjects = new();

    private const float VERTICAL_SPACING = 0.65f;

    private void Start()
    {
        BuildUI();
    }

    private void BuildUI()
    {
        foreach(GameObject tab in _cardTabGameObjects)
        {
            Destroy(tab);
        }

        _cardTabGameObjects.Clear();

        for (int i = 0; i < _tempDeck.Count; i++)
        {
            CreateCard.CreateSetupCard(_cardTabPrefab, _cardSlot, _tempDeck[i], out GameObject newCard, out BaseCard tempCard);
            newCard.transform.localPosition = new(0, -i * VERTICAL_SPACING, 0);
            _cardTabGameObjects.Add(newCard);
        }
    }

    private void RemoveFromDeck(CardData cardData)
    {
        _tempDeck.Remove(cardData);
        BuildUI();
    }

    private void OnEnable()
    {
        DeckEvents.OnRemoveFromDeck += RemoveFromDeck;
    }

    private void OnDisable()
    {
        DeckEvents.OnRemoveFromDeck -= RemoveFromDeck;

    }

}
