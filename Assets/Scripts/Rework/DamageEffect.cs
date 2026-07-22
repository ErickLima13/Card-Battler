using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Rework
{
    [CreateAssetMenu(fileName = "DamageEffect", menuName = "Scriptable Objects/CardEffect/DamageEffect")]

    public class DamageEffect : CardEffect
    {
        public override async UniTask Execute(CardContext context)
        {
            await context.BattleManager.GetPlayer().Attack();

            context.BattleManager.GetUnitBoss().TakeDamage(value);
            BossEvents.BossHit(context.Card);

            await UniTask.WaitForSeconds(0.5f);

            await context.BattleManager.GetPlayer().Return();
        }
    }
}