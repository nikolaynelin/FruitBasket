using Brightgrove.FruitBasket.Infrastructure;

namespace Brightgrove.FruitBasket.Players
{
    public class RandomPlayer : Player
    {
        public RandomPlayer(string name, IFruitBasketConfig config) : base(name, config) { }

        public override int Guess()
        {
            var guess = NextGuess();
            base.Guess(guess);
            return guess;
        }
    }
}
