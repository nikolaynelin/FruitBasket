using Brightgrove.FruitBasket.Infrastructure;

namespace Brightgrove.FruitBasket.Players
{
    public class CheaterThoroughPlayer : DishonestPlayer
    {
        private int _guessNumber;

        public CheaterThoroughPlayer(string name, IFruitBasketConfig config) : base(name, config)
        {
            _guessNumber = Config.MinFruitBasketWeight - 1;
        }

        protected override int NextGuess()
        {
            return ++_guessNumber;
        }
    }
}
