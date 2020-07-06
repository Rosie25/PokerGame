﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGame
{
    class Program
    {
        static void Main(string[] args)
        {
            int playerOneWins = 0;
            int playerTwoWins = 0;

            List<string> handOne;
            List<string> handTwo;
            int handOneScore;
            int handTwoScore;
            // get input stream 
            PokerHelper poker = new PokerHelper();

            // for each line (game) in stream
            string line;
            while (!string.IsNullOrEmpty(line = Console.ReadLine()))
            {
                poker.GetHands(line, out handOne, out handTwo);
                handOneScore = poker.GetHandScore(handOne);
                handTwoScore = poker.GetHandScore(handTwo);
            }

            // get hand one result
            // get hand two result
            // compare result
            // if different, record result
            // if the same, sort by tie breaker then record result

            // output results

            Console.WriteLine("Poker Game Results:");
            Console.WriteLine("------------------:");
            Console.WriteLine("Player 1: " + playerOneWins.ToString() + " wins");
            Console.WriteLine("Player 2: " + playerTwoWins.ToString() + " wins");
        }



    }
}
