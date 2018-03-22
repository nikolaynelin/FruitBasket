using System.Configuration;

namespace Brightgrove.FruitBasket
{
    public class FruitBasketConfig : IFruitBasketConfig
    {
        public int MinFruitBasketWeight { get; }
        public int MaxFruitBasketWeight { get; }
        public int AttemptsLimit { get; }
        public int MinPlayersCount { get; }
        public int MaxPlayersCount { get; }
        public long GameTimeout { get; }

        public  FruitBasketConfig()
        {
            try
            {
                MinFruitBasketWeight = int.Parse(ConfigurationManager.AppSettings[nameof(MinFruitBasketWeight)]);
                MaxFruitBasketWeight = int.Parse(ConfigurationManager.AppSettings[nameof(MaxFruitBasketWeight)]);
                AttemptsLimit = int.Parse(ConfigurationManager.AppSettings[nameof(AttemptsLimit)]);
                MinPlayersCount = int.Parse(ConfigurationManager.AppSettings[nameof(MinPlayersCount)]);
                MaxPlayersCount = int.Parse(ConfigurationManager.AppSettings[nameof(MaxPlayersCount)]);
                GameTimeout = long.Parse(ConfigurationManager.AppSettings[nameof(GameTimeout)]);
            }
            catch
            {
                throw new ConfigurationErrorsException($"App config is invalid! Correct it and try again!");
            }
        }
    }
}
