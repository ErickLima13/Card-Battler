using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject _bossVisual;

    [SerializeField] private int _attackDamage = 5;

    [SerializeField] private Transform _poisonCounterPosition;

    [SerializeField] private Transform _poisonHitPosition;

    [SerializeField] private GameObject _poisonPrefab;

    private Animator _visualAnimator;

    private Health _health;

    private Vector3 _originalPosition;

    public GameObject _poisonCounter;


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
        _health.TakeDamage(cardData.attackPower);
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

    private void Attack()
    {
        CheckPoison();

        if (_health.Dead())
        {
            return;
        }

        StartCoroutine(BossAttackAnimation());
    }

    private IEnumerator BossAttackAnimation()
    {
        if (_health.poisonCount > 0)
        {
            yield return new WaitForSeconds(0.5f);
        }

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

    public void SetPoison(int poison)
    {
        GameObject temp = Instantiate(_poisonPrefab, _poisonHitPosition);
        Destroy(temp, 1f);

        if (_poisonCounter == null)
        {
            _poisonCounter = Instantiate(_poisonPrefab, _poisonCounterPosition);
        }
        else
        {
            _poisonCounter.SetActive(true);
        }

        _health.SetPoison(poison);
    }

    public void CheckPoison()
    {
        if (_health.poisonCount > 0)
        {
            _health.TakeDamage(1);
            Damage();
            _health.poisonCount--;
        }
        else if( _poisonCounter != null) 
        {
            _poisonCounter.SetActive(false);
        }
    }

    private void CheckPoisonEndTurn()
    {
        if (_health.poisonCount <= 0)
        {
            _poisonCounter.SetActive(false);
        }
    }

    private void OnEnable()
    {
        BossEvents.OnBossHit += HandleBossHit;
        TurnEvents.OnBossTurnStart += Attack;
        BossEvents.OnApplyPoison += SetPoison;
        TurnEvents.OnBossTurnEnd += CheckPoisonEndTurn;

    }

    private void OnDisable()
    {
        BossEvents.OnBossHit -= HandleBossHit;
        TurnEvents.OnBossTurnStart -= Attack;
        BossEvents.OnApplyPoison -= SetPoison;
        TurnEvents.OnBossTurnEnd -= CheckPoisonEndTurn;


    }
}
