using Cysharp.Threading.Tasks;
using UnityEngine;

public class Player : Unit
{
    [SerializeField] private GameObject _playerVisual;

    private Vector3 _originalPosition;

    private Animator _visualAnimator;

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

    private async void HandleCardPlayed(CardData cardData)
    {
        CardContext context = new CardContext(this, cardData, _battleManager);

        foreach (CardEffect cardEffect in cardData.effects)
        {
            await cardEffect.Execute(context);
        }

        PlayerEvents.ActionFinished();
    }

    public void HealVfx()
    {
        _healVfx.Play();
    }

    public UniTask Attack() => AttackAnimation();

    public UniTask Return() => ReturnAnimation();

    private async UniTask AttackAnimation()
    {
        Vector3 targetPosition = _originalPosition + new Vector3(4, 0, 0);

        float duration = 0.5f;
        float timeElapsed = 0f;

        _visualAnimator.Play("attackPlayer");

        while (timeElapsed < duration)
        {
            _playerVisual.transform.position = Vector3.Lerp(_originalPosition, targetPosition, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            await UniTask.Yield();

        }

        await UniTask.Yield();
    }

    private async UniTask ReturnAnimation()
    {
        Vector3 targetPosition = _originalPosition + new Vector3(4, 0, 0);

        float duration = 0.5f;
        float timeElapsed = 0f;

        timeElapsed = 0f;

        _visualAnimator.Play("returnPlayer");

        while (timeElapsed < duration)
        {
            _playerVisual.transform.position = Vector3.Lerp(targetPosition, _originalPosition, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            await UniTask.Yield();
        }

        await UniTask.Yield();
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
