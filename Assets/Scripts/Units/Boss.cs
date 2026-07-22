using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;

public class Boss : Unit
{
    [SerializeField] private GameObject _bossVisual;

    [SerializeField] private int _attackDamage = 5;

    private Animator _visualAnimator;

    private Vector3 _originalPosition;


    private void Awake()
    {
        _visualAnimator = _bossVisual.GetComponent<Animator>();
        _health = GetComponent<Health>();
    }

    private void Start()
    {
        _originalPosition = _bossVisual.transform.position;
    }

    private void HandleBossHit(CardData cardData)
    {
        Damage();
    }

    private void Damage()
    {
        _visualAnimator.Play("hitB");

        if (_health.Dead())
        {
            _visualAnimator.Play("deathB");
            BossEvents.BossDeath();
        }
    }

    private async void BossStartTurn()
    {
        await StatusManager.OnTurnStart();

        if (_health.Dead())
        {
            return;
        }

        StartCoroutine(BossAttackAnimation());
    }

    private IEnumerator BossAttackAnimation()
    {
        Vector3 targetPosition = _originalPosition + new Vector3(-4.5f, 0, 0);

        float duration = 0.5f;
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            _bossVisual.transform.position = Vector3.Lerp(_originalPosition, targetPosition, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
            _visualAnimator.Play("attackB");
        }

        PlayerEvents.PlayerHit(_attackDamage);

        yield return new WaitForSeconds(0.5f);

        timeElapsed = 0f;
        while (timeElapsed < duration)
        {
            _bossVisual.transform.position = Vector3.Lerp(targetPosition, _originalPosition, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;

            _visualAnimator.Play("returnB");
        }

        yield return null;
    }

  

    private void OnEnable()
    {
        BossEvents.OnBossHit += HandleBossHit;
        TurnEvents.OnBossTurnStart += BossStartTurn;
    }

    private void OnDisable()
    {
        BossEvents.OnBossHit -= HandleBossHit;
        TurnEvents.OnBossTurnStart -= BossStartTurn;
    }
}
