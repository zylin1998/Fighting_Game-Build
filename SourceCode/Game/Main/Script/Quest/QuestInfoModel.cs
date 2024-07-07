using Loyufei;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using static UnityEngine.GraphicsBuffer;

namespace FightingGame
{
    public class QuestInfoModel
    {
        public QuestInfoModel(
            [Inject(Id = GameNounDeclarations.Quest)]
            IEntityForm asset,
            [Inject(Id = GameNounDeclarations.Quest)]
            IRepository repository,
            TargetQuest target)
        {
            Assets     = asset;
            Repository = repository;
            Target     = target;

            _QuestInfo = asset[target.Id].Data.To<QuestInfo>();
            _QuestData = repository.SearchAt(target.Id);
        }

        private QuestInfo _QuestInfo;
        private IReposit  _QuestData;

        public QuestTime       QuestTime       => _QuestInfo?.QuestTime;
        public CharacterInfo   CharacterInfo   => _QuestInfo?.CharacterInfo;
        public EnemyExtraStats EnemyExtraStats => _QuestInfo?.ExtraStats;
        public GameObject      Enviroment      => _QuestInfo?.Enviroment;

        public IEntityForm Assets     { get; }
        public IRepository Repository { get; }
        public TargetQuest Target     { get; }

        public bool HasNextQuest() 
        {
            var id     = Target.Id + 1;
            
            if (Assets[id]?.Data.To<QuestInfo>() == default) 
                return false;
            if (Repository.SearchAt(id) == default) 
                return false;

            return true;
        }

        public void Clear() 
        {
            _QuestData.Preserve(true);
        }
    }
}