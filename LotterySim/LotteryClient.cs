using System;
using System.Collections.Generic;
using System.Linq;

namespace LotterySim
{
    public class LotteryClient
    {
        private int _mainSetNumberMax;
        private int _extraSetNumberMax;
        private int _multiplierSetMin;
        private int _multiplierSetMax;
        private int _jackpot;

        public LotteryClient(int mainSetMax, int extraSetMax, int multiplierMin, int multiplierMax, int jackpot) {
            _mainSetNumberMax = mainSetMax;
            _extraSetNumberMax = extraSetMax;
            _multiplierSetMin = multiplierMin;
            _multiplierSetMax = multiplierMax;
            _jackpot = jackpot;
        }

        public static int CheckNumbers(PlayerPick playerPick, LotteryDraw winningDraw)
        {
            int matchingMainPicks = 0;
            foreach (var winningPick in winningDraw.MainPicks)
            {
                if (playerPick.MainPicks.Contains(winningPick))
                {
                    matchingMainPicks++;
                }
            }
            var matchingExtraPick = winningDraw.ExtraPick == playerPick.ExtraPick;

            var cashValue = 0;
            switch (matchingMainPicks) {
                case 0:
                    cashValue = matchingExtraPick ? 2 : 0;
                    break;
                case 1:
                    cashValue = matchingExtraPick ? 4 : 0;
                    break;
                case 2: 
                    cashValue = matchingExtraPick ? 10 : 0;
                    break;
                case 3:
                    cashValue = matchingExtraPick ? 200 : 10;
                    break;
                case 4:
                    cashValue = matchingExtraPick ? 10000 : 500;
                    break;
                case 5:
                    cashValue = matchingExtraPick ? winningDraw.Jackpot : 1000000;
                    break;
            }

            return playerPick.Multiplier ? cashValue * winningDraw.Multiplier : cashValue;
        }

        public PlayerPick PickPlayerNumbers(bool multiplier) { 
            var draw = PickNumbers();

            return new PlayerPick() {
                MainPicks = draw.MainPicks,
                ExtraPick = draw.ExtraPick,
                Multiplier = multiplier
            };
        }

        public LotteryDraw DrawWinningNumbers() { 
            var draw = PickNumbers();
            var multiplierSet = GenerateSet(_multiplierSetMax, _multiplierSetMin);
            Random random = new();

            return new LotteryDraw() {
                MainPicks = draw.MainPicks,
                ExtraPick = draw.ExtraPick,
                Multiplier = multiplierSet[random.Next(multiplierSet.Count)],
                Jackpot = _jackpot
            };
        }

        private Draw PickNumbers()
        {
            Random random = new();
            var mainSet = GenerateSet(_mainSetNumberMax);
            var extraSet = GenerateSet(_extraSetNumberMax);

            List<int> mainPicks = [];
            for (int i = 0; i < 5; i++) {
                var randomPick = random.Next(mainSet.Count);
                mainPicks.Add(mainSet[randomPick]);
                mainSet.RemoveAt(randomPick);
            }

            mainPicks = mainPicks.Order().ToList();

            return new Draw() {
                MainPicks = mainPicks,
                ExtraPick = extraSet[random.Next(extraSet.Count)]
            };
        }

        private static List<int> GenerateSet(int setSize, int setMin = 1)
        {
            var setToReturn = new List<int>();
            for (int i = setMin; i <= setSize; i++) {
                setToReturn.Add(i);
            }
            return setToReturn;
        }
    }
}