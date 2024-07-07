using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightingGame
{
    [CreateAssetMenu(fileName = "HealthTalent", menuName = "Fighting Game/Inventory/Item/HealthTalent", order = 1)]
    public class HealthTalent : Talent
    {
        public override int Limit => ItemLimit.Health;
        public override string Describe => string.Format(_DecribeFormat, TargetProperty, Delta);
    }
}