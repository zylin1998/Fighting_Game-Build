using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightingGame
{
    [CreateAssetMenu(fileName = "AttackTalent", menuName = "Fighting Game/Inventory/Item/AttackTalent", order = 1)]
    public class AttackTalent : Talent
    {
        public override int Limit => ItemLimit.Attack;
        public override string Describe => string.Format(_DecribeFormat, TargetProperty, Delta);
    }
}