using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Rework
{
    [CreateAssetMenu(fileName = "DamageEffect", menuName = "Scriptable Objects/CardEffect/DamageEffect")]

    public class DamageEffect : CardEffect
    {
        public override void Execute(CardContext context)
        {
            DelayAction(context).Forget();
        }

        private async UniTaskVoid DelayAction(CardContext context)
        {
            context.BattleManager.GetPlayer().Attack(context.Card);

           await UniTask.WaitForSeconds(0.5f);

            context.BattleManager.GetBoss().TakeDamage(value);
            BossEvents.BossHit(context.Card);

            await UniTask.WaitForSeconds(0.5f);

            context.BattleManager.GetPlayer().Return();
            PlayerEvents.ActionFinished();
        }
    }
}