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
            bool[] currentPlayerEffect, oppPlayerEffect;
            if (playerNo == 1) 
            {
                currentPlayerDeck = field.player1Deck;
                oppPlayerDeck = field.player2Deck;
                currentPlayerEffect = field.player1Effects;
                oppPlayerEffect = field.player2Effects;
            }
            else 
            {
                currentPlayerDeck = field.player2Deck;
                oppPlayerDeck = field.player1Deck;
                currentPlayerEffect = field.player2Effects;
                oppPlayerEffect = field.player1Effects;
            }
            
            if (card.GetType() == typeof(UnitCard))
            {
                // when using unit card
                UnitCard unitCard = (UnitCard)card;
                //currentPlayerDeck.Add(card);
                currentPlayerDeck.Add(effectHandler.AddEffectToCard(unitCard, currentPlayerEffect));
            }
            else
            {
                SpecialCard specialCard = (SpecialCard)card;
                switch (specialCard.ID)
                {
                    case 1:
                        currentPlayerEffect[specialCard.ID] = true;

                        foreach (UnitCard unitCard in currentPlayerDeck) 
                        {
                            unitCard.Value *= 2;
                        }
                        break;
                    case 2:
                        oppPlayerEffect[specialCard.ID] = true;

                        foreach (UnitCard unitCard in oppPlayerDeck)
                        {
                            unitCard.Value = 1;
                        }
                        break;
                    case 3:
                        currentPlayerEffect[specialCard.ID] = true;
                        oppPlayerEffect[specialCard.ID] = true;

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

        public void ResetField()
        {
            field.player1Deck.Clear();
            field.player2Deck.Clear();
            for (int i = 0; i < 10; i++)
            {
                field.player1Effects[i] = false;
                field.player2Effects[i] = false;
            }
            
        }

        public void PrintStatus()
        {
            Console.WriteLine("*************************************");
            foreach (Card card in field.player1Deck)
            {
                Console.Write(card.Type + " ");
            }
            Console.WriteLine();

            foreach (Card card in field.player2Deck)
            {
                Console.Write(card.Type + " ");
            }
            Console.WriteLine();

            foreach (bool bo in field.player1Effects)
            {
                Console.Write(bo + " ");
            }
            Console.WriteLine();

            foreach (bool bo in field.player2Effects)
            {
                Console.Write(bo + " ");
            }
            Console.WriteLine();
            Console.WriteLine("Player 1 Score:" + getPlayer1Points());
            Console.WriteLine("Player 2 Score:" + getPlayer2Points());
            Console.WriteLine("*************************************");
            Console.WriteLine();
        }
    }
}
