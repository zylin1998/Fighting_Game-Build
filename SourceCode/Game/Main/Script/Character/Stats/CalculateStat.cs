using Loyufei;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightingGame
{
    public class CalculateStat
    {
        public CalculateStat(Stat stat, Stat other) 
        {
            Reset(new Stat((string)stat.Identity, stat.Data + other.Data));
        }

        public CalculateStat(Stat stat) 
        {
            Reset(stat);
        }

        public string Identity  { get; protected set; }
        public float  Calculate { get; protected set; }
        public float  Standard  { get; protected set; }

        public float Normalized => Calculate / Standard;

        public Variable Decrease(float amount) 
        {
            var delta = Mathf.Clamp(amount, 0, Calculate);

            Calculate -= delta;

            return new Variable(this, amount, delta, amount - delta);
        }

        public Variable Increase(float amount)
        {
            var delta = Mathf.Clamp(amount, 0, Standard - Calculate);

            Calculate += delta;

            return new Variable(this, amount, delta, amount - delta);
        }

        public void Reset(Stat stat) 
        {
            Identity  = (string)stat.Identity;
            Standard  = stat.Data;
            Calculate = Standard;
        }

        public struct Variable 
        {
            public Variable(CalculateStat stat, float amount, float delta, float remain) 
            {
                Identity  = stat.Identity;
                Calculate = stat.Calculate;
                Standard  = stat.Standard;
                Amount    = amount;
                Delta     = delta;
                Remain    = remain;
            }

            public string Identity  { get; }
            public float  Calculate { get; }
            public float  Standard  { get; }
            public float  Amount    { get; }
            public float  Delta     { get; }
            public float  Remain    { get; }
        }
    }
}