using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class AudioManager : Singleton<AudioManager>
{

    private AudioSource _audioSource;

    [SerializeField] private AudioClip _playCard;
    [SerializeField] private AudioClip _drawCard;
    [SerializeField] private AudioClip _reshufleCard;

    [SerializeField] private AudioClip _bossDeath;
    [SerializeField] private AudioClip _playerDeath;
    [SerializeField] private AudioClip _playerHealed;

    [SerializeField] private AudioClip _playerAttack;
    [SerializeField] private AudioClip _bossAttack;


    protected override void Awake()
    {
        base.Awake();
        _audioSource = GetComponent<AudioSource>();
    }

    private void PlayAudioClip(AudioClip clip)
    {
        if(clip != null)
        {
            _audioSource.PlayOneShot(clip);
        }      
    }

    private void PlayCardSfx(CardData _)
    {
        PlayAudioClip(_playCard);
    }

    private void DrawCardSfx()
    {
        PlayAudioClip(_drawCard);
    }

    private void ReshufleCardSfx()
    {
        PlayAudioClip(_reshufleCard);
    }
    
    private void PlayerDeath()
    {
        PlayAudioClip(_playerDeath);
    }

    private void PlayerHealed()
    {
        PlayAudioClip(_playerHealed);
    }

    private void BossDeath()
    {
        PlayAudioClip(_bossDeath);
    }

    private void PlayerAttack(CardData _)
    {
        PlayAudioClip(_playerAttack);
    }

    private void BossAttack(int _)
    {
        PlayAudioClip(_bossAttack);
    }



    private void OnEnable()
    {
        PlayerEvents.OnCardPlayed += PlayCardSfx;
        PlayerEvents.OnDrawCardRequested += DrawCardSfx;
        PlayerEvents.OnReshufleResquested += ReshufleCardSfx;
        PlayerEvents.OnPlayerHit += BossAttack;
        PlayerEvents.OnPlayerDeath += PlayerDeath;
        PlayerEvents.OnPlayerHealed += PlayerHealed;


        BossEvents.OnBossHit += PlayerAttack;
        BossEvents.OnBossDeath += BossDeath;

    }

    private void OnDisable()
    {
        PlayerEvents.OnCardPlayed -= PlayCardSfx;
        PlayerEvents.OnDrawCardRequested -= DrawCardSfx;
        PlayerEvents.OnReshufleResquested -= ReshufleCardSfx;
        PlayerEvents.OnPlayerHit -= BossAttack;
        PlayerEvents.OnPlayerDeath -= PlayerDeath;
        PlayerEvents.OnPlayerHealed -= PlayerHealed;

        BossEvents.OnBossHit -= PlayerAttack;
        BossEvents.OnBossDeath -= BossDeath;
    }

}
