using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Loyufei.DomainEvents;
using UniRx.Triggers;
using Loyufei;
using UniRx;
using UnityEngine;
using FightingGame.System;

namespace FightingGame
{
    public class CharacterPresenter : AggregateRoot
    {
        public CharacterPresenter(CharacterCollection collection, CharacterModel model, DomainEventService service) : base(service)
        {
            _Model      = model;
            _Collection = collection;

            service.Register<SpawnCharacter>  (Spawn  , CharacterType);
            service.Register<DespawnCharacter>(Despawn, CharacterType);
        }

        private CharacterModel      _Model;
        private CharacterCollection _Collection;

        public virtual object GroupId       { get; }
        public virtual object CharacterType { get; }

        public void Spawn(SpawnCharacter spawn) 
        {
            var mark      = spawn.Mark;
            var posi      = spawn.Posi;
            var character = _Model.Spawn(mark);
            
            ResolveView        (character, posi);
            ResolveController  (character);
            ResolveStats       (character);
            ResolveStateMachine(character);
            ResolveStates      (character);
            
            CharacterInteract  (character);
            
            _Collection.Add(character);

            spawn.Response?.Invoke(character);
        }

        public virtual void Despawn(DespawnCharacter spawn)
        {
            var character = spawn.Character;
            var mark      = character.Mark;

            _Collection.Remove(mark);

            var events = new IDomainEvent[]
            {
                new CharacterViewDespawnEvent      (mark, character.View),
                new CharacterControllerDespawnEvent(mark, character.Controller),
                new StatsDespawnEvent              (mark, character.Stats),
                new ReleaseStateEvent       (character.StateMachine.States),
                new StateMachineDespawnEvent(character.StateMachine),
            };

            this.SettleEvents(GroupId, events);
        }

        #region Internal Resolve

        protected virtual void ResolveView(Character character, PosiInfo posi) 
        {
            this.SettleEvents(GroupId, new CharacterViewSpawnEvent(character.Mark, posi, (view) =>
            {
                character.View = view;
            }));
        }

        protected virtual void ResolveController(Character character) 
        {
            this.SettleEvents(GroupId, new CharacterControllerSpawnEvent(character.Mark, (controller) =>
            {
                character.Controller = controller;
            }));
        }

        protected virtual void ResolveStats(Character character)
        {
            var mark = character.Mark;

            this.SettleEvents(GroupId, new StatsSpawnEvent(mark, GetOtherStats(mark), (stats) =>
            {
                character.Stats = stats;
            }));
        }

        protected virtual void ResolveStateMachine(Character character) 
        {
            this.SettleEvents(GroupId, new StateMachineSpawnEvent((machine) =>
            {
                character.StateMachine = machine;
            }));
        }

        protected virtual void ResolveStates(Character character) 
        {
            var machine = character.StateMachine;
            var mark    = character.Mark;
            
            this.SettleEvents(GroupId, new FetchStateEvent(mark, (states) =>
            {
                states.ForEach(state => state.To<ISetup>().Setup(character));
            }));

            machine.SetState(machine.States.FirstOrDefault(s => s.Id == 0));
        }

        protected virtual IEnumerable<Stat> GetOtherStats(Mark mark) 
        {
            return new Stat[0];
        }

        #endregion

        #region Interact

        public virtual void CharacterInteract(Character character)
        {
            /*character.StateMachine.States
                .OfType<IAttack>()
                .ForEach(attack => attack.AttackView
                    .OnTriggerEnter2DAsObservable()
                    .Subscribe(c =>
                    {
                        AttackEvent(attack.AttackView, c);
                    }));*/
        }

        protected virtual void AttackEvent(AttackView attack, Collider2D collider)
        {
            var info  = attack.AttackInfo(collider);
            var sound = attack.SoundInfo;

            if (!info.target.IsDefault())
            {
                var from     = info.from;
                var target   = info.target;
                var statName = info.statName;
                var damage   = info.damage;
                var type     = target.CharacterType;
                var force    = info.force.x < 0 ? 1 : -1;
                
                collider.attachedRigidbody.AddForce(new Vector2(force * 1.7f, 0), ForceMode2D.Impulse);

                var stats = new StatDecreaseEvent(from, target, statName, damage);
                var sfx   = new PlayAudioClipEvent(sound.id, sound.clip, sound.playMode);
                
                this.SettleEvents(type   , stats);
                this.SettleEvents(GroupId, sfx);
            }
        }

        #endregion
    }

    public class SpawnCharacter : DomainEventBase 
    {
        public SpawnCharacter(Mark mark, PosiInfo posi, Action<ICharacter> response) 
        {
            Mark     = mark;
            Posi     = posi;    
            Response = response;
        }

        public Mark               Mark     { get; }
        public PosiInfo           Posi     { get; }
        public Action<ICharacter> Response { get; }
    }

    public class DespawnCharacter : DomainEventBase
    {
        public DespawnCharacter(ICharacter character)
        {
            Character = character;
        }

        public ICharacter Character { get; }
    }

    public class FetchCharacterEvent : DomainEventBase 
    {
        public FetchCharacterEvent(Action<IEnumerable<ICharacter>> response)
        {
            Response = response;
        }

        public Action<IEnumerable<ICharacter>> Response { get; }
    }
}
