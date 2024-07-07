using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightingGame
{
    [CreateAssetMenu(fileName = "Currency", menuName = "Fighting Game/Inventory/Item/Currency", order = 1)]
    public class Currency : Item
    {
        public override int    Limit => 0;
        public override string Describe => string.Format(_DecribeFormat);
    }
}