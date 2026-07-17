using TMPro;
using UnityEngine;

public class BaseCard : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _illustrationRender;

    [SerializeField] private TextMeshPro _cardName;
    [SerializeField] private TextMeshPro _description;
    [SerializeField] private TextMeshPro _actionCost;

    public CardData CardData { get; private set; }


    public void LoadCardData(CardData data)
    {
        CardData = data;
        _illustrationRender.sprite = data.illustration;
        _cardName.text = data.cardName;
        _description.text = data.description;
        _actionCost.text = data.actionCost.ToString();
    }
}
