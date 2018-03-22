using System.Collections.Generic;

namespace Brightgrove.FruitBasket.Infrastructure
{
    public abstract class DishonestPlayer : Player
    {
        private readonly HashSet<int> _uniqueGuessesOfAllPlayers = new HashSet<int>();

        protected DishonestPlayer(string name, IFruitBasketConfig config) : base(name, config)
        {
        }

        public void Cheat(object responder, FruitBasketGameEventArgs e)
        {
            _uniqueGuessesOfAllPlayers.Add(e.Guess);
        }

        public override int Guess()
        {
            var guess = NextGuess();
            while (_uniqueGuessesOfAllPlayers.Contains(guess))
            {
                guess = NextGuess();
            }
            base.Guess(guess);
            return guess;
        }
    }
}
