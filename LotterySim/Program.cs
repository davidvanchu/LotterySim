using System;
using System.Collections.Generic;

namespace LotterySim
{
    public class Program
    {
        //Simulator options
        static int _playsPerDraw = 1;
        static int _numberOfDraws = 104;
        static bool _useMultiplier = true;

        //Megamillions config
        static int MEGAMILLIONS_MAIN_SET_MAX = 70;
        static int MEGAMILLIONS_EXTRA_SET_MAX = 25;
        static int MEGAMILLIONS_MEGAPLIER_MIN = 2;
        static int MEGAMILLIONS_MEGAPLIER_MAX = 5;
        static int MEGAMILLIONS_JACKPOT = 365000000;

        public static void Main()
        {
            Console.WriteLine("Welcome to LotterySim!");
            Console.WriteLine($"Simulating {_numberOfDraws} draws with {_playsPerDraw} plays per draw.");
            Console.WriteLine();

            var totalCashWon = 0;
            var numberOfWins = 0;
            var numberOfLosses = 0;

            LotteryClient lotto = new(
                MEGAMILLIONS_MAIN_SET_MAX,
                MEGAMILLIONS_EXTRA_SET_MAX,
                MEGAMILLIONS_MEGAPLIER_MIN,
                MEGAMILLIONS_MEGAPLIER_MAX,
                MEGAMILLIONS_JACKPOT
            );

            for (int i = 0; i < _numberOfDraws; i++) {
                var userDraws = new List<PlayerPick>();

                for (int j = 0; j < _playsPerDraw; j++) {
                    userDraws.Add(lotto.PickPlayerNumbers(_useMultiplier));
                }

                var winningLottoNums = lotto.DrawWinningNumbers();
                var win = false;

                foreach (var userDraw in userDraws) {
                    var cashWon = LotteryClient.CheckNumbers(userDraw, winningLottoNums);

                    if (cashWon > 0)
                    {
                        win = true;
                        totalCashWon += cashWon;
                    }
                }

                if (win)
                    numberOfWins++;
                else
                    numberOfLosses++;
            }
            var costOfTickets = _numberOfDraws * _playsPerDraw * (_useMultiplier ? 3 : 2);
            Console.WriteLine($"Number of Wins: {numberOfWins}");
            Console.WriteLine($"Number of Loses: {numberOfLosses}");
            Console.WriteLine($"Total Cash Won: ${totalCashWon}");
            Console.WriteLine($"Cost of Tickets: ${costOfTickets}");
            Console.WriteLine($"Net Proceeds: ${totalCashWon - costOfTickets}");
            Console.WriteLine();
        }
    }
}