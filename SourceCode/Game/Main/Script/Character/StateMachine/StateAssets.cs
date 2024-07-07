using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Loyufei;

namespace FightingGame
{
    [CreateAssetMenu(fileName = "State Assets", menuName = "Fighting Game/Character/State/State Assets", order = 1)]
    public class StateAssets : BindableEntityFormAsset<StateMachineSetting, StateMachineSetting>
    {
        public override void BindToContainer(DiContainer container, object group = null)
        {
            _Entities.ForEach(setting =>
            {
                var id = setting.Identity;

                container.BindMemoryPool<CharacterView, CharacterViewPool>()
                    .WithId(id)
                    .FromComponentInNewPrefab(setting.Prefab)
                    .AsCached();

                setting.Data.States.ForEach(state =>
                {
                    container
                        .Bind<State>()
                        .WithId(id)
                        .To(state.GetType())
                        .FromNewScriptableObject(state)
                        .AsTransient();
                });
            });
        }
    }
}