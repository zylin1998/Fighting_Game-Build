using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Loyufei;

namespace FightingGame
{
    public class CharacterCollection
    {
        public CharacterCollection() 
        {
            Groups = new();
        }

        public Dictionary<object, CharacterGroup> Groups { get; }

        public void Add(ICharacter character) 
        {
            var mark  = character.Mark;
            var group = Groups.GetorAdd(mark.CharacterType, () => new());

            group.Add(mark, character);
        }

        public bool Remove(Mark mark)
        {
            var group = Groups.GetorAdd(mark.CharacterType, () => new());

            return group.Remove(mark);
        }

        public CharacterGroup GetGroup(object characterType) 
        {
            return Groups.GetorAdd(characterType, () => new());
        }
    }

    public class CharacterGroup 
    {
        public CharacterGroup() 
        {
            Characters = new();
        }

        public Dictionary<Mark, ICharacter> Characters { get; }

        public ICharacter this[Mark mark] => Characters.TryGetValue(mark, out var c) ? c : default;

        public void Add(Mark mark, ICharacter character) 
        {
            Characters.Add(mark, character);
        }

        public bool Remove(Mark mark)
        {
            return Characters.Remove(mark);
        }
    }
}
