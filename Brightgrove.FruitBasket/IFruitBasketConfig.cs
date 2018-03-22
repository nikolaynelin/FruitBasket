namespace Brightgrove.FruitBasket
{
    public interface IFruitBasketConfig
    {
        int MinFruitBasketWeight { get; }
        int MaxFruitBasketWeight { get; }
        int AttemptsLimit { get; }
        int MinPlayersCount { get; }
        int MaxPlayersCount { get; }
        long GameTimeout { get; }
    }
}