using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Zenject;
using Loyufei;

namespace FightingGame
{
    public class QuestFetchModel
    {
        private IEntityForm _QuestInfo;
        private IRepository _QuestData;

        [Inject]
        public QuestFetchModel(
            [Inject(Id = GameNounDeclarations.Quest)] IEntityForm entityForm, 
            [Inject(Id = GameNounDeclarations.Quest)] IRepository questData) 
        {
            _QuestInfo = entityForm;
            _QuestData = questData;
        }

        public IEnumerable<(int id, QuestInfo info, bool clear)> GetQuestInfo() 
        {
            var infos = _QuestInfo
                .Select(e => ((int)e.Identity, (QuestInfo)e.Data, (bool)_QuestData.SearchAt(e.Identity).Data));

            return infos;
        }

        public QuestInfo GetQuestInfo(int id) 
        {
            return _QuestInfo[id]?.Data.To<QuestInfo>();
        }
    }
}