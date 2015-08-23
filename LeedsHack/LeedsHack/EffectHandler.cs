using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeedsHack
{
    public class EffectHandler
    {
        public UnitCard AddEffectToCard(UnitCard card, bool[] effects)
        {
            if (effects[0])
            {

            }

            if (effects[1])
            {
                card.Value *= 2;
            }

            if (effects[2])
            {
                card.Value = 1;
            }

            if (effects[3])
            {
                if (card.Type == "Melee")
                {
                    card.Value = 1;
                }
            }

            if (effects[4])
            {
                if (card.Type == "Range")
                {
                    card.Value = 1;
                }
            }

            if (effects[5])
            {
                if (card.Type == "Siege")
                {
                    card.Value = 1;
                }
            }

            if (effects[6])
            {

            }

            return card;
        }
    }
}
