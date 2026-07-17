using UnityEngine;

public  class CreateCard : MonoBehaviour
{
    public static void CreateSetupCard(GameObject cardPrefab, Transform position, CardData cardData, out GameObject newCard, out Card tempCard)
    {
        newCard = Instantiate(cardPrefab, position);
        tempCard = newCard.GetComponent<Card>();
        tempCard.LoadCardData(cardData);
    }
}
