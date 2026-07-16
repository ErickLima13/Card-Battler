using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TurnSystem : Singleton<TurnSystem>
{
    [SerializeField] private float _turnWaitTime = 2f;

    [SerializeField] private int _maxAction;
    [SerializeField] private int _remainingAction;

    [SerializeField] private int _drawConsume = 1;
    [SerializeField] private int _reshufleConsume = 2;



    [SerializeField] private TextMeshProUGUI _remainingActionText;

    private void Start()
    {
        _remainingAction = _maxAction;

        UpdateActionsUI();
        TurnEvents.PlayerTurnStart();
    }

    public bool HasReimainingAction() => _remainingAction > 0;

    public bool CanDrawCard() => _remainingAction >= _drawConsume;

    public bool CanReshufleDiscard() => _remainingAction >= _reshufleConsume;

    private void ReshufleRequested()
    {
        ConsumeAction(_reshufleConsume);
    }

    private void DrawCardRequested()
    {
        ConsumeAction(_drawConsume);
    }

    private void ConsumeAction(int amount)
    {
        if (!HasReimainingAction())
        {
            return;
        }

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
        PlayerEvents.OnReshufleResquested += ReshufleRequested;
        PlayerEvents.OnDrawCardRequested += DrawCardRequested;
    }

    private void OnDisable()
    {
        PlayerEvents.OnCardPlayed -= CardPlayed;
        PlayerEvents.OnReshufleResquested -= ReshufleRequested;
        PlayerEvents.OnDrawCardRequested -= DrawCardRequested;
    }


}
