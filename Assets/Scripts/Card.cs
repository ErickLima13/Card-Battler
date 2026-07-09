using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _illustrationRender;

    [SerializeField] private TextMeshPro _cardName;
    [SerializeField] private TextMeshPro _description;
    [SerializeField] private TextMeshPro _actionCost;

    [SerializeField] private CardData tempData;


    private void Start()
    {
        LoadCardData(tempData);
    }

    private void LoadCardData(CardData data)
    {
        _illustrationRender.sprite = data.illustration;
        _cardName.text = data.cardName;
        _description.text = data.description;
        _actionCost.text = data.actionCost.ToString();
    }
}
