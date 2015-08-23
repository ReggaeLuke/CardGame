using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LeedsHack;

namespace CardGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LockAllButton();
        }

        GameStateController gameStateController;
        Field field;

        private void DisplayCardList()
        {
            var buttons = new Button[] { card1button, card2button, card3button, card4button, card5button, card6button, card7button, card8button, card9button, card10button };

            foreach (Button button in buttons)
            {
                button.Image = null;
                button.Text = "";
            }
            List<Card> deck;
            if (gameStateController.gameState.player1Turn)
            {
                deck = gameStateController.gameState.player1.playerDeck;
            }
            else
            {
                deck = gameStateController.gameState.player2.playerDeck;
            }

            foreach (Card card in deck)
            {
                Button tempButton = buttons.FirstOrDefault(b => b.Image == null);
                tempButton.Text = card.Name;
                tempButton.Image = Image.FromFile(@"C:\Users\The Chemist\Documents\GitHub\CardGame\Images\" + card.Name + ".PNG");
            }
        }

        private void startGame_Click(object sender, EventArgs e)
        {
            EffectHandler effectHandler = new EffectHandler();
            field = new Field();
            FieldHandler fieldHandler = new FieldHandler(field, effectHandler);
            GameState gameState = new GameState();
            gameStateController = new GameStateController(fieldHandler, gameState);

            clearBoard();

            gameStateController.StartTheGame();

            player1name.Text = gameStateController.gameState.player1.Name;
            player2name.Text = gameStateController.gameState.player2.Name;
            UpdateField();
            DisplayCardList();
            UnlockAllButton();
        }

        private bool playerButtonClick(Button button)
        {
            if (gameStateController.AskForPlayerMoveOrEndTheRoundNow_ReturnWinner() != -1)
            {
                StartNewRound(gameStateController.AskForPlayerMoveOrEndTheRoundNow_ReturnWinner());
                return false;
            }

            roundStatus.Text = "A unit has been summoned to battle!";

            List<Card> deck;
            if (gameStateController.gameState.player1Turn)
            {
                deck = gameStateController.gameState.player1.playerDeck;
            }
            else
            {
                deck = gameStateController.gameState.player2.playerDeck;
            }

            foreach (Card card in deck)
            {
                if (card.Name == button.Text)
                {

                    if (gameStateController.gameState.player1Turn)
                    {
                        if (button.Text == "Melee")
                        {
                            var array = new PictureBox[] { player1melee1, player1melee2, player1melee3, player1melee4, player1melee5, player1melee6, player1melee7 };
                            var box = array.FirstOrDefault(a => a.Image == null);
                            box.Image =Image.FromFile(@"C:\Users\The Chemist\Documents\GitHub\CardGame\Images\" + card.Name + ".PNG");
                        }
                        else if (button.Text == "Range")
                        {
                            var array = new PictureBox[] { player1range1, player1range2, player1range3, player1range4, player1range5, player1range6, player1range7 };
                            var box = array.FirstOrDefault(a => a.Image == null);
                            box.Image = Image.FromFile(@"C:\Users\The Chemist\Documents\GitHub\CardGame\Images\" + card.Name + ".PNG");
                        }
                        else if (button.Text == "Siege")
                        {
                            var array = new PictureBox[] { player1siege1, player1siege2, player1siege3, player1siege4, player1siege5, player1siege6, player1siege7 };
                            var box = array.FirstOrDefault(a => a.Image == null);
                            box.Image = Image.FromFile(@"C:\Users\The Chemist\Documents\GitHub\CardGame\Images\" + card.Name + ".PNG");
                        }
                        else
                        {
                            var array = new PictureBox[] { player1special1, player1special2, player1special3 };
                            var box = array.FirstOrDefault(a => a.Image == null);
                            box.Image = Image.FromFile(@"C:\Users\The Chemist\Documents\GitHub\CardGame\Images\" + card.Name + ".PNG");
                        }
                    }
                    else
                    {
                        if (button.Text == "Melee")
                        {
                            var array = new PictureBox[] { player2melee1, player2melee2, player2melee3, player2melee4, player2melee5, player2melee6, player2melee7 };
                            var box = array.FirstOrDefault(a => a.Image == null);
                            box.Image = Image.FromFile(@"C:\Users\The Chemist\Documents\GitHub\CardGame\Images\" + card.Name + ".PNG");
                        }
                        else if (button.Text == "Range")
                        {
                            var array = new PictureBox[] { player2range1, player2range2, player2range3, player2range4, player2range5, player2range6, player2range7 };
                            var box = array.FirstOrDefault(a => a.Image == null);
                            box.Image = Image.FromFile(@"C:\Users\The Chemist\Documents\GitHub\CardGame\Images\" + card.Name + ".PNG");
                        }
                        else if (button.Text == "Siege")
                        {
                            var array = new PictureBox[] { player2siege1, player2siege2, player2siege3, player2siege4, player2siege5, player2siege6, player2siege7 };
                            var box = array.FirstOrDefault(a => a.Image == null);
                            box.Image = Image.FromFile(@"C:\Users\The Chemist\Documents\GitHub\CardGame\Images\" + card.Name + ".PNG");
                        }
                        else
                        {
                            var array = new PictureBox[] { player2special1, player2special2, player2special3 };
                            var box = array.FirstOrDefault(a => a.Image == null);
                            box.Image = Image.FromFile(@"C:\Users\The Chemist\Documents\GitHub\CardGame\Images\" + card.Name + ".PNG");
                        }
                    }
                    gameStateController.PlayACard(card);
                    break;
                }
            }

            UpdateField();
            return true;
        }

        private void UpdateField()
        {
            int pl1meleescore = 0;
            int pl1rangescore = 0;
            int pl1siegescore = 0;
            int pl2meleescore = 0;
            int pl2rangescore = 0;
            int pl2siegescore = 0;

            foreach (Card card in field.player1Deck)
            {
                if (card.GetType() == typeof(UnitCard))
                {
                    UnitCard unitCard = (UnitCard)card;
                    switch (unitCard.Type)
                    {
                        case "Melee":
                            pl1meleescore += unitCard.Value;
                            break;
                        case "Range":
                            pl1rangescore += unitCard.Value;
                            break;
                        case "Siege":
                            pl1siegescore += unitCard.Value;
                            break;
                        default:
                            break;
                    }
                }
            }

            foreach (Card card in field.player2Deck)
            {
                if (card.GetType() == typeof(UnitCard))
                {
                    UnitCard unitCard = (UnitCard)card;
                    switch (unitCard.Type)
                    {
                        case "Melee":
                            pl2meleescore += unitCard.Value;
                            break;
                        case "Range":
                            pl2rangescore += unitCard.Value;
                            break;
                        case "Siege":
                            pl2siegescore += unitCard.Value;
                            break;
                        default:
                            break;
                    }
                }
            }

            if (gameStateController.gameState.player1Turn)
            {
                currentTurn.Text = "Player 1 Turn";
            }
            else
            {
                currentTurn.Text = "Player 2 Turn";
            }

            int pl1scores = pl1meleescore + pl1rangescore + pl1siegescore;
            int pl2scores = pl2meleescore + pl2rangescore + pl2siegescore;
            player1meleescore.Text = pl1meleescore.ToString();
            player1rangescore.Text = pl1rangescore.ToString();
            player1siegescore.Text = pl1siegescore.ToString();
            player2meleescore.Text = pl2meleescore.ToString();
            player2rangescore.Text = pl2rangescore.ToString();
            player2siegescore.Text = pl2siegescore.ToString();
            player1score.Text = pl1scores.ToString();
            player2score.Text = pl2scores.ToString();
            player2roundswon.Text = gameStateController.gameState.player2.RoundWin.ToString();
            player1roundswon.Text = gameStateController.gameState.player1.RoundWin.ToString();
        }

        private void clearBoard()
        {
            var array = new PictureBox[] {
                player1melee1, player1melee2, player1melee3, player1melee4, player1melee5, player1melee6, player1melee7,
                player1range1, player1range2, player1range3, player1range4, player1range5, player1range6, player1range7,
                player1siege1, player1siege2, player1siege3, player1siege4, player1siege5, player1siege6, player1siege7,
                player2melee1, player2melee2, player2melee3, player2melee4, player2melee5, player2melee6, player2melee7,
                player2range1, player2range2, player2range3, player2range4, player2range5, player2range6, player2range7,
                player2siege1, player2siege2, player2siege3, player2siege4, player2siege5, player2siege6, player2siege7,
                player1special1, player1special2, player1special3, player2special1, player2special2, player2special3
            };

            foreach (PictureBox box in array)
            {
                box.Image = null;
            }

            roundStatus.Text = "";
            gameStatus.Text = "";
        }

        private void card1button_Click(object sender, EventArgs e)
        {
            playerButtonClick(card1button);
            DisplayCardList();
        }

        private void card2button_Click(object sender, EventArgs e)
        {
            playerButtonClick(card2button);
            DisplayCardList();
        }

        private void card3button_Click(object sender, EventArgs e)
        {
            playerButtonClick(card3button);
            DisplayCardList();
        }

        private void card4button_Click(object sender, EventArgs e)
        {
            playerButtonClick(card4button);
            DisplayCardList();
        }

        private void card5button_Click(object sender, EventArgs e)
        {
            playerButtonClick(card5button);
            DisplayCardList();
        }

        private void card6button_Click(object sender, EventArgs e)
        {
            playerButtonClick(card6button);
            DisplayCardList();
        }

        private void card7button_Click(object sender, EventArgs e)
        {
            playerButtonClick(card7button);
            DisplayCardList();
        }

        private void card8button_Click(object sender, EventArgs e)
        {
            playerButtonClick(card8button);
            DisplayCardList();
        }

        private void card9button_Click(object sender, EventArgs e)
        {
            playerButtonClick(card9button);
            DisplayCardList();
        }

        private void card10button_Click(object sender, EventArgs e)
        {
            playerButtonClick(card10button);
            DisplayCardList();
            UpdateField();
        }

        private void pass_Click(object sender, EventArgs e)
        {
            if (gameStateController.Skip() != -1)
            {
                StartNewRound(gameStateController.Skip());
            }
            else
            {
                roundStatus.Text = "Skip.";
                DisplayCardList();
                UpdateField();
            }
        }

        private void giveUp_Click(object sender, EventArgs e)
        {
            StartNewRound(gameStateController.GiveUp());
        }

        private void StartNewRound(int playerWon)
        {
            clearBoard();
            UpdateField();
            switch (playerWon)
            {
                case 1:
                    roundStatus.Text = "Player 1 won this round!";
                    break;
                case 2:
                    roundStatus.Text = "Player 2 won this round!";
                    break;
                case 3:
                    roundStatus.Text = "This round was a draw";
                    break;
            }

            switch (gameStateController.IsGameEnd())
            {
                case 1:
                    gameStatus.Text = "Player 1 won the game!!!!";
                    LockAllButton();
                    break;
                case 2:
                    gameStatus.Text = "Player 2 won the game!!!!";
                    LockAllButton();
                    break;
                case 3:
                    gameStatus.Text = "This game was a draw!!!";
                    LockAllButton();
                    break;
                default:
                    break;
            }
        }

        private void LockAllButton() {
            var buttons = new Button[] { card1button, card2button, card3button, card4button, card5button, card6button, card7button, card8button, card9button, card10button, pass, giveUp };

            foreach (Button button in buttons)
            {
                button.Enabled = false;
            }
        }

        private void UnlockAllButton()
        {
            var buttons = new Button[] { card1button, card2button, card3button, card4button, card5button, card6button, card7button, card8button, card9button, card10button, pass, giveUp };

            foreach (Button button in buttons)
            {
                button.Enabled = true;
            }
        }
    }
}
