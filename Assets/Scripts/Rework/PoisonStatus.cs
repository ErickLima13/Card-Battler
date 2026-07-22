using Cysharp.Threading.Tasks;

public class PoisonStatus : StatusEffect
{
    public PoisonStatus(Unit owner, int stacks)
        : base(owner, stacks)
    {
    }

    public override async UniTask OnTurnStart()
    {
        owner.TakeDamage(stacks);

        RemoveStacks(1);

        await UniTask.CompletedTask;
    }

    public override bool IsFinished => Stacks <= 0;

    public override string Name => "Poison";
}