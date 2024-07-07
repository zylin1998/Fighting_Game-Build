using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace FightingGame
{
    public class StatsManageModel
    {
        public StatsManageModel(CharacterGroup group) 
        {
            Group = group;
        }

        public CharacterGroup Group;

        public (CalculateStat.Variable variable, ICharacter character) Increase(Mark target, string statName, float delta) 
        {
            var character = Group[target];

            return (character.Stats.Increase(statName, delta), character);
        }

        public (CalculateStat.Variable variable, ICharacter character) Decrease(Mark target, string statName, float delta)
        {
            var character = Group[target];
            
            return (character.Stats.Decrease(statName, delta), character);
        }
    }
}
