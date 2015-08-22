using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeedsHack
{
    public class SpecialCardFactory
    {
        public SpecialCard GetCard(int num)
        {
            SpecialCard card = new SpecialCard();
            card.ID = num;
            switch (num)
            {
                case 1:
                    card.Type = "The Big Double";
                    break;
                case 2:
                    card.Type = "Sure Winner";
                    break;
                case 3:
                    card.Type = "Measly Melee";
                    break;
                default:
                    break;
            }
            return card;

        }
    }
}
