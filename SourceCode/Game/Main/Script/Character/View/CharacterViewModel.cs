using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Loyufei;

namespace FightingGame
{
    public class CharacterViewModel
    {
        public CharacterViewModel(DiContainer container)
        {
            _Container = container;

            _Pools = new();
            _Roots = new();
        }

        private DiContainer _Container;

        private Dictionary<object, CharacterViewPool> _Pools;
        private Dictionary<object, Transform>         _Roots;

        public CharacterView Spawn(Mark mark, PosiInfo posi)
        {
            var view = GetPool(mark)?.Spawn(mark, posi);

            view.transform.SetParent(GetRoot(mark.CharacterType));
            view.gameObject.layer = LayerMask.NameToLayer(mark.CharacterType);

            return view;
        }

        public void Despawn(Mark mark, CharacterView view)
        {
            view.transform.SetParent(GetRoot(string.Format("{0} Pool", mark.CharacterId)));

            GetPool(mark)?.Despawn(view);
        }

        protected virtual CharacterViewPool GetPool(Mark mark)
        {
            var id = mark.CharacterId;

            return _Pools.GetorAdd(id, () => _Container.ResolveId<CharacterViewPool>(id));
        }

        protected virtual Transform GetRoot(string rootName) 
        {
            return _Roots.GetorAdd(rootName, () => new GameObject(rootName).transform);
        }
    }
}
