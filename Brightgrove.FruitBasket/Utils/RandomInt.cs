using System;

namespace Brightgrove.FruitBasket.Utils
{
    public static class RandomInt
    {
        private static readonly Random Rand = new Random();
        public static int Next(int min, int max)
        {
            return Rand.Next(min, max);
        }
    }
}
