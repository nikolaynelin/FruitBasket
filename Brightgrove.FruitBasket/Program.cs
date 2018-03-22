using System;
using System.Collections.Generic;
using System.Configuration;
using Brightgrove.FruitBasket.Enums;
using Brightgrove.FruitBasket.Infrastructure;

namespace Brightgrove.FruitBasket
{
    //feedback от brightgrove (отрефакторил 18 июля 2017 с учетом пожеланий)
    //1. Использование статического свойства FruitBasketGame.UniqueGuessesOfAllPlayers вводит неявную зависимость и никак не гарантирует,
    //   что эта коллекция будет кем-то заполняться;
    //2. Временная связанность(temporal coupling) метода FruitBasketConfig.Init и свойствами класса FruitBasketConfig;
    //3. Усложнение логики за счёт двух вариантов возвращения числа от игрока (через возвращаемое значение и событие);
    //4. Шаги Guesses.Add, OnGuess(), return guess общие для всех игроков, но повторяются в каждом классе;
    //5. Идентичные методы Guess у CheaterThoroughPlayer и CheaterPlayer.

    class Program
    {
        static void Main(string[] args)
        {
            IFruitBasketConfig config = null;
            try
            {
                config = new FruitBasketConfig();
            }
            catch (ConfigurationErrorsException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                Environment.Exit(-1);
            }

            var players = new List<Player>
            {
                PlayerFactory.Create("Random", PlayerType.Random, config),
                PlayerFactory.Create("Memory", PlayerType.Memory, config),
                PlayerFactory.Create("Thorough", PlayerType.Thorough, config),
                PlayerFactory.Create("Cheater", PlayerType.Cheater, config),
                PlayerFactory.Create("ThoroughCheater", PlayerType.ThoroughCheater, config),

                PlayerFactory.Create("Random2", PlayerType.Random, config),
                PlayerFactory.Create("Memory2", PlayerType.Memory, config),
                PlayerFactory.Create("Thorough2", PlayerType.Thorough, config)
            };

            var game = new FruitBasketGame(players, config);

            //subscribe cheaters for all players guesses
            players.ForEach(x =>
            {
                var cheater = x as DishonestPlayer;
                if (cheater != null)
                {
                    game.GuessEvent += cheater.Cheat;
                }
            });

            Console.WriteLine($"Secret: {game.Secret}");

            var result = game.Start();

            Console.WriteLine($"{Environment.NewLine}{result}");
            Console.ReadKey();
        }
    }
}
