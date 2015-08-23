using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeedsHack
{
    public class UnitCardFactory
    {
        public UnitCard GetCard(int num)
        {
            UnitCard card = new UnitCard();

            if (num == 1)
            {
                card.Name = "The mighty melee";
                card.Value = 5;
                card.Type = "Melee";
            }

            else if (num == 2)
            {
                card.Name = "The mighty range";
                card.Value = 4;
                card.Type = "Range";
            }

            else if (num == 3)
            {
                card.Name = "The mighty siege";
                card.Value = 7;
                card.Type = "Siege";
            }
            return card;
        }
    }
}
