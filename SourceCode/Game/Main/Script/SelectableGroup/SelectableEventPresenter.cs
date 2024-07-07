using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Loyufei;
using Loyufei.DomainEvents;
using UniRx;
using UniRx.Triggers;
using FightingGame.System;
using UnityEngine.EventSystems;

namespace FightingGame
{
    public class SelectableEventPresenter : AggregateRoot
    {
        public SelectableEventPresenter(IEnumerable<ISelectableGroup> groups, SoundEffectClips soundEffect, DomainEventService service) 
            : base(service) 
        {
            _Groups = groups.ToList();

            _SoundEffect = soundEffect;

            Subscribing();
        }

        public virtual object GroupId { get; }

        private List<ISelectableGroup> _Groups;

        private SoundEffectClips _SoundEffect;

        private void Subscribing() 
        {
            _Groups.ForEach(group =>
            {
                var selectables = group.Selectables;

                selectables.ForEach(selectable =>
                {
                    selectable
                        .OnPointerEnterAsObservable()
                        .Subscribe((data) => selectable.Select());
                    selectable
                        .OnSelectAsObservable()
                        .Subscribe((data) =>
                        {
                            Select(data);

                            group.LastSelect = selectable;
                        });
                    selectable
                        .OnPointerClickAsObservable()
                        .Subscribe((data) => { if (selectable.interactable) Click(data); });
                    selectable
                        .OnSubmitAsObservable()
                        .Subscribe((data) => { if (selectable.interactable) Click(data); });
                });
            });
        }

        protected virtual void Select(BaseEventData data) 
        {
            this.SettleEvents(GroupId, new PlayAudioClipEvent(2, _SoundEffect.Select, EPlayMode.OneShot));
        }

        protected virtual void Click(BaseEventData data)
        {
            this.SettleEvents(GroupId, new PlayAudioClipEvent(2, _SoundEffect.Click, EPlayMode.OneShot));
        }
    }
}
