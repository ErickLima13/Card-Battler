using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Testes
{
    public class Hero : MonoBehaviour, IEntity
    {
        public AnimationManager animationManager;

        AnimationManager IEntity.animationManager => animationManager;

        public void Attack()
        {
            print("attack");
        }

        public void Hit()
        {
            print("Hit");
        }

        public void Return()
        {
            print("Return");
        }
    }
}

public interface IEntity
{
    void Attack();
    void Return();
    void Hit();

    AnimationManager animationManager { get; }
}

public interface ICommand
{
    UniTask Execute();
}

public abstract class HeroCommand : ICommand
{
    protected readonly IEntity entity;

    protected HeroCommand(IEntity entity)
    {
        this.entity = entity;
    }

    public abstract UniTask Execute();

    public static T Create<T>(IEntity entity) where T : HeroCommand
    {
        return (T)System.Activator.CreateInstance(typeof(T), entity);
    }

}

public class AttackCommand : HeroCommand
{
    public AttackCommand(IEntity entity) : base(entity)
    {
    }

    public override async UniTask Execute()
    {
        entity.Attack();
        await entity.animationManager.AttackAnimation();
        entity.animationManager.IdleAnimation();
    }
}

public class ReturnCommand : HeroCommand
{
    public ReturnCommand(IEntity entity) : base(entity)
    {
    }

    public override async UniTask Execute()
    {
        entity.Return();
        await entity.animationManager.ReturnAnimation();
        entity.animationManager.IdleAnimation();
    }
}

public class HitCommand : HeroCommand
{
    public HitCommand(IEntity entity) : base(entity)
    {
    }

    public override async UniTask Execute()
    {
        entity.Hit();
        await entity.animationManager.HitAnimation();
        entity.animationManager.IdleAnimation();
    }
}

public class CommandInvoker
{
    public async UniTask ExecuteCommand(List<ICommand> commands)
    {
        foreach (ICommand command in commands)
        {
            await command.Execute();
        }
    }
}