using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeedsHack
{
    public class EffectHandler
    {
        public void AddEffectToCard(UnitCard card, bool[] effects)
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
        }
    }
}
