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
                    card.Image = "~/Content/Images/BigDouble.PNG";
                    break;
                case 2:
                    card.Name = "Sure Winner";
                    card.Image = "~/Content/Images/SureWinner.PNG";
                    break;
                case 3:
                    card.Name = "Measly Melee";
                    card.Image = "~/Content/Images/MeaslyMelee.PNG";
                    break;
                default:
                    break;
            }
            return card;

        }
    }
}
