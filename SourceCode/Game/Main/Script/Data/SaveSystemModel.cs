using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Loyufei;
using Zenject;

namespace FightingGame
{
    public class SaveSystemModel
    {
        public SaveSystemModel(
            [Inject(Id = GameNounDeclarations.Player)] SaveSystem player,
            [Inject(Id = GameNounDeclarations.System)] SaveSystem system) 
        {
            _Datas = new()
            {
                { GameNounDeclarations.Player, player },
                { GameNounDeclarations.System, system }
            };
        }

        public Dictionary<string, SaveSystem> _Datas;

        public void Save(string id) 
        {
            _Datas[id].Save();
        }
    }
}