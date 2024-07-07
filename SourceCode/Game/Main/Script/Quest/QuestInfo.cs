using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Loyufei;

namespace FightingGame
{
    [CreateAssetMenu(fileName = "QuestInfo", menuName = "Fighting Game/Quest/QuestInfo", order = 1)]
    public class QuestInfo : ScriptableObject
    {
        [SerializeField]
        private QuestTime       _QuestTime;
        [SerializeField]
        private CharacterInfo   _CharacterInfo;
        [SerializeField]
        private EnemyExtraStats _ExtraStats;
        [SerializeField]
        private GameObject      _Enviroment;

        public QuestTime       QuestTime     => _QuestTime;
        public CharacterInfo   CharacterInfo => _CharacterInfo;
        public EnemyExtraStats ExtraStats    => _ExtraStats;
        public GameObject      Enviroment    => _Enviroment;
    }

    [Serializable]
    public class QuestTime
    {
        [SerializeField, Min(1f)]
        private float _LimitTime;
        [SerializeField, Min(1f)]
        private float _Interval;
        [SerializeField, Min(0.1f)]
        private float _SpawnDuration;

        public float LimitTime     => _LimitTime;
        public float Interval      => _Interval;
        public float SpawnDuration => _SpawnDuration;
    }

    [Serializable]
    public class CharacterInfo 
    {
        [Serializable]
        public class CharacterSpawn
        {
            [SerializeField]
            private string _CharacterId;
            [SerializeField]
            private int _LocateIndex;

            public string CharacterId => _CharacterId;
            public int LocateIndex => _LocateIndex;
        }

        [SerializeField]
        private List<Vector3> _SpawnPositions;
        [SerializeField]
        private CharacterSpawn _Player;
        [SerializeField]
        private List<CharacterSpawn> _Enemies;
        [SerializeField]
        private List<CharacterSpawn> _Boss;

        private SpawnInfo CreateInfo(int id, string characterType, CharacterSpawn spawn)
        {
            var mark = new Mark(id, spawn.CharacterId, characterType);
            var posi = new PosiInfo(_SpawnPositions[spawn.LocateIndex], Quaternion.identity);

            return new SpawnInfo(mark, posi);
        }

        public SpawnInfo GetPlayerInfo(int id) 
        {
            var characterType = GameNounDeclarations.Player;

            return CreateInfo(id, characterType, _Player);
        }

        public IEnumerable<SpawnInfo> GetEnemyInfos(int headerId)
        {
            var id            = headerId;
            var characterType = GameNounDeclarations.Enemy;

            return _Enemies.Select(enemy => CreateInfo(id++, characterType, enemy));
        }

        public IEnumerable<SpawnInfo> GetBossInfos(int headerId)
        {
            var id = headerId;
            var characterType = GameNounDeclarations.Boss;

            return _Boss.Select(enemy => CreateInfo(id++, characterType, enemy));
        }
    }

    [Serializable]
    public class EnemyExtraStats 
    {
        [Serializable]
        public class ExtraStats
        {
            [SerializeField]
            private string _EnemyId;
            [SerializeField]
            private List<Stat> _Stats;

            public string     EnemyId => _EnemyId;
            public List<Stat> Stats   => _Stats;
        }

        [SerializeField]
        private List<ExtraStats> _Stats;

        public IEnumerable<Stat> GetStats(string enemyId) 
        {
            var extraStats = _Stats.FirstOrDefault((stats) => stats.EnemyId == enemyId);
            
            return extraStats.IsDefault() ? new Stat[0] : extraStats.Stats;
        }
    }

    public class SpawnInfo 
    {
        public SpawnInfo(Mark mark, PosiInfo posi) 
        {
            Mark = mark;
            Posi = posi;
        }

        public Mark     Mark { get; }
        public PosiInfo Posi { get; }
    }
}