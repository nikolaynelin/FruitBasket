using System.Collections.Generic;
using Brightgrove.FruitBasket.Utils;

namespace Brightgrove.FruitBasket.Infrastructure
{
    public abstract class Player
    {
        protected readonly IFruitBasketConfig Config;
        public string Name { get; protected set; }
        public readonly List<int> Guesses = new List<int>();


        public abstract int Guess();

        protected virtual void Guess(int guess)
        {
            Guesses.Add(guess);
        }

        protected virtual int NextGuess()
        {
            return RandomInt.Next(Config.MinFruitBasketWeight, Config.MaxFruitBasketWeight);
        }


        protected Player(string name, IFruitBasketConfig config)
        {
            Check.NotNullOrEmpty(name, nameof(name));
            Check.NotNull(config, nameof(config));
            Name = name;
            Config = config;
        }
    }
}
