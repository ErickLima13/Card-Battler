using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Rework
{
    [CreateAssetMenu(fileName = "DamageEffect", menuName = "Scriptable Objects/CardEffect/DamageEffect")]

    public class DamageEffect : CardEffect
    {
        public override void Execute(CardContext context)
        {
            context.BattleManager.GetPlayer().Attack(context.Card);
        }
    }
}