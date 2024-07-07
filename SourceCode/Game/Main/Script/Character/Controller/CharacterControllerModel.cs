using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Zenject;
using Loyufei;

namespace FightingGame
{
    public class CharacterControllerModel
    {
        public CharacterControllerModel(DiContainer container)
        {
            Container = container;

            _Pools = new();
        }

        public DiContainer Container { get; }

        private Dictionary<object, CharacterControllerPool> _Pools;

        public CharacterController Spawn(Mark mark)
        {
            return GetPool(mark)?.Spawn();
        }

        public void Despawn(Mark mark, CharacterController controller)
        {
            GetPool(mark)?.Despawn(controller);
        }

        protected virtual CharacterControllerPool GetPool(Mark mark)
        {
            var id = mark.CharacterType;

            return _Pools.GetorAdd(id, () => Container.ResolveId<CharacterControllerPool>(id));
        }

    }
}
