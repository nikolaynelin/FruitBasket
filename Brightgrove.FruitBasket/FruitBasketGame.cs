using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Brightgrove.FruitBasket.Infrastructure;
using Brightgrove.FruitBasket.Utils;

namespace Brightgrove.FruitBasket
{
    public class FruitBasketGameEventArgs : EventArgs
    {
        public int Guess { get; }

        public FruitBasketGameEventArgs(int guess)
        {
            Guess = guess;
        }
    }

    public class FruitBasketGame
    {
        public readonly int Secret;

        private readonly IFruitBasketConfig _config;
        private readonly List<Player> _players;
        private static bool _gameIsOver = false;

        private readonly List<Thread> _threads = new List<Thread>();
        private static readonly object SyncObj = new object();

        public event EventHandler<FruitBasketGameEventArgs> GuessEvent;

        public FruitBasketGame(List<Player> players, IFruitBasketConfig config)
        {
            Check.NotNull(players, nameof(players));
            Check.NotNull(config, nameof(config));

            _config = config;
            _players = players;

            if (_players.Count < _config.MinPlayersCount || _players.Count > _config.MaxPlayersCount)
            {
                throw new ArgumentException($"Number of players must be between {_config.MinPlayersCount} and {_config.MaxPlayersCount}");
            }
            Secret = RandomInt.Next(_config.MinFruitBasketWeight, _config.MaxFruitBasketWeight);
        }

        private void OnGuess(int guess)
        {
            if (GuessEvent != null)
            {
                GuessEvent(this, new FruitBasketGameEventArgs(guess));
            }
        }

        /// <summary>
        /// Starts game and returns result info
        /// </summary>
        /// <returns>Result info</returns>
        public string Start()
        {
            _players.ForEach(x => _threads.Add(new Thread(StartInternally)));

            var timer = new Timer(x =>
            {
                lock (SyncObj)
                {
                    _gameIsOver = true;
                }
            }, null, _config.GameTimeout, 1);


            for (var i = 0; i < _players.Count; i++)
            {
                _threads[i].Start(_players[i]);
            }

            _threads.ForEach(x => x.Join());

            timer.Dispose();

            return GetResultInfo();
        }

#if DEBUG
        private static int _attemptsCountOfAllPlayers = 0;
#endif

        private void StartInternally(object player)
        {
            var fruitBasketPlayer = (Player) player;
            var attemptsCount = 0;

            while (attemptsCount < _config.AttemptsLimit)
            {
                int guess;
                lock (SyncObj)
                {
                    if (_gameIsOver)
                    {
                        break;
                    }

                    guess = fruitBasketPlayer.Guess();
                    OnGuess(guess);
#if DEBUG
                    Console.WriteLine($"{++_attemptsCountOfAllPlayers}. {fruitBasketPlayer.Name} guesses {guess} (total attempts: {fruitBasketPlayer.Guesses.Count}) [secret = {Secret}]");
#endif
                    if (guess == Secret)
                    {
                        _gameIsOver = true;
                        break;
                    }
                }
                Thread.Sleep(GetDelta(Secret, guess));
                attemptsCount++;
            }
        }

        private string GetResultInfo()
        {
            var winner = _players.FirstOrDefault(p => p.Guesses.Contains(Secret));

            if (winner != null)
            {
                return $"{winner.Name} won! Attempts: {winner.Guesses.Count}";
            }

            var closestPlayerIndex = _players.Where(x => x.Guesses.Any())
                .Select(
                    x =>
                        new
                        {
                            IndexOfPlayer = _players.IndexOf(x),
                            ClosestGuessDelta = GetClosestGuessDelta(x.Guesses)
                        })
                .OrderBy(x => x.ClosestGuessDelta)
                .ThenBy(x => x.IndexOfPlayer)
                .Select(x => x.IndexOfPlayer)
                .First();

            winner = _players[closestPlayerIndex];
            var closestGuess = GetClosestGuess(winner.Guesses);
            return $"{winner.Name} won! Attempts: {winner.Guesses.Count}; Closest guess: {closestGuess}";
        }

        private int GetClosestGuessDelta(List<int> guesses)
        {
            return guesses.Select(x => new {GuessDelta = GetDelta(x, Secret), GuessIndex = guesses.IndexOf(x)})
                .OrderBy(x => x.GuessDelta)
                .ThenBy(x => x.GuessIndex)
                .First()
                .GuessDelta;
        }

        private int GetClosestGuess(List<int> guesses)
        {
            return guesses.OrderBy(x => GetDelta(x, Secret)).First();
        }

        private static int GetDelta(int x, int y)
        {
            return Math.Abs(x - y);
        }
    }
}