using FGT.Prototypes.DamagePopup;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TurnSystem : Singleton<TurnSystem>
{
    private enum TurnState { PlayerTurn,BossTurn}

    [SerializeField] private TurnState _currentTurn = TurnState.PlayerTurn;

    [SerializeField] private float _turnWaitTime = 2f;

    [SerializeField] private int _maxAction;
    [SerializeField] private int _remainingAction;

    [SerializeField] private int _drawConsume = 1;
    [SerializeField] private int _reshufleConsume = 2;

    [SerializeField] private TextMeshProUGUI _remainingActionText;

    [SerializeField] private TextMeshProUGUI _turnDisplayText;

    public int playerTurnAccount;
    public int bossTurnAccount;

    private void Start()
    {
        SetDisplay("Player's Turn");
        StartPlayerTurn();        
    }

    private void StartPlayerTurn()
    {
        playerTurnAccount++;
        _currentTurn = TurnState.PlayerTurn;
        _remainingAction = _maxAction;
        UpdateActionsUI();
        TurnEvents.PlayerTurnStart();
    }

    public void EndPlayerTurn()
    {
        TurnEvents.PlayerTurnEnd();
        StartCoroutine(WaitBetweenTurns());
    }

    private IEnumerator StartBossTurn()
    {
        bossTurnAccount++;
        _currentTurn = TurnState.BossTurn;
        yield return new WaitForSeconds(1);
        BossTurn();
    }

    private IEnumerator EndBossTurn()
    {   
        TurnEvents.BossTurnEnd();
        yield return new WaitForSeconds(1);
        StartCoroutine(WaitBetweenTurns());
    }

    private IEnumerator WaitBetweenTurns()
    {
        for(int i = (int)_turnWaitTime; i >= 0; i--)
        {
            SetDisplay(i + "...");
            yield return new WaitForSeconds(1);
        }

        if (GameManager.Instance.IsGameActive)
        {
            if (_currentTurn != TurnState.PlayerTurn)
            {
                SetDisplay("Player's Turn");
                StartPlayerTurn();
            }
            else
            {
                SetDisplay("Boss's Turn");
                StartCoroutine(StartBossTurn());
            }
        }
    }

    public bool HasReimainingAction() => _remainingAction > 0;

    public bool CanDrawCard() => _remainingAction >= _drawConsume;

    public bool CanPlayCard(int cost) => _remainingAction < cost;

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

        string temp = "- " + amount;
        DamagePopup.Create($"{temp}", Vector3.up, _remainingActionText.rectTransform, Color.white, 10);

        if (_remainingAction <= 0)
        {
            EndPlayerTurn();
        }
    }

    private void CardPlayed(CardData cardData)
    {
        ConsumeAction(cardData.actionCost);
    }

    private void BossTurn()
    {
        TurnEvents.BossTurnStart();
        StartCoroutine(EndBossTurn());
    }

    private void UpdateActionsUI()
    {
        _remainingActionText.text = _remainingAction.ToString() + "/" + _maxAction;
    }

    private void SetDisplay(string value)
    {
        _turnDisplayText.text = value;
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
