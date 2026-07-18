using UnityEngine;

public class CardTab : BaseCard
{
    private void OnMouseDown()
    {
        DeckEvents.RemoveFromDeck(CardData);
    }
}
