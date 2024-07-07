using Loyufei;
using System.Collections;
using System.Collections.Generic;

namespace FightingGame
{
    public class CharacterModel
    {
        public CharacterModel(CharacterPool pool)
        {
            _Pool = pool;
        }

        protected CharacterPool _Pool;

        public virtual Character Spawn(Mark mark) 
        {
            return _Pool.Spawn(mark);
        }

        public virtual void Despawn(Character character) 
        {
            _Pool.Despawn(character);
        }
    }
}