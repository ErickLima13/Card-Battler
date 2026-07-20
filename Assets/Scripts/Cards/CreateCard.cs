using UnityEngine;

public  class CreateCard : MonoBehaviour
{
    public static void CreateSetupCard(GameObject cardPrefab, Transform position, CardData cardData, out GameObject newCard, out BaseCard tempCard)
    {
        newCard = Instantiate(cardPrefab, position);
        tempCard = newCard.GetComponent<BaseCard>();
        tempCard.LoadCardData(cardData);
    }
}
