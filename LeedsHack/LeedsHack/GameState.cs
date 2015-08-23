using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeedsHack
{
    public class GameState
    {
        public Player player1 = new Player();
        public Player player2 = new Player();

        public bool player1Turn;
        public int roundNumber = 1;
        public bool didPreviousPlayerSkip;
    }
}
