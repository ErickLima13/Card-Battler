using UnityEngine;

namespace Assets.Scripts.Rework
{
    public class BattleManager : MonoBehaviour
    {

        [SerializeField] private Player _player;
        [SerializeField] private Unit _boss;

        public Player GetPlayer()
        {
            return _player;
        }

        public Unit GetBoss()
        {
            return _boss;
        }
    }
}