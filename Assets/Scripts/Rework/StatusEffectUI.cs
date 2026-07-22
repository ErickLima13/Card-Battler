using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffectUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _stackText;


    private StatusEffect _currentStatus;


    public void Setup(StatusEffect status)
    {
        _currentStatus = status;

        UpdateVisual();
    }


    public void UpdateVisual()
    {
        if (_currentStatus == null)
            return;


        //_icon.sprite = _currentStatus.Data.icon;
        //_icon.color = _currentStatus.Data.color;

        _stackText.text = _currentStatus.Stacks.ToString();
    }
}