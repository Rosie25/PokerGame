using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGame
{
    class Program
    {
        static void Main()
        {
            int playerOneWins = 0;
            int playerTwoWins = 0;
            int ties = 0;
            PokerHelper poker = new PokerHelper();

            // read file
            System.IO.StreamReader file = new System.IO.StreamReader(@"c:\temp\poker-hands.txt");
            // for each line (game) in stream
            string line;
            while ((line = file.ReadLine()) != null)
            {
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

            // output results
            Console.WriteLine("Poker Game Results:");
            Console.WriteLine("------------------:");
            Console.WriteLine("Player 1: " + playerOneWins.ToString() + " win(s)");
            Console.WriteLine("Player 2: " + playerTwoWins.ToString() + " win(s)");
            Console.WriteLine("Ties: " + ties.ToString() + " win(s)");
            Console.WriteLine("The End :)");

        }

    }
}
