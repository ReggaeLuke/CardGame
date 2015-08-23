using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeedsHack
{
    public class GameStateController
    {
        private FieldHandler fieldHandler;
        private GameState gameState;
        
        public GameStateController(FieldHandler fieldHandler, GameState gameState)
        {
            this.gameState = gameState;
            this.fieldHandler = fieldHandler;
        }

        public void StartTheGame() 
        {
            Random rand = new Random();

            for (int i = 0; i < 3; i++)
            {
                UnitCardFactory factory = new UnitCardFactory();

                gameState.player1.playerDeck.Add(factory.GetCard(rand.Next(1, 4)));
                gameState.player2.playerDeck.Add(factory.GetCard(rand.Next(1, 4)));
            }

            for (int i = 0; i < 1; i++)
            {
                SpecialCardFactory sfactory = new SpecialCardFactory();

                gameState.player1.playerDeck.Add(sfactory.GetCard(rand.Next(1, 4)));
                gameState.player2.playerDeck.Add(sfactory.GetCard(rand.Next(1, 4)));
            }

            gameState.player1Turn = true;
        }

        public int AskForPlayerMoveOrEndTheRoundNow_ReturnWinner()
        {
            if (gameState.player1.playerDeck.Count == 0 && gameState.player2.playerDeck.Count == 0)
            {
                return 3;
            }
            else if (gameState.player1.playerDeck.Count == 0)
            {
                return 2;
            }
            else if (gameState.player2.playerDeck.Count == 0)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        public int Skip()
        {
            int result = -1;
            if (gameState.didPreviousPlayerSkip)
            {
                result = EndRound();
            }
            gameState.player1Turn = !gameState.player1Turn;
            return result;
        }

        public int GiveUp()
        {
            if (gameState.player1Turn)
            {
                return EndRound(2);
            }
            else
            {
                return EndRound(1);
            }
        }

        public void PlayACard(Card cardToPlay)
        {
            Player player;
            int playerTurn;
            if (gameState.player1Turn)
            {
                playerTurn = 1;
                player = gameState.player1;
            }
            else
            {
                playerTurn = 2;
                player = gameState.player2;
            }

            foreach (Card tempCard in player.playerDeck)
            {
                if (tempCard.Name == cardToPlay.Name)
                {
                    player.playerDeck.Remove(tempCard);
                    fieldHandler.Execute(playerTurn, tempCard);
                    break;
                }
            }

            gameState.player1Turn = !gameState.player1Turn;
        }

        public int IsGameEnd()
        {
            if (gameState.player1.RoundWin > 1)
            {
                //Console.WriteLine("player1 is the winner at last");
                return 1;
            }
            else if (gameState.player2.RoundWin > 1)
            {
                //Console.WriteLine("player2 is the winner at last");
                return 2;
            }
            else if (gameState.roundNumber > 3 && gameState.player1.RoundWin == gameState.player2.RoundWin)
            {
                return 3;
                //Console.WriteLine("It's a draw eew");
            }
            else
            {
                return -1;
            }
        }
        public int EndRound()
        {
            gameState.player1Turn = true;
            gameState.roundNumber++;
            fieldHandler.ResetField();
            
            // this is for calculating everything including on-hand card
            int player1FinalPoints = fieldHandler.getPlayer1Points();
            int player2FinalPoints = fieldHandler.getPlayer2Points();

            if (gameState.roundNumber == 3)
            {
                foreach (Card card in gameState.player1.playerDeck)
                {
                    if (card.GetType() == typeof(UnitCard))
                    {
                        UnitCard unitCard = (UnitCard)card;
                        player1FinalPoints += unitCard.Value;
                    }
                }

                foreach (Card card in gameState.player2.playerDeck)
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
                gameState.player1.RoundWin++;
                return 1;
            }
            else if (player1FinalPoints < player2FinalPoints)
            {
                gameState.player2.RoundWin++;
                return 2;
            }
            else
            {
                return 3;
            }
        }

        public int EndRound(int winner)
        {
            gameState.player1Turn = true;
            gameState.roundNumber++;
            fieldHandler.ResetField();

            if (winner == 1)
            {
                //Console.WriteLine("player1 is the winner at last");
                gameState.player1.RoundWin++;
                return 1;
            }
            else
            {
                //Console.WriteLine("player2 is the winner at last");
                gameState.player2.RoundWin++;
                return 2;
            }
        }
    }
}
