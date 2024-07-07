using Loyufei;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FightingGame.Player
{
    [Serializable]
    public class QuestData : DataRepository<bool> 
    {
        public QuestData(int capacity) : base(capacity) 
        { 
        
        }

        protected override Reposit DefaultReposit(int id)
        {
            return new Reposit(id, false);
        }
    }
}