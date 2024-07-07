using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Zenject;

namespace FightingGame
{
    public interface ICharacter
    {
        public Mark                  Mark         { get; set; }
        public CharacterView         View         { get; }
        public CharacterController   Controller   { get; }
        public CalculateStats        Stats        { get; }
        public CharacterStateMachine StateMachine { get; }
    }

    public class Character : ICharacter
    {
        public Mark                  Mark         { get; set; }  
        public CharacterView         View         { get; set; }
        public CharacterController   Controller   { get; set; }
        public CalculateStats        Stats        { get; set; }
        public CharacterStateMachine StateMachine { get; set; }
    }

    public class CharacterPool : MemoryPool<Mark, Character> 
    {
        protected override void Reinitialize(Mark mark, Character character)
        {
            character.Mark = mark;
        }

        protected override void OnDespawned(Character character)
        {
            character.Mark         = default;
            character.View         = default;
            character.Controller   = default;
            character.Stats        = default;
            character.StateMachine = default;
        }
    }
}
