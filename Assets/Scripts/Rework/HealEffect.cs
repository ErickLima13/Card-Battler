using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Rework
{
    [CreateAssetMenu(fileName = "HealEffect", menuName = "Scriptable Objects/CardEffect/HealEffect")]
    public class HealEffect : CardEffect
    {
        public override async UniTask Execute(CardContext context)
        {
            context.BattleManager.GetPlayer().HealVfx();
            context.Source.Heal(value);

            PlayerEvents.PlayerHealed();
        }
    }
}