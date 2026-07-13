using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
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
    [SerializeField] private Collider2D _cardCollider;

    [SerializeField] private float _hoverScale = 2;
    [SerializeField] private float _hoverOffset = 3;

    private static bool _isBeingDragged;

    public CardData CardData { get; private set; }


    private void Start()
    {
        _originalScale = transform.localScale;
        _originalSortOrder = _sortingGroup.sortingOrder;
        _originalPosition = transform.localPosition;
    }

    public void CardInDiscardZone(int sortOrder)
    {
        _sortingGroup.sortingOrder = sortOrder;
        _cardCollider.enabled = false;
    }

    public void LoadCardData(CardData data)
    {
        CardData = data;
        _illustrationRender.sprite = data.illustration;
        _cardName.text = data.cardName;
        _description.text = data.description;
        _actionCost.text = data.actionCost.ToString();
    }

    private void OnMouseEnter()
    {
        if (_isBeingDragged) return;

        print("mouse enter");
        transform.localScale = _originalScale * _hoverScale;
        transform.localPosition += new Vector3(0,_hoverOffset,0);
        _sortingGroup.sortingOrder += 1;
    }

    private void OnMouseExit()
    {
        if (_isBeingDragged) return;


        print("mouse exit");

        transform.localScale = _originalScale;
        transform.localPosition = _originalPosition;
        _sortingGroup.sortingOrder = _originalSortOrder;
    }

    private void OnMouseDrag()
    {
        _isBeingDragged = true;
        gameObject.transform.position = GetMousePosition();
    }

    private Vector3 GetMousePosition()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        mousePosition.z = transform.position.z - Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private void OnMouseUp()
    {
        print("mouse up");
        _isBeingDragged = false;
        transform.localScale = _originalScale * _hoverScale;
        transform.localPosition += new Vector3(0, _hoverOffset, 0);
        _sortingGroup.sortingOrder += 1;
    }

    private void OnDestroy()
    {
        _isBeingDragged = false;
    }
}
