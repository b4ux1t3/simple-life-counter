using System;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace lifeCounter
{
    class Player
    {
        public int lifeTotal { get; protected set; }
        public int poisonCounters { get; protected set; }
        public int experienceCounters { get; protected set; }

        public Player()
        {
            lifeTotal = 20;
            poisonCounters = 0;
            experienceCounters = 0;
        }

        public Player(int life)
        {
            lifeTotal = life;
            poisonCounters = 0;
            experienceCounters = 0;
        }

        public void IncrementLife()
        {
            lifeTotal++;
        }

        public void IncrementLife(int amount)
        {
            lifeTotal += amount;
        }

        public void DecrementLife()
        {
            lifeTotal--;
        }

        public void DecrementLife(int amount)
        {
            lifeTotal-= amount;
        }

        public void SetLife(int newTotal)
        {
            lifeTotal = newTotal;
        }
        
    }
}