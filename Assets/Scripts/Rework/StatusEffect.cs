using Cysharp.Threading.Tasks;

public abstract class StatusEffect
{
    protected readonly Unit owner;

    public int stacks;

    protected StatusEffect(Unit owner)
    {
        this.owner = owner;
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

    public virtual void Merge(StatusEffect other) { }

    public abstract bool IsFinished { get; }
}