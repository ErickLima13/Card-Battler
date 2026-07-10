using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class Card : MonoBehaviour
{
    private Vector3 _originalScale;
    private Vector3 _originalPosition;

    private int _originalSortOrder;

    [SerializeField] private SpriteRenderer _illustrationRender;

    [SerializeField] private TextMeshPro _cardName;
    [SerializeField] private TextMeshPro _description;
    [SerializeField] private TextMeshPro _actionCost;

    [SerializeField] private SortingGroup _sortingGroup;

    [SerializeField] private float _hoverScale = 2;
    [SerializeField] private float _hoverOffset = 3;

    private void Start()
    {
        _originalSortOrder = _sortingGroup.sortingOrder;
        _originalPosition = transform.localPosition;
        _originalScale = transform.localScale;
    }

    public void LoadCardData(CardData data)
    {
        _illustrationRender.sprite = data.illustration;
        _cardName.text = data.cardName;
        _description.text = data.description;
        _actionCost.text = data.actionCost.ToString();
    }

    private void OnMouseEnter()
    {
        print("mouse enter");
        transform.localScale = _originalScale * _hoverScale;
        transform.localPosition += new Vector3(0,_hoverOffset,0);
        _sortingGroup.sortingOrder += 1;
    }

    private void OnMouseExit()
    {
        print("mouse exit");

        transform.localScale = _originalScale;
        transform.localPosition = _originalPosition;
        _sortingGroup.sortingOrder = _originalSortOrder;
    }
}
