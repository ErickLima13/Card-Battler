using UnityEngine;

namespace Assets.Scripts.Rework
{
    [CreateAssetMenu(fileName = "PoisonEffect", menuName = "Scriptable Objects/CardEffect/PoisonEffect")]

    public class PoisonEffect : CardEffect
    {
        public override void Execute(CardContext context)
        {
            context.BattleManager.GetBoss().StatusManager.AddStatus( new PoisonStatus( context.BattleManager.GetBoss(), value));
            PlayerEvents.ActionFinished();
        }
    }
}