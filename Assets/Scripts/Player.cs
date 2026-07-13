using UnityEngine;

public class Player : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerEvents.OnCardPlayed += HandleCardPlayed;
    }

    private void OnDisable()
    {
        PlayerEvents.OnCardPlayed -= HandleCardPlayed;
    }

    private void HandleCardPlayed(CardData cardData)
    {
        print("handler ran");
    }
}
