using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject _bossVisual;

    private Animator _visualAnimator;

    private Health _health;



    private void Awake()
    {
        _visualAnimator = _bossVisual.GetComponent<Animator>();
        _health = GetComponent<Health>();
    }

    private void HandleBossHit(CardData cardData)
    {
        print("boss hit");
        _health.TakeDamage(cardData.attackPower);
        _visualAnimator.Play("hitB");

        if (_health.IsAlive)
        {
            _visualAnimator.Play("deathB");
        }  
    }


    private void OnEnable()
    {
        BossEvents.OnBossHit += HandleBossHit;
    }

    private void OnDisable()
    {
        BossEvents.OnBossHit -= HandleBossHit;

    }
}
