using Loyufei;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightingGame
{
    [CreateAssetMenu(fileName = "Stats Asset", menuName = "Fighting Game/Character/Stat/Stats Asset")]
    public class StatsAsset : EntityFormAsset<float, Stat>, IEntity<StatsAsset>
    {
        [SerializeField]
        private string     _CharacterId;
        
        public object     Identity => _CharacterId;
        public StatsAsset Data     => this;
    }
}