using Loyufei;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightingGame
{
    [CreateAssetMenu(fileName = "Item Asset", menuName = "Fighting Game/Inventory/Items", order = 1)]
    public class ItemAsset : EntityFormAsset<Item, Entity<int, Item>>
    {

    }
}