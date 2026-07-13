using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _playerVisual;

    private Vector3 _originalPosition;

    private Animator _visualAnimator;

    private void Awake()
    {
        _visualAnimator = _playerVisual.GetComponent<Animator>();
    }

    private void Start()
    {
        _originalPosition = _playerVisual.transform.position;
    }

    private void HandleCardPlayed(CardData cardData)
    {
        print("handler ran");

        if (cardData.attackPower > 0)
        {
            Attack(cardData);
        }
    }

    private void Attack(CardData cardData)
    {
        print("ATTACK");
        StartCoroutine(PlayerAttackAnimation());
    }

    private IEnumerator PlayerAttackAnimation()
    {
        Vector3 targetPosition = _originalPosition + new Vector3(4, 0, 0);

        float duration = 0.5f;
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            _playerVisual.transform.position = Vector3.Lerp(_originalPosition, targetPosition, timeElapsed / duration);
            timeElapsed += Time.deltaTime;


            yield return null;

            _visualAnimator.Play("attackPlayer");
        }

        yield return new WaitForSeconds(0.5f);

        timeElapsed = 0f;
        while (timeElapsed < duration)
        {
            _playerVisual.transform.position = Vector3.Lerp(targetPosition, _originalPosition, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;

            _visualAnimator.Play("returnPlayer");
        }

        yield return null;
    }

    private void OnEnable()
    {
        PlayerEvents.OnCardPlayed += HandleCardPlayed;
    }

    private void OnDisable()
    {
        PlayerEvents.OnCardPlayed -= HandleCardPlayed;
    }
}
