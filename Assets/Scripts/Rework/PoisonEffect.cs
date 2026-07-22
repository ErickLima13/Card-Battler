using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Rework
{
    [CreateAssetMenu(fileName = "PoisonEffect", menuName = "Scriptable Objects/CardEffect/PoisonEffect")]

    public class PoisonEffect : CardEffect
    {
        public override async UniTask Execute(CardContext context)
        {
            context.BattleManager.GetUnitBoss().StatusManager.AddStatus( new PoisonStatus( context.BattleManager.GetUnitBoss(), value,statusData));

            await context.BattleManager.GetBossView().ApplyPoisonVfx();
        }
    }
}