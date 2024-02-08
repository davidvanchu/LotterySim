using System.Collections.Generic;

namespace LotterySim
{
    public class Draw
    {
        public List<int> MainPicks = [];
        public int ExtraPick;
    }

    public class LotteryDraw : Draw {
        public int Multiplier = 1;
        public int Jackpot;
    }

    public class PlayerPick : Draw {
        public bool Multiplier;
    }
}