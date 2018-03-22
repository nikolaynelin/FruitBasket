using Brightgrove.FruitBasket.Infrastructure;

namespace Brightgrove.FruitBasket.Players
{
    public class CheaterPlayer : DishonestPlayer
    {
        public CheaterPlayer(string name, IFruitBasketConfig config) : base(name, config) { }
    }
}
