using System.Collections;
using UnityEngine;

public class TurnSystem : Singleton<TurnSystem>
{
    [SerializeField] private float _turnWaitTime = 3f;


    private void CardPlayed(CardData cardData)
    {
        TurnEvents.PlayerTurnEnd();
        StartCoroutine(BossTurn());
    }

    private IEnumerator BossTurn()
    {
        yield return new WaitForSeconds(_turnWaitTime);

        TurnEvents.BossTurnStart();

        yield return new WaitForSeconds(_turnWaitTime);

        TurnEvents.PlayerTurnStart();
    }

    private void OnEnable()
    {
        PlayerEvents.OnCardPlayed += CardPlayed;
    }

    private void OnDisable()
    {
        PlayerEvents.OnCardPlayed -= CardPlayed;

    }
}
