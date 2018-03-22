using System;
using Brightgrove.FruitBasket.Enums;
using Brightgrove.FruitBasket.Players;

namespace Brightgrove.FruitBasket.Infrastructure
{
    public static class PlayerFactory
    {
        public static Player Create(string name, PlayerType type, IFruitBasketConfig config)
        {
            switch (type)
            {
                case PlayerType.Random:
                    return new RandomPlayer(name, config);
                case PlayerType.Memory:
                    return new MemoryPlayer(name, config);
                case PlayerType.Thorough:
                    return new ThoroughPlayer(name, config);
                case PlayerType.Cheater:
                    return new CheaterPlayer(name, config);
                case PlayerType.ThoroughCheater:
                    return new CheaterThoroughPlayer(name, config);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
