using Assets.Scripts.Rework;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField]
    protected Health _health;

    [SerializeField]
    protected BattleManager _battleManager;

    [SerializeField]
    protected StatusManager _statusManager;

    public StatusManager StatusManager => _statusManager;
    public BattleManager BattleManager => _battleManager;



    public virtual void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);
    }

    public virtual void Heal(int amount)
    {
        _health.Heal(amount);
    }

    public Health Health => _health;
}