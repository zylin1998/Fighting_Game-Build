using Loyufei;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace FightingGame
{
    [CreateAssetMenu(fileName = "Stats Assets", menuName = "Fighting Game/Character/Stat/Stats Assets")]
    public class StatsAssets : BindableEntityFormAsset<StatsAsset, StatsAsset>
    {
        public override void BindToContainer(DiContainer container, object group = null)
        {
            container
                .Bind<IEntityForm>()
                .WithId(GameNounDeclarations.Character)
                .FromInstance(this)
                .AsSingle();

            container
                .BindMemoryPool<CalculateStats, CalculateStatsPool>()
                .WithId(GameNounDeclarations.Character)
                .AsCached();
        }
    }
}