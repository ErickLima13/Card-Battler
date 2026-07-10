using UnityEngine;

public class PlayZoneTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Card card))
        {
            print("CARD enter");
        }
                    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Card card))
        {
            print("CARD left");
        }
    }
}
