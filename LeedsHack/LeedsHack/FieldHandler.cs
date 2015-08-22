using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeedsHack
{
    public class FieldHandler
    {
        private Field field;
        private EffectHandler effectHandler;

        public FieldHandler(Field field, EffectHandler effectHandler)
        {
            this.field = field;
            this.effectHandler = effectHandler;
        }
        public void Execute(int playerNo, Card card)
        {
            List<Card> currentPlayerDeck, oppPlayerDeck;
            if (playerNo == 1) 
            {
                currentPlayerDeck = field.player1Deck;
                oppPlayerDeck = field.player2Deck;
            }
            else 
            {
                currentPlayerDeck = field.player2Deck;
                oppPlayerDeck = field.player1Deck;
            }
            
            if (card.GetType() == typeof(UnitCard))
            {
                // when using unit card
                UnitCard unitCard = (UnitCard)card;
                //currentPlayerDeck.Add(card);
                currentPlayerDeck.Add(effectHandler.AddEffectToCard(card, field.effect));
            }
            else
            {
                SpecialCard specialCard = (SpecialCard)card;
                field.effects[specialCard.ID] = true;

                switch (specialCard.ID)
                {
                    case 1:
                        foreach (UnitCard unitCard in currentPlayerDeck) 
                        {
                            unitCard.Value *= 2;
                        }
                        break;
                    case 2:
                        foreach (UnitCard unitCard in oppPlayerDeck)
                        {
                            unitCard.Value -= 1;
                        }
                        break;
                    case 3:
                        foreach (UnitCard unitCard in currentPlayerDeck) 
                        {
                            if (unitCard.Type == "Melee") 
                            {
                                unitCard.Value = 1; 
                            }
                        }
                        foreach (UnitCard unitCard in oppPlayerDeck)
                        {
                            if (unitCard.Type == "Melee")
                            {
                                unitCard.Value = 1;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public int getPlayer1Points()
        {
            int total = 0;
            foreach (UnitCard card in field.player1Deck)
            {
                total += card.Value;
            }
            return total;
        }

        public int getPlayer2Points()
        {
            int total = 0;
            foreach (UnitCard card in field.player2Deck)
            {
                total += card.Value;
            }
            return total;
        }
    }
}
