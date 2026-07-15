using System.Collections;
using TMPro;
using UnityEngine;

public class TurnSystem : Singleton<TurnSystem>
{
    [SerializeField] private float _turnWaitTime = 3f;

    [SerializeField] private int _maxAction;
    [SerializeField] private int _remainingAction;

    [SerializeField] private TextMeshProUGUI _remainingActionText;

    private void Start()
    {
        _remainingAction = _maxAction;
        UpdateActionsUI();
    }

    public bool HasReimainingAction() => _remainingAction > 0;

    private void DrawCardRequested()
    {
        ConsumeAction(1);
    }

    private void ConsumeAction(int amount)
    {
        _remainingAction -= amount;

        UpdateActionsUI();

        if (_remainingAction <= 0)
        {
            TurnEvents.PlayerTurnEnd();
            StartCoroutine(BossTurn());
        }
    }

    private void CardPlayed(CardData cardData)
    {
        ConsumeAction(cardData.actionCost);
    }

    private IEnumerator BossTurn()
    {
        yield return new WaitForSeconds(_turnWaitTime);
        TurnEvents.BossTurnStart();
        yield return new WaitForSeconds(_turnWaitTime);

        
        _remainingAction = _maxAction;
        UpdateActionsUI();
        TurnEvents.PlayerTurnStart();
    }

    private void UpdateActionsUI()
    {
        _remainingActionText.text = _remainingAction.ToString() + "/" + _maxAction;
    }

    private void OnEnable()
    {
        PlayerEvents.OnCardPlayed += CardPlayed;
        PlayerEvents.OnDrawCardRequested += DrawCardRequested;
    }

    private void OnDisable()
    {
        PlayerEvents.OnCardPlayed -= CardPlayed;
        PlayerEvents.OnDrawCardRequested -= DrawCardRequested;
    }
}
