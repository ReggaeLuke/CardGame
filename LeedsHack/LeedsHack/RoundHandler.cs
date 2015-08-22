using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeedsHack
{
    public class RoundHandler
    {
        private FieldHandler fieldHandler;

        public RoundHandler(FieldHandler fieldHandler)
        {
            this.fieldHandler = fieldHandler;
        }

        public int playOneRound(Player player1, Player player2, int roundNo)
        {
            int didSomeOneSkip = 0;

            while (player1.playerDeck.Count > 0 && player2.playerDeck.Count > 0 && didSomeOneSkip < 2)
            {

                // print out all available cards
                foreach (Card item in player1.playerDeck)
                {
                    Console.WriteLine(item.Name);
                }

                // pick 1 card or choose to give up
                Console.WriteLine("Pick a card to play, player 1 ");
                string cardToPlay = Console.ReadLine();
                Console.WriteLine();

                switch (cardToPlay)
                {
                    case "giveup":
                        fieldHandler.ResetField();
                        return 2;
                    case "skip":
                        didSomeOneSkip++;
                        break;
                    default:
                        didSomeOneSkip = 0;
                        foreach (Card card in player1.playerDeck)
                        {
                            if (card.Name == cardToPlay)
                            {
                                player1.playerDeck.Remove(card);
                                fieldHandler.Execute(1, card);
                                break;
                            }
                        }
                        break;
                }

                fieldHandler.PrintStatus();

                // Pint out all cards
                foreach (Card item in player2.playerDeck)
                {
                    Console.WriteLine(item.Name);
                }

                // Pick one card or choose to give up
                Console.WriteLine("Pick a card to play, player 2 ");
                cardToPlay = Console.ReadLine();
                Console.WriteLine();

                switch (cardToPlay)
                {
                    case "giveup":
                        fieldHandler.ResetField();
                        return 1;
                    case "skip":
                        didSomeOneSkip++;
                        break;
                    default:
                        didSomeOneSkip = 0;
                        foreach (Card card in player2.playerDeck)
                        {
                            if (card.Name == cardToPlay)
                            {
                                player2.playerDeck.Remove(card);
                                fieldHandler.Execute(2, card);
                                break;
                            }
                        }
                        break;
                }

                fieldHandler.PrintStatus();
            }

            // this is for calculating everything including on-hand card
            int player1FinalPoints = fieldHandler.getPlayer1Points();
            int player2FinalPoints = fieldHandler.getPlayer2Points();
            if ((player1.playerDeck.Count == 0 || player2.playerDeck.Count == 0) && roundNo == 3)
            {
                foreach (Card card in player1.playerDeck)
                {
                    if (card.GetType() == typeof(UnitCard))
                    {
                        UnitCard unitCard = (UnitCard)card;
                        player1FinalPoints += unitCard.Value;
                    }
                }

                foreach (Card card in player2.playerDeck)
                {
                    if (card.GetType() == typeof(UnitCard))
                    {
                        UnitCard unitCard = (UnitCard)card;
                        player2FinalPoints += unitCard.Value;
                    }
                }
            }

            if (player1FinalPoints > player2FinalPoints)
            {
                fieldHandler.ResetField();
                return 1;
            }
            else if (player1FinalPoints < player2FinalPoints)
            {
                fieldHandler.ResetField();
                return 2;
            }
            else
            {
                fieldHandler.ResetField();
                return 3;
            }
        }
    }
}
