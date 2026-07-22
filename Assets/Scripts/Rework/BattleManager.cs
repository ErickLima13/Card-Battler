using UnityEngine;

namespace Assets.Scripts.Rework
{
    public class BattleManager : MonoBehaviour
    {

        [SerializeField] private Player _player;
        [SerializeField] private Unit _unitBoss;

        [SerializeField] private Boss _boss;
        [SerializeField] private BossView _bossView;

        public Player GetPlayer()
        {
            return _player;
        }

        public Unit GetUnitBoss()
        {
            return _boss;
        }

        public Boss GetBoss() => _boss;

        public BossView GetBossView() => _bossView;


    }
}