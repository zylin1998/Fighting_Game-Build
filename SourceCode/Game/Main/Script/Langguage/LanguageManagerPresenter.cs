using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Loyufei;
using Loyufei.DomainEvents;
using FightingGame.System;

namespace FightingGame
{
    public class LanguageManagerPresenter : AggregateRoot
    {
        public LanguageManagerPresenter(LanguageModel model, LanguageManager manager, DomainEventService service, IEnumerable<ILanguageGroup> groups) 
            : base(service) 
        {
            _Model   = model;
            _Manager = manager;

            service.Register<SetLangguageEvent>  (SetLangguage, GroupId);
            service.Register<FetchLangguageEvent>(FetchData   , GroupId);
            service.Register<TextsRegisterEvent> (Register    , GroupId);
            
            groups.ForEach(g => g.LanguageTexts.ForEach(t => _Manager.Register(t)));

            _Manager.SetLangguage(_Model.FetchData());
        }

        private LanguageModel   _Model;
        private LanguageManager _Manager;

        public virtual object GroupId { get; }

        public void SetLangguage(SetLangguageEvent set) 
        {
            _Model  .SetLangguage(set.Langguage);
            _Manager.SetLangguage(set.Langguage);

            this.SettleEvents(GroupId, new SaveEvent(GameNounDeclarations.System));
        }

        public void FetchData(FetchLangguageEvent fetch) 
        {
            fetch.Response?.Invoke(_Model.FetchData());
        }

        public void FetchText(FetchTextsEvent fetch)
        {
            fetch.Response?.Invoke(_Manager.FetchTexts(_Model.FetchData(), fetch.TextId.ToArray()));
        }

        public void Register(TextsRegisterEvent register) 
        {
            register.Texts.ForEach(t => _Manager.Register(t));
            
            _Manager.SetLangguage(_Model.FetchData());
        }
    }
 
    public class SetLangguageEvent : DomainEventBase 
    {
        public SetLangguageEvent(SystemLanguage language) 
        {
            Langguage = language;
        }

        public SystemLanguage Langguage { get; }
    }

    public class TextsRegisterEvent : DomainEventBase 
    {
        public TextsRegisterEvent(IEnumerable<ILanguageText> texts)
        {
            Texts = texts;
        }

        public IEnumerable<ILanguageText> Texts { get; }
    }

    public class FetchLangguageEvent : DomainEventBase 
    {
        public FetchLangguageEvent(Action<SystemLanguage> response)
        {
            Response = response;
        }

        public Action<SystemLanguage> Response { get; }
    }

    public class FetchTextsEvent : DomainEventBase
    {
        public FetchTextsEvent(IEnumerable<int> textId, Action<(TMP_FontAsset font, IEnumerable<(int id, string text)>)> response)
        {
            Response = response;
            TextId   = textId;
        }

        public IEnumerable<int>                                                 TextId   { get; }
        public Action<(TMP_FontAsset font, IEnumerable<(int id, string text)>)> Response { get; }
    }
}