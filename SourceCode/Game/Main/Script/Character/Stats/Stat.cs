using Loyufei;
using System;
using System.Collections;
using System.Collections.Generic;

namespace FightingGame
{
    [Serializable]
    public class Stat : Entity<string, float>
    {
        public Stat(string identity, float data) : base(identity, data)
        {

        }
    }
}