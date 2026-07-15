using System.Collections;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    [SerializeField] private float _turnWaitTime = 3f;


    private void CardPlayed(CardData cardData)
    {
        TurnEvents.PlayerTurnEnd();
        print("player turn end");
        StartCoroutine(BossTurn());
    }

    private IEnumerator BossTurn()
    {
        yield return new WaitForSeconds(_turnWaitTime);

        TurnEvents.BossTurnStart();
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
