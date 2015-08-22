using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeedsHack
{
    public class Field
    {
        public List<Card> player1Deck = new List<Card>();
        public List<Card> player2Deck = new List<Card>();

        public bool[] player1Effects = new bool[10];
        public bool[] player2Effects = new bool[10];
 
    }
}
