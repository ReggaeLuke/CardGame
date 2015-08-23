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
                    card.Name = "The Big Double";
                    break;
                case 2:
                    card.Name = "Sure Winner";
                    break;
                case 3:
                    card.Name = "Measly Melee";
                    break;
                case 4:
                    card.Name = "Rotten Range";
                    break;
                case 5:
                    card.Name = "Stingy Siege";
                    break;
                case 6:
                    card.Name = "The Banisher";
                    break;
                case 7:
                    card.Name = "Deja Vu";
                    break;
                case 8:
                    card.Name = "10 Up";
                    break;
                case 9:
                    card.Name = "Forfeit";
                    break;
                case 10:
                    card.Name = "The Santa Clause";
                    break;
                default:
                    break;
            }
            return card;

        }
    }
}
