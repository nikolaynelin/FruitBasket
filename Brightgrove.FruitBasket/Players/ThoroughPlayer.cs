using Brightgrove.FruitBasket.Infrastructure;

namespace Brightgrove.FruitBasket.Players
{
    public class ThoroughPlayer : Player
    {
        private int _guessNumber;

        public ThoroughPlayer(string name, IFruitBasketConfig config) : base(name, config)
        {
            _guessNumber = Config.MinFruitBasketWeight - 1;
        }

        public override int Guess()
        {
            var guess = NextGuess();
            base.Guess(guess);
            return guess;
        }

        protected override int NextGuess()
        {
            return ++_guessNumber;
        }
    }
}
