using Cysharp.Threading.Tasks;

public class PoisonStatus : StatusEffect
{
    public PoisonStatus(Unit owner, int stacks)
        : base(owner)
    {
        this.stacks = stacks;
    }

    public override async UniTask OnTurnStart()
    {
        owner.TakeDamage(stacks);

        stacks--;

        await UniTask.CompletedTask;
    }

    public override void Merge(StatusEffect other)
    {
        PoisonStatus poison = (PoisonStatus)other;

        stacks += poison.stacks;
    }

    public override bool IsFinished => stacks <= 0;
}