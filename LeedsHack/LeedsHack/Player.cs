using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeedsHack
{
    public class Player
    {
        //public int Points { get; set; }

        public string Name { get; set; }

        public int RoundWin { get; set; }

        public List<Card> playerDeck = new List<Card>();

        //public List<UnitCard> playedCards = new List<UnitCard>();
    }
}
