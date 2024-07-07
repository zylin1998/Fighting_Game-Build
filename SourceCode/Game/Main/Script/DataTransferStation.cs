using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Loyufei;
using FightingGame.Player;
using FightingGame.System;
using FightingGame.ViewManagement;

namespace FightingGame
{
    [CreateAssetMenu(fileName = "DataTransferStation", menuName = "Fighting Game/DataTransferStation", order = 1)]
    public class DataTransferStation : BindableAsset
    {
        [SerializeField]
        private PlayerSave       _PlayerSave;
        [SerializeField]
        private SystemSave       _SystemSave;
        [SerializeField]
        private QuestInfoAsset   _QuestInfoAsset;
        [SerializeField]
        private ItemAsset        _ItemAsset;
        [SerializeField]
        private SoundEffectClips _SoundEffect;
        [SerializeField]
        private TargetQuest      _TargetQuest = new();
        
        public override void BindToContainer(DiContainer container, object group = null)
        {
            var playerData = _PlayerSave.FetchData();
            var systemData = _SystemSave.FetchData();

            container
                .Bind<SoundEffectClips>()
                .FromInstance(_SoundEffect)
                .AsSingle();
            
            container
                .Bind<TargetQuest>()
                .FromInstance(_TargetQuest)
                .AsSingle();

            #region EntityAsset

            container
                .Bind<IEntityForm>()
                .WithId(GameNounDeclarations.Quest)
                .FromInstance(_QuestInfoAsset)
                .AsCached();

            container
                .Bind<IEntityForm>()
                .WithId(GameNounDeclarations.Inventory)
                .FromInstance(_ItemAsset)
                .AsCached();

            #endregion

            #region SaveData

            container
                .Bind<SaveSystem>()
                .WithId(GameNounDeclarations.Player)
                .FromInstance(_PlayerSave)
                .AsCached();

            container
                .Bind<SaveSystem>()
                .WithId(GameNounDeclarations.System)
                .FromInstance(_SystemSave)
                .AsCached();

            #endregion

            #region RepositoryData

            container
                .Bind<IRepository>()
                .WithId(GameNounDeclarations.Inventory)
                .FromInstance(playerData._Inventory)
                .AsCached();

            container
                .Bind<IRepository>()
                .WithId(GameNounDeclarations.Quest)
                .FromInstance(playerData._Quest)
                .AsCached();

            container
                .Bind<IRepository>()
                .WithId(GameNounDeclarations.Volumn)
                .FromInstance(systemData._Volumn)
                .AsCached();

            #endregion

            #region Structed Data

            container
                .Bind<ScreenData>()
                .WithId(GameNounDeclarations.Screen)
                .FromInstance(systemData._Screen)
                .AsSingle();

            container
                .Bind<LanguageData>()
                .WithId(GameNounDeclarations.Language)
                .FromInstance(systemData._Langguage)
                .AsSingle();

            #endregion

            #region Model

            container
                .Bind<ViewManager>()
                .AsSingle();

            container
                .Bind<LanguageManager>()
                .AsSingle();

            container
                .Bind<LanguageModel>()
                .AsSingle();

            container
                .Bind<QuestFetchModel>()
                .AsSingle();

            container
                .Bind<ScreenModel>()
                .AsSingle();

            container
                .Bind<VolumnModel>()
                .AsSingle();

            container
                .Bind<InventoryModel>()
                .AsSingle();

            container
                .Bind<SaveSystemModel>()
                .AsSingle();

            #endregion

            #region Service

            container
                .Bind<StatService>()
                .AsSingle()
                .NonLazy();

            #endregion
        }
    }

    [Serializable]
    public class TargetQuest 
    {
        [SerializeField]
        private int _Id;

        public int Id 
        { 
            get => _Id; 
            
            set => _Id = value;
        }
    }

    [Serializable]
    public class SoundEffectClips 
    {
        [SerializeField]
        private AudioClip _Select;
        [SerializeField]
        private AudioClip _Click;

        public AudioClip Select => _Select;
        public AudioClip Click  => _Click;
    }
}