using UnityEngine;

namespace Assets.Scripts.Rework
{
    [CreateAssetMenu(fileName = "HealEffect", menuName = "Scriptable Objects/CardEffect/HealEffect")]
    public class HealEffect : CardEffect
    {
        public override void Execute(CardContext context)
        {
            context.Source.Heal(value);
            context.BattleManager.GetPlayer().HealVfx();

            PlayerEvents.PlayerHealed();
            PlayerEvents.ActionFinished();
        }
    }
}