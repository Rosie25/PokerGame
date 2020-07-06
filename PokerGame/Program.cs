using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGame
{
    class Program
    {
        static int playerOneWins = 0;
        static int playerTwoWins = 0;
        static int ties = 0;

        static void Main()
        {
             if (Console.IsInputRedirected)
            {
                // for each line (game) in piped stream
                string line;
                while ((line = Console.ReadLine()) != null)
                {
                    PlayPoker(line);
                }
            }
            else
            {
                // get cards from console
                string line = Console.ReadLine();
                PlayPoker(line);
            }

            // output results
            Console.WriteLine("");
            Console.WriteLine("Poker Game Results");
            Console.WriteLine("------------------");
            Console.WriteLine("");
            Console.WriteLine("Player 1: " + playerOneWins.ToString() + " win(s)");
            Console.WriteLine("");
            Console.WriteLine("Player 2: " + playerTwoWins.ToString() + " win(s)");
        }

        /// <summary>
        /// function to simulate the poker game and determine the hand winner
        /// </summary>
        /// <param name="line">the card data</param>
        public static void PlayPoker(string line)
        {
            PokerHelper poker = new PokerHelper(); 
            poker.GetHands(line, out Hand handOne, out Hand handTwo);

            // get hand results
            handOne.SetHandValues();
            handTwo.SetHandValues();
            // compare result
            if (handOne.Score > handTwo.Score)
            {
                playerOneWins++;
            }
            if (handOne.Score < handTwo.Score)
            {
                playerTwoWins++;
            }
            if (handOne.Score == handTwo.Score)
            {
                // if the same, sort by tie breaker then record result
                if (poker.IsTie(handOne, handTwo))
                    ties++;
                else if (poker.HandOneWinsTieBreak(handOne, handTwo))
                    playerOneWins++;
                else
                    playerTwoWins++;
            }
        }

    }
}
