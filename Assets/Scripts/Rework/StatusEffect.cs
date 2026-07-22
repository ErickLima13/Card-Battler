using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class StatusEffect
{
    protected readonly Unit owner;
    protected int stacks;

    public abstract string Name { get; }

    public int Stacks => stacks;

    protected StatusEffect(Unit owner, int stacks)
    {
        this.owner = owner;
        this.stacks = stacks;
    }

    public virtual void AddStacks(int amount)
    {
        stacks += amount;
    }

    public virtual void RemoveStacks(int amount)
    {
        stacks -= amount;
    }

    public virtual void OnApply() { }

    public virtual UniTask OnTurnStart()
    {
        return UniTask.CompletedTask;
    }

    public virtual UniTask OnTurnEnd()
    {
        return UniTask.CompletedTask;
    }

    public virtual void OnRemove() { }

    public abstract bool IsFinished { get; }
}
