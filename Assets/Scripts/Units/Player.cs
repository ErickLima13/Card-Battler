using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _playerVisual;

    private Vector3 _originalPosition;

    private Animator _visualAnimator;

    private Health _health;

    private ParticleSystem _healVfx;

    private void Awake()
    {
        _visualAnimator = _playerVisual.GetComponent<Animator>();
        _health = GetComponent<Health>();
        _healVfx = _playerVisual.GetComponentInChildren<ParticleSystem>();
    }

    private void Start()
    {
        _originalPosition = _playerVisual.transform.position;
    }

    private void HandleCardPlayed(CardData cardData)
    {
        if (cardData.attackPower > 0)
        {
            Attack(cardData);
        }

        if(cardData.healPower  > 0)
        {
            Heal(cardData);
        }
    }

    private void Heal(CardData cardData)
    {
        print("heal " + cardData.healPower);
        _health.HealDamage(cardData.healPower);
        _healVfx.Play();
        PlayerEvents.PlayerHealed();
    }

    private void Attack(CardData cardData)
    {
        StartCoroutine(PlayerAttackAnimation(cardData));
    }

    private IEnumerator PlayerAttackAnimation(CardData cardData)
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

        BossEvents.BossHit(cardData);

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

    private void PlayerHit(int amount)
    {
        _health.TakeDamage(amount);
        _visualAnimator.Play("hitPlayer");

        if (_health.Dead())
        {
            _visualAnimator.Play("deathPlayer");
            PlayerEvents.PlayerDeath();
        }
    }

    private void OnEnable()
    {
        PlayerEvents.OnCardPlayed += HandleCardPlayed;
        PlayerEvents.OnPlayerHit += PlayerHit;
    }

    private void OnDisable()
    {
        PlayerEvents.OnCardPlayed -= HandleCardPlayed;
        PlayerEvents.OnPlayerHit -= PlayerHit;

    }
}
