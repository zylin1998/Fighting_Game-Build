using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;
using Loyufei;
using FightingGame.Event;
using FightingGame.ViewManagement;

namespace FightingGame
{
    [RequireComponent(typeof(CanvasGroup))]
    public class MenuBase : ViewMono, ILanguageGroup, ISelectableGroup
    {
        [SerializeField]
        protected CanvasGroup _CanvasGroup;
        [SerializeField, Range(0f, 1f)]
        protected float       _FadeDuration = 0.5f;

        public override object ViewId { get; }

        private Selectable _First;
        private Selectable _LastSelect;

        public IEnumerable<Selectable> Selectables 
            => GetComponentsInChildren<Selectable>();

        public virtual IEnumerable<ILanguageText> LanguageTexts
            => GetComponentsInChildren<ILanguageText>();

        public Selectable First => _First;

        public Selectable LastSelect
        {
            get => _LastSelect;

            set => _LastSelect = value;
        }

        protected override void Awake()
        {
            base.Awake();

            _First = Selectables.FirstOrDefault();
        }

        public override Tween Open()
        {
            _CanvasGroup.alpha = 0;

            Observable
                .Timer(TimeSpan.FromSeconds(_FadeDuration))
                .Subscribe(t => _First?.Select());

            return _CanvasGroup.DOFade(1f, _FadeDuration);
        }

        public override Tween Close()
        {
            _CanvasGroup.alpha = 1;

            return _CanvasGroup.DOFade(0f, _FadeDuration);
        }

        public override IEnumerator<IListenerAdapter> GetEnumerator()
        {
            return GetComponentsInChildren<IListenerAdapter>()
                    .To<IEnumerable<IListenerAdapter>>()
                    .GetEnumerator();
        }
    }
}