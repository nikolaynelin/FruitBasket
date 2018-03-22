using Brightgrove.FruitBasket.Infrastructure;

namespace Brightgrove.FruitBasket.Players
{
    public class MemoryPlayer : Player
    {
        public MemoryPlayer(string name, IFruitBasketConfig config) : base(name, config) { }

        public override int Guess()
        {
            var guess = NextGuess();

            while (Guesses.Contains(guess))
            {
                guess = NextGuess();
            }
            base.Guess(guess);
            return guess;
        }
    }
}
