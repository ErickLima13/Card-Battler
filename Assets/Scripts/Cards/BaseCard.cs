using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class BaseCard : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _illustrationRender;

    [SerializeField] private TextMeshPro _cardName;
    [SerializeField] private TextMeshPro _description;
    [SerializeField] private TextMeshPro _actionCost;

    [SerializeField] protected SortingGroup _sortingGroup;
    [SerializeField] protected Collider2D _cardCollider;

    public CardData CardData { get; private set; }


    public void LoadCardData(CardData data)
    {
        CardData = data;
        _illustrationRender.sprite = data.illustration;
        _cardName.text = data.cardName;

        if (_description != null)
        {
            _description.text = data.description;
        }

        _actionCost.text = data.actionCost.ToString();
    }

    public void CardInDiscardZone(int sortOrder)
    {
        SetOrderCard(sortOrder);
        ActiveCard(false);
    }

    public void SetOrderCard(int sortOrder)
    {
        _sortingGroup.sortingOrder = sortOrder;
    }

    public void ActiveCard(bool value)
    {
        _cardCollider.enabled = value;
    }
}
