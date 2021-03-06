﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeedsHack;

namespace LeedsHack_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Random rand = new Random();

            Field field = new Field();
            EffectHandler effectHandler = new EffectHandler();
            FieldHandler fieldHandler = new FieldHandler(field, effectHandler);
            RoundHandler round = new RoundHandler(fieldHandler);

            Player player1 = new Player();
            Player player2 = new Player();

            for (int i = 0; i < 3; i++)
            {
                UnitCardFactory factory = new UnitCardFactory();

                player1.playerDeck.Add(factory.GetCard(rand.Next(1, 4)));
                player2.playerDeck.Add(factory.GetCard(rand.Next(1, 4)));
            }

            for (int i = 0; i < 1; i++)
            {
                SpecialCardFactory sfactory = new SpecialCardFactory();

                player1.playerDeck.Add(sfactory.GetCard(rand.Next(1, 4)));
                player2.playerDeck.Add(sfactory.GetCard(rand.Next(1, 4)));
            }

            int roundNumber = 1;
            while (player1.RoundWin < 2 && player2.RoundWin < 2 && roundNumber < 4)
            {
                int winner = round.playOneRound(player1, player2, roundNumber);
                if (winner == 1)
                {
                    player1.RoundWin++;
                    Console.WriteLine("player 1 is the winner at round " + roundNumber);
                }
                else if (winner == 2)
                {
                    player2.RoundWin++;
                    Console.WriteLine("player 2 is the winner at round " + roundNumber);
                }
                else
                {
                    Console.WriteLine("Draw at " + roundNumber);
                }

                roundNumber++;
            }

            if (player1.RoundWin > player2.RoundWin)
            {
                Console.WriteLine("player1 is the winner at last");
            }
            else if (player1.RoundWin < player2.RoundWin)
            {
                Console.WriteLine("player2 is the winner at last");
            }
            else
            {
                Console.WriteLine("It's a draw eew");
            }
        }

       
    }
}
